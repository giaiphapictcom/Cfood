
namespace V308CMS.Data.Models.UI
{
   public class Pagination
    {
        public Pagination()
        {
            Page = 1;
            Total = 0;
            PageItemLimit = 4;
        }
        public int Page { get; set; }
        public int Total { get; set; }
        public int PageItemLimit { get; set; }
    }
}