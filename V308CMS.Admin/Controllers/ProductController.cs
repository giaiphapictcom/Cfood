using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class ProductController : BaseController
    {
        #region SAN PHAM
      
        [CheckAdminAuthorize(1)]
        public ActionResult Index(int? pType, int? pPage, int? pMarket = 0, string pKey = "")
        {
            ProductPage mProductPage = new ProductPage();
            ProductType mProductType = new ProductType() { Parent = 0 };
            string mLevel = "";
            if (pType == null)
            {
                if (Session["SanPhamType"] != null)
                    pType = (int)Session["SanPhamType"];
                else
                    pType = 0;
            }
            else
            {
                Session["SanPhamType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["SanPhamPage"] != null)
                    pPage = (int)Session["SanPhamPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["SanPhamPage"] = pPage;
            }
            //
            if (pMarket == 0)
            {
                if (Session["SanPhamMarket"] != null)
                    pMarket = (int)Session["SanPhamMarket"];
                else
                    pMarket = 0;
            }
            else
            {
                Session["SanPhamMarket"] = pMarket;
            }
            //
            if (pKey.Length == 0)
            {
                //if (Session["SanPhamKey"] != null)
                //    pKey = (string)Session["SanPhamKey"];
                //else
                pKey = "";
            }
            else
            {
                Session["SanPhamKey"] = pKey;
            }
            #endregion

            //lay Level cua Type
            if (pType != 0)
            {
                mProductType = ProductsService.LayProductTypeTheoId((int)pType);
                if (mProductType != null)
                    mLevel = mProductType.Level.Trim();
            }
            //
            mProductPage.Market = (int)pMarket;
            mProductPage.Key = pKey;
            //lay danh sach cac sieu thi
            var mMarketList = MarketService.getAll(1000);
            mProductPage.MarketList = mMarketList;
            /*Lay danh sach cac tin theo page*/
            var mProductList = ProductsService.getByTypeAndNameAndMarket((int)pPage, 18, (int)pType, (int)pMarket, pKey, mLevel);
            //lay danh sach cac kieu san pham
            var mProductTypeList = ProductsService.LayProductTypeAll();
            var mProductTypeChildList = ProductsService.LayProductTypeTheoParentId((int)pType);
            if (mProductList.Count < 18)
                mProductPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mProductPage.Html = V308HTMLHELPER.TaoDanhSachSanPham(mProductList, (int)pPage);
            mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductTypeHome(mProductTypeList, (int)pPage, (int)pType);
            mProductPage.ProductTypeLt = mProductTypeChildList;
            mProductPage.Page = (int)pPage;
            mProductPage.TypeId = (int)pType;
            mProductPage.pProductType = mProductType;
            return View("Index", mProductPage);
        }       
        [CheckAdminJson(1)]
        [HttpPost]
        [ActionName("Edit")]
        public JsonResult OnEdit(int[] pId, int[] pNumber, bool[] pHome, bool[] pBestSale, bool[] pHide, bool[] pDelete)
        {          
            var mProductList = ProductsService.getProductByIdList(pId);
            foreach (Product it in mProductList)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (pId.Length > i)
                    {
                        if (it.ID == pId[i])
                        {
                            if (pHide.Length > i)
                            {
                                if (it.Status != pHide[i])
                                {
                                    it.Status = pHide[i];
                                }
                            }
                            if (pHome.Length > i)
                            {
                                if (it.Hot != pHome[i])
                                {
                                    it.Hot = pHome[i];
                                }
                            }
                            if (pBestSale.Length > i)
                            {
                                if (it.Visible != pBestSale[i])
                                {
                                    it.Visible = pBestSale[i];
                                }
                            }
                            if (pNumber.Length > i)
                            {
                                if (it.Number != pNumber[i])
                                {
                                    it.Number = pNumber[i];
                                }
                            }
                            if (pDelete.Length > i)
                            {
                                if (pDelete[i] == true)
                                {
                                    MpStartEntities.DeleteObject(it);
                                }
                            }
                        }
                    }
                }
            }
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Cập nhật thành công!" });

        }      
        [CheckAdminJson(1)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {
            var mProduct = ProductsService.LayTheoId(pId);
            if (mProduct != null)
            {
                MpStartEntities.DeleteObject(mProduct);
                //Tim danh sach anh
                var mProductImageList = ProductsService.LayProductImageTheoIDProduct(pId);
                var mProductAttribute = ProductsService.LayProductAttributeTheoIDProduct(pId);
                if (mProductAttribute.Count > 0)
                {
                    foreach (ProductAttribute it in mProductAttribute)
                    {
                        MpStartEntities.DeleteObject(it);
                    }
                }
                if (mProductImageList.Count > 0)
                {
                    foreach (ProductImage it in mProductImageList)
                    {
                        MpStartEntities.DeleteObject(it);
                    }
                }
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm cần xóa." });

        }      
        [CheckAdminJson(1)]
        [HttpPost]
        [ActionName("ChangeStatus")]
        public JsonResult OnChangeStatus(int pId = 0)
        {           
            var mProduct = ProductsService.LayTheoId(pId);
            if (mProduct != null)
            {
                mProduct.Status = !mProduct.Status;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message =(mProduct.Status ==true? "Hiện thành công!":"Ẩn thành công!")  });
            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm cần ẩn." });
        }       
         
        [CheckAdminAuthorize(1)]
        public ActionResult Create()
        {
 
            ProductPage mProductPage = new ProductPage();

            var mListProductType = ProductsService.LayProductTypeAll();
            var mListProductDistributor = ProductsService.LayProductDistributorAll();
            var mListProductManufacturer = ProductsService.LayProductManufacturerAll();
            var mMarketList = MarketService.LayMarketTheoTrangAndType(1, 1000, 0);
            //Tao danh sach cac nhom tin
            mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType4(mListProductType, 0);
            mProductPage.HtmlNhom2 = V308HTMLHELPER.TaoDanhSachProductDistributor4(mListProductDistributor, 0);
            mProductPage.HtmlNhom3 = V308HTMLHELPER.TaoDanhSachProductManufacturer4(mListProductManufacturer, 0);
            mProductPage.HtmlNhom4 = V308HTMLHELPER.TaoDanhSachMarket(mMarketList, 0);
            return View("Create", mProductPage);
        }
        [HttpPost]       
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate
            (
                int? pTransport1, int? pTransport2, int? pTransport12,
                int? pTransport22, string pTitle, string pDapAn1,
                string pDapAn2, string pDapAn3, string pDapAn4,
                int? pKetQua, int? pGroup, int? pProductDistributor, 
                int? pProductManufacturer, string pImage, int? pPrice,
                int? pPrice2, int? pPrice3, string pUnit1,
                string pUnit2, string pUnit3, string pSeri, 
                string pSummary, string[] imageslide, string[] pNamePro,
                string[] pValuePro, string editor1, int? pOrder, 
                int? pSaleOff, string pActive, int? pBaoHanh,
                int? pSize, int? pPower, int? pMotoGroup, 
                int? pMarket, string pDescription, string pKeyWord
            )
        {                       
            int i = 0;
            var mProduct = new Product()
            {
                Name = pTitle,
                Choice1 = pDapAn1,
                Choice2 = pDapAn2,
                Choice3 = pDapAn3,
                Choice4 = pDapAn4,
                Price2 = pPrice2,
                Price3 = pPrice3,
                Unit1 = pUnit1,
                Unit2 = pUnit2,
                Unit3 = pUnit3,
                Answer = pKetQua,
                Type = pGroup,
                Distributor = pProductDistributor,
                Manufacturer = pProductManufacturer,
                AccountId = 0,
                Date = DateTime.Now,
                Detail = editor1,
                Image = pImage,
                Number = pOrder,
                Price = pPrice,
                MarketId = pMarket,
                SeriNumber = pSeri,
                Status = true,
                Summary = pSummary,
                Visible = true,
                SaleOff = pSaleOff,
                BaoHanh = pBaoHanh,
                Size = pSize,
                Power = pPower,
                Group = pMotoGroup,
                Description = pDescription,
                Keyword = pKeyWord,
                Transport1 = pTransport1,
                Transport2 = pTransport2,
                Transport12 = pTransport12,
                Transport22 = pTransport22
            };
            MpStartEntities.AddToProduct(mProduct);
            MpStartEntities.SaveChanges();
            //Tao cac anh Slide va properties
            if (imageslide != null)
            {
                foreach (string it in imageslide)
                {
                    if (!String.IsNullOrEmpty(it))
                    {
                        var mProductImage = new ProductImage() { Name = it, Number = 1, ProductID = mProduct.ID, Title = pTitle };
                        MpStartEntities.AddToProductImage(mProductImage);
                    }
                }
            }
            //Tao cac gia tri thuoc tinh
            if (pNamePro != null)
            {
                foreach (string it in pNamePro)
                {
                    if (!String.IsNullOrEmpty(it))
                    {
                        var mProductAttribute = new ProductAttribute() { Name = pNamePro[i], Value = pValuePro[i], ProductID = mProduct.ID };
                        MpStartEntities.AddToProductAttribute(mProductAttribute);
                    }
                    i++;
                }
            }
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu sản phẩm thành công." });

        }      
        [CheckAdminAuthorize(1)]
        public ActionResult Edit(int pId)
        {          
            ProductPage mProductPage = new ProductPage();
            var mProduct = ProductsService.LayTheoId(pId);
            if (mProduct != null)
            {
                var mListProductType = ProductsService.LayProductTypeAll();
                var mListProductDistributor = ProductsService.LayProductDistributorAll();
                var mListProductManufacturer = ProductsService.LayProductManufacturerAll();
                var mMarketList = MarketService.LayMarketTheoTrangAndType(1, 1000, 0);
                //lay cac anh cu va xoa di
                var mProductImageList = ProductsService.LayProductImageTheoIDProduct(mProduct.ID);
                //lay danh sach cac thuoc tinh cu
                var mProductAttributeList = ProductsService.LayProductAttributeTheoIDProduct(mProduct.ID);
                //Tao danh sach cac nhom tin
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType4(mListProductType, (int)mProduct.Type);
                mProductPage.HtmlNhom2 = V308HTMLHELPER.TaoDanhSachProductDistributor4(mListProductDistributor, (int)mProduct.Distributor);
                mProductPage.HtmlNhom3 = V308HTMLHELPER.TaoDanhSachProductManufacturer4(mListProductManufacturer, (int)mProduct.Manufacturer);
                mProductPage.HtmlNhom4 = V308HTMLHELPER.TaoDanhSachMarket(mMarketList, (int)mProduct.MarketId);
                mProductPage.HtmlProductAttribute = V308HTMLHELPER.TaoDanhSachAnhAttributeSanPham(mProductAttributeList);
                mProductPage.HtmlProductImage = V308HTMLHELPER.TaoDanhSachAnhSlideSanPham(mProductImageList);
                mProductPage.pProduct = mProduct;
            }
            else
            {
                mProductPage.Html = "Không tìm thấy sản phẩm cần sửa.";
            }
            return View("Edit", mProductPage);
        }
        [HttpPost]      
        [CheckAdminJson(1)]
        [ValidateInput(false)]        
        public JsonResult OnEdit(
                int pId, int? pTransport1, int? pTransport2, 
                int? pTransport12, int? pTransport22,
                string pTitle, string pDapAn1, string pDapAn2,
                string pDapAn3, string pDapAn4, int? pKetQua,
                int? pGroup, int? pProductDistributor, int? pProductManufacturer,
                string pImage, int? pPrice, int? pPrice2, 
                int? pPrice3, string pUnit1, string pUnit2, 
                string pUnit3, string pSeri, string pSummary, 
                string[] imageslide, string[] pNamePro, string[] pValuePro,
                string editor1, int? pOrder, int? pSaleOff, 
                string pActive, int? pBaoHanh, int? pSize, 
                int? pPower, int? pMotoGroup, int? pMarket, 
                string pDescription, string pKeyWord
            )
        {
            int i = 0;
            var mProduct = ProductsService.LayTheoId(pId);
            if (mProduct != null)
            {
                mProduct.Name = pTitle;
                mProduct.Choice1 = pDapAn1;
                mProduct.Choice2 = pDapAn2;
                mProduct.Choice3 = pDapAn3;
                mProduct.Choice4 = pDapAn4;
                mProduct.Price2 = pPrice2;
                mProduct.Price3 = pPrice3;
                mProduct.Unit1 = pUnit1;
                mProduct.Unit2 = pUnit2;
                mProduct.Unit3 = pUnit3;
                mProduct.Unit3 = pUnit3;
                mProduct.Answer = pKetQua;
                mProduct.Date = DateTime.Now;
                mProduct.Detail = editor1;
                mProduct.Image = pImage;
                mProduct.Number = pOrder;
                mProduct.Summary = pSummary;
                mProduct.Type = pGroup;
                mProduct.SeriNumber = pSeri;
                mProduct.Manufacturer = pProductManufacturer;
                mProduct.Distributor = pProductDistributor;
                mProduct.SaleOff = pSaleOff;
                mProduct.MarketId = pMarket;
                mProduct.BaoHanh = pBaoHanh;
                mProduct.Size = pSize;
                mProduct.Power = pPower;
                mProduct.Price = pPrice;
                mProduct.Group = pMotoGroup;
                mProduct.Description = pDescription;
                mProduct.Keyword = pKeyWord;
                mProduct.Transport1 = pTransport1;
                mProduct.Transport2 = pTransport2;
                mProduct.Transport12 = pTransport12;
                mProduct.Transport22 = pTransport22;
                //Tao cac anh Slide va properties
                if (imageslide != null)
                {
                    if (imageslide.Any())
                    {
                        //lay cac anh cu va xoa di
                        var mProductImageList = ProductsService.LayProductImageTheoIDProduct(mProduct.ID);
                        //xoa cac anh cu nay
                        foreach (ProductImage it in mProductImageList)
                        {
                            MpStartEntities.DeleteObject(it);
                        }
                        //luu ket qua
                        MpStartEntities.SaveChanges();
                        //them cac anh moi
                        foreach (string it in imageslide)
                        {
                            if (!String.IsNullOrEmpty(it))
                            {
                                var mProductImage = new ProductImage() { Name = it, Number = 1, ProductID = mProduct.ID, Title = pTitle };
                                MpStartEntities.AddToProductImage(mProductImage);
                            }
                        }
                        //luu ket qua
                        MpStartEntities.SaveChanges();
                    }
                }
                //Tao cac gia tri thuoc tinh
                if (pNamePro != null)
                {
                    if (pNamePro.Any())
                    {
                        //lay danh sach cac thuoc tinh cu
                        var mProductAttributeList = ProductsService.LayProductAttributeTheoIDProduct(mProduct.ID);
                        //xoa cac anh cu nay
                        foreach (ProductAttribute it in mProductAttributeList)
                        {
                            MpStartEntities.DeleteObject(it);
                        }
                        //luu ket qua
                        MpStartEntities.SaveChanges();
                        foreach (string it in pNamePro)
                        {
                            if (!String.IsNullOrEmpty(it))
                            {
                                var mProductAttribute = new ProductAttribute() { Name = pNamePro[i], Value = pValuePro[i], ProductID = mProduct.ID };
                                MpStartEntities.AddToProductAttribute(mProductAttribute);
                            }
                            i++;
                        }
                        //luu ket qua
                        MpStartEntities.SaveChanges();
                    }
                }
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sửa sản phẩm thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm để sửa." });
        }      
    }
}