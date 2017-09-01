using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Presentation.Shared.Views
{
    public static class SharedViews
    {
        public static readonly string Layout = GetFullName("_Layout");
        public static readonly string Alert = GetFullName("_Alert");
        public static readonly string Pagination = GetFullName("_Pagination");
        public static readonly string AccountNavigation = GetFullName("_AccountNavigation");

        [NotNull]
        private static string GetFullName([NotNull] string name)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            return $"~/Shared/Views/{name}.cshtml";
        }
    }
}