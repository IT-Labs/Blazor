using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared.Interfaces;

namespace Core.Shared.Repository
{
    /// <summary>
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> AsQueryable<T>() where T : class;

        List<T> SqlQuery<T>(string query) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void Insert<T>(T item) where T : class, IEntity;
        void Insert<T>(IEnumerable<T> items) where T : class, IEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        void Update<T>(T item) where T : class, IEntity;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <typeparam name="T"></typeparam>
        void Delete<T>(Expression<Func<T, bool>> exp) where T : class, IEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(Expression<Func<T, bool>> exp) where T : class, IEntity;


        void InsertWithChildEntities<T>(T item) where T : class, IEntity;
        void UpdateMultiple<T>(IEnumerable<T> items) where T : class, IEntity;

    }
}