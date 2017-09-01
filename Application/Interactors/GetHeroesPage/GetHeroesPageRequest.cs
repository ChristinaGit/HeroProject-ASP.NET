using System.Security.Claims;
using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHeroesPage
{
    public sealed class GetHeroesPageRequest
    {
        public GetHeroesPageRequest([NotNull] ClaimsPrincipal creator, int pageIndex, int pageSize)
        {
            Preconditions.RequireNotNull(creator, nameof(creator));

            Creator = creator;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        [NotNull]
        public ClaimsPrincipal Creator { get; }

        public int PageIndex { get; }
        public int PageSize { get; }
    }
}