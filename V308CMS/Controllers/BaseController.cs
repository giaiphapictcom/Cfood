﻿using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Helpers;
using V308CMS.Respository;

namespace V308CMS.Controllers
{
    public abstract class BaseController : Controller
    {
        private static V308CMSEntities _mEntities;
        private static V308CMSEntities EnsureV308CmsEntitiesNotNull()
        {
            return _mEntities ?? (_mEntities = new V308CMSEntities());
        }

        private readonly Data.ProductRepository _productService;
        private readonly NewsRepository _newsService;
        private readonly AccountRepository _accountService;
        private readonly FileRepository _fileService;
        private readonly IProductWishlistRepositry _productWishlistService;
        private readonly ImagesRepository _imagesRepository;
        private readonly MarketRepository _marketRepository;
        private readonly IContactRepository _contactRepository;
        private readonly MenuConfigRespository _meuMenuConfigRespository;
        private readonly Data.ProductTypeRepository _productTypeRepository;

        protected BaseController()
        {
            _mEntities = EnsureV308CmsEntitiesNotNull();
            _productService = new Data.ProductRepository();
            _newsService = new NewsRepository(_mEntities);

            _accountService = new AccountRepository(_mEntities);
            _fileService = new FileRepository(_mEntities);
            _productWishlistService = new ProductWishlistRepositry(_mEntities);
            _imagesRepository = new ImagesRepository(_mEntities);
            _marketRepository = new MarketRepository(_mEntities);
            _contactRepository = new ContactRepository();
            _meuMenuConfigRespository = new MenuConfigRespository();
            _productTypeRepository = new Data.ProductTypeRepository();
        }
        public Data.ProductTypeRepository ProductTypeService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _productTypeRepository;
            }
        }
        public MenuConfigRespository MenuConfigService
        {
            get
            {
                EnsureV308CmsEntitiesNotNull();
                return _meuMenuConfigRespository;
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

        protected Data.ProductRepository ProductsService
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
    }
}
