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
        LinkRepository LinkRepo;
        BannerRepository BannerRepo;
        TicketRepository TicketRepo;
        CouponRepository CouponRepo;
        int PageSize = 10;
        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
            ProductRepos.PageSize = PageSize;
            ProductHelper.ProductShowLimit = ProductRepos.PageSize;
            AccountRepos = new AccountRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);
            CommentRepo = new TestimonialRepository(mEntities);
            CategoryRepo = new CategoryRepository(mEntities);
            LinkRepo = new LinkRepository(mEntities);
            BannerRepo = new BannerRepository(mEntities);
            TicketRepo = new TicketRepository(mEntities);
            CouponRepo = new CouponRepository(mEntities);
            CouponRepo.PageSize = PageSize;
        }

        private void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();
           
            AccountRepos.Dispose();
            NewsRepos.Dispose();
            CommentRepo.Dispose();
            CategoryRepo.Dispose();
            LinkRepo.Dispose();
            BannerRepo.Dispose();
            TicketRepo.Dispose();
            CouponRepo.Dispose();
        }
        #endregion

        [AffiliateAuthorize]
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
                    Session["UserId"] = mETLogin.Account.ID;
                    Session["UserName"] = mETLogin.Account.UserName;
                    Session["Role"] = mETLogin.Account.Role;
                    Session["Account"] = mETLogin.Account;
                    FormsAuthentication.SetAuthCookie(email, true);

                    return Redirect("/dashboard");
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


        #region Support Send
        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult SupportRequest()
        {
            return View();
        }
        [HttpPost, ActionName("SupportRequest")]
        [AffiliateAuthorize]
        public ActionResult SupportRequestPost()
        {
            try
            {
                CreateRepos();
                TicketRepo.Insert(Request["title"], Request["content"], int.Parse(Session["UserId"].ToString()));
                return Redirect("/dashboard");
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
        #endregion

        #region LinkForm Action

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult Links()
        {
            try
            {
                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }

                CreateRepos();
                AffiliateLinksPage Model = new AffiliateLinksPage();
                Model.Links = LinkRepo.GetItems(nPage);
                Model.LinkTotal = LinkRepo.GetItemsTotal();

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
        
        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult LinkReport()
        {
            try
            {
                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }

                CreateRepos();
                AffiliateLinksPage Model = new AffiliateLinksPage();
                Model.Links = LinkRepo.GetItems(nPage);
                Model.LinkTotal = LinkRepo.GetItemsTotal();

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
        

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult LinkForm()
        {
            AffiliateLinkFormPage Model = new AffiliateLinkFormPage();
            Model.url = Request["l"];
            return View(Model);
        }

        [HttpPost, ActionName("LinkForm")]
        [AffiliateAuthorize]
        public ActionResult LinkFormPost()
        {
            try
            {
                CreateRepos();
                string url = Request["url"];
                LinkRepo.Insert(Request["url"], int.Parse(Session["UserId"].ToString()), Request["source"], Request["taget"], Request["name"], Request["summary"]);
                return Redirect("/link");
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
        #endregion

        #region Products
        [HttpGet]
        [AffiliateAuthorize]
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
        #endregion

        #region Banner Action

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult Banners()
        {
            return View();
        }

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult BannerForm()
        {
            return View();
        }

        [HttpPost, ActionName("BannerForm")]
        [AffiliateAuthorize]
        public ActionResult BannerFormPost()
        {
            try
            {
                CreateRepos();
                BannerRepo.Insert(Request["image"], Request["title"], Request["summary"], Request["url"]);
                return Redirect("/banner");
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
        #endregion

        #region Coupon
        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult Coupons()
        {
            try
            {

                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }
                CreateRepos();
                CouponsPage Model = CouponRepo.GetItemsPage(nPage);
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

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult CouponForm()
        {
            return View();
        }

        [HttpPost, ActionName("CouponForm")]
        [AffiliateAuthorize]
        public ActionResult CouponFormPost()
        {
            try
            {
                CreateRepos();
                BannerRepo.Insert(Request["image"], Request["title"], Request["summary"], Request["url"]);
                return Redirect("/banner");
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
        #endregion
    
    #region Orders
        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult Orders()
        {
            try
            {
                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }
                CreateRepos();

                OrdersPage Model = ProductRepos.GetOrdersAffiliatePage(nPage, int.Parse(Session["UserId"].ToString()));
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

        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult OrderReport()
        {
            try
            {
                int nPage = Convert.ToInt32(Request.QueryString["p"]);
                if (nPage < 1)
                {
                    nPage = 1;
                }
                CreateRepos();

                DateTime today = DateTime.Today;
                //int currentDayOfWeek = (int)today.DayOfWeek;
                //DateTime sunday = today.AddDays(-currentDayOfWeek);
                //DateTime monday = sunday.AddDays(1);
                //// If we started on Sunday, we should actually have gone *back*
                //// 6 days instead of forward 1...
                //if (currentDayOfWeek == 0)
                //{
                //    monday = monday.AddDays(-7);
                //}


                OrdersReportByDaysPage Model = ProductRepos.GetOrderReport7DayPage(nPage, int.Parse(Session["UserId"].ToString()));
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
    #endregion
    }
}
