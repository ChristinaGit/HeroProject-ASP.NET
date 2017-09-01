using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace HeroProject.Presentation.Hero.ViewModels
{
    public abstract class FormViewModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [CanBeNull]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public int Strength { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public int Dexterity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public int Intelligence { get; set; }

        [CanBeNull]
        public IFormFile Avatar { get; set; }
    }
}
