﻿using Core.Shared.Response;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorApp.Client.Shared
{
    public partial class GenericList<T>
    {
        [Parameter] public string ContainerClassName { get; set; }
        [Parameter] public RenderFragment<T> ElementTemplate { get; set; }
        [Parameter] public PagedResponse<T> Elements { get; set; }
        [Parameter] public EventCallback<int> OnPageChange { get; set; }
        [Parameter] public int ActivePage 
        { 
            get
            {
                return _activePage;
            }
            set
            {
                _activePage = value;
                OnPageChange.InvokeAsync(value + 1);
            }
        }

        private int _activePage;

        private int GetPageCount()
        {
            var meta = Elements.Meta;
            var total = (double)meta.TotalCount / (double)meta.PageSize;
            return (int)Math.Ceiling(total);
        }
    }
}
