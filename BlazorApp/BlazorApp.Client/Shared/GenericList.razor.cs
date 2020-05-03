using Core.Shared.Response;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Shared
{
    public partial class GenericList<T>
    {
        [Parameter] public RenderFragment<T> ElementTemplate { get; set; }
        [Parameter] public PagedResponse<T> Elements { get; set; }
    }
}
