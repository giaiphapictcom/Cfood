using System.Web.Mvc;
using V308CMS.Data;
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
        public BannerRepository BannerRepo;
        public TicketRepository TicketRepo;
        public CouponRepository CouponRepo;
        public int PageSize = 10;
        public void CreateRepos()
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

        public void DisposeRepos()
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

        protected BaseController()
        {

        }

        protected void SetFlashMessage(string message)
        {
            TempData["Message"] = message;
        }
    }
}
