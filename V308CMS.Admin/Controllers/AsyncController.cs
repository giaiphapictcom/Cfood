using System.Web.Mvc;
using V308CMS.Admin.Attributes;

namespace V308CMS.Admin.Controllers
{
    [CheckGroupPermission(false)]
    public class AsyncController : BaseController
    {
        //
        // GET: /Async/

        public PartialViewResult CountOrderAsync()
        {
            return PartialView("_CountOrder");
        }

        public PartialViewResult CountUserAsync()
        {
            return PartialView("_CountUser");
        }
        public PartialViewResult CountContactAsync()
        {
             
            return PartialView("_CountContact");
        }

        public PartialViewResult CountProductAsync()
        {
            return PartialView("_CountProduct");
        }

        public PartialViewResult LoadLastestOrderAsync()
        {
            return PartialView("_LastestOrder");
        }

        public PartialViewResult LoadLastestUserAsync()
        {
            return PartialView("_LastestUser");
        }
        public PartialViewResult LoadLastestContactAsync()
        {
            return PartialView("_LastestContact");
        }


    }
}
