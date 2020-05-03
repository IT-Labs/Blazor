using BlazorApp.Shared.Enums.Sort;
using Core.Shared.Requests;

namespace BlazorApp.Shared.Requests.Movies
{
    public class GetMoviesRequest : SortablePageableRequest<SortColumnCodes.Movies>
    {

    }
}
