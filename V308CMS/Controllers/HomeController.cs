using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ServiceStack.Text;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Helpers;
using V308CMS.Models;
using V308CMS.Respository;

namespace V308CMS.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }
        public async Task<ActionResult> Index()
        {

            IndexPageContainer mIndexPageContainer = new IndexPageContainer();
            List<IndexPage> mIndexPageList = new List<IndexPage>();
            var mListParent = ProductsService.LayProductTypeTheoParentId(0);
            foreach (ProductType it in mListParent)
            {
                //lay danh sach san pham
                var mList = ProductsService.LayTheoTrangAndType(1, !Request.Browser.IsMobileDevice ? 4 : 50, it.ID, it.Level);
                //lay danh sach nhom san pham con
                var mTypeList = ProductsService.getProductTypeByParent(it.ID);
                IndexPage mIndexPage = new IndexPage
                {
                    Id = it.ID,
                    Name = it.Name,
                    Image = it.Image,
                    ImageBanner = "/Content/Images/stepbuy.png",
                    ProductTypeList = mTypeList,
                    ProductList = mList
                };
                //it.ImageBanner;
                mIndexPageList.Add(mIndexPage);
            }

            mIndexPageContainer.IndexPageList = mIndexPageList;

            //lay cac san pham ban chay
            var mBestBuyList = ProductsService.LaySanPhamBanChay(1, !Request.Browser.IsMobileDevice ? 10 : 50);

            if (!mBestBuyList.Any())
            {
                mBestBuyList = ProductsService.getProductsRandom(18);
            }
            mIndexPageContainer.BestBuyList = mBestBuyList;

            mIndexPageContainer.ProductLastest = ProductsService.getProductsLastest(18);
            if (!mIndexPageContainer.ProductLastest.Any())
            {
                mIndexPageContainer.ProductLastest = ProductsService.getProductsRandom(18);
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

        public  ActionResult ListByCategory(int categoryId = 0,string filter = "", int sort = (int) SortEnum.Default, int page = 1,
            int pageSize = 18)
        {
            var category = ProductTypeService.Find(categoryId);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var listFilter = FilterParser.ParseTokenizer(filter);
            int totalRecords;
            var result = new ProductCategoryViewModels
            {
                Category = category,
                ListSubCategory = ProductTypeService.GetAllByCategoryId(categoryId),              
                ListProduct = ProductsService.GetListByCategoryId(categoryId, listFilter, sort, out totalRecords, page, pageSize),
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ListSort = DataHelper.ListEnumType<SortEnum>(sort),
                TotalRecord = totalRecords,
                Filter = filter
                
            };

            return View("ListByCategory", result);

        }
        public ActionResult Category(int pGroupId = 0)
        {

            ProductCategoryPageContainer model = new ProductCategoryPageContainer();

            int nPage = Convert.ToInt32(Request.QueryString["p"]);
            if (nPage < 1)
            {
                nPage = 1;
            }

            List<Product> mProductList = new List<Product>();
            List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();

            ProductType productCategory = ProductsService.LayLoaiSanPhamTheoId(pGroupId);
            if (productCategory != null)
            {
                CategoryPage categoryPage = ProductHelper.getProductsByCategory(productCategory.ID, nPage);

                model.Products = categoryPage.Products;
                model.ProductTotal = categoryPage.ProductTotal;
                model.Brands = ProductsService.getRandomBrands(productCategory.ID, 6);
                List<ProductType> mProductTypeList;
                if (productCategory.Parent == 0)
                    mProductTypeList = ProductsService.getProductTypeByProductType(productCategory.ID);//lay danh sach cap 1
                else
                {
                    mProductTypeList = new List<ProductType>
                    {
                        productCategory
                    };
                    //mProductTypeList = productRepository.getProductTypeByProductType((int)mProductType.Parent);//lay danh sach cap 2
                }

                if (mProductTypeList.Count > 0)
                {
                    mProductPageList.AddRange(mProductTypeList.Select(it => ProductHelper.GetCategoryPage(it, nPage)));
                }
                else
                {
                    mProductPageList.Add(ProductHelper.GetCategoryPage(productCategory, nPage));

                }
                //lay danh sach cac nhom so che
                //mSoCheList = productRepos.LayProductTypeTheoParentId(147);
                //Model.ProductTypeList = mSoCheList;
                //lay cac san pham ban chay
                //mBestBuyList = productRepos.getBestBuyWithType(1, 10, 147, "10030");
                //Model.ProductList = mBestBuyList;
            }
            model.List = mProductPageList;
            model.ProductType = productCategory;
            model.BestSeller = ProductsService.getProductsRandom();
            //if (mProductList.Count < 40)
            //    Model.IsEnd = true;
            model.Page = nPage;
            return View("Category", model);
            //if (!Request.Browser.IsMobileDevice)
            //    return View("Category", model);
            //else
            //    return View("MobileCategory", model);

        }
     
        public async Task<ActionResult> Detail(int pId = 0)
        {
            var product = await ProductsService.FindAsync(pId, true);
            ViewBag.CategoryPath =  ProductTypeService.GetListCategoryPath(product.Type.Value);
            return View("Detail", product);           
        }

        public ActionResult Search(string q, int page=1, int pageSize=30)
        {
            int totalRecord;
            int totalPage = 0;
            var listProduct = ProductsService.Search(q, out totalRecord, page, pageSize);
            if (totalRecord > 0)
            {

                totalPage = totalRecord / pageSize;
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
