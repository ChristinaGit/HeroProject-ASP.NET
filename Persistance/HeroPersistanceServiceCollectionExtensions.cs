using System;
using HeroProject.Common;
using HeroProject.Persistance.Databases;
using HeroProject.Persistance.Files;
using HeroProject.Persistance.Models;
using HeroProject.Persistance.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeroProject.Persistance
{
    public static class HeroPersistanceServiceCollectionExtensions
    {
        [NotNull]
        public static IServiceCollection AddHeroAppStorage(
            [NotNull] this IServiceCollection @this,
            [NotNull] IHostingEnvironment env,
            [CanBeNull] Action<IdentityOptions> setupAction = null)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));
            Preconditions.RequireNotNull(env, nameof(env));

            var configuration = HeroPersistanceConfiguration.Create(env);

            @this
                .AddDbContext<HeroDbContext>(
                    o => o.UseSqlServer(configuration.GetHeroDbConnectionString()))
                .AddIdentity<HeroAppUser, IdentityRole>(setupAction)
                .AddEntityFrameworkStores<HeroDbContext>();

            @this.AddTransient<IHeroRepository, DbHeroRepository>();

            return @this;
        }

        [NotNull]
        public static IServiceCollection AddHeroAppUploads(
            [NotNull] this IServiceCollection @this,
            [NotNull] string directory)
        {
            Preconditions.RequireNotNull(@this, nameof(@this));

            @this.AddSingleton<IHeroUploadFileManager>(
                s => new HeroUploadFileManager(s.GetRequiredService<IHostingEnvironment>(), directory));

            return @this;
        }
    }
}