using System.Threading.Tasks;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace HeroProject.Application.Interactors.LogoutHeroApp
{
    public sealed class LogoutHeroAppInteractor : IActionInteractor
    {
        [NotNull]
        private readonly SignInManager<HeroAppUser> signInManager;

        public LogoutHeroAppInteractor([NotNull] SignInManager<HeroAppUser> signInManager)
        {
            Preconditions.RequireNotNull(signInManager, nameof(signInManager));

            this.signInManager = signInManager;
        }

        [NotNull]
        public async Task ExecuteAsync() => await signInManager.SignOutAsync();
    }
}