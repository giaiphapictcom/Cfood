using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    public abstract class BaseController : Controller
    {
        private static V308CMSEntities _mEntities;
        private static V308CMSEntities EnsureV308CmsEntitiesNotNull()
        {
            return _mEntities ?? (_mEntities = new V308CMSEntities());
        }

        private readonly ProductRepository _productService;
        private readonly NewsRepository _newsService;
        private readonly AccountRepository _accountService;
        private readonly FileRepository _fileService;
        private readonly IProductWishlistRepositry _productWishlistService;
        private readonly ImagesRepository _imagesRepository;
        private readonly MarketRepository _marketRepository;
        private readonly IContactRepository _contactRepository;
        private readonly SupportRepository _supportRepository;
        private readonly INewsGroupRepository _newsGroupRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISiteConfigRespository _siteConfigRepository;

        protected BaseController()
        {
            _mEntities = EnsureV308CmsEntitiesNotNull();
            _productService = new ProductRepository(_mEntities);
            _newsService = new NewsRepository(_mEntities);

            _accountService = new AccountRepository(_mEntities);
            _fileService = new FileRepository(_mEntities);
            _productWishlistService = new ProductWishlistRepositry(_mEntities);
            _imagesRepository = new ImagesRepository(_mEntities);
            _marketRepository = new MarketRepository(_mEntities);
            _contactRepository = new ContactRepository(_mEntities);
            _supportRepository = new SupportRepository(_mEntities);
            _newsGroupRepository = new NewsGroupRepository(_mEntities);
            _productTypeRepository = new ProductTypeRepository(_mEntities);
            _siteConfigRepository = new SiteConfigRespository(_mEntities);
        }
        public ISiteConfigRespository SiteConfigService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _siteConfigRepository;
            }
        }
        public IProductTypeRepository ProductTypeService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _productTypeRepository;
            }
        }
        public INewsGroupRepository NewsGroupService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _newsGroupRepository;
            }
        }
        protected SupportRepository SupportService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _supportRepository;
            }
        }
        protected IContactRepository ContactService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _contactRepository;
            }
        }
        protected V308CMSEntities MpStartEntities
        {
            get { return _mEntities; }
        }
        protected ImagesRepository ImagesService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _imagesRepository;
            }
        }
        protected MarketRepository MarketService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _marketRepository;
            }
        }
        protected NewsRepository NewsService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _newsService;
            }
        }
        protected IProductWishlistRepositry ProductWishlistService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _productWishlistService;
            }
        }

        protected ProductRepository ProductsService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _productService;
            }

        }

        protected AccountRepository AccountService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _accountService;
            }
        }
        protected FileRepository FileService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _fileService;
            }
        }

        protected object GetState(string name,object value,object defaultValue)
        {
            var controller = ControllerContext.RouteData.Values["controller"].ToString();
            var sessionName = $"{controller}{name}";
            if (value == null){
                value = Session[sessionName] != null ? Session[sessionName].ToString() : defaultValue;
            }
            else{
                SetState(sessionName, defaultValue);
            }
            return value;
        }

        private void SetState(string key, object value)
        {
            Session[key] = value;

        }

        protected void SetFlashMessage(string message)
        {
            TempData["Message"] = message;
        }
    }
}