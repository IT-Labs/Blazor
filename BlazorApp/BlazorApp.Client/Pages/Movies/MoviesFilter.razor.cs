using BlazorApp.Client.Models;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class MoviesFilter
    {
        [Parameter] public EventCallback<GetMoviesRequest> OnSearch { get; set; }

        private GetMoviesRequest _request = new GetMoviesRequest();
        private string _titleFilter;
        private DropdownData<SortColumnCodes.Movies> _sortBy = new DropdownData<SortColumnCodes.Movies>();
        private DropdownData<SortOrderEnum> _ascDesc = new DropdownData<SortOrderEnum>();

        public void SearchMovies(MouseEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(_titleFilter) && _sortBy.Item == null && _ascDesc.Item == null)
                return;

            _request.Title = _titleFilter;

            if (_sortBy.Item != null)
                _request.OrderColumnName = _sortBy.Item.Value;

            if (_ascDesc.Item != null)
                _request.SortOrder = _ascDesc.Item.Value;

            OnSearch.InvokeAsync(_request);
        }

        public void Clear(MouseEventArgs args)
        {
            _titleFilter = string.Empty;
            _sortBy = new DropdownData<SortColumnCodes.Movies>();
            _ascDesc = new DropdownData<SortOrderEnum>();
            _request = new GetMoviesRequest();

            OnSearch.InvokeAsync(_request);
        }
    }
}
