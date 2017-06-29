using System;
using V308CMS.Data;
using System.Web.Mvc;

namespace V308CMS.Sale.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        
        public ActionResult Index(string alias = "")
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroupByAlias(alias);
                if (Model.NewsGroups != null)
                {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name;
                }
                
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

            //return View();
        }

        
        public ActionResult Items(string alias = "") {
            return View();
        }

    }
}
