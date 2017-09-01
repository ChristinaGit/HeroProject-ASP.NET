using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HeroProject.Presentation
{
    public static class HeroAppViewDataExtensions
    {
        public static readonly string KeyPrefix = "HeroApp_";

        private static readonly string KeyPageTitle = CreateKey("Title");

        [NotNull]
        public static string CreateKey([NotNull] string name)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            return KeyPrefix + name;
        }

        [CanBeNull]
        public static string GetTitle([NotNull] this ViewDataDictionary @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return (string) @this[KeyPageTitle];
        }

        public static void SetTitle([NotNull] this ViewDataDictionary @this, [CanBeNull] string value)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            @this[KeyPageTitle] = value;
        }
    }
}