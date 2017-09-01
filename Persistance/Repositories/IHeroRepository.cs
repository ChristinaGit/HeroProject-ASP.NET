using HeroProject.Common.Persistance;
using HeroProject.Persistance.Models;

namespace HeroProject.Persistance.Repositories
{
    public interface IHeroRepository : IRepository<HeroEntity, int>
    {
    }
}