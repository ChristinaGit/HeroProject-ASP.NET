using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.SignupHeroApp
{
    public sealed class SignupHeroAppRequest
    {
        public SignupHeroAppRequest([NotNull] string userName, [NotNull] string password)
        {
            Preconditions.RequireNotNull(userName, nameof(userName));
            Preconditions.RequireNotNull(password, nameof(password));

            UserName = userName;
            Password = password;
        }

        [NotNull]
        public string UserName { get; }

        [NotNull]
        public string Password { get; }
    }
}