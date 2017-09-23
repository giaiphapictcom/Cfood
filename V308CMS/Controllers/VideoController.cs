using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Filters;
using V308CMS.Data.Models;

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
            var site = ViewBag.domain;
            var video = VideoService.Find(id);
            if (video == null) {
                var news = NewsService.Find(id);
                if (news != null && news.Summary.Length > 0) {
                    video = new Video() {
                        Id = news.ID,
                        Title = news.Title,
                        Code = news.Summary,
                        Link = news.Summary,
                        Order = 1,
                        Position = 1,
                        Status = 1
                    };
                    
                }
                
            }
            return View("Video", video);
        }

    }
}
