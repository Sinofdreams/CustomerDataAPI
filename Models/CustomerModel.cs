using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataAPI.Models
{
    [Table("MarcoCustomers")]
    public class CustomerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public string CountryCode { get; set; }
    }
}
