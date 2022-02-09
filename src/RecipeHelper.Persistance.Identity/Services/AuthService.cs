using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Auth;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Auth;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Common.Helpers;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Enums;
using RecipeHelper.Persistance.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeHelper.Persistance.Identity.Services
{
    public class AuthService : IAuthService
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly RecipeHelperIdentityDbContext _dbContext;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextUserService _userService;

        public AuthService(UserManager<ApplicationUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            RecipeHelperIdentityDbContext dbContext,
            ILogger<AuthService> logger,
            IHttpContextUserService userService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dbContext = dbContext;
            _logger = logger;
            _userService = userService;
        }

        public async Task<Response<AuthDto>> AuthenticateAsync(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null) 
                return Response<AuthDto>.Fail("User does not exist");
         
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userHasValidPassword is false) 
                return Response<AuthDto>.Fail("User/password combination is incorrect");

            _logger.LogInformation($"Successful login for user with id {user.Id}", user);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<Response<AuthDto>> RegisterAsync(AuthRegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null) return Response<AuthDto>.Fail($"User with email {request.Email} Already exists");

            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.UserName ?? "Anonymous",
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded) return Response<AuthDto>.Fail(result.Errors.Select(x => x.Description).ToList());

            // Adding new user the Standard RoleType 'User'
            await _userManager.AddToRoleAsync(newUser, Enum.GetName(typeof(RoleType), RoleType.User));

            _logger.LogInformation($"New User created with id {newUser.Id}");

            await _userManager.AddClaimAsync(newUser, new Claim("admin.view", "false"));

            return await GenerateAuthenticationResultForUserAsync(newUser);
        }

        #region RefreshToken

        public async Task<Response<AuthDto>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetPrincipalFromToken(request.Token);

            if (validatedToken == null) return Response<AuthDto>.Fail("Invalid Token");
  

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow) return Response<AuthDto>.Fail("This token hasn't expired yet");

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _dbContext.RefreshToken.SingleOrDefaultAsync(r => r.TokenId == request.RefreshToken);

            if (storedRefreshToken == null) return Response<AuthDto>.Fail("This refresh token does not exist");

            if (DateTime.UtcNow > storedRefreshToken.ExpireDate) return Response<AuthDto>.Fail("This refresh token has expired");

            if (storedRefreshToken.Invalidated) return Response<AuthDto>.Fail("This refresh token has been invalidated");

            if (storedRefreshToken.Used) return Response<AuthDto>.Fail("This refresh token has been used");

            if (storedRefreshToken.JwtId != jti) return Response<AuthDto>.Fail("This refresh token does not match this JWT");

            storedRefreshToken.Used = true;
            _dbContext.RefreshToken.Update(storedRefreshToken);
            await _dbContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await GenerateAuthenticationResultForUserAsync(user);
        }
        public async Task<Response<RefreshTokenRevokeDto>> RevokeRefreshTokenAsync()
        {
            var userClaims = _userService.GetUser();
            var userName = userClaims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = userClaims?.FindFirst("uid")?.Value;
            var userIp = userClaims?.FindFirst("ip")?.Value;

            if (string.IsNullOrWhiteSpace(userId)) 
                throw new Exception("Logged in User not found - the request could therefore not continue it's process");

            var refreshToken = await _dbContext.RefreshToken
                .Where(x => x.UserId == userId).OrderByDescending(x => x.CreationDate)
                .FirstOrDefaultAsync();

            if (refreshToken is null) return Response<RefreshTokenRevokeDto>.Fail("Refreshtoken is null");

            try
            {
                _dbContext.RefreshToken.Remove(refreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Database exception - could not remove Refreshtoken from database", ex);
            }

            return Response<RefreshTokenRevokeDto>
                .Success(new RefreshTokenRevokeDto { TokenRevoked = true }, "Refreshtoken successfully removed");

        }

        #endregion RefreshToken

        #region Auth Private Methods
        private async Task<Response<AuthDto>> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret!);
            var userRoles = await _userManager.GetRolesAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < userRoles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", userRoles[i]));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", IpHelper.GetIpAddress())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(1),
            };

            await _dbContext.RefreshToken.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            var authDto = new AuthDto
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.JwtId,
                RefreshTokenExpiration = refreshToken.ExpireDate
            };

            return Response<AuthDto>.Success(authDto, "Authenticated");

        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Auth Private Methods
    }
}
