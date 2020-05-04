using Core.Shared.Enums;
using Core.Shared.Interfaces;
using System;

namespace BlazorApp.Client.Models
{
    public class SortAndOrder<T> where T : struct, Enum
    {
        public DropdownData<T> SortBy = new DropdownData<T>();
        public DropdownData<SortOrderEnum> AscDesc = new DropdownData<SortOrderEnum>();

        public IPageRequest<T> AddSortAndOrderItems(IPageRequest<T> request)
        {
            if (SortBy.Item != null)
                request.OrderColumnName = SortBy.Item.Value;

            if (AscDesc.Item != null)
                request.SortOrder = AscDesc.Item.Value;

            return request;
        }

        public void Reset()
        {
            SortBy = new DropdownData<T>();
            AscDesc = new DropdownData<SortOrderEnum>();
        }
    }
}
