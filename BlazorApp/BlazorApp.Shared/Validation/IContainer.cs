using System;
using System.Collections.Generic;
using BlazorApp.Shared.Repository;

namespace BlazorApp.Shared.Validation
{
    /// <summary>
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IValidator TryGetInstance(Type type);
        IMap TryGetMapInstance(Type type);
        T GetInstance<T>();
        T TryGetInstance<T>();

        IEnumerable<T> TryGetAllByType<T>();
    }
}