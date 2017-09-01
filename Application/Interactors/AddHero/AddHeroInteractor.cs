using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Models;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace HeroProject.Application.Interactors.AddHero
{
    public sealed class AddHeroInteractor : IInteractor<AddHeroRequest, AddHeroResponse>
    {
        [NotNull]
        private readonly IHeroRepository repository;

        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        [NotNull]
        private readonly UserManager<HeroAppUser> userManager;

        public AddHeroInteractor(
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
        public async Task<AddHeroResponse> ExecuteAsync([NotNull] AddHeroRequest request)
        {
            Preconditions.RequireNotNull(request, nameof(request));

            int? id = null;
            try
            {
                id = repository.Add(
                    new HeroEntity
                    {
                        Name = request.Name,
                        Strength = request.Strength,
                        Dexterity = request.Dexterity,
                        Intelligence = request.Intelligence,
                        CreatorId = (await userManager.GetUserAsync(request.Creator)).Id
                    });

                var avatar = request.Avatar;
                if (avatar != null && avatar.Length > 0)
                {
                    await uploadManager.SaveHeroAvatarAsync(id.Value, avatar);
                }
            }
            catch (Exception)
            {
                if (id != null)
                {
                    uploadManager.DeleteHeroAvatar(id.Value);
                }
                throw;
            }

            return new AddHeroResponse(id.Value);
        }
    }
}