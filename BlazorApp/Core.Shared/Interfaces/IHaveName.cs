using System;

namespace Core.Shared.Interfaces
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