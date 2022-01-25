using System.Globalization;

namespace RecipeHelper.Domain.Exceptions
{
    [Serializable]
    public class ApplicationRoleViolationException : Exception
    {
        public ApplicationRoleViolationException() : base() { }
        public ApplicationRoleViolationException(string message) : base(message) { }
        public ApplicationRoleViolationException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
