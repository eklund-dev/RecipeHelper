using System.Globalization;

namespace RecipeHelper.Domain.Exceptions
{
    [Serializable]
    public class ApplicationUserViolationException : Exception
    {
        public ApplicationUserViolationException() : base() { }
        public ApplicationUserViolationException(string message) : base(message) { }
        public ApplicationUserViolationException(string message, params object[] args) 
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
