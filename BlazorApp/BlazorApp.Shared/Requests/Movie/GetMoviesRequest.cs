using BlazorApp.Shared.Enums.Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Requests.Movie
{
    public class GetMoviesRequest : SortablePageableRequest<SortColumnCodes.Movies>
    {

    }
}
