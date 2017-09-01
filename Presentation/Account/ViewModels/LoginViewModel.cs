using JetBrains.Annotations;

namespace HeroProject.Presentation.Account.ViewModels
{
    public sealed class LoginViewModel : FormViewModel
    {
        [CanBeNull]
        public string ReturnUrl { get; set; }
    }
}