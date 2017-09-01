using System;
using JetBrains.Annotations;

namespace HeroProject.Common
{
    public static class AspRoutes
    {
        public const string Contoller = "controller";

        public const string Action = "action";

        [NotNull]
        public static string GetControllerName<T>()
        {
            var type = typeof(T);

            var typeName = type.Name;

            var controllerName = typeName;
            if (typeName.EndsWith(Contoller, StringComparison.OrdinalIgnoreCase))
            {
                controllerName = typeName.Substring(0, typeName.Length - Contoller.Length);
            }

            return controllerName;
        }
    }
}