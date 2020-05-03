using Core.Shared.Repository;
using Core.Shared.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework
{
    public class InfrastructureContainer : IContainer
    {
        private readonly Lamar.IContainer _container;

        public InfrastructureContainer(Lamar.IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public IValidator TryGetInstance(Type type)
        {
            return _container.TryGetInstance(type) as IValidator;
        }

        public IMap TryGetMapInstance(Type type)
        {
            return _container.TryGetInstance(type) as IMap;
        }
        public T TryGetInstance<T>()
        {
            var result = _container.TryGetInstance<T>();
            return result;
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public IEnumerable<T> TryGetAllByType<T>()
        {
            return _container.GetAllInstances<T>();
        }
    }
}
