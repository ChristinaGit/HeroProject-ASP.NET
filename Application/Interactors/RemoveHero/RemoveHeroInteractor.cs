using System.Threading.Tasks;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.RemoveHero
{
    public sealed class RemoveHeroInteractor : IActionInteractor<RemoveHeroRequest>
    {
        [NotNull]
        private readonly IHeroRepository repository;

        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        public RemoveHeroInteractor(
            [NotNull] IHeroRepository repository,
            [NotNull] IHeroUploadFileManager uploadManager)
        {
            Preconditions.RequireNotNull(repository, nameof(repository));
            Preconditions.RequireNotNull(uploadManager, nameof(uploadManager));

            this.repository = repository;
            this.uploadManager = uploadManager;
        }

        [NotNull]
        public Task ExecuteAsync([NotNull] RemoveHeroRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            return Task.Run(() =>
            {
                repository.Remove(request.Id);
                uploadManager.DeleteHeroAvatar(request.Id);
            });
        }
    }
}