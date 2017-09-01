using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Presentation.Hero.Views
{
    public static class HeroViews
    {
        public static readonly string List = GetFullName("List");
        public static readonly string Edit = GetFullName("Edit");
        public static readonly string Details = GetFullName("Details");
        public static readonly string Create = GetFullName("Create");
        public static readonly string Form = GetFullName("_Form");

        [NotNull]
        private static string GetFullName([NotNull] string name)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            return $"~/Hero/Views/{name}.cshtml";
        }
    }
}