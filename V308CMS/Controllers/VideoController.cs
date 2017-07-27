using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Filters;
using V308CMS.Helpers;

namespace V308CMS.Controllers
{
    public class VideoController : BaseController
    {
        private const int PageSize = 10;
        private int NewsType = 0;

        public VideoController()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MpStartViewEngine(false));
            NewsGroups videoGroup = NewsService.SearchNewsGroup("video");
            VisisterRepo.UpdateView();
        

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
