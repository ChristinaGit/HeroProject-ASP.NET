using System.Linq;
using System.Threading.Tasks;
using HeroProject.Application.Interactors.GetHero.Exception;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHero
{
    public sealed class GetHeroInteractor : IInteractor<GetHeroRequest, GetHeroResponse>
    {
        [NotNull]
        private readonly IHeroRepository repository;

        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        public GetHeroInteractor(
            [NotNull] IHeroRepository repository,
            [NotNull] IHeroUploadFileManager uploadManager)
        {
            Preconditions.RequireNotNull(repository, nameof(repository));
            Preconditions.RequireNotNull(uploadManager, nameof(uploadManager));

            this.repository = repository;
            this.uploadManager = uploadManager;
        }

        [NotNull]
        public Task<GetHeroResponse> ExecuteAsync(GetHeroRequest request)
        {
            return Task.Run(() =>
            {
                var entity = repository.GetAll().FirstOrDefault(e => e.Id == request.Id);

                if (entity == null)
                {
                    throw new HeroNotFoundException(request.Id);
                }

                return new GetHeroResponse(
                    entity.Id,
                    entity.Name,
                    uploadManager.IsHeroAvatarExists(entity.Id)
                        ? uploadManager.GetHeroAvatarVirtualPath(entity.Id)
                        : null,
                    entity.Strength,
                    entity.Dexterity,
                    entity.Intelligence);
            });
        }
    }
}