using BlazorApp.Shared;
using BlazorApp.Shared.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Core.Framework.Extensions
{
    public static class HistoryExtensions
    {
        public static List<HistoryResponse<OldNewValueResponse>> ProcessOldNewValues<T>(this HistoryData data, List<string> excludedProperties, string userFullName) where T : DeletableEntity
        {
            var result = new List<HistoryResponse<OldNewValueResponse>>();

            var oldVal = data.OldVal.DeserializeToEntity<T>();
            var newVal = data.NewVal.DeserializeToEntity<T>();

            var oldPropValues = oldVal.GetEnityPropertyValues(true, excludedProperties);
            var newPropValues = newVal.GetEnityPropertyValues(true, excludedProperties);

            foreach (var newProp in newPropValues)
            {
                var oldProp = oldPropValues.FirstOrDefault(x => x.Key == newProp.Key);
                //if the oldVal of property is the same with newVal, do not show
                if (oldProp.Value?.ToString() == newProp.Value?.ToString())
                    continue;

                result.Add(new HistoryResponse<OldNewValueResponse>
                {
                    Id = data.Id,
                    Date = data.Date,
                    Item = new OldNewValueResponse
                    {
                        Date = data.Date,
                        User = userFullName ?? string.Empty,
                        Field = newProp.Key.ToTitleCase(),
                        OldValue = oldProp.Value?.ToString() ?? string.Empty,
                        NewValue = newProp.Value?.ToString(),
                        Action = data.Operation.GetDescription()
                    }
                });
            }

            return result;
        }
    }
}
