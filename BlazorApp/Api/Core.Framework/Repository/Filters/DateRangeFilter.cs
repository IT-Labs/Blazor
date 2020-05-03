using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Framework.Repository.Queries;

namespace Core.Framework.Repository.Filters
{
    public class DateRangeFilter<T> : IQueryFilter<T> where T : DeletableEntity, IHaveDateRange
    {
        private readonly DateTime _date;
        private readonly DateCompare _compare;

        public enum DateCompare
        {
            None,
            Before,
            In,
            After
        }

        public DateRangeFilter(DateTime date, DateCompare compare)
        {
            _date = date;
            _compare = compare;
        }

        public virtual IQueryable<T> Filter(IQueryable<T> query)
        {
            switch (_compare)
            {
                case DateCompare.None:
                    return query;
                case DateCompare.Before:
                    return query.Where(x => x.StartDate.Date < _date.Date);
                case DateCompare.In:
                    return query.Where(x => x.StartDate.Date >= _date.Date && _date.Date < x.EndDate.Date);
                case DateCompare.After:
                    return query.Where(x => DateTime.Today > x.EndDate.Date);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public static class FilterExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, List<IQueryFilter<T>> filters)
        {
            if (query == null || filters == null) return query;
            foreach (var filter in filters)
            {
                query = filter.Filter(query);
            }
            return query;
        }
    }
}
