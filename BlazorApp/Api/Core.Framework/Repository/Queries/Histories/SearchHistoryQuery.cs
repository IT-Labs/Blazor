using BlazorApp.Shared;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests.Histories;
using Core.Framework.Extensions;
using System.Linq;

namespace Core.Framework.Repository.Queries.Histories
{
    public class SearchHistoryQuery<T> : BaseListQuery<SearchHistoryRequest, HistoryData> where T : DeletableEntity
    {
        public override IListResult<HistoryData> Execute(IContext dataContext)
        {
            string tableName = $"h_history_tables_{typeof(T).Name.ToPostgreConvention().ToPlural()}";
            string query = $"SELECT * FROM ucc.{tableName} ";

            if (Request.RecordId.HasValue && Request.Date.HasValue)
                query += $"WHERE {nameof(HistoryData.RecordId).ToPostgreConvention()} = '{Request.RecordId}' " +
                    $" AND {nameof(HistoryData.Date).ToPostgreConvention()}='{Request.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}'";
            else if (Request.NewValParameters.Any())
            {
                query += "WHERE new_val @>'{";
                foreach (var param in Request.NewValParameters)
                {
                    query += "\"" + param.Key.ToPostgreConvention() + "\": \"" + param.Value + "\",";
                }
                query = query.TrimEnd(',') + "}'";
            }

            var data = dataContext.SqlQuery<HistoryData>(query);

            return new ListResult<HistoryData>(data, Request)
            {
                Total = data.Count
            };
        }
    }
}
