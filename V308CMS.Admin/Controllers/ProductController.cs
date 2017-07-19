using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Admin.Models.UI;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{

    [Authorize]
    [CheckGroupPermission(true, "Sản phẩm")]
    public class ProductController : BaseController
    {
        [NonAction]
        private List<MutilCategoryItem> BuildListCategory()
        {
           return ProductTypeService.GetAll().Select
               (
                   cate => new MutilCategoryItem
                   {
                       Name = cate.Name,
                       Id = cate.ID,
                       ParentId = cate.Parent
                   }
               ).ToList();
        }
        [NonAction]
        private List<SelectListItem> BuildListColor(string[] listColorSelected = null)
        {
            return ColorService.GetAll().AsEnumerable()
                   .Select(c => new SelectListItem
                   {
                       Value = c.Code,
                       Text = c.Name,
                       Selected = (listColorSelected != null && listColorSelected.Length > 0 && listColorSelected.Contains(c.Code))
                   }).ToList();

        }
        [NonAction]
        private List<SelectListItem> BuildListSize(string[] listSizeSelected = null)
        {
          return  SizeService.GetAll().AsEnumerable()
            .Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Description,
                Selected = (listSizeSelected != null && listSizeSelected.Length > 0 && listSizeSelected.Contains(c.Name))
            }).ToList();
        }     
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(
            string keyword ="",int categoryId =0,
            int quantity =0,int state =0, int brand=0,
            int manufact =0,int provider =0,
            int page  = 1, int pageSize = 25)
        {
           
            int totalRecords = 0;
            int totalPages = 0;
            var data = ProductService.GetList(out totalRecords,
                categoryId, quantity, state,
                brand,manufact,provider,
                keyword, page, pageSize);

            if (totalRecords > 0)
            {

                totalPages = totalRecords / pageSize;
                if (totalRecords % pageSize > 0)
                    totalPages += 1;
            }
            var model = new ProductViewModels
            {
                Keyword = keyword,
                State = state,
                CategoryId = categoryId,
                Quantity = quantity,
                Data = data,
                Page = page,
                PageSize =  pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Brand = brand,
                Manufact = manufact,
                Provider= provider
            };        
            ViewBag.ListStateFilter = DataHelper.ListEnumType<StateFilterEnum>();
            ViewBag.ListQuantityFilter = DataHelper.ListEnumType<QuantityFilterEnum>();
            ViewBag.ListBrand = ProductBrandService.GetAll();
            ViewBag.ListManufacturer = ProductManufacturerService.GetAll();
            ViewBag.ListProvider = AccountService.GetListAdminByType((byte)AccountType.Provider);
            ViewBag.ListCategory = BuildListCategory();
            return View("Index", model);
        }
                
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {

            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListBrand = ProductBrandService.GetAll();
            ViewBag.ListCountry = CountryService.GetAll();
            ViewBag.ListStore = ProductStoreService.GetAll();
            ViewBag.ListManufacturer = ProductManufacturerService.GetAll();                      
            ViewBag.ListUnit = UnitService.GetAll();
            ViewBag.ListColor = BuildListColor();
            ViewBag.ListSize = BuildListSize();
            ViewBag.ListHour = DataHelper.ListHour;
            return View("Create", new ProductModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]       
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult OnCreate(ProductModels product)
        {
           
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(product.Code))
                {
                    product.Code = string.Format("{0}-{1}",ConfigHelper.ProductCode,DateTime.Now.Ticks.GetHashCode() );
                }
                product.ImageUrl = product.Image != null ?
                   product.Image.Upload() :
                   product.ImageUrl;
                var newProduct = new Product
                {
                    Name = product.Name,
                    Type = product.CategoryId,
                    Summary = product.Summary,
                    Code = product.Code,
                    Image = product.ImageUrl,
                    BrandId = product.BrandId,
                    Country = product.Country,
                    Store = product.Store,
                    Manufacturer = product.ManufacturerId,                   
                    AccountId = 0,
                    Number =product.Number,
                    Unit1 = product.Unit,
                    Weight = product.Weight,
                    Quantity = product.Quantity,
                    Npp = Convert.ToDouble(product.Npp) ,
                    Width = product.Width,
                    Height = product.Height,
                    Depth =  product.Depth,
                    Detail =  product.Detail,
                    WarrantyTime = product.WarrantyTime,
                    ExpireDate = product.ExpireDate,
                    Title =  product.MetaTitle,
                    Keyword = product.MetaKeyword,
                    Description = product.MetaDescription,
                    Price = product.Price,
                    Transport1 =  product.Transport1,
                    Transport2 = product.Transport2,
                    Status = false
                };
                var result =ProductService.Insert(newProduct);
                if (newProduct.Npp > 0) {
                    var RevenueAdd = RevenueGainRepo.Add(newProduct.ID, float.Parse(product.Npp), currentUser.UserId);
                }
                //Tao cac anh Slide va properties
                if (product.ProductImages != null && product.ProductImages.Length>0)
                {
                    ProductImageService.Insert(newProduct.ID, product.Name, product.ProductImages);
                }
                //Tao cac gia tri thuoc tinh
                if (product.AttrLabel != null && product.AttrLabel.Length >0)
                {
                    ProductAttributeService.Insert(newProduct.ID, product.AttrLabel, product.AttrDesc);
                }
                //Tao saleOff
                if (!string.IsNullOrWhiteSpace(product.Percent))
                {
                    ProductSaleOffService.Insert(new ProductSaleOff
                    {
                        ProductID = newProduct.ID,
                        Percent = Convert.ToDouble(product.Percent),
                        StartTime = Convert.ToDateTime(product.StartDate),
                        EndTime = Convert.ToDateTime(product.EndDate)
                    });
                }
                //Tao kich co
                if (product.Size != null && product.Size.Length > 0)
                {
                    ProductSizeService.Insert(newProduct.ID, product.Size);                    
                }
                //Tao mau sac
                if (product.Color != null && product.Color.Length > 0)
                {
                    ProductColorService.Insert(newProduct.ID, product.Color);                 
                }               
                SetFlashMessage(result== Result.Ok? string.Format("Thêm sản phẩm '{0}' thành công.",product.Name): "Lỗi khi thêm mới sản phẩm");
                if (product.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListCategory = BuildListCategory();
                ViewBag.ListBrand = ProductBrandService.GetAll();
                ViewBag.ListCountry = CountryService.GetAll();
                ViewBag.ListStore = ProductStoreService.GetAll();
                ViewBag.ListManufacturer = ProductManufacturerService.GetAll();              
                ViewBag.ListUnit = UnitService.GetAll();
                ViewBag.ListColor = BuildListColor(product.Color);
                ViewBag.ListSize = BuildListSize(product.Size);
                ViewBag.ListHour = DataHelper.ListHour;
                ModelState.Clear();
                return View("Create", product.ResetValue());

            }
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListBrand = ProductBrandService.GetAll();
            ViewBag.ListCountry = CountryService.GetAll();
            ViewBag.ListStore = ProductStoreService.GetAll();
            ViewBag.ListManufacturer = ProductManufacturerService.GetAll();            
            ViewBag.ListUnit = UnitService.GetAll();
            ViewBag.ListColor = BuildListColor(product.Color);
            ViewBag.ListSize = BuildListSize(product.Size);
            ViewBag.ListHour = DataHelper.ListHour;
            
            return View("Create", product);
        }
        [CheckPermission(2, "Sửa")]       
        public ActionResult Edit(int id)
        {
            var product = ProductService.FindToModify(id);
            if (product == null)
            {
                return RedirectToAction("Index");

            }
            var modelEdit = new ProductModels();
            try {
                modelEdit.Id = product.ID;
                modelEdit.Name = product.Name;
                modelEdit.ImageUrl = product.Image;
                modelEdit.CategoryId = product.Type ?? 0;
                modelEdit.Code = product.Code;
                modelEdit.BrandId = product.BrandId;
                modelEdit.Store = product.Store;
                modelEdit.Country = product.Country;
                modelEdit.ManufacturerId = product.Manufacturer;
                modelEdit.AccountId = product.AccountId;
                modelEdit.Number = product.Number ?? 0;
                modelEdit.Unit = product.Unit1.ToString();
                modelEdit.Quantity = 0;
                if (product.Quantity.ToString().Length > 0) {
                    modelEdit.Quantity = int.Parse(product.Quantity.ToString());
                }
                
                modelEdit.Weight = product.Weight;
                modelEdit.Width = product.Width;
                modelEdit.Npp = product.Npp.ToString();
                modelEdit.Depth = product.Depth;
                modelEdit.Summary = product.Summary;
                modelEdit.Detail = product.Detail;
                modelEdit.WarrantyTime = product.WarrantyTime;
                modelEdit.ExpireDate = product.ExpireDate;
                modelEdit.Transport1 = product.Transport1 ?? 0;
                modelEdit.Transport2 = product.Transport2 ?? 0;
                modelEdit.MetaTitle = product.Title;
                modelEdit.MetaDescription = product.Description;
                modelEdit.MetaKeyword = product.Keyword;
                modelEdit.Price = (int)(product.Price ?? 0);
                modelEdit.ListProductImages = product.ProductImages.ToList();
                modelEdit.ListProductAttribute = product.ProductAttribute.ToList();


                if (product.ProductSaleOff != null && product.ProductSaleOff.Count > 0)
                {
                    var saleOffItem = product.ProductSaleOff.FirstOrDefault();
                    //modelEdit.Percent = saleOffItem.Percent?.ToString() ?? "";
                    //modelEdit.StartDate = saleOffItem.StartTime?.ToString() ?? "";
                    //modelEdit.EndDate = saleOffItem.EndTime?.ToString() ?? "";
                    modelEdit.Percent = saleOffItem.Percent.ToString();
                    modelEdit.StartDate = saleOffItem.StartTime.ToString();
                    modelEdit.EndDate = saleOffItem.EndTime.ToString();
                }
            }
            catch (Exception e) {
                Console.Write(e);
            }

            

            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListBrand = ProductBrandService.GetAll();
            ViewBag.ListCountry = CountryService.GetAll();
            ViewBag.ListStore = ProductStoreService.GetAll();
            ViewBag.ListManufacturer = ProductManufacturerService.GetAll();
            ViewBag.ListUnit = UnitService.GetAll();
            ViewBag.ListColor = BuildListColor(
                //product.ProductColor?.Select(item => item.ColorCode).ToArray()
                product.ProductColor.Select(item => item.ColorCode).ToArray()
            );
            ViewBag.ListSize =
                BuildListSize(
                //product.ProductSize?.Select(item => item.Size).ToArray()
                  product.ProductSize.Select(item => item.Size).ToArray()
            );
            ViewBag.ListHour = DataHelper.ListHour;

            return View("Edit", modelEdit);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]       
        [ActionName("Edit")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(ProductModels product)
        {
            var productUpdate = ProductService.Find(product.Id);
            if (productUpdate != null)
            {

                if (string.IsNullOrWhiteSpace(product.Code))
                {
                    product.Code = string.Format("MP-{0}",DateTime.Now.Ticks.GetHashCode());
                }
                product.ImageUrl = product.Image != null ?
                                   product.Image.Upload() :
                                   product.ImageUrl.ToImageOriginalPath();
                productUpdate.Name = product.Name;
                productUpdate.Type = product.CategoryId;
                productUpdate.Summary = product.Summary;
                productUpdate.Code = product.Code;
                productUpdate.Image = product.ImageUrl;
                productUpdate.BrandId = product.BrandId;
                productUpdate.Country = product.Country;
                productUpdate.Store = product.Store;
                productUpdate.Manufacturer = product.ManufacturerId;
                productUpdate.AccountId = product.AccountId;
                productUpdate.Number = product.Number;
                productUpdate.Unit1 = product.Unit;
                productUpdate.Weight = product.Weight;
                productUpdate.Quantity = 0;
                if (product.Quantity.ToString().Length > 0)
                {
                    productUpdate.Quantity = int.Parse(product.Quantity.ToString());
                }
                productUpdate.Npp = Convert.ToDouble(product.Npp);
                productUpdate.Width = product.Width;
                productUpdate.Height = product.Height;
                productUpdate.Depth = product.Depth;
                productUpdate.Detail = product.Detail;
                productUpdate.WarrantyTime = product.WarrantyTime;
                productUpdate.ExpireDate = product.ExpireDate;
                productUpdate.Title = product.MetaTitle;
                productUpdate.Keyword = product.MetaKeyword;
                productUpdate.Description = product.MetaDescription;
                productUpdate.Price = product.Price;
                productUpdate.Transport1 = product.Transport1;
                productUpdate.Transport2 = product.Transport2;
                var result = ProductService.Update(productUpdate);

                if (productUpdate.Npp > 0)
                {
                    var RevenueAdd = RevenueGainRepo.Add(productUpdate.ID, float.Parse(product.Npp), currentUser.UserId);
                }

                //Tao cac anh san pham
                if (product.ProductImages != null && product.ProductImages.Length > 0)
                {
                    ProductImageService.Insert(productUpdate.ID, product.Name, product.ProductImages);                   
                }
                //Tao cac gia tri thuoc tinh
                if (product.AttrLabel != null && product.AttrLabel.Length > 0)
                {
                    ProductAttributeService.Insert(productUpdate.ID, product.AttrLabel, product.AttrDesc);                   
                }
                //Tao saleOff
                if (!string.IsNullOrWhiteSpace(product.Percent))
                {
                    ProductSaleOffService.Insert(new ProductSaleOff
                    {
                        ProductID = productUpdate.ID,
                        Percent = Convert.ToDouble(product.Percent),
                        StartTime = Convert.ToDateTime(product.StartDate),
                        EndTime = Convert.ToDateTime(product.EndDate)
                    });

                }
                //Tao kich co
                if (product.Size != null && product.Size.Length > 0)
                {
                    ProductSizeService.Insert(productUpdate.ID, product.Size);
                }
                //Tao mau sac
                if (product.Color != null && product.Color.Length > 0)
                {
                    ProductColorService.Insert(productUpdate.ID, product.Color);
                }           
              
                SetFlashMessage(result == Result.Ok?
                    string.Format("Cập nhật sản phẩm '{0}' thành công.",product.Name):
                    string.Format("Lỗi khi cập nhật sản phẩm '{0}'",product.Name)
                    );
                if (product.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListCategory = BuildListCategory();
                ViewBag.ListBrand = ProductBrandService.GetAll();
                ViewBag.ListCountry = CountryService.GetAll();
                ViewBag.ListStore = ProductStoreService.GetAll();
                ViewBag.ListManufacturer = ProductManufacturerService.GetAll();
                ViewBag.ListUnit = UnitService.GetAll();
                ViewBag.ListColor = BuildListColor(product.Color);
                ViewBag.ListSize = BuildListSize(product.Size);
                ViewBag.ListHour = DataHelper.ListHour;
                return View("Edit", product);
            }
            ViewBag.ListBrand = ProductBrandService.GetAll();
            ViewBag.ListCountry = CountryService.GetAll();
            ViewBag.ListStore = ProductStoreService.GetAll();
            ViewBag.ListManufacturer = ProductManufacturerService.GetAll();
            ViewBag.ListUnit = UnitService.GetAll();
            ViewBag.ListColor = BuildListColor(product.Color);
            ViewBag.ListSize = BuildListSize(product.Size);
            ViewBag.ListHour = DataHelper.ListHour;
            return View("Edit",product);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]       
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductService.Delete(id);
            SetFlashMessage(result == Result.Ok
                ? "Xóa sản phẩm thành công."
                : "Không tìm thấy sản phẩm cần xóa.");

            return RedirectToAction("Index");

        }      
        [HttpPost]
        [CheckPermission(4, "Thay đổi trạng thái")]
      
        public ActionResult ChangeStatus(int id)
        {
            var result = ProductService.ChangeStatus(id);
            SetFlashMessage(result == Result.Ok
                ? "Thay đổi trạng thái sản phẩm thành công."
                : "Không tìm thấy sản phẩm cần thay đổi trạng thái.");
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckPermission(5, "Cập nhật thứ tự")]        
        public ActionResult UpdateProductOrder(int id, string order)
        {
            int orderValue;
            int.TryParse(order, out orderValue);
            var result = ProductService.UpdateOrder(id, orderValue);
            if (result == Result.NotExists)
            {
                return Json(new { message = "Không tìm thấy sản phẩm để cập nhật thứ tự" });
            }
            return Json(new { message = "Cập nhật thứ tự của sản phẩm " + result + " thành công." });
        }
        [HttpPost]
        [CheckPermission(6, "Cập nhật số lượng")]       
        public ActionResult UpdateProductQuantity(int id, string quantity)
        {
            int quantityValue;
            int.TryParse(quantity, out quantityValue);
            var result = ProductService.UpdateQuantity(id, quantityValue);
            if (result == Result.NotExists)
            {
                return Json(new { message = "Không tìm thấy sản phẩm để cập nhật số lượng" });
            }
            return Json(new { message = "Cập nhật số lượng của sản phẩm " + result + " thành công." });
        }
        [HttpPost]
        [CheckPermission(7, "Cập nhật giá")]        
        public ActionResult UpdatePrice(int id, string price)
        {
            double priceValue;
            double.TryParse(price, out priceValue);
            var result = ProductService.UpdatePrice(id, priceValue);
            if (result == Result.NotExists)
            {
                return Json(new { message = "Không tìm thấy sản phẩm để cập nhật giá." });
            }
            return Json(new { message = "Cập nhật giá của sản phẩm " + result + " thành công." });
        }
        [HttpPost]
        [CheckPermission(8, "Cập nhật Npp")]       
        public ActionResult UpdateNpp(int id, string npp)
        {
            float nppValue;
            float.TryParse(npp, out nppValue);
            
            //var result = ProductService.UpdateNpp(id, nppValue);
            var result = RevenueGainRepo.Add(id, nppValue, currentUser.UserId);
            if (result == Result.NotExists)
            {
                return Json(new { message = "Không tìm thấy sản phẩm để cập nhật chiết khấu." });
            }
            return Json(new { message = "Cập nhật chiết khấu của sản phẩm " + result + " thành công." });
        }
        [HttpPost]
        [CheckPermission(9, "Cập nhật Mã sản phẩm")]        
        public ActionResult UpdateCode(int id, string code)
        {

            var result = ProductService.UpdateCode(id, code);
            if (result == Result.NotExists)
            {
                return Json(new { message = "Không tìm thấy sản phẩm để cập nhật mã" });
            }
            return Json(new { message = "Cập nhật mã của sản phẩm " + result + " thành công." });
        }
        [HttpPost]
        [CheckPermission(10, "Xóa nhiều sản phẩm lựa chọn")]                   
        public ActionResult DeleteAll(string listId)
        {
            if (!string.IsNullOrWhiteSpace(listId))
            {
                var result = ProductService.DeleteAll(listId);
               
                return Json(new { code = (result == Result.Ok? "ok":result), message = "Xóa sản phẩm thành công." });
            }
            return Json(new {code = "not_exists"});


        }
        [CheckPermission(11, "Ẩn nhiều sản phẩm lựa chọn")]
        [HttpPost]
        public ActionResult HideAll(string listId)
        {
            if (!string.IsNullOrWhiteSpace(listId))
            {
                var ressult = ProductService.HideAll(listId);
              
                return Json(new { code = (ressult == Result.Ok ? "ok" : ressult), message = "Ẩn sản phầm lựa chọn thành công." });
            }
            return Json(new { code = "not_exists" });
        }
       
       
    }
}