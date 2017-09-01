using System.IO;
using HeroProject.Common;
using HeroProject.Persistance.Files;
using JetBrains.Annotations;

namespace HeroProject.Persistance
{
    public static class HeroUploadFileManagerExtensions
    {
        public static bool IsHeroAvatarExists([NotNull] this IHeroUploadFileManager @this, int id)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            return File.Exists(@this.GetHeroAvatarPath(id));
        }

        [CanBeNull]
        public static byte[] GetHeroAvatar([NotNull] this IHeroUploadFileManager @this, int id)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            byte[] avatar = null;
            if (@this.IsHeroAvatarExists(id))
            {
                using (var avatarStream = @this.OpenHeroAvatarStream(id, FileMode.Open))
                {
                    avatar = avatarStream.ReadAll();
                }
            }

            return avatar;
        }
    }
}