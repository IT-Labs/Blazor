using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Framework.Extensions
{
    /// <summary>
    ///     Extends Type object
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get type name without any generics info
        /// </summary>
        /// <param name="type">Type object</param>
        /// <returns></returns>
        public static string GetNameWithoutGenericArity(this Type type)
        {
            string name = type.Name;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }


        /// <summary>
        /// Returns all <see cref="Type"/> that inherits from <paramref name="baseClass"/>
        /// </summary>
        public static IEnumerable<Type> GetInheritances(this Type baseClass)
        {
            //TODO: Assembly.GetAssembly method is not supported in net core
            return Assembly.GetEntryAssembly()
             .GetTypes()
             .Where(myType => myType.GetTypeInfo().IsClass && !myType.GetTypeInfo().IsAbstract && myType.GetTypeInfo().IsSubclassOf(baseClass));
        }

        public static string[] GetIEnumerableProps(this object obj)
        {
            var propertyInfos = obj.GetType().GetProperties();

            var infos = propertyInfos
                .Where(x => x.PropertyType.IsConstructedGenericType && x.PropertyType.GetInterfaces().Any(y => y == typeof(IEnumerable)));


            var filtered = infos.Select(x => x.Name);
            return filtered.ToArray();
        }
    }
}
