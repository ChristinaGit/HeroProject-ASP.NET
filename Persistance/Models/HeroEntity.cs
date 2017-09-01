using System.ComponentModel.DataAnnotations;

namespace HeroProject.Persistance.Models
{
    public class HeroEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }

        [Required]
        public string CreatorId { get; set; }
        public HeroAppUser Creator { get; set; }
    }
}