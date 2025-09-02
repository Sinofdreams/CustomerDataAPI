using System.ComponentModel.DataAnnotations;

namespace CustomerDataAPI.DTO
{
    public class Customer
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email{ get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(2, ErrorMessage = "Country Code cannot exceed 2 characters")]
        public string CountryCode { get; set; }
    }
}
