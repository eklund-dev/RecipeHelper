namespace RecipeHelper.Application.Features.Identity.Requests.Users
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Password { get; set; }
    }
}
