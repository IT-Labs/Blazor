using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlazorApp.Shared;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Commands
{
    public class SetMultipleEntityIsActiveCommand<T, TEntity> : BaseCommand<SetMultipleActiveStatusRequest, bool>
        where T : DeletableEntity
        where TEntity : DeletableEntity
    {
        private readonly Func<IQueryable<T>, IQueryable<T>> _queryInclude;
        public Expression<Func<T, bool>> Predicate { get; set; }
        private readonly Expression<Func<T, IEnumerable<TEntity>>> _select;

        public SetMultipleEntityIsActiveCommand(Func<IQueryable<T>, IQueryable<T>> queryInclude, Expression<Func<T, IEnumerable<TEntity>>> select)
        {
            _select = select;
            _queryInclude = queryInclude;
        }

        public override IResult<bool> Execute(IContext dataContext)
        {
            if (_select == null)
                throw new ArgumentNullException(nameof(_select));
            if (_queryInclude == null)
                throw new ArgumentNullException(nameof(_queryInclude));

            var query = dataContext.AsQueryable<T>();
            if (_queryInclude != null)
            {
                query = _queryInclude(query);
            }
            if (Predicate != null)
            {
                query = query.Where(Predicate);
            }

            var entities = query.SelectMany(_select).ToList();

            foreach (var entity in entities)
            {
                if (!Request.SetParent && (entity.IsActive ) == Request.IsActive)
                {
                    return new Result<bool>(
                        //errorCode: Request.IsActive ? ValidationCodes.Common.Cmn022 : ValidationCodes.Common.Cmn023,
                        entityName: typeof(T).Name.SplitCamelCase());
                }
               
                 entity.IsActive = Request.IsActive;               
               

                entity.UpdateAuditableProperties(
                    Request.IsActive
                        ? AuditableEntity.AuditableAction.Reactivate
                        : AuditableEntity.AuditableAction.Delete, UserId);
            }

            dataContext.UpdateMultiple(entities);

            return new Result<bool>
            {
                Value = true
            };
        }
    }
}