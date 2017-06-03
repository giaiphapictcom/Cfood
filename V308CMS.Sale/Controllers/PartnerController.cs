using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using System.Web.Security;

namespace V308CMS.Sale.Controllers
{
    public class PartnerController : Controller
    {
        #region Repository
        static V308CMSEntities mEntities;
        ProductRepository ProductRepos;
        AccountRepository AccountRepos;
        NewsRepository NewsRepos;
        TestimonialRepository CommentRepo;
        CategoryRepository CategoryRepo;
        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
            ProductRepos.PageSize = 50;
            ProductHelper.ProductShowLimit = ProductRepos.PageSize;
            AccountRepos = new AccountRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);
            CommentRepo = new TestimonialRepository(mEntities);
            CategoryRepo = new CategoryRepository(mEntities);
        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();
            AccountRepos.Dispose();
            NewsRepos.Dispose();
            CommentRepo.Dispose();
            CategoryRepo.Dispose();
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        public ActionResult LoginPost()
        {
            try {
                ETLogin mETLogin;
                CreateRepos();
                string email = Request["email"];
                string password = Request["password"];
                mETLogin = AccountRepos.CheckDangNhap(email, password);
                if (mETLogin.code == 1 && (mETLogin.role == 1 || mETLogin.role == 3))
                {
                    mETLogin.message = "Đăng nhập thành công.";
                    Session["UserId"] = mETLogin.Admin.ID;
                    Session["UserName"] = mETLogin.Admin.UserName;
                    Session["Role"] = mETLogin.Admin.Role;
                    Session["Admin"] = mETLogin.Admin;
                    FormsAuthentication.SetAuthCookie(email, true);

                    Response.BufferOutput = true;
                    Response.Redirect("/dashboard");
                }
                
                return View(mETLogin);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult Register()
        {
            return View();
        }



        #region LinkForm Action
        
        [HttpGet]
        public ActionResult Links()
        {
            return View();
        }

        [HttpPost, ActionName("Links")]
        public ActionResult LinksPost()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LinkForm()
        {
            return View();
        }

        [HttpPost, ActionName("LinkForm")]
        public ActionResult LinkFormPost()
        {
            return View();
        }
        #endregion

        #region Products

        [HttpGet]
        public ActionResult Products()
        {
            try {
                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }

                CreateRepos();
                AffiliateProductPage Model = new AffiliateProductPage();
                Model.Products = ProductRepos.GetItems(nPage);
                Model.ProductTotal = ProductRepos.GetItemsTotal();

                Model.Page = nPage;
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        [HttpPost, ActionName("Products")]
        public ActionResult ProductsPost()
        {
            return View();
        }
        #endregion


    }
}
