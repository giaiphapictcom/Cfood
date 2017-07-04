using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Helpers;
using V308CMS.Helpers.Url;
using V308CMS.Models;
using V308CMS.Respository;

namespace V308CMS.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Gio hang
        /// </summary>
        protected ShoppingCart MyCart => ShoppingCart.Instance;

        /// <summary>
        /// Xoa gio hang, duoc su dung khi qua trinh mua ban hoan tat
        /// </summary>
        protected void ClearCart()
        {
            MyCart.Clear();
        }


        private readonly ProductRepository _productService;
        private readonly NewsRepository _newsService;
        private readonly AccountRepository _AccountService;
        private readonly FileRepository _fileService;
        private readonly IProductWishlistRepositry _productWishlistService;
        private readonly ImagesRepository _imagesRepository;
        private readonly MarketRepository _marketRepository;
        private readonly IContactRepository _contactRepository;
        private readonly MenuConfigRespository _meuMenuConfigRespository;
        private readonly Data.ProductTypeRepository _productTypeRepository;
        private readonly CartRepository _CartRepository;
        private readonly CartItemRepository _CartItemRepository;

        private readonly BannerRespository _bannerService;
        public MenuConfigRespository _MenuConfigRepos;

        //protected BaseController()
        //{
        //    _mEntities = EnsureV308CmsEntitiesNotNull();
        //    //V308CMSEntities mEntities = new V308CMSEntities();

        //    _productService = new ProductRepository(_mEntities);
        //    //_productService = new Data.ProductRepository();

        //    _newsService = new NewsRepository(_mEntities);

            //_accountService = new AccountRepository(_mEntities);
        //    _fileService = new FileRepository(_mEntities);
        //    //_productWishlistService = new ProductWishlistRepositry(_mEntities);
        //    _imagesRepository = new ImagesRepository(_mEntities);
        //    _marketRepository = new MarketRepository(_mEntities);

        //    _contactRepository = new ContactRepository(_mEntities);

        //    _CartRepository = new CartRepository(_mEntities);
        //    _CartItemRepository = new CartItemRepository(_mEntities);

        //    _meuMenuConfigRespository = new MenuConfigRespository(_mEntities);
        //    _productTypeRepository = new Data.ProductTypeRepository(_mEntities);

        //    _bannerService = new BannerRespository();

        //    _MenuConfigRepos = new MenuConfigRespository(_mEntities);
        //}
        /// <summary>
        /// Kiem tra gio hang trong
        /// </summary>
        /// <returns></returns>
        protected bool IsEmptyCart()
        {
            return MyCart.Items.Count ==0;

        }

        /// <summary>
        /// Xoa san pham trong gio hang
        /// </summary>
        /// <param name="itemId"></param>
        protected void RemoveItemInCart(int itemId)
        {
            MyCart.RemoveItem(new ProductModels { Id = itemId });
        }
        protected string TransactionId => GetTempSession("TransactionId");
        /// <summary>
        /// Callback Url
        /// </summary>

        protected string ReturnUrl
        {
            get { return GetTempSession("ReturnUrl");}
            set { if (!string.IsNullOrEmpty(value)){ SetTempSession("ReturnUrl", value);}}
        }
        protected ActionResult RedirectToUrl(string url, string defaultUrl = "/")
        {
            if (url.IsLocalUrl())
            {
                return Redirect(url);
            }
            return Redirect(defaultUrl);

        }
        /// <summary>
        /// Xac nhan bat dau mot giao dich
        /// </summary>
        /// <param name="transactionId"></param>
        protected void BeginTransaction(string transactionId)
        {
            SetTempSession("TransactionId", transactionId);
        }

        /// <summary>
        /// Ghi nhan da hoan tat giao dich
        /// </summary>
        protected void EndTransaction()
        {
            ResetTempSession("TransactionId");
        }
        /// <summary>
        /// Thong tin User dang nhap
        /// </summary>
        protected virtual new CustomPrincipal User => HttpContext.User as CustomPrincipal;
        /// <summary>
        /// Load cac config can su dung
        /// </summary>
        private void LoadSiteConfig()
        {
            var siteConfigs = SiteConfig.ConfigTable;
            if (siteConfigs.Any())
            {
                ViewBag.SiteName = SiteConfigService.ReadSiteConfig(siteConfigs, "site-name");
                ViewBag.Hotline = SiteConfigService.ReadSiteConfig(siteConfigs, "hotline");
                ViewBag.CompanyFullname = SiteConfigService.ReadSiteConfig(siteConfigs, "company-fullname");
                ViewBag.CompanyEmail = SiteConfigService.ReadSiteConfig(siteConfigs, "company-email");
                ViewBag.CompanyPosition = SiteConfigService.ReadSiteConfig(siteConfigs, "company-position");
                ViewBag.FooterCompanyContact = SiteConfigService.ReadSiteConfig(siteConfigs, "company-footer-contact");
                ViewBag.CompanyHeaderAddress = SiteConfigService.ReadSiteConfig(siteConfigs, "company-header-address");
                ViewBag.FacebookPage = SiteConfigService.ReadSiteConfig(siteConfigs, "facebook-page");
                ViewBag.GPlus = SiteConfigService.ReadSiteConfig(siteConfigs, "gplus");
                ViewBag.Zalo = SiteConfigService.ReadSiteConfig(siteConfigs, "zalo");
                ViewBag.Youtube = SiteConfigService.ReadSiteConfig(siteConfigs, "youtube-channel");
                ViewBag.ProductViewText = SiteConfigService.ReadSiteConfig(siteConfigs, "product-text-view");
                ViewBag.HomeAliasText = SiteConfigService.ReadSiteConfig(siteConfigs, "home-text-alias");
                ViewBag.SubscribeNews = SiteConfigService.ReadSiteConfig(siteConfigs, "subscribe-news");
            }

        }
        /// <summary>
        /// Khoi tao cac service can su dung
        /// </summary>
        protected BaseController()
        {
            ProductsService = new ProductRepository();
            NewsService = new NewsRepository();
            _AccountService = new AccountRepository();
            FileService = new FileRepository();
            ProductWishlistService = new ProductWishlistRepositry();
            ImagesService = new ImagesRepository();
            MarketService = new MarketRepository();
            ContactService = new ContactRepository();
            CartService = new CartRepository();
            CartItemService = new CartItemRepository();
            MenuConfigService = new MenuConfigRespository();
            ProductTypeService = new Data.ProductTypeRepository();
            SiteConfigService = new Data.SiteConfigRespository();
            BoxContentService = new BoxContentRespository();
            RegionService = new RegionRespository();
            ShippingService = new ShippingAddressRespository();
            OrderTransactionService = new OrderTransactionRespository();
            _MenuConfigRepos = new MenuConfigRespository();
            _bannerService = new BannerRespository();
            LoadSiteConfig();

        }

        public OrderTransactionRespository OrderTransactionService;
        public ShippingAddressRespository ShippingService { get; }
        public RegionRespository RegionService { get; }
        public BoxContentRespository BoxContentService { get; }
        public Data.SiteConfigRespository SiteConfigService { get; }
        public Data.ProductTypeRepository ProductTypeService { get; }

        public MenuConfigRespository MenuConfigService { get; }

        protected IContactRepository ContactService { get; }

        protected ImagesRepository ImagesService { get; }

        protected MarketRepository MarketService { get; }

        protected NewsRepository NewsService { get; }

        protected IProductWishlistRepositry ProductWishlistService { get; }

        protected Data.ProductRepository ProductsService { get; }

        protected AccountRepository AccountService { get { return _AccountService; }  }

        protected FileRepository FileService { get; }


        public CartRepository CartService { get; }

        public CartItemRepository CartItemService { get; }

        private void SetTempSession(string name, string value ="")
        {
            Session[name] = value;
        }

        private string GetTempSession(string name, string defaultValue = "")
        {
            return Session[name]?.ToString() ?? defaultValue;
        }

        private void ResetTempSession(string name,string defaultValue="")
        {
            Session[name] = defaultValue;
        }


        public BannerRespository BannerService
        {
            get
            {
                //EnsureV308CmsEntitiesNotNull();
                return _bannerService;
            }
        }

        public MenuConfigRespository MenuConfigRepos
        {
            get
            {
                //EnsureV308CmsEntitiesNotNull();
                return _MenuConfigRepos;
            }

        }

    }
}
