using System;
using System.Linq;
using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Framework.Repository.Queries;

namespace Core.Framework.Repository.Filters
{
    public class EndDateFilter<T> : IQueryFilter<T> where T
        : AuditableEntity, IHaveEndDate
    {
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;

        public EndDateFilter(DateTime fromDate, DateTime toDate)
        {
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public IQueryable<T> Filter(IQueryable<T> query)
        {
            return query.Where(x => x.EndDate >= _fromDate && x.EndDate <= _toDate);
        }
    }
   
}
