using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class LinkController : BaseController
    {
        public ActionResult Click(string code="") {
            try {
                using (var entiry = new V308CMSEntities()) {
                    var link = entiry.AffiliateLink.Where(l => l.code == code).FirstOrDefault();
                    if (link == null)
                    {
                        return RedirectToAction("Index","Home");
                    } else {
                        link.click++;
                        entiry.SaveChanges();
                        if (link.created_by == null) {
                            link.created_by = 0;
                        }
                        VisisterRepo.UpdateView(0,int.Parse(link.created_by.ToString()));
                        LinkRepo.UpdateClick(link.ID);
                        Response.Redirect(link.url);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
