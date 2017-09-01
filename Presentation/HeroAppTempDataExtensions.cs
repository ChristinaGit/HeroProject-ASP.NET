using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HeroProject.Presentation
{
    public static class HeroAppTempDataExtensions
    {
        public static readonly string KeyPrefix = "HeroApp_";

        private static readonly string KeyMessage = CreateKey("Message");

        [NotNull]
        public static string CreateKey([NotNull] string name)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            return KeyPrefix + name;
        }

        [CanBeNull]
        public static string GetMessage([NotNull] this ITempDataDictionary @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return (string) @this[KeyMessage];
        }

        public static void SetMessage([NotNull] this ITempDataDictionary @this, [CanBeNull] string value)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            @this[KeyMessage] = value;
        }
    }
}