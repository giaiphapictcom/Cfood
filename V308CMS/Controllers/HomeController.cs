using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class HomeController : BaseController
    {      
        public HomeController()
        {
        }

        public async Task<ActionResult> Index()

        {

            //IndexPageContainer mIndexPageContainer = new IndexPageContainer();
            //List<IndexPage> mIndexPageList = new List<IndexPage>();
            //var mListParent = ProductsService.LayProductTypeTheoParentId(0);
            //foreach (ProductType it in mListParent)
            //{


            IndexPageContainer mIndexPageContainer = new IndexPageContainer();
            List<IndexPage> mIndexPageList = new List<IndexPage>();
            StringBuilder str = new StringBuilder();
            List<Market> mMarketList = new List<Market>();

            List<ProductType> mTypeList;
            //List<ProductType> mSoCheList;
            //List<Product> mBestSoCheList;
            List<Product> mList;
            List<ProductType> mListParent;
            mListParent = ProductsService.LayProductTypeTheoParentId(0);
            foreach (ProductType it in mListParent)
            {
                //lay danh sach san pham
                if (!Request.Browser.IsMobileDevice)
                    mList = ProductsService.LayTheoTrangAndType(1, 4, it.ID, it.Level);
                else
                    mList = ProductsService.LayTheoTrangAndType(1, 50, it.ID, it.Level);
                //lay danh sach nhom san pham con
                mTypeList = ProductsService.getProductTypeByParent(it.ID);
                IndexPage mIndexPage = new IndexPage();
                mIndexPage.Id = it.ID;
                mIndexPage.Name = it.Name;
                mIndexPage.Image = it.Image;
                mIndexPage.ImageBanner = "/Content/Images/stepbuy.png"; //it.ImageBanner;
                mIndexPage.ProductTypeList = mTypeList;
                mIndexPage.ProductList = mList;
                mIndexPageList.Add(mIndexPage);
            }
            mIndexPageContainer.IndexPageList = mIndexPageList;
            //lay cac san pham ban chay
            //if (!Request.Browser.IsMobileDevice)
            //    mBestBuyList = ProductRepos.LaySanPhamBanChay(1, 6);
            //else
            //    mBestBuyList = ProductRepos.LaySanPhamBanChay(1, 6);

            //if (mBestBuyList.Count() < 1)
            //{
            //    mBestBuyList = ProductRepos.getProductsRandom(6);
            //}
            //mIndexPageContainer.BestBuyList = mBestBuyList;


            mIndexPageContainer.ProductLastest = ProductsService.getProductsLastest(6);
            if (!mIndexPageContainer.ProductLastest.Any())
            {
                mIndexPageContainer.ProductLastest = ProductsService.getProductsRandom(6);
            }
            if (Theme.domain == "myshopify")
            {
                List<ProductType> homeCategorys = new List<ProductType>();
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(177));
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(176));
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(179));
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(180));

                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(183));
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(175));
                homeCategorys.Add(ProductsService.LayLoaiSanPhamTheoId(332));

                mIndexPageContainer.ProductTypeList = homeCategorys;
            }
            else if (Theme.domain == "mamoo")
            {
                //List<ProductType> homeCategorys = ProductsService.GetCategoryInHome(6);
                //mIndexPageContainer.ProductTypeList = homeCategorys;
            }


            var mBestBuyList = ProductsService.LaySanPhamBanChay(1, 12);

            if (!mBestBuyList.Any())
            {
                mBestBuyList = ProductsService.getProductsRandom(12);
            }
            mIndexPageContainer.BestBuyList = mBestBuyList;

            mIndexPageContainer.ProductLastest = ProductsService.getProductsLastest(12);
            if (!mIndexPageContainer.ProductLastest.Any())
            {
                mIndexPageContainer.ProductLastest = ProductsService.getProductsRandom(12);
            }
            ViewBag.ListCategoryRootHome = await ProductTypeService.GetListHomeAsync();
          
            string view = Theme.viewPage("home");
            if (view.Length > 0)
            {
                return View("Home", mIndexPageContainer);
            }

            if (!Request.Browser.IsMobileDevice)
                return View(mIndexPageContainer);
            else
                return View("MobileIndex", mIndexPageContainer);
        }

        public ActionResult Category(int categoryId = 0, string filter = "", int sort = (int) SortEnum.Default,
            int page = 1,
            int pageSize = 18)
        {
            var category = ProductTypeService.Find(categoryId);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var listFilter = FilterParser.ParseTokenizer(filter);
            int totalRecord;
            int totalPage = 0;
            var listSubCategory = ProductTypeService.GetAllByCategoryId(categoryId);
            var listCategoryFilter = "";
            if (categoryId > 0)
            {
                listCategoryFilter = "," + categoryId;
            }
            if (listSubCategory == null || listSubCategory.Count == 0)
            {
                listCategoryFilter += ",";
            }
            else
            {
                listCategoryFilter =
                    listSubCategory.Aggregate(listCategoryFilter,
                        (current, subCategory) => current + "," + subCategory.ID) + ",";
            }
            pageSize = 9;
            var listProduct = ProductsService.GetListByCategoryId(listCategoryFilter, listFilter, sort, out totalRecord,
                page,
                pageSize);

            if (totalRecord > 0)
            {

                totalPage = totalRecord/pageSize;
                if (totalRecord%pageSize > 0)
                    totalPage += 1;
            }

            var result = new ProductCategoryViewModels
            {
                Category = category,
                CategoryId = categoryId,
                ListSubCategory = listSubCategory,
                ListProduct = listProduct,
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ListSort = DataHelper.ListEnumType<SortEnum>(sort),
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Filter = filter
            };

            return View("Category", result);
        }
       
        public async Task<ActionResult> Detail(int pId = 0)
        {
            var product = await ProductsService.FindAsync(pId,true);
            if (product == null) {
                product = await ProductsService.FindAsync(pId, false);
            }
            ViewBag.CategoryPath =  ProductTypeService.GetListCategoryPath(product.Type.Value);
            return View("Detail", product);           
        }

        public ActionResult Search(string q, int page = 1, int pageSize = 20)
        {
            int totalRecord;
            int totalPage = 0;
            if (q == null || q.Length < 1) {
                q = Request.QueryString["pKey"];
            }
            var listProduct = ProductsService.Search(q, out totalRecord, page, pageSize);
            if (totalRecord > 0)
            {

                totalPage = totalRecord/pageSize;
                if (totalRecord % pageSize > 0)
                    totalPage += 1;
            }

            var searchModel = new SearchViewModels
            {
                Page = page,
                PageSize = pageSize,
                ListProduct = listProduct,
                Keyword = q,
                TotalPage = totalPage
            };
            return View("Search", searchModel);
        }
        

        


        public ActionResult YoutubeDetail(int pId = 0)
        {
            NewsPage mCommonModel = new NewsPage();
            StringBuilder mStr = new StringBuilder();
            //lay chi tiet san pham
            var mNews = NewsService.LayTinTheoId(pId);
            if (mNews != null)
            {

                mCommonModel.pNews = mNews;
                var mListLienQuan = NewsService.LayTinTucLienQuan(mNews.ID, 26, 5);
                //tao Html tin tuc lien quan
                mCommonModel.List = mListLienQuan;
            }
            else
            {
                mCommonModel.Html = "Không tìm thấy sản phẩm";
            }

            return View("Video", mCommonModel);

        }

        public ActionResult Market(string pMarketName = "")
        {

            Market mMarket;
            ProductCategoryPageBox mProductCategoryPageBox = new ProductCategoryPageBox();
            ProductType mProductType = new ProductType();
            //lay chi tiet ProductType
            //lay danh sach các nhóm sản phẩm con
            //lay cac san pham cua nhom
            mMarket = MarketService.getByName(pMarketName.Replace('-', ' '));
            if (mMarket != null)
            {
                mProductCategoryPageBox.List = new List<ProductCategoryPage>();
                mProductCategoryPageBox.Market = mMarket;
                var mProductTypeList = ProductsService.getProductTypeParent();
                foreach (ProductType it in mProductTypeList)
                {
                    ProductCategoryPage mProductPage = new ProductCategoryPage();
                    mProductPage.Name = it.Name;
                    mProductPage.Value = mMarket.UserName;
                    mProductPage.Id = (int)it.ID;
                    mProductPage.Image = it.Image;
                    List<Product> mProductList = ProductsService.getByPageSizeMarketId(1, 4, (int)it.ID, mMarket.ID, it.Level);
                    mProductPage.List = mProductList;
                    mProductCategoryPageBox.List.Add(mProductPage);
                }

            }
            return View("Market", mProductCategoryPageBox);
        }
        public ActionResult MarketCategory(string pMarketName = "", int pGroupId = 0, int pPage = 1)
        {
            Market mMarket;
            ProductCategoryPage mProductPage = new ProductCategoryPage();
            ProductType mProductType = new ProductType();
            //lay chi tiet ProductType
            //lay danh sach các nhóm sản phẩm con
            //lay cac san pham cua nhom
            mMarket = MarketService.getByName(pMarketName.Replace('-', ' '));
            mProductType = ProductsService.LayLoaiSanPhamTheoId(pGroupId);
            if (mMarket != null)
            {
                mProductPage.Market = mMarket;
                mProductPage.Name = mProductType.Name;
                mProductPage.Id = mProductType.ID;
                mProductPage.Image = mProductType.Image;
                List<Product> mProductList = ProductsService.getByPageSizeMarketId(1, 25, mProductType.ID, mMarket.ID, mProductType.Level);
                mProductPage.List = mProductList;
                if (mProductList.Count < 25)
                    mProductPage.IsEnd = true;
                mProductPage.Page = pPage;
            }
            return View(mProductPage);

        }

        public JsonResult GuiThongTinLienHe(string pfirstname = "", string plastname = "", string pemail = "", string pphone = "", string pcontent = "")
        {
            string emailContent = "Thông tin liên hệ từ " + pfirstname + " " + plastname + " của Đồng Xuân Media: <br/> " + pcontent + " <br/> Email: " + pemail + "<br/> Mobile: " + pphone + " <br/> Ngày : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "";
            V308Mail.SendMail("sales@dongxuanmedia.vn", "Thông tin liên hệ từ " + pfirstname + " " + plastname + " của Đồng Xuân Media: " + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "", emailContent);
            return Json(new { code = 1, message = "Yêu cầu được gửi thành công ! Chúng tôi sẽ sớm liên lạc lại với bạn." });
        }
        public ActionResult Add()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository();
            MarketRepository marketRepository = new MarketRepository();
            StringBuilder str = new StringBuilder();
            var mMarketList = marketRepository.getAll(18);
            foreach (Market it in mMarketList)
            {
                //lay danh sach cac nhom san pham
                var mMarketProductTypeList = marketRepository.getAllMarketProductType(it.ID);
                //lap khap cac kieu nay
                foreach (MarketProductType ut in mMarketProductTypeList)
                {
                    //lay danh sach cac nhom con
                    var mProductTypeList = productRepository.getProductTypeByParent((int)ut.Parent);
                    //them cac san pham
                    foreach (ProductType ht in mProductTypeList)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Product mProduct = new Product()
                            {
                                Name = "Sản phẩm " + it.ID + "_" + ht.ID,
                                Type = ht.ID,
                                MarketId = it.ID,
                                Image = "/Content/Images/pimg.png",
                                Price = 1000,
                                Summary = "Rau muống " + it.ID,
                                Status = true,
                                Date = DateTime.Now
                            };
                            mEntities.AddToProduct(mProduct);
                        }
                    }
                }
                mEntities.SaveChanges();
            }
            //
            return Content("OK"); ;
        }


        public ActionResult MarketList(int ptype = 0)
        {
            List<Market> mList = new List<Market>();
            //lay danh sach nhom tin
            mList = MarketService.getMarketByType(100, ptype);
            ViewBag.MkType = ptype;
            //lay danh sach cac tin theo nhom
            return View(mList);
        }
        [HttpPost]
        public JsonResult addEmail(string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            EmailRepository emailRepository = new EmailRepository(mEntities);
            if (ValidateInput.IsValidEmailAddress(pEmail))
            {
                VEmail mVEmail = new VEmail() { CreatedDate = DateTime.Now, State = true, Type = 1, Value = pEmail };
                mEntities.AddToVEmail(mVEmail);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Email đã được thêm vào hệ thống." });
            }
            else
            {
                return Json(new { code = 0, message = "Email bạn nhập không chính xác." });
            }

        }
        public ActionResult MarketRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckMarketRegister(string pUserName, string pPassWord, string pPassWord2, string pEmail, string pMobile, string InvisibleCaptchaValue, string Captcha = "", bool rbtAgree = false, string pFullName = "")
        {
            var mEtLogin = AccountService.CheckDangKyHome(pUserName, pPassWord, pPassWord2, pEmail, pFullName, pMobile);
            if (mEtLogin.code == 1)
            {
                //SET session cho UserId
                Session["UserId"] = mEtLogin.Account.ID;
                Session["UserName"] = mEtLogin.Account.UserName;
                if (Session["ShopCart"] == null)
                    Session["ShopCart"] = new ShopCart();
                //Thuc hien Authen cho User.
                FormsAuthentication.SetAuthCookie(pUserName, true);
                return Json(new { code = 1, message = "Đăng ký thành công. Tài khoản là : " + pUserName + "." });
            }
            else
            {
                return Json(new { code = 0, message = mEtLogin.message });
            }
        }


    }

}
