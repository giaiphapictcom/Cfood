using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public abstract class BaseController : Controller
    {
        private static readonly V308CMSEntities MEntities = new V308CMSEntities() ;

        protected BaseController()
        {           
            NewsService = new NewsRepository(MEntities);
        }

        protected V308CMSEntities MpStartEntities => MEntities;
        protected NewsRepository NewsService { get; }

        protected string FindView(string name)
        {
            return Theme.viewPage(name);
        }
    }
}
