
using CustomerDataAPI.DTO;
using CustomerDataAPI.Models;
using System.Runtime.CompilerServices;

namespace CustomerDataAPI.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerModel ToCustomerModel(this Customer createCustomer)
        {
            return new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = createCustomer.Name,
                Email = createCustomer.Email,
                CreatedDate = createCustomer.CreatedDate,
                IsActive = createCustomer.IsActive,
                CountryCode = createCustomer.CountryCode
            };
        }

        public static Customer ToCustomer(this CustomerModel customer)
        {
            return new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                CreatedDate = customer.CreatedDate,
                IsActive = customer.IsActive,
                CountryCode = customer.CountryCode
            };
        }
    }
}
