using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class MoviesFilter
    {
        [Parameter] public EventCallback<string> OnSearch { get; set; }

        private string _titleFilter;

        public void SearchMovies(MouseEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(_titleFilter))
                return;

            OnSearch.InvokeAsync(_titleFilter);
        }

        public void Clear(MouseEventArgs args)
        {
            _titleFilter = string.Empty;
            OnSearch.InvokeAsync(_titleFilter);
        }
    }
}
