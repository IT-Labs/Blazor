using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApp.Client.Shared
{
    public partial class GenericList<T>
    {
        [Parameter] public RenderFragment<T> ElementTemplate { get; set; }
        [Parameter] public List<T> Elements { get; set; }
    }
}
