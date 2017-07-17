using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using V308CMS.Common;
using V308CMS.Data;
using System.Web.Security;
using V308CMS.Sale.Models;
using V308CMS.Sale.Helpers;
using V308CMS.Data.Helpers;
using V308CMS.Data.Models;

namespace V308CMS.Sale.Controllers
{
    public class PartnerController : BaseController
    {
        int nPage = 1;
        public PartnerController() {
            if (Request != null) {
                var UriPage = Request.QueryString["p"];
                if (UriPage == null || UriPage == "")
                {
                    UriPage = "1";
                }
                int SetPage = Convert.ToInt32(UriPage);
                if (SetPage > 0)
                {
                    nPage = SetPage;
                }
            }
            
            //CreateRepos();
        }

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
                mETLogin = AccountRepos.CheckDangNhap(email, password, Site.affiliate);
                //var result = AccountService.CheckAccount(email, password);
                if (mETLogin.code == 1 && (mETLogin.role == 1 || mETLogin.role == 3))
                {
                    mETLogin.message = "";
                    SetFlashMessage("Đăng nhập thành công.");
                    Session["UserId"] = mETLogin.Account.ID;
                    Session["UserName"] = mETLogin.Account.UserName;
                    Session["Role"] = mETLogin.Account.Role;
                    Session["Account"] = mETLogin.Account;
                    FormsAuthentication.SetAuthCookie(email, true);

                    return Redirect("/dashboard");
                }
                SetFlashError(mETLogin.message);
                return View(mETLogin);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại."+ ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session.Abandon();
            var authenCookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(authenCookie);
            var sessionCookie = new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };

