using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class AsyncController : BaseController
    {
        //
        // GET: /Async/      
        public PartialViewResult LoadListBrandAsync(int categoryId, int limit = 6)
        {
            return PartialView("_ListBrandAsync", ProductsService.getRandomBrands(categoryId, limit));
        }

        public PartialViewResult LoadListProductByCategoryAsync(int categoryId, int limit = 6)
        {
            return PartialView("_ListProductByCategoryAsync", BoxContentService.GetListProductByCategory(categoryId, limit));

        }
        [ChildActionOnly]
        public async Task<ActionResult> LoadBoxItemAsync(ProductType category, int subCategoryLimit = 3, int productLimit = 6)
        {

            var listSubcategory = await ProductTypeService.GetListSubByParentIdAsync(category.ID, subCategoryLimit);
            var boxContent = new BoxContent
            {
                Category = category,
                ListSubCategory = listSubcategory
            };
            for (int k = 0, subTotal = listSubcategory.Count; k < subTotal; k++)
            {
                var subCategory = listSubcategory[k];
                var boxContentItem = new BoxContentItem
                {
                    SubCategory = subCategory,
                    Products = await ProductsService.GetListByCategoryWithImagesAsync(subCategory.ID, productLimit)                   
                };
                boxContent.ListContentItem.Add(boxContentItem);

            }
            return View("_BoxItem", boxContent);

        }

    }
}