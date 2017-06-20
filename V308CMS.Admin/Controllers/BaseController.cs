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
            SiteConfigService = new SiteConfigRespository();
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
            OrderService = new ProductOrderRespository();
        }
        public ProductOrderRespository OrderService { get; set; }
        public BannerRespository BannerService { get; set; }
        public AdminRespository AdminAccountService { get; set; }
        public ProductRespository ProductService { get; set; }
        public ProductSaleOffRespository ProductSaleOffService { get; }
        public ProductColorRespository ProductColorService { get; }
        public ProductSizeRespository ProductSizeService { get; }       
        public ProductDistributorRespository ProductDistributorService { get; }

        public ProductManufacturerRespository ProductManufacturerService { get; }

        public ProductBrandRespository ProductBrandService { get; }
        public ProductImageRespository ProductImageService { get; }

        public ProductAttributeRespository ProductAttributeService { get; }

        public PermissionRespository PermissionService { get; }
        public RoleRespository RoleService { get; }

        public UserRespository UserService { get; }

        public SizeRespository SizeService { get; }

        public CountryRespository CountryService { get; }

        public ColorRespository ColorService { get; }

        public UnitRespository UnitService { get; }

        public StoreRespository ProductStoreService { get; }
        public MenuConfigRespository MenuConfigService { get; }

        public EmailConfigRepository EmailConfigService { get; }

        public SiteConfigRespository SiteConfigService { get; }

        public Respository.ProductTypeRepository ProductTypeService { get; }

        public NewsGroupRepository NewsGroupService { get; }

        protected ContactRepository ContactService { get; }

       
        protected NewsRepository NewsService { get; }     

        protected AccountRepository AccountService { get; }

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