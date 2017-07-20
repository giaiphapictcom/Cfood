using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Respository;
using V308CMS.Common;
using V308CMS.Social;
using V308CMS.Sale.Helpers;


namespace V308CMS.Sale.Controllers
{
    public abstract class BaseController : Controller
    {

        #region Repository
        static V308CMSEntities mEntities;
        public ProductRepository ProductRepos;

        public AccountRepository AccountRepos;
        public NewsRepository NewsRepos;
        public TestimonialRepository CommentRepo;
        public CategoryRepository CategoryRepo;
        public LinkRepository LinkRepo;
        public BannerRepository BannerDaraRepo;
        public BannerRespository BannerRepo;
        public TicketRepository TicketRepo;
        public CouponRepository CouponRepo;
        public MenuConfigRespository MenuRepos;
        public NewsGroupRepository NewsGroupRepos;
        public SupportManRepository SupportManRepos;
        public WebsiteRequestRepository WebsiteRequestRepo;


        public int PageSize = 10;
        public void CreateRepos()
        {
           

            mEntities = new V308CMSEntities();

            ProductRepos = new ProductRepository();
            ProductRepos.PageSize = PageSize;
            ProductHelper.ProductShowLimit = ProductRepos.PageSize;

            AccountRepos = new AccountRepository();
            NewsRepos = new NewsRepository();
            NewsGroupRepos = new NewsGroupRepository();
            CommentRepo = new TestimonialRepository(mEntities);
            CategoryRepo = new CategoryRepository(mEntities);
            LinkRepo = new LinkRepository(mEntities);
            BannerDaraRepo = new BannerRepository(mEntities);
            BannerRepo = new BannerRespository();
            TicketRepo = new TicketRepository(mEntities);
            CouponRepo = new CouponRepository(mEntities);
            MenuRepos = new MenuConfigRespository();
            SupportManRepos = new SupportManRepository();
            WebsiteRequestRepo = new WebsiteRequestRepository();

            CouponRepo.PageSize = PageSize;
        }

        public void DisposeRepos()
        {
            mEntities.Dispose();
            //ProductRepos.Dispose();

            //AccountRepos.Dispose();
            //NewsRepos.Dispose();

            if (CommentRepo != null) {
                CommentRepo.Dispose();
            }
            if (CategoryRepo != null) {
                CategoryRepo.Dispose();
            }
            if (LinkRepo != null) {
                LinkRepo.Dispose();
            }
            if (BannerDaraRepo != null) {
                BannerDaraRepo.Dispose();
            }
            if (TicketRepo != null) {
                TicketRepo.Dispose();
            }
            if (CouponRepo != null) {
                CouponRepo.Dispose();
            }
            if (MenuRepos != null) {
                //MenuRepos.Dispose();
            }
            
        }

        private readonly AccountRepository _AccountService;
        protected AccountRepository AccountService { get { return _AccountService; } }
        #endregion

        protected BaseController()
        {
            _AccountService = new AccountRepository();
            ConfigRepo = new SiteRepository();

            ConfigHelper.RecaptchaSecretKey = ConfigRepo.SiteConfig("RecaptchaSecretKey");
            ConfigHelper.RecaptchaSitekey = ConfigRepo.SiteConfig("RecaptchaSitekey");
            ConfigHelper.SiteLogo = ConfigRepo.SiteConfig("site-logo",Data.Helpers.Site.affiliate);
            GoogleplusService = new GoogleplusService(ConfigHelper.GoogleAppId, ConfigHelper.GoogleAppSecret);
            FacebookService = new FacebookService(ConfigHelper.FacebookAppId, ConfigHelper.FacebookAppSecret);
            CreateRepos();
        }
        public FacebookService FacebookService { get; }
        public SiteRepository ConfigRepo { get; }
        public GoogleplusService GoogleplusService { get; }
        protected void SetFlashMessage(string message)
        {
            TempData["Message"] = message;
        }

        protected void SetFlashError(string message)
        {
            TempData["Error"] = message;
        }

        private string GetTempSession(string name, string defaultValue = "")
        {
            return Session[name]?.ToString() ?? defaultValue;
        }
        private void ResetTempSession(string name, string defaultValue = "")
        {
            Session[name] = defaultValue;
        }
        private void SetTempSession(string name, string value = "")
        {
            Session[name] = value;
        }
        protected string ReturnUrl
        {
            get { return GetTempSession("ReturnUrl"); }
            set { if (!string.IsNullOrEmpty(value)) { SetTempSession("ReturnUrl", value); } }
        }

        protected ActionResult RedirectToUrl(string url, string defaultUrl = "/")
        {
            if (url.IsLocalUrl())
            {
                return Redirect(url);
            }
            return Redirect(defaultUrl);

        }
    }
}
