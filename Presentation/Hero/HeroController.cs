using System.Linq;
using System.Threading.Tasks;
using HeroProject.Application.Interactors.AddHero;
using HeroProject.Application.Interactors.GetHero;
using HeroProject.Application.Interactors.GetHero.Exception;
using HeroProject.Application.Interactors.GetHeroAvatar;
using HeroProject.Application.Interactors.GetHeroAvatar.Exception;
using HeroProject.Application.Interactors.GetHeroesPage;
using HeroProject.Application.Interactors.GetHeroesPage.Exception;
using HeroProject.Application.Interactors.RemoveHero;
using HeroProject.Application.Interactors.UpdateHero;
using HeroProject.Common;
using HeroProject.Presentation.Hero.ViewModels;
using HeroProject.Presentation.Shared;
using HeroProject.Presentation.Shared.ViewModels;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroProject.Presentation.Hero
{
    [Authorize]
    public sealed class HeroController : AppControllerBase
    {
        public static readonly string Name = AspRoutes.GetControllerName<HeroController>();
        public static readonly string Segment = "heroes";

        [HttpGet]
        [NotNull]
        public async Task<IActionResult> List(
            [FromServices] [NotNull] GetHeroesPageInteractor interactor,
            [FromRoute(Name = Actions.List.ParamPage)] int page = Actions.List.StartPage,
            [FromQuery(Name = Actions.List.ParamPageSize)] int pageSize = Actions.List.DefaultPageSize)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));
            Preconditions.RequireInRange(page, Actions.List.StartPage, int.MaxValue, nameof(page));
            Preconditions.RequireInRange(pageSize, 1, int.MaxValue, nameof(pageSize));

            GetHeroesPageResponse response;
            try
            {
                response = await interactor.ExecuteAsync(new GetHeroesPageRequest(HttpContext.User, page, pageSize));
            }
            catch (PageIndexOutOfRangeException ex)
            {
                return RedirectToHeroList(ex.PageCount, pageSize);
            }

            ViewData.SetTitle("Heroes");

            return View(
                new ListViewModel(
                    response.Items.Select(m => new ListViewModel.Item(m.Id, m.Name, m.AvatarFileName)),
                    new PageViewModel(page, response.StartIndex, pageSize, response.PageCount))
            );
        }

        [HttpGet]
        [NotNull]
        public async Task<IActionResult> Details(
            [FromServices] [NotNull] GetHeroInteractor interactor,
            [FromRoute(Name = Actions.ParamId)] int id)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));

            GetHeroResponse response;
            try
            {
                response = await interactor.ExecuteAsync(new GetHeroRequest(id));
            }
            catch (HeroNotFoundException)
            {
                return NotFound();
            }

            ViewData.SetTitle($"Details: Hero ({id})");

            return View(new DetailsViewModel
            {
                Id = response.Id,
                AvatarFileName = response.AvatarFileName,
                Name = response.Name,
                Strength = response.Strength,
                Dexterity = response.Dexterity,
                Intelligence = response.Intelligence
            });
        }

        [HttpGet]
        [NotNull]
        public async Task<IActionResult> Avatar(
            [FromServices] [NotNull] GetHeroAvatarInteractor interactor,
            [FromRoute(Name = Actions.ParamId)] int id)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));

            GetHeroAvatarResponse response;
            try
            {
                response = await interactor.ExecuteAsync(new GetHeroAvatarRequest(id));
            }
            catch (HeroAvatarNotFoundException)
            {
                return NotFound();
            }

            return File(response.FileName, response.MimeType);
        }

        [HttpGet]
        [NotNull]
        public IActionResult Create()
        {
            ViewData.SetTitle("Create hero");

            return View(new CreateViewModel());
        }

        [HttpGet]
        [NotNull]
        public async Task<IActionResult> Edit(
            [FromServices] [NotNull] GetHeroInteractor interactor,
            [FromRoute(Name = Actions.ParamId)] int id)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));

            GetHeroResponse response;
            try
            {
                response = await interactor.ExecuteAsync(new GetHeroRequest(id));
            }
            catch (HeroNotFoundException)
            {
                return NotFound();
            }

            ViewData.SetTitle($"Edit: Hero ({id})");

            return View(new EditViewModel
            {
                Id = id,
                Name = response.Name,
                Strength = response.Strength,
                Dexterity = response.Dexterity,
                Intelligence = response.Intelligence
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotNull]
        public async Task<IActionResult> Create(
            [FromServices] [NotNull] AddHeroInteractor interactor,
            [FromForm] [NotNull] CreateViewModel model)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));
            Preconditions.RequireNotNull(model, nameof(model));

            if (ModelState.IsValid)
            {
                byte[] avatar = null;
                if (model.Avatar != null)
                {
                    avatar = await model.Avatar.ReadAllAsync();
                }

                var hero = await interactor.ExecuteAsync(
                    new AddHeroRequest(
                        HttpContext.User,
                        model.Name,
                        avatar,
                        model.Strength,
                        model.Dexterity,
                        model.Intelligence));

                TempData.SetMessage($"Hero \"{model.Name}\" created!");

                return RedirectToHeroDetails(hero.Id);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [NotNull]
        public async Task<IActionResult> Delete(
            [FromServices] [NotNull] RemoveHeroInteractor interactor,
            [FromRoute(Name = Actions.ParamId)] int id)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));

            await interactor.ExecuteAsync(new RemoveHeroRequest(id));

            return RedirectToHeroList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotNull]
        public async Task<IActionResult> Edit(
            [FromServices] [NotNull] UpdateHeroInteractor interactor,
            [NotNull] EditViewModel model)
        {
            Preconditions.RequireNotNull(interactor, nameof(interactor));
            Preconditions.RequireNotNull(model, nameof(model));

            if (ModelState.IsValid)
            {
                byte[] avatar = null;
                if (model.Avatar != null)
                {
                    avatar = await model.Avatar.ReadAllAsync();
                }

                await interactor.ExecuteAsync(
                    new UpdateHeroRequest(
                        model.Id,
                        model.Name,
                        avatar,
                        model.Strength,
                        model.Dexterity,
                        model.Intelligence));

                TempData.SetMessage($"Hero \"{model.Name}\" changed!");

                return RedirectToHeroDetails(model.Id);
            }
            else
            {
                return View();
            }
        }

        public static class Actions
        {
            public const string ParamId = "id";

            public static class List
            {
                public const string Name = nameof(HeroController.List);

                public const string ParamPage = PageControllerParams.ParamPage;
                public const string ParamPageSize = PageControllerParams.ParamPageSize;

                public const int StartPage = PageControllerParams.StartPage;
                public const int DefaultPageSize = 10;
            }

            public static class Details
            {
                public const string Name = nameof(HeroController.Details);
            }

            public static class Avatar
            {
                public const string Name = nameof(HeroController.Avatar);
            }

            public static class Create
            {
                public const string Name = nameof(HeroController.Create);
            }

            public static class Edit
            {
                public const string Name = nameof(HeroController.Edit);
            }

            public static class Delete
            {
                public const string Name = nameof(HeroController.Delete);
            }
        }
    }
}