using System;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Dto
{
    public class EntityDescription : IHaveName, IHaveDescription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}