namespace RecipeHelper.Application.Common.Requests.Users
{
    public class ManageUserRoleRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
