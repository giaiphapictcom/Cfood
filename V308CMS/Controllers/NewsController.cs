using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Filters;

namespace V308CMS.Controllers
{

    public class NewsController : BaseController
    {
        public NewsController()
        {
            //VisisterRepo.UpdateView();
        }
        private const int PageSize = 10;
        private const int NewsType = 58;

        private int GetTotalPage(int totalItem)
        {
            int totalPage = totalItem / PageSize;           
            if (totalItem % PageSize != 0)
            {
                totalPage += 1;
            }
            return totalPage;
        }

       
        public ActionResult Index(int type = NewsType, int page = 1)
        {
            var newsIndexViewModel = new NewsIndexPageContainer();
            var newsGroup = NewsService.LayTheLoaiTinTheoId(type);
            string level = string.Empty;     
            if (newsGroup != null)
            {
                newsIndexViewModel.NewsGroups = newsGroup;
                level = newsGroup.Level;
            }
            newsIndexViewModel.ListNews = NewsService.LayTinTheoTrangAndGroupIdAndLevel(page, 10, type, level);
            newsIndexViewModel.Page = page;
            newsIndexViewModel.TotalPage = GetTotalPage(newsIndexViewModel.ListNews.Count);
            newsIndexViewModel.ListNewsMostView = NewsService.GetListNewsMostView(type, level);
            return View("News.Index", newsIndexViewModel);            
        }
        [NewsUpdateView] 
        public ActionResult Detail(int id, string slug)

        {
            var newsItem = NewsService.GetById(id);
            if (newsItem == null)
            {
                return HttpNotFound("Tin này không tồn tại trên hệ thống");
            }        
            var newsDetailViewModel = new NewsDetailPageContainer
            {
                NewsItem = newsItem, 
                NextNewsItem = NewsService.GetNext(id),
                PreviousNewsItem = NewsService.GetPrevious(id),
                ListNewsMostView = NewsService.GetListNewsMostView(NewsType, "")               
            };          

            return View("News.Detail", newsDetailViewModel);
        }

        public ActionResult ListByTag(string tag, int page = 1)
        {
            return Content("ok");
        }
    }
}
