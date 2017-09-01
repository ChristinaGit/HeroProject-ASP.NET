using System.Linq;
using System.Threading.Tasks;
using HeroProject.Application.Interactors.GetHeroesPage.Exception;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Models;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace HeroProject.Application.Interactors.GetHeroesPage
{
    public sealed class GetHeroesPageInteractor :
        IInteractor<GetHeroesPageRequest, GetHeroesPageResponse>
    {
        [NotNull]
        private readonly IHeroRepository repository;

        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        [NotNull]
        private readonly UserManager<HeroAppUser> userManager;

        public GetHeroesPageInteractor(
            [NotNull] IHeroRepository repository,
            [NotNull] IHeroUploadFileManager uploadManager,
            [NotNull] UserManager<HeroAppUser> userManager)
        {
            Preconditions.RequireNotNull(repository, nameof(repository));
            Preconditions.RequireNotNull(uploadManager, nameof(uploadManager));
            Preconditions.RequireNotNull(userManager, nameof(userManager));

            this.repository = repository;
            this.uploadManager = uploadManager;
            this.userManager = userManager;
        }

        [NotNull]
        public async Task<GetHeroesPageResponse> ExecuteAsync([NotNull] GetHeroesPageRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;

            var entities = repository.GetAll().ToArray();
            var itemCount = entities.Length;

            var pageCount = itemCount / pageSize + (itemCount % pageSize == 0 ? 0 : 1);

            if (pageIndex > pageCount)
            {
                if (pageCount == 0)
                {
                    return new GetHeroesPageResponse(Enumerable.Empty<GetHeroesPageResponse.PageItem>(), 0, 1);
                }
                throw new PageIndexOutOfRangeException(pageCount);
            }

            var user = await userManager.GetUserAsync(request.Creator);

            return new GetHeroesPageResponse(
                entities
                    .Where(m => m.CreatorId == user.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(t => t.Id)
                    .Select(e =>
                        new GetHeroesPageResponse.PageItem(
                            e.Id,
                            e.Name,
                            uploadManager.IsHeroAvatarExists(e.Id)
                                ? uploadManager.GetHeroAvatarVirtualPath(e.Id)
                                : null)),
                (pageIndex - 1) * pageSize + 1,
                pageCount);
        }
    }
}