            System.Web.HttpContext.Current.Response.Cookies.Add(sessionCookie);
            return Redirect("/");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost, ActionName("Register")]
        public ActionResult RegisterPost()
        {
            try {
                CreateRepos();
                string email = Request["email"];
                string fullname = Request["fullname"];
                string mobile = Request["mobile"];
                string password = Request["password"];
                string repass = Request["repassword"];
                if (fullname.Length < 1)
                {
                    SetFlashError("Bạn phải nhập vào họ  và tên.");
                    return View("Register");
                }
                if (email.Length < 1)
                {
                    SetFlashError("Bạn phải nhập vào email.");
                    return View("Register");
                }
                if (password != repass) {
                    SetFlashError("Mật khẩu không giống nhau.");
                    return View("Register");
                }
                var InsertReturn = AccountRepos.InsertAffiliate(email, password, fullname, mobile);
                if (InsertReturn == Result.Exists) {
                    SetFlashError("Người dùng đã tồn tại trên hệ thống, vui lòng nhập lại");
                    return View("Register");
                }

                if (InsertReturn == Result.Ok)
                {
                    SetFlashMessage(string.Format("Cảm ơn bạn đã đăng ký tài khoản trên hệ thống của {0}. Vui lòng kiểm tra email để kích hoạt tài khoản.", ViewBag.SiteName));
                    return View("Login");
                }

                return Redirect("/dang-nhap");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại."+ex.ToString());
            }

        }

        [AffiliateAuthorize]
        public ActionResult AccountInfomation(AccountModel account)
        {
            if (Session["UserId"] != null) {
                int uid = int.Parse(Session["UserId"].ToString());

                CreateRepos();
                var userLogined = AccountRepos.Find(uid);
                account = userLogined.CloneTo<AccountModel>();
            }
           
            return View(account);
        }

        [HttpPost, ActionName("AccountInfomation")]
        public ActionResult AccountInfomationPost(AccountModel account)
        {
            account.cmt_front = account.FileFront != null ?
                  account.FileFront.Upload() :
                  account.cmt_front;
            if (account.cmt_front != null) {
                account.cmt_front = account.cmt_front.Replace("\\Content\\Images\\", "");
            }
            
            account.cmt_back = account.FileBack != null ?
                  account.FileBack.Upload() :
                  account.cmt_back;
            if (account.cmt_back != null ) {
                account.cmt_back = account.cmt_back.Replace("\\Content\\Images\\", "");
            }
            

            var accountNew = account.CloneTo<Account>(new[] { "FileFront","FileBack" });

            CreateRepos();
            var result = AccountRepos.UpdateObject(accountNew);
             if (result == Result.Ok)
            {
                SetFlashMessage(string.Format("Cập nhật dữ liệu thành công"));
                return View("AccountInfomation", AccountRepos.Find(account.id).CloneTo<AccountModel>());
            }

            SetFlashError(string.Format("Có lỗi xảy ra"));
            return View("AccountInfomation", account);
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
             
                AffiliateProductPage Model = new AffiliateProductPage();

                
                

                Model.Page = nPage;
                Model.ck_order = Request.QueryString["ck_order"];
                Model.saletop_order = Request.QueryString["saletop_order"];

                var cate_get = Request.QueryString["category"];
                if (cate_get != null && cate_get.Length > 0) {
                    Model.category = int.Parse(cate_get);
                }
                var PageSize_get = Request.QueryString["plimit"];
                if (PageSize_get != null && PageSize_get.Length > 0)
                {
                    Model.plimit = int.Parse(PageSize_get);
                    
                }
                ProductRepos.PageSize = Model.plimit;
                ProductHelper.ProductShowLimit = ProductRepos.PageSize;

                Model.search = Request.QueryString["search"];

                using (var entities = new V308CMSEntities())
                {
                    var products = entities.Product.Select(p => p);
                    products = products.OrderByDescending(p => p.ID);

                    if (Model.category > 0) {
                        products = products.Where(p=>p.Type== Model.category);
                    }

                    if (Model.ck_order == "desc")
                    {
                        products = products.OrderByDescending(p => p.SaleOff);
                    } else if (Model.ck_order == "asc")
                    {
                        products = products.OrderBy(p => p.SaleOff);
                    }

                    if (Model.saletop_order == "desc")
                    {
                       
                    }
                    else if (Model.ck_order == "asc")
                    {
                       
                    }

                    if (Model.search !=null && Model.search.Length > 0)
                    {
                        products = products.Where(p => p.Name.ToLower().Contains(Model.search.ToLower()));
                    }

                    Model.ProductTotal = products.Count();
                    Model.Products = products.Skip((Model.Page - 1) * PageSize).Take(Model.plimit).ToList();
                }


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
            try
            {

                AffiliateBannerPage Model = BannerDaraRepo.GetItemsPage(nPage);
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
        public ActionResult BannerForm(BannerModel Model)
        {
            return View(Model);
        }

        [HttpPost, ActionName("BannerForm")]
        [AffiliateAuthorize]
        public ActionResult BannerFormPost(BannerModel Data)
        {
            try
            {
                CreateRepos();
                var newItem = Data.CloneTo<AffiliateBanner>(new[] { "File" });

                newItem.image = Data.File != null ? Data.File.Upload() : Data.Image;
                BannerDaraRepo.InsertObject(newItem);
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
            var Model = new CouponModel();
            if (Request != null) {
                Model.ProductCode = Request.QueryString["pcode"];
            }
                
            return View(Model);
        }

        [HttpPost]
        [ActionName("CouponForm")]
        [AffiliateAuthorize]
        public ActionResult CouponFormPost(CouponModel coupon)
        {
            coupon.Image = coupon.File != null ?
                   coupon.File.Upload() :
                   coupon.Image;

            var newCoupon = coupon.CloneTo<Counpon>(new[] { "File" });

            if (coupon.Image == null || coupon.Image.Length < 1)
            {
                ModelState.AddModelError("", "Cần phải chọn hình ảnh.");
                return View("CouponForm", coupon);
            }
            CreateRepos();
            var result = CouponRepo.InsertObject(newCoupon);
            if (result == Result.Exists) {
                SetFlashMessage(string.Format("Mã Voucher đã tồn tại, hãy nhập mã mới"));
                return View("CouponForm", coupon);
            } else if (result == Result.Ok) {
                SetFlashMessage(string.Format("Thêm voucher '{0}' thành công.", newCoupon.title));
                return View("CouponForm", coupon.ResetValue());
            }

            SetFlashMessage(string.Format("Có lỗi xảy ra"));
            return View("CouponForm", coupon.ResetValue());

        }

        //[HttpPost, ActionName("CouponForm")]
        //[AffiliateAuthorize]
        //public ActionResult CouponFormPost()
        //{
        //    try
        //    {
        //        CreateRepos();
        //        BannerRepo.Insert(Request["image"], Request["title"], Request["summary"], Request["url"]);
        //        return Redirect("/banner");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //        return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
        //    }
        //    finally
        //    {
        //        DisposeRepos();
        //    }
        //}
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

        #region Website Request
        [HttpGet]
        [AffiliateAuthorize]
        public ActionResult WebsiteRequest()
        {
            return View(new WebsiteRequestModel());
        }
        
        [HttpPost, ActionName("WebsiteRequest")]
        [AffiliateAuthorize]
        public ActionResult WebsiteRequestPost(WebsiteRequestModel data)
        {
            try
            {
                var newItem = data.CloneTo<WebsiteRequest>();
                newItem.created_by = int.Parse(Session["UserId"].ToString());
                WebsiteRequestRepo.Insert(newItem);
                SetFlashMessage("Gửi yêu cầu Website thành công.");
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
    }
}
