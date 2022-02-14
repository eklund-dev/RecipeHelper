namespace RecipeHelper.Application.Features.Identity.Responses
{
    public class ApplicationUserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
