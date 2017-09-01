namespace HeroProject.Application.Interactors.AddHero
{
    public sealed class AddHeroResponse
    {
        public AddHeroResponse(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}