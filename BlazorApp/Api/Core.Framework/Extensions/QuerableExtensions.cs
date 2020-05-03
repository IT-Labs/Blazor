using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Requests;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Framework.Extensions
{

    public static class QuerableExtensions
    {
        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query, IPageRequest pageRequest, out int total)
              where TEntity : class, IEntity
        {
            if (pageRequest == null)
            {
                throw new ArgumentNullException(nameof(pageRequest));
            }
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            IQueryable<TEntity> result;
            if (!string.IsNullOrEmpty(pageRequest.OrderColumnName))
            {
                result = query.OrderBy(pageRequest.OrderColumnName + " " + pageRequest.SortOrder);
            }
            else
            {
                result = query.OrderBy("Id");
            }

            total = result.Count();

            return result.Skip(pageRequest.PageSize * (pageRequest.CurrentPage - 1))
                .Take(pageRequest.PageSize);
        }


        public static IList<T> SortAndPage<T, V>(this IQueryable<T> source, Dictionary<V, Expression<Func<T, object>>> sortDictionary,
                SortablePageableRequest<V> request)
                where T : class, IEntity
                where V : struct
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var sortFunc = sortDictionary.ContainsKey(request.OrderColumnName)
                ? sortDictionary[request.OrderColumnName]
                : sortDictionary.First().Value;

            var data = request.SortOrder == SortOrderEnum.Ascending
                ? source.OrderBy(sortFunc)
                : source.OrderByDescending(sortFunc);

            return request.All
                ? data.ToList()
                : data.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                          Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly PropertyInfo NodeTypeProviderField = QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

        private static readonly MethodInfo CreateQueryParserMethod = QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly FieldInfo QueryCompilationContextFactoryField = typeof(Database).GetTypeInfo().DeclaredFields.Single(x => x.Name == "_queryCompilationContextFactory");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            if (!(query is EntityQueryable<TEntity>) && !(query is InternalDbSet<TEntity>))
            {
                throw new ArgumentException("Invalid query");
            }

            var queryCompiler = (IQueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var nodeTypeProvider = (INodeTypeProvider)NodeTypeProviderField.GetValue(queryCompiler);
            var parser = (IQueryParser)CreateQueryParserMethod.Invoke(queryCompiler, new object[] { nodeTypeProvider });
            var queryModel = parser.GetParsedQuery(query.Expression);
            var database = DataBaseField.GetValue(queryCompiler);
            var queryCompilationContextFactory = (IQueryCompilationContextFactory)QueryCompilationContextFactoryField.GetValue(database);
            var queryCompilationContext = queryCompilationContextFactory.Create(false);
            /*var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();*/

            return null;
        }

    }
}