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
            //VisisterRepo.UpdateView();
        }

        public ActionResult Index()

        {
            return View("Home",  ProductTypeService.GetListHomeAsync());
        }
        [CategoryUpdateView("categoryId")]
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
            var product = await ProductsService.FindAsync(pId, true);
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
