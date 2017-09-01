using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.UpdateHero
{
    public sealed class UpdateHeroRequest
    {
        public UpdateHeroRequest(
            int id,
            [NotNull] string name,
            [CanBeNull] byte[] avatar,
            int strength,
            int dexterity,
            int intelligence)
        {
            Preconditions.RequireNotNull(name, nameof(name));

            Id = id;
            Name = name;
            Avatar = avatar;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
        }

        public int Id { get; }

        [NotNull]
        public string Name { get; }

        [CanBeNull]
        public byte[] Avatar { get; }

        public int Strength { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }
    }
}