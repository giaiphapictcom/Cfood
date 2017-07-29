using System.Web.Mvc;
using V308CMS.Data;

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

        //public ActionResult Index(int page = 1, int pageSize =10)
        //{        
        //    return View("Video.Index", VideoService.GetListVideo(page,pageSize));

        }

        public ActionResult Detail(int id)
        {
            return View("Video", VideoService.Find(id));
        }

    }
}
