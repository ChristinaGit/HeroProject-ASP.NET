using System.Threading.Tasks;
using HeroProject.Application.Interactors.LoginHeroApp.Exception;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace HeroProject.Application.Interactors.LoginHeroApp
{
    public sealed class LoginHeroAppInteractor : IActionInteractor<LoginHeroAppRequest>
    {
        [NotNull]
        private readonly SignInManager<HeroAppUser> signInManager;

        [NotNull]
        private readonly UserManager<HeroAppUser> userManager;

        public LoginHeroAppInteractor(
            [NotNull] UserManager<HeroAppUser> userManager,
            [NotNull] SignInManager<HeroAppUser> signInManager)
        {
            Preconditions.RequireNotNull(userManager, nameof(userManager));
            Preconditions.RequireNotNull(signInManager, nameof(signInManager));

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [NotNull]
        public async Task ExecuteAsync([NotNull] LoginHeroAppRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if (!result.Succeeded)
                {
                    throw new LoginHeroAppException();
                }
            }
            else
            {
                throw new LoginHeroAppException();
            }
        }
    }
}