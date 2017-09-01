using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Presentation.Account.Views
{
    public static class AccountViews
    {
        public static readonly string Form = GetFullName("_Form");
        public static readonly string Login = GetFullName("Login");
        public static readonly string Signup = GetFullName("Signup");

        [NotNull]
        private static string GetFullName([NotNull] string name)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            return $"~/Account/Views/{name}.cshtml";
        }
    }
}