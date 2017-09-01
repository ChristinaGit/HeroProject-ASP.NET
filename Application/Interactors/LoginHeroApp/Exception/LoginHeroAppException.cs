using System.Collections.Generic;
using System.Linq;
using HeroProject.Common.Interactors;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.LoginHeroApp.Exception
{
    public class LoginHeroAppException : InteractorException
    {
        public LoginHeroAppException([CanBeNull] IEnumerable<string> errorDescriptions = null)
        {
            ErrorDescriptions = errorDescriptions ?? Enumerable.Empty<string>();
        }

        [NotNull]
        public IEnumerable<string> ErrorDescriptions { get; }
    }
}