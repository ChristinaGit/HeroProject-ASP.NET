using System.Collections.Generic;
using System.IO;
using HeroProject.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HeroProject.Persistance
{
    public static class HeroPersistanceConfiguration
    {
        private const string AppSettings = @"Persistance\Properties\appsettings.json";
        private const string ConnectionStringKeyHeroDb = @"HeroDb";

        [NotNull]
        public static IConfigurationRoot Create([NotNull] IHostingEnvironment env)
        {
            Preconditions.RequireNotNull(env, nameof(env));

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(env.ContentRootPath).FullName)
                .AddJsonFile(AppSettings)
                .Build();
        }

        [NotNull]
        public static string GetHeroDbConnectionString([NotNull] this IConfigurationRoot @this)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            var connectionString = @this.GetConnectionString(ConnectionStringKeyHeroDb);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new KeyNotFoundException(ConnectionStringKeyHeroDb);
            }

            return connectionString;
        }
    }
}