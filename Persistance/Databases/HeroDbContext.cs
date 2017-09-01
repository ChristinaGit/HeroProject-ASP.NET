using HeroProject.Persistance.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeroProject.Persistance.Databases
{
    public sealed class HeroDbContext : IdentityDbContext<IdentityUser>
    {
        public HeroDbContext(DbContextOptions<HeroDbContext> options)
            : base(options)
        {
        }

        public DbSet<HeroEntity> Heroes { get; set; }
    }
}