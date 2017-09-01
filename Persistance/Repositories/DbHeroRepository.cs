using System.Linq;
using HeroProject.Common;
using HeroProject.Persistance.Databases;
using HeroProject.Persistance.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace HeroProject.Persistance.Repositories
{
    public sealed class DbHeroRepository : IHeroRepository
    {
        [NotNull]
        private readonly HeroDbContext dbContext;

        public DbHeroRepository([NotNull] HeroDbContext dbContext)
        {
            Preconditions.RequireNotNull(dbContext, nameof(dbContext));

            this.dbContext = dbContext;
        }

        public void Remove(int id)
        {
            dbContext.Entry(dbContext.Heroes.First(e => e.Id == id)).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        [NotNull]
        public IQueryable<HeroEntity> GetAll() => dbContext.Heroes;

        public int Add([NotNull] HeroEntity entity)
        {
            Preconditions.RequireNotNull(entity, nameof(entity));

            dbContext.Heroes.Add(entity);
            dbContext.SaveChanges();

            return entity.Id;
        }

        public void Update([NotNull] HeroEntity entity)
        {
            Preconditions.RequireNotNull(entity, nameof(entity));

            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
    }
}