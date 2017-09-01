using System;
using JetBrains.Annotations;

namespace HeroProject.Common.Interactors
{
    public class InteractorException : Exception
    {
        public InteractorException()
        {
        }

        public InteractorException([CanBeNull] string message) : base(message)
        {
        }

        public InteractorException(
            [CanBeNull] string message,
            [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }
    }
}