namespace RecipeHelper.Persistance.Identity.Validators
{
    public static class IdentityValidators
    {
        public static bool ValidateIntegerInput(params int?[] intValues)
        {
            foreach (int? nbr in intValues)
            {
                if (nbr != null && nbr == 0 || nbr < 0)
                    return false;
            }

            return true;
        }
    }
}
