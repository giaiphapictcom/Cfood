﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Helpers;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class ProductController : BaseController
    {
        #region Repository
        static V308CMSEntities mEntities;
        ProductRepository ProductRepos;

        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
           
        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();

        }
        #endregion

        private void ProductController() {
            CreateRepos();
        }

        public ActionResult Index(int id)
        {
            var product = ProductRepos.GetById(id);
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
        
        public ActionResult BigSale(){
            try {
                ProductItemsPage Model = new ProductItemsPage();
                var products = ProductRepos.GetItemsBySaleoff(1,15,">");
                return View("Search", Model);  
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

    }
}
