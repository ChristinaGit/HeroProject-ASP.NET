using System.Collections.Generic;
using System.Linq;
using HeroProject.Common.Interactors;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.SignupHeroApp.Exception
{
    public sealed class SignupHeroAppException : InteractorException
    {
        public SignupHeroAppException([CanBeNull] IEnumerable<string> errorDescriptions = null)
        {
            ErrorDescriptions = errorDescriptions ?? Enumerable.Empty<string>();
        }

        [NotNull]
        public IEnumerable<string> ErrorDescriptions { get; }
    }
}