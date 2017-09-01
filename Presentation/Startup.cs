using HeroProject.Application;
using HeroProject.Common;
using HeroProject.Persistance;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeroProject.Presentation
{
    public sealed class Startup
    {
        [NotNull]
        private readonly IHostingEnvironment env;

        public Startup([NotNull] IHostingEnvironment env)
        {
            Preconditions.RequireNotNull(env, nameof(env));

            this.env = env;
        }

        public void ConfigureServices([NotNull] IServiceCollection services)
        {
            Preconditions.RequireNotNull(services, nameof(services));

            services.AddClean();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            services.AddHeroAppStorage(env, o => o.ConfigureHeroAppIdentityRoutes());
            services.AddHeroAppUploads("uploads");
            services.AddHeroAppInteractors();
            services.ConfigureHeroAppRoutes();
        }

        public void Configure(
            [NotNull] IApplicationBuilder app,
            [NotNull] ILoggerFactory loggerFactory)
        {
            Preconditions.RequireNotNull(app, nameof(app));
            Preconditions.RequireNotNull(loggerFactory, nameof(loggerFactory));

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseStatusCodePages();
            app.UseMvc(routes => { routes.AddHeroAppRoutes(); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                loggerFactory.AddConsole(LogLevel.Debug);
                loggerFactory.AddDebug(LogLevel.Debug);
            }
        }
    }
}