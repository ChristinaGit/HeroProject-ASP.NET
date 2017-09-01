using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace HeroProject.Presentation
{
    public static class CleanArchitectureServiceCollectionExtensions
    {
        [NotNull]
        public static IServiceCollection AddClean([NotNull] this IServiceCollection @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("~/Shared/Views/{0}.cshtml");
                o.ViewLocationFormats.Add("~/{1}/Views/{0}.cshtml");
            });
        }
    }
}