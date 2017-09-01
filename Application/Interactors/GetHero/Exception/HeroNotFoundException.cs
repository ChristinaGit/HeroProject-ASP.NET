using HeroProject.Common.Interactors;

namespace HeroProject.Application.Interactors.GetHero.Exception
{
    public sealed class HeroNotFoundException : InteractorException
    {
        public HeroNotFoundException(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}