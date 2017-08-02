using System.Web.Mvc;
using V308CMS.Filters;

namespace V308CMS.Controllers
{
    public class VideoController : BaseController
    {    
        public ActionResult Index(int page = 1, int pageSize =10)
        {        
            return View("Video.Index", VideoService.GetListVideo(page,pageSize));
        }
        [VideoUpdateView]
        public ActionResult Detail(int id)
        {
            return View("Video", VideoService.Find(id));
        }

    }
}
