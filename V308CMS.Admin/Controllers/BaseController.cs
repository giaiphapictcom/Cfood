using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Data;
using V308CMS.Respository;

namespace V308CMS.Admin.Controllers
{
    [CheckGroupPermission(false)]
    public abstract class BaseController : Controller
    {
        protected virtual new CustomPrincipal User => HttpContext.User as CustomPrincipal;
        
        protected BaseController()
        {
         
        

            NewsService = new NewsRepository();
            NewsGroupService = new NewsGroupRepository();
            AccountService = new AccountRepository();             
            ContactService = new ContactRepository();          
            ProductTypeService = new Respository.ProductTypeRepository();
            SiteConfigService = new Respository.SiteConfigRespository();
            EmailConfigService= new EmailConfigRepository();
            MenuConfigService = new MenuConfigRespository();
            ProductBrandService = new ProductBrandRespository();
            ProductManufacturerService = new ProductManufacturerRespository();
            ProductDistributorService = new ProductDistributorRespository();
            ProductStoreService = new StoreRespository();
            UnitService = new UnitRespository();
            ColorService = new ColorRespository();
            CountryService = new CountryRespository();
            SizeService =  new SizeRespository();
            ProductAttributeService = new ProductAttributeRespository();
            ProductImageService = new ProductImageRespository();
            UserService = new UserRespository();
            RoleService = new RoleRespository();
            PermissionService = new PermissionRespository();
            ProductSizeService = new ProductSizeRespository();
            ProductColorService = new ProductColorRespository();
            ProductSaleOffService = new ProductSaleOffRespository();
            ProductService = new ProductRespository();
            AdminAccountService = new AdminRespository();
            BannerService = new BannerRespository();
            SupportService = new SupportRepository();
            ProductsService = new ProductRepository();
            MarketService = new MarketRepository();
            ImagesService = new ImagesRepository();
            FileService = new FileRepository();
           
            OrderService = new ProductOrderRespository();
        }
        public ProductOrderRespository OrderService { get; set; }
        public BannerRespository BannerService { get; set; }
        public AdminRespository AdminAccountService { get; set; }
        public ProductRespository ProductService { get; set; }
        public ProductSaleOffRespository ProductSaleOffService { get;set; }
        public ProductColorRespository ProductColorService { get;set; }
        public ProductSizeRespository ProductSizeService { get;set; }       
        public ProductDistributorRespository ProductDistributorService { get; set;}

        public ProductManufacturerRespository ProductManufacturerService { get;set; }

        public ProductBrandRespository ProductBrandService { get; set;}
        public ProductImageRespository ProductImageService { get; set;}

        public ProductAttributeRespository ProductAttributeService { get; set;}

        public PermissionRespository PermissionService { get; set;}
        public RoleRespository RoleService { get; set;}

        public UserRespository UserService { get; set;}

        public SizeRespository SizeService { get; set;}

        public CountryRespository CountryService { get; set;}

        public ColorRespository ColorService { get; set;}

        public UnitRespository UnitService { get; set;}

        public StoreRespository ProductStoreService { get; set;}
        public MenuConfigRespository MenuConfigService { get; set;}

        public EmailConfigRepository EmailConfigService { get; set;}

        public Respository.SiteConfigRespository SiteConfigService { get; set;}

        public Respository.ProductTypeRepository ProductTypeService { get; set;}

        public NewsGroupRepository NewsGroupService { get; set;}

        protected ContactRepository ContactService { get; set;}
        public SupportRepository SupportService { get; set; }
        public ProductRepository ProductsService { get;set;}
        public MarketRepository MarketService { get; set; }
        public ImagesRepository ImagesService { get; set; }
        public FileRepository FileService { get; set; }



        public NewsRepository NewsService { get; set; }     

        protected AccountRepository AccountService { get; set; }

        protected object GetState(string name,object value,object defaultValue)
        {
            var controller = ControllerContext.RouteData.Values["controller"].ToString();
            var sessionName = string.Format("{0}{1}",controller,name);
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

        protected void AddViewData(params dynamic[] data )
        {
            if (data != null && data.Length > 0)
            {
                for (int i = 0; i < data.Length; i+=2)
                {
                    ViewData[data[i]] = data[i+1];
                }
            }
            
        }
    }
}