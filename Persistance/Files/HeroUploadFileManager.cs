using System.IO;
using System.Threading.Tasks;
using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;

namespace HeroProject.Persistance.Files
{
    public sealed class HeroUploadFileManager : IHeroUploadFileManager
    {
        [NotNull]
        private readonly IHostingEnvironment env;

        [NotNull]
        private readonly string virtualDirectory;

        public HeroUploadFileManager([NotNull] IHostingEnvironment env, [NotNull] string directory)
        {
            Preconditions.RequireNotNull(env, nameof(env));
            Preconditions.RequireNotNull(directory, nameof(directory));

            this.env = env;
            virtualDirectory = directory;
        }

        [NotNull]
        public async Task SaveHeroAvatarAsync(int id, [NotNull] byte[] avatar)
        {
            Preconditions.RequireNotNull(avatar, nameof(avatar));

            using (var outputStream = new FileStream(GetHeroAvatarPath(id), FileMode.Create))
            {
                await outputStream.WriteAsync(avatar, 0, avatar.Length);
            }
        }

        public void DeleteHeroAvatar(int id)
        {
            var path = GetHeroAvatarPath(id);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        [NotNull]
        public string GetHeroAvatarsDirectory() => Path.Combine(env.WebRootPath, GetHeroAvatarsVirtualDirectory());

        [NotNull]
        public string GetHeroAvatarsVirtualDirectory() => Path.Combine(virtualDirectory, "images", "hero", "avatars");

        [NotNull]
        public string GetHeroAvatarPath(int id) => Path.Combine(GetHeroAvatarsDirectory(), GetAvatarFileName(id));

        [NotNull]
        public string GetHeroAvatarVirtualPath(int id) => Path.Combine(GetHeroAvatarsVirtualDirectory(),
            GetAvatarFileName(id));

        [NotNull]
        private string GetAvatarFileName(int id) => $"{id}.jpg";

        private void PrepareHeroAvatarsDirectory()
        {
            var directory = GetHeroAvatarsDirectory();
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}