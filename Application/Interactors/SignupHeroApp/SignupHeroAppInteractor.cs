using System.Linq;
using System.Threading.Tasks;
using HeroProject.Application.Interactors.SignupHeroApp.Exception;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace HeroProject.Application.Interactors.SignupHeroApp
{
    public sealed class SignupHeroAppInteractor : IActionInteractor<SignupHeroAppRequest>
    {
        [NotNull]
        private readonly SignInManager<HeroAppUser> signInManager;

        [NotNull]
        private readonly UserManager<HeroAppUser> userManager;

        public SignupHeroAppInteractor(
            [NotNull] SignInManager<HeroAppUser> signInManager,
            [NotNull] UserManager<HeroAppUser> userManager)
        {
            Preconditions.RequireNotNull(signInManager, nameof(signInManager));
            Preconditions.RequireNotNull(userManager, nameof(userManager));

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [NotNull]
        public async Task ExecuteAsync([NotNull] SignupHeroAppRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            var user = new HeroAppUser
            {
                UserName = request.UserName
            };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new SignupHeroAppException(result.Errors.Select(e => e.Description));
            }

            await signInManager.PasswordSignInAsync(user, request.Password, false, false);
        }
    }
}