using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
