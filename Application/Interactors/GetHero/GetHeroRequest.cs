namespace HeroProject.Application.Interactors.GetHero
{
    public sealed class GetHeroRequest
    {
        public GetHeroRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}