using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace HeroProject.Common
{
    public static class UrlHelperExtensions
    {
        [NotNull]
        public static string AsVirtual([NotNull] this IUrlHelper @this, [NotNull] string path)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));
            Preconditions.RequireNotNull(path, nameof(path));

            return $"~/{path}";
        }

        [NotNull]
        public static string AsContent([NotNull] this IUrlHelper @this, [NotNull] string path)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));
            Preconditions.RequireNotNull(path, nameof(path));

            return @this.Content(@this.AsVirtual(path));
        }

        [NotNull]
        public static string ApplicationRoot([NotNull] this IUrlHelper @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this.AsContent(string.Empty);
        }
    }
}