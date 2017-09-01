using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHero
{
    public sealed class GetHeroResponse
    {
        public GetHeroResponse(
            int id,
            [NotNull] string name,
            [CanBeNull] string avatarFileName,
            int strength,
            int dexterity,
            int intelligence)
        {
            Id = id;
            Name = name;
            AvatarFileName = avatarFileName;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
        }

        public int Id { get; }

        [NotNull]
        public string Name { get; }

        [CanBeNull]
        public string AvatarFileName { get; }

        public int Strength { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }
    }
}