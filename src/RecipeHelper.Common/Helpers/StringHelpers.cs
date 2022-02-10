using System.Globalization;

namespace RecipeHelper.Common.Helpers
{
    public static class StringHelpers
    {
        public static string ToTitleCase(this string str)
        {
            var info = new CultureInfo("sv-SE", false).TextInfo;
            return info.ToTitleCase(str);
        }
    }
}
