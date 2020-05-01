using System.Collections.Generic;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
{
    public abstract class SortablePageableRequest<T> : PageableRequest, IPageRequest<T> where T : struct
    {
        /// <summary>
        ///     Gets or sets sorting of the page request
        /// </summary>
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.Descending;

        /// <summary>
        ///     Gets or sets sorting column of the page request
        /// </summary>
        public T OrderColumnName { get; set; }

    }

    public abstract class PageRequest : IPageRequest
    {
        /// <summary>
        ///     Gets or sets current page
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        ///     Gts or sets page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        ///     Gets or sets sorting of the page request
        /// </summary>
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.Descending;

        /// <summary>
        ///     Gets or sets sorting column of the page request
        /// </summary>
        public string OrderColumnName { get; set; }
    }
}