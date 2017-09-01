using System.Linq;
using System.Threading.Tasks;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.UpdateHero
{
    public sealed class UpdateHeroInteractor : IActionInteractor<UpdateHeroRequest>
    {
        [NotNull]
        private readonly IHeroRepository repository;

        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        public UpdateHeroInteractor(
            [NotNull] IHeroRepository repository,
            [NotNull] IHeroUploadFileManager uploadManager)
        {
            Preconditions.RequireNotNull(repository, nameof(repository));
            Preconditions.RequireNotNull(uploadManager, nameof(uploadManager));

            this.repository = repository;
            this.uploadManager = uploadManager;
        }

        [NotNull]
        public async Task ExecuteAsync([NotNull] UpdateHeroRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            var entity = repository.GetAll().First(m => m.Id == request.Id);

            entity.Name = request.Name;
            entity.Strength = request.Strength;
            entity.Dexterity = request.Dexterity;
            entity.Intelligence = request.Intelligence;

            repository.Update(entity);

            var avatar = request.Avatar;
            if (avatar != null && avatar.Length > 0)
            {
                await uploadManager.SaveHeroAvatarAsync(request.Id, avatar);
            }
        }
    }
}