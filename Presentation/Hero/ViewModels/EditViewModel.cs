using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace HeroProject.Presentation.Hero.ViewModels
{
    public sealed class EditViewModel : FormViewModel
    {
        public int Id { get; set; }
    }
}