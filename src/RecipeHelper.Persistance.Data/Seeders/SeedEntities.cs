using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Enum;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Seeders
{
    public static class SeedEntities
    {
        public static async Task SeedRecipes(RecipeHelperDbContext dbContext)
        {
            if (!dbContext.Recipes!.Any(x => x.Name.Equals("Kyckling Ris & Curry")))
            {
                var FoodTypeId = dbContext.FoodTypes?.FirstOrDefault(x => x.Name.Equals("Chicken"))?.Id;
                var allIngredients = dbContext?.Ingredients?.ToList();
                var recipeList = new List<Recipe>
                {
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kyckling Ris & Curry",
                        Description = "Enkel maträtt till vardagen för den som älskar kyckling",
                        Duration = new TimeSpan(0, 30, 0),
                        Instructions = new List<string>
                        {
                            "Hacka kycklingen i bitar",
                            "Stek kyckling noggrannt",
                            "Koka ris"
                        },
                        Difficulty = Difficulty.Novice,
                        TypeOfOccasion = TypeOfOccasion.Dinner,
                        TypeOfMeal = TypeOfMeal.MainCourse,
                        FoodTypeId = FoodTypeId,
                        RecipeCategories = null,
                        RecipeIngredients = null,
                        CreatedBy = "Seed",
                        CreatedDate = DateTime.UtcNow
                    }
                };

                await dbContext!.Recipes!.AddRangeAsync(recipeList);
                await dbContext.SaveChangesAsync();
            }

           
        }
    }
}
