using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Filters;


namespace V308CMS.Controllers
{
    public class VideoController : BaseController

    {
     
        public VideoController()
        {
          
        }
        


        public ActionResult Index(int page = 1)
        {
            var newsIndexViewModel = new NewsIndexPageContainer();
            //var newsGroup = NewsService.LayTheLoaiTinTheoId(NewsType);
            NewsGroups newsGroup = NewsService.SearchNewsGroup("video");
            string level = string.Empty;
            if (newsGroup != null)
            {
                newsIndexViewModel.NewsGroups = newsGroup;
                level = newsGroup.Level;
            }
            newsIndexViewModel.ListNews = NewsService.GetVideos(page, PageSize, newsGroup.ID);
            newsIndexViewModel.Page = page;

            //newsIndexViewModel.ListNewsMostView = NewsService.GetListNewsMostView(NewsType, level);
            return View("Video.Index", newsIndexViewModel);


        }

        //[VideoUpdateView]
        public ActionResult Detail(int id)
        {
            return View("Video", VideoService.Find(id));
        }

    }
}
