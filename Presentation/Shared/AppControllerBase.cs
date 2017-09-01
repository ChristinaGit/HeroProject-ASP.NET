using HeroProject.Presentation.Hero;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HeroProject.Presentation.Shared
{
    public abstract class AppControllerBase : Controller
    {
        [NotNull]
        public IActionResult RedirectToHeroList(
            int page = HeroController.Actions.List.StartPage,
            int pageSize = HeroController.Actions.List.DefaultPageSize)
            => RedirectToAction(
                HeroController.Actions.List.Name,
                HeroController.Name,
                new RouteValueDictionary
                {
                    [HeroController.Actions.List.ParamPage] = page,
                    [HeroController.Actions.List.ParamPageSize] = pageSize
                });

        [NotNull]
        public IActionResult RedirectToHeroDetails(int id)
            => RedirectToAction(
                HeroController.Actions.Details.Name,
                HeroController.Name,
                new RouteValueDictionary
                {
                    [HeroController.Actions.ParamId] = id
                });

        [NotNull]
        public IActionResult RedirectOrDefault([CanBeNull] string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToHeroList();
            }
        }
    }
}