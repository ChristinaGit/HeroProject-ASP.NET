using System.Linq;
using JetBrains.Annotations;

namespace HeroProject.Common.Persistance
{
    public interface IRepository<TEntity, TKey>
    {
        [NotNull]
        IQueryable<TEntity> GetAll();

        int Add([NotNull] TEntity entity);

        void Remove(TKey id);

        void Update([NotNull] TEntity entity);
    }
}