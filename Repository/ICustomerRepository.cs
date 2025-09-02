using CustomerDataAPI.DTO;
using CustomerDataAPI.Helpers;
using CustomerDataAPI.Models;

namespace CustomerDataAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetAllCustomers(QueryObject query);
        Task<int> GetCustomerCountAsync(QueryObject query);
        Task<CustomerModel?> GetCustomer(string id);
        Task<CustomerModel?> AddCustomer(CustomerModel customer);
        Task<CustomerModel?> UpdateCustomer(string id, Customer customer);
        Task<CustomerModel?> DeleteCustomer(string id);

    }
}
