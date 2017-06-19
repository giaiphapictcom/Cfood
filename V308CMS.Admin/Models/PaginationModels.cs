namespace V308CMS.Admin.Models
{
    public class PaginationModels
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RecordPerPages { get; set; }
        public string BaseUrl { get; set; }
        public string PageParamFormat { get; set; }
        public int Links { get; set; }
        

    }
}