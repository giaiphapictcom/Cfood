using System.Web.Mvc;
using V308CMS.Data;
<<<<<<< HEAD
using V308CMS.Filters;
using V308CMS.Helpers;
=======
//using V308CMS.Filters;
>>>>>>> 80cc5f7e5b4271fc74adbe046b3ae514ea8a1a30

namespace V308CMS.Controllers
{
    public class VideoController : BaseController
    {
        private const int PageSize = 10;
        private int NewsType = 0;

        public VideoController()
        {
            ViewEngines.Engines.Clear();
<<<<<<< HEAD
            ViewEngines.Engines.Add(new MpStartViewEngine(false));
            NewsGroups videoGroup = NewsService.SearchNewsGroup("video");
=======
            ViewEngines.Engines.Add(new V308CMS.Helpers.MpStartViewEngine(false));
            NewsGroups videoGroup = NewsService.SearchNewsGroup("video",Data.Helpers.Site.home);
>>>>>>> 80cc5f7e5b4271fc74adbe046b3ae514ea8a1a30
            NewsType = videoGroup.ID;
        }
        

        public ActionResult Index(int page = 1)
        {
            var newsIndexViewModel = new NewsIndexPageContainer();
            var newsGroup = NewsService.LayTheLoaiTinTheoId(NewsType);
            string level = string.Empty;
            if (newsGroup != null)
            {
                newsIndexViewModel.NewsGroups = newsGroup;
                level = newsGroup.Level;
            }
            newsIndexViewModel.ListNews = NewsService.GetVideos(page, PageSize, NewsType);
            newsIndexViewModel.Page = page;

            //newsIndexViewModel.ListNewsMostView = NewsService.GetListNewsMostView(NewsType, level);
            return View("Video.Index", newsIndexViewModel);
        }

        //public ActionResult Detail(int id )
        //{
        //    return Content("ok");
        //}
<<<<<<< HEAD
        [NewsUpdateView]
=======
       // [UpdateView]
>>>>>>> 80cc5f7e5b4271fc74adbe046b3ae514ea8a1a30
        public ActionResult Detail(int id = 0)
        {


            NewsPage mCommonModel = new NewsPage();
            //StringBuilder mStr = new StringBuilder();
            //lay chi tiet san pham
            var mNews = NewsService.LayTinTheoId(id);
            if (mNews != null)
            {

                mCommonModel.pNews = mNews;
                var mListLienQuan = NewsService.LayTinTucLienQuan(mNews.ID, 26, 5);
                 mCommonModel.List = mListLienQuan;
            }
            else
            {
                mCommonModel.Html = "Không tìm thấy video";
            }

            return View("Video", mCommonModel);

        }

    }
}
