using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Helpers;

namespace V308CMS.Controllers
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
      
        protected BaseController()
        {
            _mEntities = EnsureV308CmsEntitiesNotNull();
            _productService = new ProductRepository(_mEntities);
            _newsService = new NewsRepository(_mEntities);

            _accountService = new AccountRepository(_mEntities);
            _fileService = new FileRepository(_mEntities);
            _productWishlistService = new ProductWishlistRepositry(_mEntities);
        }

        protected V308CMSEntities MpStartEntities
        {
            get { return _mEntities; }
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
        protected FileRepository FileService {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _fileService;
            }
        }
      

        protected string FindView(string name)
        {
            return Theme.viewPage(name);
        }
    }
}
