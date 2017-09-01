using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHeroAvatar
{
    public sealed class GetHeroAvatarResponse
    {
        public GetHeroAvatarResponse(
            [NotNull] string fileName,
            [NotNull] string mimeType)
        {
            Preconditions.RequireNotNull(fileName, nameof(fileName));
            Preconditions.RequireNotNull(mimeType, nameof(mimeType));

            FileName = fileName;
            MimeType = mimeType;
        }

        [NotNull]
        public string FileName { get; }

        [NotNull]
        public string MimeType { get; }
    }
}