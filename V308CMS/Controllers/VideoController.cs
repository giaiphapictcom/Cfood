using System.Web.Mvc;

namespace V308CMS.Controllers
{
    public class VideoController : BaseController
    {
     
        public VideoController()
        {
          
        }
        

        public ActionResult Index(int page = 1, int pageSize =10)
        {        
            return View("Video.Index", VideoService.GetListVideo(page,pageSize));
        }

        public ActionResult Detail(int id)
        {
            return View("Video", VideoService.Find(id));
        }

    }
}
