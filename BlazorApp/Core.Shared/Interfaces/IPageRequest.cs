using Core.Shared.Enums;

namespace Core.Shared.Interfaces
{
    /// <summary>
    ///     Definition for paging request
    /// </summary>
    public interface IPageRequest : IRequest, IPageable
    {
        /// <summary>
        ///     Gets or sets sorting of the paged request
        /// </summary>
        SortOrderEnum SortOrder { get; set; }

        /// <summary>
        ///     Gets or sets sorting column of the page drequest
        /// </summary>
        string OrderColumnName{ get; set; }
    }

    public interface IPageRequest<T> : IRequest, IPageable where T : struct
    {
        bool All { get; set; }
        /// <summary>
        ///     Gets or sets sorting of the paged request
        /// </summary>
        SortOrderEnum SortOrder { get; set; }

        /// <summary>
        ///     Gets or sets sorting column of the page request
        /// </summary>
        T OrderColumnName { get; set; }
    }
}