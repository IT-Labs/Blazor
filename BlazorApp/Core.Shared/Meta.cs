namespace Core.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class Meta
    {
        /// <summary>
        ///     Gets or sets total numer of records
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        ///     Gets or sets current page
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        ///     Gets or sets page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets unique numer of records
        /// </summary>
        public int UniqueTotal { get; set; }
    }
}