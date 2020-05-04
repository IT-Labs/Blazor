using BlazorApp.Client.Models;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Requests.Movies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class MoviesFilter
    {
        [Parameter] public EventCallback<GetMoviesRequest> OnSearch { get; set; }

        private GetMoviesRequest _request = new GetMoviesRequest();
        private SortAndOrder<SortColumnCodes.Movies> _sortAndOrder = new SortAndOrder<SortColumnCodes.Movies>();

        public void SearchMovies(MouseEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(_request.Title) && 
                _sortAndOrder.SortBy.Item == null && 
                _sortAndOrder.AscDesc.Item == null)
                return;

            _sortAndOrder.AddSortAndOrderItems(_request);

            OnSearch.InvokeAsync(_request);
        }

        public void Clear(MouseEventArgs args)
        {
            _sortAndOrder.Reset();
            _request = new GetMoviesRequest();

            OnSearch.InvokeAsync(_request);
        }
    }
}
