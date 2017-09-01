using HeroProject.Application.Interactors.AddHero;
using HeroProject.Application.Interactors.GetHero;
using HeroProject.Application.Interactors.GetHeroAvatar;
using HeroProject.Application.Interactors.GetHeroesPage;
using HeroProject.Application.Interactors.LoginHeroApp;
using HeroProject.Application.Interactors.LogoutHeroApp;
using HeroProject.Application.Interactors.RemoveHero;
using HeroProject.Application.Interactors.SignupHeroApp;
using HeroProject.Application.Interactors.UpdateHero;
using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace HeroProject.Application
{
    public static class HeroInteractorServiceCollectionExtensions
    {
        [NotNull]
        public static IServiceCollection AddHeroAppInteractors([NotNull] this IServiceCollection @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            @this.AddTransient<GetHeroesPageInteractor>();
            @this.AddTransient<GetHeroInteractor>();
            @this.AddTransient<GetHeroAvatarInteractor>();
            @this.AddTransient<AddHeroInteractor>();
            @this.AddTransient<RemoveHeroInteractor>();
            @this.AddTransient<UpdateHeroInteractor>();

            @this.AddTransient<LoginHeroAppInteractor>();
            @this.AddTransient<SignupHeroAppInteractor>();
            @this.AddTransient<LogoutHeroAppInteractor>();

            return @this;
        }
    }
}