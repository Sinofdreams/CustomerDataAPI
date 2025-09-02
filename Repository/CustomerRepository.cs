using CustomerDataAPI.Data;
using CustomerDataAPI.DTO;
using CustomerDataAPI.Helpers;
using CustomerDataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerDataAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustomerRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerModel>> GetAllCustomers(QueryObject query)
        {
            var customers = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                customers = customers.Where(c => c.Name.Contains(query.Search) || c.Email.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.CountryCode))
            {
                customers = customers.Where(c => c.CountryCode.Contains(query.CountryCode));
            }

            if (query.StartDate.HasValue)
            {
                customers = customers.Where(c => c.CreatedDate >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                query.EndDate = query.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                customers = customers.Where(c => c.CreatedDate <= query.EndDate.Value);
            }

            if (query.Active.HasValue)
            {
                customers = customers.Where(c => c.IsActive == query.Active);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                Dictionary<string, Expression<Func<CustomerModel, object>>> sortExpressions = new Dictionary<string, Expression<Func<CustomerModel, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { "name", c => c.Name },
                    { "email", c => c.Email },
                    { "createdDate", c => c.CreatedDate },
                    { "country", c => c.CountryCode }
                };

                if (!string.IsNullOrWhiteSpace(query.SortBy) && sortExpressions.TryGetValue(query.SortBy, out var sortExpression))
                {
                    customers = query.IsDescending
                        ? customers.OrderByDescending(sortExpression)
                        : customers.OrderBy(sortExpression);
                }
            }

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await customers.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<int> GetCustomerCountAsync(QueryObject query)
        {
            var queryable = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                queryable = queryable.Where(c => c.Name.Contains(query.Search) || c.Email.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.CountryCode))
            {
                queryable = queryable.Where(c => c.CountryCode.Contains(query.CountryCode));
            }

            if (query.Active.HasValue)
            {
                queryable = queryable.Where(c => c.IsActive == query.Active);
            }

            if (query.StartDate.HasValue)
            {
                queryable = queryable.Where(c => c.CreatedDate >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                query.EndDate = query.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                queryable = queryable.Where(c => c.CreatedDate <= query.EndDate.Value);
            }

            return await queryable.CountAsync();
        }

        public async Task<CustomerModel?> GetCustomer(string id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }


        public async Task<CustomerModel?> AddCustomer(CustomerModel customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<CustomerModel?> DeleteCustomer(string id)
        {
            CustomerModel? customerModel = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (customerModel == null)
            {
                return null;
            }

            _dbContext.Customers.Remove(customerModel);
            await _dbContext.SaveChangesAsync();

            return customerModel;
        }

        public async Task<CustomerModel?> UpdateCustomer(string id, Customer customer)
        {
            CustomerModel? customerModel = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (customerModel == null)
            {
                return null;
            }

            customerModel.Name = customer.Name;
            customerModel.Email = customer.Email;
            customerModel.IsActive = customer.IsActive;
            customerModel.CountryCode = customer.CountryCode;

            await _dbContext.SaveChangesAsync();

            return customerModel;
        }
    }
}
