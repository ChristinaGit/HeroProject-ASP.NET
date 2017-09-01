using System.Threading.Tasks;
using HeroProject.Application.Interactors.GetHeroAvatar.Exception;
using HeroProject.Common;
using HeroProject.Common.Interactors;
using HeroProject.Persistance;
using HeroProject.Persistance.Files;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHeroAvatar
{
    public sealed class GetHeroAvatarInteractor : IInteractor<GetHeroAvatarRequest, GetHeroAvatarResponse>
    {
        [NotNull]
        private readonly IHeroUploadFileManager uploadManager;

        public GetHeroAvatarInteractor([NotNull] IHeroUploadFileManager uploadManager)
        {
            Preconditions.RequireNotNull(uploadManager, nameof(uploadManager));

            this.uploadManager = uploadManager;
        }

        [NotNull]
        public Task<GetHeroAvatarResponse> ExecuteAsync(GetHeroAvatarRequest request)
        {
            if (!uploadManager.IsHeroAvatarExists(request.Id))
            {
                return Task.FromException<GetHeroAvatarResponse>(new HeroAvatarNotFoundException(request.Id));
            }

            return Task.FromResult(
                new GetHeroAvatarResponse(
                    uploadManager.GetHeroAvatarVirtualPath(request.Id),
                    MimeTypes.Image.Jpeg));
        }
    }
}