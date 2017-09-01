using System.Security.Claims;
using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.AddHero
{
    public sealed class AddHeroRequest
    {
        public AddHeroRequest(
            [NotNull] ClaimsPrincipal creator,
            [NotNull] string name,
            [CanBeNull] byte[] avatar,
            int strength,
            int dexterity,
            int intelligence)
        {
            Preconditions.RequireNotNull(creator, nameof(creator));
            Preconditions.RequireNotNull(name, nameof(name));

            Name = name;
            Avatar = avatar;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Creator = creator;
        }

        [NotNull]
        public ClaimsPrincipal Creator { get; }

        [NotNull]
        public string Name { get; }

        public int Strength { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }

        [CanBeNull]
        public byte[] Avatar { get; }
    }
}