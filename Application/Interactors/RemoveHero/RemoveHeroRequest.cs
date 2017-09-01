namespace HeroProject.Application.Interactors.RemoveHero
{
    public sealed class RemoveHeroRequest
    {
        public RemoveHeroRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}