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
using V308CMS.Filters;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class HomeController : BaseController
    {      
        public HomeController()
        {

        }

        public ActionResult Index()

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
            //ViewBag.ListCategoryRootHome = await ProductTypeService.GetListHomeAsync();
          
            string view = Theme.viewPage("home");
            if (view.Length > 0)
            {
                return View("Home", mIndexPageContainer);
                //return View("Home", await ProductTypeService.GetListHomeAsync());
            }

            if (!Request.Browser.IsMobileDevice)
                return View(mIndexPageContainer);
            else
                return View("MobileIndex", mIndexPageContainer);


            return View("Home",  ProductTypeService.GetListHomeAsync());

        }

        //[CategoryUpdateView("categoryId")]

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
    }

}
