using Core.Shared.Enums;
using System;

namespace Core.Shared.Interfaces
{
    public interface IHaveIsActive
    {
        bool IsActive { get; set; }
    }
    public interface IHaveAddress
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        State? StateId { get; set; }
        Country CountryId { get; set; }
    }


    public interface IMayHaveAddress
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        State? StateId { get; set; }
        Country? CountryId { get; set; }
    }

    public interface IHaveAddressId
    {
        Guid AddressId { get; set; }
    }
}
