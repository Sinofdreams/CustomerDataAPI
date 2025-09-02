
namespace CustomerDataAPI.DTO
{
    public class CustomerResponse
    {
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<Customer>? Customers { get; set; }
    }
}
