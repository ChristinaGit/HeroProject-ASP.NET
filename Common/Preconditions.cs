using System;
using JetBrains.Annotations;

namespace HeroProject.Common
{
    public static class Preconditions
    {
        public static bool Require(
            bool condition,
            [CanBeNull] string message = null,
            [CanBeNull] Exception ex = null)
        {
            if (!condition)
            {
                throw new PreconditionException(message, ex);
            }
            return condition;
        }

        [NotNull]
        public static T RequireNotNull<T>(
            [CanBeNull] T value,
            [CanBeNull] string paramName = null,
            [CanBeNull] Exception ex = null)
        {
            Require(value != null, $"{paramName ?? "Value"} is null.", ex);

            return value;
        }

        public static int RequireInRange(
            int value,
            int min,
            int max,
            [CanBeNull] string paramName = null,
            [CanBeNull] Exception ex = null)
        {
            Require(
                min <= value && value <= max,
                $"{paramName ?? "Value"} ({value}) is not in range [{min}, {max}].",
                ex);

            return value;
        }

        public static void Unreachable(
            [CanBeNull] string message = null,
            [CanBeNull] Exception ex = null)
        {
            Require(false, message, ex);
        }
    }
}