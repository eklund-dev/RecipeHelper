namespace RecipeHelper.Application.Common.Dtos
{
    public class RecipeUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<RecipeDto> Recipes { get; set; }
    }
}
