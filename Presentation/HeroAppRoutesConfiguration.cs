using HeroProject.Common;
using HeroProject.Presentation.Account;
using HeroProject.Presentation.Hero;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace HeroProject.Presentation
{
    public static class HeroAppRoutesConfiguration
    {
        [NotNull]
        public static IServiceCollection ConfigureHeroAppRoutes([NotNull] this IServiceCollection @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this.Configure<RouteOptions>(o =>
            {
                o.LowercaseUrls = true;
                o.AppendTrailingSlash = true;
            });
        }

        public static void ConfigureHeroAppIdentityRoutes([NotNull] this IdentityOptions @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            var applicationCookie = @this.Cookies.ApplicationCookie;
            applicationCookie.LoginPath =
                $"/{AccountController.Segment}/{AccountController.Actions.Login.Name}".ToLower();
            applicationCookie.ReturnUrlParameter = AccountController.Actions.ParamReturn;
        }

        [NotNull]
        public static IRouteBuilder AddHeroAppRoutes([NotNull] this IRouteBuilder @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this
                .AddHeroAppAccountRoutes()
                .AddHeroAppHeroRoutes()
                .AddHomeRoutes();
        }

        [NotNull]
        public static IRouteBuilder AddHomeRoutes([NotNull] this IRouteBuilder @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this.MapRoute(
                "home",
                string.Empty,
                new RouteValueDictionary
                {
                    [AspRoutes.Contoller] = HeroController.Name,
                    [AspRoutes.Action] = HeroController.Actions.List.Name
                });
        }

        [NotNull]
        public static IRouteBuilder AddHeroAppHeroRoutes([NotNull] this IRouteBuilder @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this
                .MapRoute(
                    "heroes_pagination",
                    $"{HeroController.Segment}/page/{{{HeroController.Actions.List.ParamPage}:range({HeroController.Actions.List.StartPage}, {int.MaxValue})?}}",
                    new RouteValueDictionary
                    {
                        [AspRoutes.Contoller] = HeroController.Name,
                        [AspRoutes.Action] = HeroController.Actions.List.Name
                    })
                .MapRoute(
                    "hero_action",
                    $"{HeroController.Segment}/{{{HeroController.Actions.ParamId}:int}}/{{{AspRoutes.Action}}}",
                    new RouteValueDictionary
                    {
                        [AspRoutes.Contoller] = HeroController.Name,
                        [AspRoutes.Action] = HeroController.Actions.Details.Name
                    })
                .MapRoute(
                    "heroes_action",
                    $"{HeroController.Segment}/{{{AspRoutes.Action}}}",
                    new RouteValueDictionary
                    {
                        [AspRoutes.Contoller] = HeroController.Name
                    })
                .MapRoute(
                    "heroes",
                    $"{HeroController.Segment}",
                    new RouteValueDictionary
                    {
                        [AspRoutes.Contoller] = HeroController.Name,
                        [AspRoutes.Action] = HeroController.Actions.List.Name
                    });
        }

        [NotNull]
        public static IRouteBuilder AddHeroAppAccountRoutes([NotNull] this IRouteBuilder @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return @this
                .MapRoute(
                    "accounts_action",
                    $"{AccountController.Segment}/{{{AspRoutes.Action}}}",
                    new RouteValueDictionary
                    {
                        [AspRoutes.Contoller] = AccountController.Name
                    });
        }
    }
}