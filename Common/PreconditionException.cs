using System;
using JetBrains.Annotations;

namespace HeroProject.Common
{
    public class PreconditionException : Exception
    {
        public PreconditionException()
        {
        }

        public PreconditionException([CanBeNull] string message) : base(message)
        {
        }

        public PreconditionException(
            [CanBeNull] string message,
            [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }
    }
}