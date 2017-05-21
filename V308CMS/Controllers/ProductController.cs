﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Helpers;

namespace V308CMS.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Index(int id)
        {
            var product = ProductsService.GetById(id);
            if (product != null)
            {
                var producResult = new
                {
                    id = product.ID,
                    title = product.Name,
                    url = url.productURL(product.Name, product.ID),
                    price = product.Price.HasValue ? string.Format("{0} đ", product.Price.Value.ToString("N0")) : "",
                    compare_at_price =
                        string.Format("{0} đ",
                            (product.SaleOff > 0
                                ? (product.Price - ((product.Price/100)*product.SaleOff))
                                : product.Price).Value.ToString("N0")),
                    description = product.Summary,
                    available = product.Number > 0 ? 1 : 0,
                    inventory_quantity = product.Number,
                    vendor = product.ProductManufacturer.Name,
                    featured_image = product.Image.ToUrl(388, 407),
                    images = product.ProductImages.Select(item => new
                    {
                        grand = item.Name.ToUrl(1200),
                        compact = item.Name.ToUrl(107, 113)
                    } ).ToArray()

                };
                return Json(producResult, JsonRequestBehavior.AllowGet);


            }
            return Json(new
            {
               message ="Mã sản phẩm không đúng."

            },JsonRequestBehavior.AllowGet);

        }

    }
}
