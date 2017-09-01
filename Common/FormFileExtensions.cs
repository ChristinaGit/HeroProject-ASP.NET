using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace HeroProject.Common
{
    public static class FormFileExtensions
    {
        [NotNull]
        public static byte[] ReadAll([NotNull] this IFormFile @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            using (var avatarStream = @this.OpenReadStream())
            {
                return avatarStream.ReadAll();
            }
        }

        [NotNull]
        public static async Task<byte[]> ReadAllAsync([NotNull] this IFormFile @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            using (var avatarStream = @this.OpenReadStream())
            {
                return await avatarStream.ReadAllAsync();
            }
        }
    }
}