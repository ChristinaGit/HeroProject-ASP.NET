using JetBrains.Annotations;

namespace HeroProject.Presentation.Hero.ViewModels
{
    public sealed class DetailsViewModel
    {
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string AvatarFileName { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
    }
}