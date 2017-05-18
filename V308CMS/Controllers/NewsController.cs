using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class NewsController : BaseController
    {
        private const int PageSize = 10;

        private int GetTotalPage(int totalItem)
        {
            int totalPage = totalItem / PageSize;           
            if (totalItem % PageSize != 0)
            {
                totalPage += 1;
            }
            return totalPage;
        }

       
        public ActionResult Index(int type = 58, int page = 1)
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
            return View(FindView("News.Index"), newsIndexViewModel);            
        }
    
        public ActionResult Detail(int id)
        {
            var newsItem = NewsService.LayTinTheoId(id);
            if (newsItem == null)
            {
                return HttpNotFound("Tin này không tồn tại trên hệ thống");
            }
            var newsDetailViewModel = new NewsDetailPageContainer();
            newsDetailViewModel.NewsItem = newsItem;
            newsDetailViewModel.NextNewsItem = NewsService.GetNext(id);
            newsDetailViewModel.PreviousNewsItem = NewsService.GetPrevious(id);

            return View(FindView("News.Detail"), newsDetailViewModel);
        }

    }
}
