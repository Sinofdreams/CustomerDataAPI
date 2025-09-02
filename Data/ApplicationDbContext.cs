using CustomerDataAPI.DTO;
using CustomerDataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace CustomerDataAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<CustomerModel> Customers { get; set; }

    }
}
