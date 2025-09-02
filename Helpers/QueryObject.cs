namespace CustomerDataAPI.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; }

        public string? CountryCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
