using HeroProject.Common.Interactors;

namespace HeroProject.Application.Interactors.GetHeroAvatar.Exception
{
    public sealed class HeroAvatarNotFoundException : InteractorException
    {
        public HeroAvatarNotFoundException(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}