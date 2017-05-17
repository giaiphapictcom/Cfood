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
            newsIndexViewModel.Page = page;
            newsIndexViewModel.ListNews = NewsService.LayTinTheoTrangAndGroupIdAndLevel(page, 10, type, level);
            newsIndexViewModel.ListNewsLastest = NewsService.GetListNewsLastest(type, level);
            return View(FindView("News.Index"), newsIndexViewModel);            
        }

        public ActionResult Detail(int id)
        {
            var newsItem = NewsService.LayTinTheoId(id);
            if (newsItem == null)
            {
                return HttpNotFound("Tin này không tồn tại trên hệ thống");
            }

            return View(FindView("Detail"), newsItem);
        }

    }
}
