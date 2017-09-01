using System.Threading.Tasks;
using HeroProject.Application.Interactors.LoginHeroApp;
using HeroProject.Application.Interactors.LoginHeroApp.Exception;
using HeroProject.Application.Interactors.LogoutHeroApp;
using HeroProject.Application.Interactors.SignupHeroApp;
using HeroProject.Application.Interactors.SignupHeroApp.Exception;
using HeroProject.Common;
using HeroProject.Presentation.Account.ViewModels;
using HeroProject.Presentation.Shared;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroProject.Presentation.Account
{
    [Authorize]
    public sealed class AccountController : AppControllerBase
    {
        public static readonly string Name = AspRoutes.GetControllerName<AccountController>();
        public static readonly string Segment = "accounts";

        [HttpGet]
        [AllowAnonymous]
        [NotNull]
        public IActionResult Signup() => View();

        [HttpGet]
        [AllowAnonymous]
        [NotNull]
        public IActionResult Login([FromQuery(Name = Actions.ParamReturn)] [CanBeNull] string returnUrl = null) =>
            View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });

        [HttpPost]
        [AllowAnonymous]
        [NotNull]
        public async Task<IActionResult> Signup(
            [FromServices] [NotNull] SignupHeroAppInteractor interactor,
            [FromForm] [NotNull] SignupViewModel model)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));
            Preconditions.RequireNotNull(model, nameof(model));

            if (ModelState.IsValid)
            {
                try
                {
                    await interactor.ExecuteAsync(new SignupHeroAppRequest(model.Name, model.Password));
                }
                catch (SignupHeroAppException ex)
                {
                    foreach (var errorDescription in ex.ErrorDescriptions)
                    {
                        ModelState.AddModelError(string.Empty, errorDescription);
                    }

                    return View(model);
                }
                TempData.SetMessage($"Hello, {model.Name}!");

                return RedirectToHeroList();
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [NotNull]
        public async Task<IActionResult> Login(
            [FromServices] [NotNull] LoginHeroAppInteractor interactor,
            [FromForm] [NotNull] LoginViewModel model)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));
            Preconditions.RequireNotNull(model, nameof(model));

            if (ModelState.IsValid)
            {
                try
                {
                    await interactor.ExecuteAsync(new LoginHeroAppRequest(model.Name, model.Password));
                }
                catch (LoginHeroAppException ex)
                {
                    foreach (var errorDescription in ex.ErrorDescriptions)
                    {
                        ModelState.AddModelError(string.Empty, errorDescription);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid user or password");
                    return View();
                }

                TempData.SetMessage($"Hello, {model.Name}!");

                return RedirectOrDefault(model.ReturnUrl);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [NotNull]
        public async Task<IActionResult> Logout(
            [FromServices] [NotNull] LogoutHeroAppInteractor interactor,
            [CanBeNull] string returnUrl = null)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));

            await interactor.ExecuteAsync();

            return RedirectOrDefault(returnUrl);
        }

        public static class Actions
        {
            public const string ParamReturn = "return";

            public static class Login
            {
                public const string Name = nameof(AccountController.Login);
            }

            public static class Signup
            {
                public const string Name = nameof(AccountController.Signup);
            }

            public static class Logout
            {
                public const string Name = nameof(AccountController.Logout);
            }
        }
    }
}