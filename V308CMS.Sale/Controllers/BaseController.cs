using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Respository;
using V308CMS.Sale.Helpers;
using V308CMS.Data.Helpers;


namespace V308CMS.Sale.Controllers
{
    public class BaseController : Controller
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
            CreateRepos();
        }

        protected void SetFlashMessage(string message)
        {
            TempData["Message"] = message;
        }

        protected void SetFlashError(string message)
        {
            TempData["Error"] = message;
        }
    }
}
