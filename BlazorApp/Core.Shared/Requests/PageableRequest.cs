using Core.Shared.Interfaces;

namespace Core.Shared.Requests
{
    public abstract class PageableRequest : IPageable
    {
        public bool All { get; set; }
        /// <summary>
        ///     Gets or sets current page
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        ///     Gts or sets page size
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
