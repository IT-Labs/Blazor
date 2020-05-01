using System;
using System.Collections.Generic;
using BlazorApp.Shared;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Validation;

namespace Core.Framework
{
    /// <summary>
    ///     Factory for generating new Validator objects
    /// </summary>
    public static class ContainerFactory
    {
        private static IContainer _container;

        /// <summary>
        /// Sets the container instance
        /// </summary>
        /// <param name="container"></param>
        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        ///     Returns created Validator
        /// </summary>
        /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
        /// <returns>Definition of created validator</returns>
        public static IValidator<TEntity> GetValidator<TEntity>()
        {
            var validator = _container?.TryGetInstance(typeof(IValidator<TEntity>));
            return validator as IValidator<TEntity>;
        }

        /// <summary>
        ///     Returns created Validator
        /// </summary>
        /// <param name="t">Type of Entity on which this validation rule is implemented</param>
        /// <returns>Definition of created validator</returns>
        public static IValidator GetValidator(Type t)
        {
            var validator = _container?.TryGetInstance(t);
            return validator;
        }

        public static IMap<TEntity, TRequest> GetMap<TEntity, TRequest>() where TEntity : AuditableEntity
        {
            var map = _container?.TryGetMapInstance(typeof(IMap<TEntity, TRequest>));
            return map as IMap<TEntity, TRequest>;
        }
        /// <summary>
        /// Call this for interface, not concrete class
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static TEntity TryGetInstance<TEntity>() where TEntity : class
        {
            var instance = _container.TryGetInstance<TEntity>();
            return instance;
        }
        /// <summary>
        /// CAll this for concrete class, not interface
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static TEntity GetInstance<TEntity>() where TEntity : class
        {
            var instance = _container.GetInstance<TEntity>();
            return instance;
        }
        public static IEnumerable<T> GetAllInstances<T>()
        {
            return _container?.TryGetAllByType<T>();
        }
    }
}