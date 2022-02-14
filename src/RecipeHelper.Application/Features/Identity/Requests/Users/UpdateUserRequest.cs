namespace RecipeHelper.Application.Features.Identity.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool AdminClearence { get; set; } = false;
        public IEnumerable<string> Roles { get; set; }
    }
}
