namespace HeroProject.Application.Interactors.GetHeroAvatar
{
    public sealed class GetHeroAvatarRequest
    {
        public GetHeroAvatarRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}