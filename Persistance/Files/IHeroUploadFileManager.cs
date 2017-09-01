using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HeroProject.Persistance.Files
{
    public interface IHeroUploadFileManager
    {
        [NotNull]
        Task SaveHeroAvatarAsync(int id, [NotNull] byte[] avatar);

        [NotNull]
        FileStream OpenHeroAvatarStream(int id, FileMode fileMode);

        void DeleteHeroAvatar(int id);

        [NotNull]
        string GetHeroAvatarPath(int id);

        [NotNull]
        string GetHeroAvatarVirtualPath(int id);

        [NotNull]
        string GetHeroAvatarsDirectory();

        [NotNull]
        string GetHeroAvatarsVirtualDirectory();
    }
}