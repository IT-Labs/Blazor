using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveName
    {
        string Name { get; }
    }

    public interface IHaveFirstName
    {
        string FirstName { get; }
    }

    public interface IHaveLastName
    {
        string LastName { get; }
    }
}