using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using Core.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Framework
{
    public abstract class CoreDataContext : DbContext, IContext
    {
        private readonly ILogger<CoreDataContext> _logger;

        protected CoreDataContext(DbContextOptions options, ILogger<CoreDataContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        private void SetContextEntry<T>(T item, EntityState state = EntityState.Added) where T : class, IEntity
        {

            var entry = Entry(item);
            if (state == EntityState.Added)
            {
                Set<T>().Add(item);
                entry.State = state;
            }

            if (entry.State == EntityState.Detached)
            {

                //try
                //{
                Set<T>().Attach(item);
                //}
                //catch
                //{
                //    Set<T>().Add(item);
                //}

                entry.State = state;
            }

        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {

            return Set<T>().AsQueryable();
        }

        public List<T> SqlQuery<T>(string query) where T : class, new()
        {
            var result = new List<T>();
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        result.Add(row.MapDataRowToEntity<T>());
                    }
                }

                Database.CloseConnection();
            }

            return result;
        }

        public void Insert<T>(T item) where T : class, IEntity
        {
            SetContextEntry(item);

            SaveChanges();

        }

        public void Insert<T>(IEnumerable<T> items) where T : class, IEntity
        {
            foreach (var item in items)
            {
                SetContextEntry(item);
            }
            SaveChanges();
        }

        public void Update<T>(T item) where T : class, IEntity
        {

            SetContextEntry(item, EntityState.Modified);
            SaveChanges();

        }

        public void Delete<T>(Expression<Func<T, bool>> exp) where T : class, IEntity
        {

            var items = Set<T>().Where(exp).ToList();
            if (!items.Any())
            {
                return;
            }
            Set<T>().RemoveRange(items);
            SaveChanges();

        }

        public T Get<T>(Expression<Func<T, bool>> exp) where T : class, IEntity
        {
            return Set<T>().FirstOrDefault(exp);
        }

        public void InsertWithChildEntities<T>(T item) where T : class, IEntity
        {
            Set<T>().Add(item);
            SaveChanges();

        }

        public void UpdateMultiple<T>(IEnumerable<T> items) where T : class, IEntity
        {
            foreach (var item in items)
            {
                SetContextEntry(item, EntityState.Modified);
            }

            SaveChanges();
        }

        private void LogException(string errorMessage)
        {
            _logger.LogError(errorMessage);
            throw new Exception(errorMessage);
        }
    }
}
