using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HeroProject.Common
{
    public static class StreamExtensions
    {
        [NotNull]
        public static byte[] ReadAll([NotNull] this Stream @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            var data = new byte[@this.Length];

            @this.Read(data, 0, data.Length);

            return data;
        }

        [NotNull]
        public static async Task<byte[]> ReadAllAsync([NotNull] this Stream @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            var data = new byte[@this.Length];

            await @this.ReadAsync(data, 0, data.Length);

            return data;
        }
    }
}