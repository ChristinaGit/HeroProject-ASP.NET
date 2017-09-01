using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HeroProject.Presentation.Account.ViewModels
{
    public abstract class FormViewModel
    {
        [Required]
        [CanBeNull]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        [CanBeNull]
        public string Password { get; set; }
    }
}
