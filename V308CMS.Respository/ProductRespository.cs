﻿using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Respository
{
    public interface IProductRespository
    {
         List<ProductItem> GetList(
            out int totalRecord, int categoryId = 0,
            int quantity = 0, int state = 0,
            int brand = 0, int manufact = 0,
            int provider = 0,
            string keyword = "",
            int page = 1, int pageSize = 15);

        List<Product> GetListProductInListId(string listId, bool includeData = true);
        string ChangeStatus(int id);       
        string UpdateOrder(int productId, int order);
        string UpdateQuantity(int productId, int quantity);
        string UpdatePrice(int productId, double price);
        string UpdateNpp(int productId, double npp);
        string UpdateCode(int productId, string code);
        string HideAll(string listId);
        string DeleteAll(string listId);

    }
    public class ProductRespository: IBaseRespository<Product>, IProductRespository
    {
        public Product Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.Product                
                        where item.ID == id
                        select item
                ).FirstOrDefault();
            }
        }
        public Product FindToModify(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.Product.
                    Include("ProductImages").
                    Include("ProductColor").
                    Include("ProductSize").
                    Include("ProductAttribute").
                    Include("ProductSaleOff")
                        where item.ID == id
                        select item
                ).FirstOrDefault();
            }

        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var productDelete = (from item in entities.Product.
                      Include("ProductImages").
                      Include("ProductColor").
                      Include("ProductSize").
                      Include("ProductAttribute").
                      Include("ProductSaleOff")
                                     where item.ID == id
                                     select item
                  ).FirstOrDefault();
                if (productDelete != null)
                {
                    if (productDelete.ProductAttribute != null && productDelete.ProductAttribute.Count > 0)
                    {
                        var listProductAttribute = productDelete.ProductAttribute.ToList();
                        var productAttributeRespository = new ProductAttributeRespository();
                        foreach (var productAttribute in listProductAttribute)
                        {
                            productAttributeRespository.Delete(productAttribute.ID);
                        }                       
                    }
                    if (productDelete.ProductColor != null && productDelete.ProductColor.Count > 0)
                    {
                        var listProductColor = productDelete.ProductColor.ToList();
                        var productColorRespository = new ProductColorRespository();
                        foreach (var productColor in listProductColor)
                        {
                            productColorRespository.Delete(productColor.Id);
                        }                        
                    }
                    if (productDelete.ProductSize != null && productDelete.ProductSize.Count > 0)
                    {
                        var listProductSize = productDelete.ProductSize.ToList();
                        var productSizeRespository = new ProductSizeRespository();
                        foreach (var productSize in listProductSize)
                        {
                            productSizeRespository.Delete(productSize.Id);
                        }                        

                    }
                    if (productDelete.ProductSaleOff != null && productDelete.ProductSaleOff.Count > 0)
                    {
                        var listProductSaleOff = productDelete.ProductSaleOff.ToList();
                        var productSaleOffRespository = new ProductSaleOffRespository();
                        foreach (var saleOff in listProductSaleOff)
                        {
                            productSaleOffRespository.Delete(saleOff.ID);
                        }                        
                    }
                    if (productDelete.ProductImages != null && productDelete.ProductImages.Count > 0)
                    {
                        var listProductImages = productDelete.ProductImages.ToList();
                        var productImageRespository = new ProductImageRespository();
                        foreach (var productImage in listProductImages)
                        {
                            productImageRespository.Delete(productImage.ID);
                        }                       
                    }
                    entities.Product.Remove(productDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";


            }
          
        }

        public string Update(Product data)
        {
            using (var entities = new V308CMSEntities())
            {
                var productUpdate = (from item in entities.Product
                               where item.ID == data.ID
                               select item
               ).FirstOrDefault();
                if (productUpdate != null)
                {
                    productUpdate.Name = data.Name;
                    productUpdate.Type = data.Type;
                    productUpdate.Summary = data.Summary;
                    productUpdate.Code = data.Code;
                    productUpdate.Image = data.Image;
                    productUpdate.BrandId = data.BrandId;
                    productUpdate.Country = data.Country;
                    productUpdate.Store = data.Store;
                    productUpdate.Manufacturer = data.Manufacturer;
                    productUpdate.AccountId = data.AccountId;
                    productUpdate.Number = data.Number;
                    productUpdate.Unit1 = data.Unit1;
                    productUpdate.Weight = data.Weight;
                    productUpdate.Quantity = data.Quantity;
                    productUpdate.Npp = Convert.ToDouble(data.Npp);
                    productUpdate.Width = data.Width;
                    productUpdate.Height = data.Height;
                    productUpdate.Depth = data.Depth;
                    productUpdate.Detail = data.Detail;
                    productUpdate.WarrantyTime = data.WarrantyTime;
                    productUpdate.ExpireDate = data.ExpireDate;
                    productUpdate.Title = data.Title;
                    productUpdate.Keyword = data.Keyword;
                    productUpdate.Description = data.Description;
                    productUpdate.Price = data.Price;
                    productUpdate.Transport1 = data.Transport1;
                    productUpdate.Transport2 = data.Transport2;

                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }

        public string Insert(Product data)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkProduct = (from product in entities.Product
                    where product.Title == data.Title
                          && product.Type == data.Type
                    select product
                    ).FirstOrDefault();
                if (checkProduct == null)
                {
                    entities.Product.Add(data);
                    entities.SaveChanges();
                    return "ok";


                }
                return "exists";

            }
          
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<Product> GetListProductInListId(string listId, bool includeData = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return includeData ? (from item in entities.Product.
                    Include("ProductImages").
                    Include("ProductColor").
                    Include("ProductSize").
                    Include("ProductAttribute").
                    Include("ProductSaleOff").AsEnumerable()
                                      where listId.Contains(item.ID.ToString())
                                      select item
              ).ToList() :
              (from item in entities.Product.AsEnumerable()
               where listId.Contains(item.ID.ToString())
               select item
              ).ToList();
            }

          
        }

        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var product = (from item in entities.Product
                               where item.ID == id
                               select item
               ).FirstOrDefault();
                if (product != null)
                {
                    product.Status = !product.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

     

        public string UpdateOrder(int productId, int order)
        {
            using (var entities = new V308CMSEntities())
            {
                var productOrder = (from product in entities.Product
                                    where product.ID == productId
                                    select product
               ).FirstOrDefault();
                if (productOrder != null)
                {
                    productOrder.Number = order;
                    entities.SaveChanges();
                    return productOrder.Name;
                }
                return "not_exists";
            }
            
        }

        public string UpdateQuantity(int productId, int quantity)
        {
            using (var entities = new V308CMSEntities())
            {
                var productQuantity = (from product in entities.Product
                                       where product.ID == productId
                                       select product
               ).FirstOrDefault();
                if (productQuantity != null)
                {
                    productQuantity.Quantity = quantity;
                    entities.SaveChanges();
                    return productQuantity.Name;
                }
                return "not_exists";
            }

           
        }

        public string UpdatePrice(int productId, double price)
        {
            using (var entities = new V308CMSEntities())
            {
                var productPrice = (from product in entities.Product
                                    where product.ID == productId
                                    select product
              ).FirstOrDefault();
                if (productPrice != null)
                {
                    productPrice.Price = price;
                    entities.SaveChanges();
                    return productPrice.Name;
                }
                return "not_exists";
            }
          
        }

        public string UpdateNpp(int productId, double npp)
        {
            using (var entities = new V308CMSEntities())
            {
                var productNpp = (from product in entities.Product
                                  where product.ID == productId
                                  select product
               ).FirstOrDefault();
                if (productNpp != null)
                {
                    productNpp.Npp = npp;
                    entities.SaveChanges();
                    return productNpp.Name;
                }
                return "not_exists";
            }
           
        }

        public string UpdateCode(int productId, string code)
        {
            using (var entities = new V308CMSEntities())
            {
                var productCode = (from product in entities.Product
                                   where product.ID == productId
                                   select product
               ).FirstOrDefault();
                if (productCode != null)
                {
                    productCode.Code = code;
                    entities.SaveChanges();
                    return productCode.Name;
                }
                return "not_exists";
            }
            
        }

        public string HideAll(string listId)
        {
            using (var entities = new V308CMSEntities())
            {
                var listProductHide = (from item in entities.Product.AsEnumerable()
                                       where listId.Contains(item.ID.ToString())
                                       select item
              ).ToList();
               
                if (listProductHide.Count > 0)
                {
                    var productHided = "";
                    foreach (var product in listProductHide)
                    {
                        product.Status = false;
                        entities.SaveChanges();
                        productHided = productHided + "," + product.ID;
                    }
                    return productHided;
                }
                return "not_exists";

            }
        }

        public string DeleteAll(string listId)
        {
            using (var entities = new V308CMSEntities())
            {
                var listProductDelete = (from item in entities.Product.
                Include("ProductImages").
                Include("ProductColor").
                Include("ProductSize").
                Include("ProductAttribute").
                Include("ProductSaleOff").AsEnumerable()
                                         where listId.Contains(item.ID.ToString())
                                         select item
                ).ToList();
                var productDeleted = "";
                if (listProductDelete.Count > 0)
                {
                    foreach (var product in listProductDelete)
                    {
                        Delete(product.ID);
                        productDeleted = productDeleted + "," + product.ID;
                    }
                    return "ok";
                }
                return "not_exists";
            }
        }

        public List<ProductItem> GetList(out int totalRecord, int categoryId = 0, int quantity = 0, int state = 0, int brand = 0, int manufact = 0,
            int provider = 0, string keyword = "", int page = 1, int pageSize = 15)
        {
            using (var entities = new V308CMSEntities())
            {
                IEnumerable<Product> data = (from product in entities.Product.Include("ProductType")
                                             select product
                                         ).ToList();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var keywordLower = Ultility.LocDau(keyword.ToLower());
                    data = (from product in entities.Product.AsEnumerable()
                            where Ultility.LocDau(product.Code.ToLower()).Contains(keywordLower) ||
                                   Ultility.LocDau(product.Name.ToLower()).Contains(keywordLower)

                            select product
                        ).ToList();
                }
                if (categoryId > 0)
                {
                    data = (from product in data
                            where product.Type == categoryId
                            select product
                      ).ToList();

                }
                if (quantity > 0)
                {
                    data = quantity == 1 ? (from product in data
                                            where product.Quantity > 0
                                            select product
                     ).ToList() : (from product in data
                                   where product.Quantity == 0
                                   select product
                     ).ToList();
                }
                if (state > 0)
                {
                    if (state == (int)StateFilterEnum.Active)
                    {
                        data = (from product in data
                                where product.Status == true
                                select product
                            ).ToList();
                    }
                    if (state == (int)StateFilterEnum.Pending)
                    {
                        data = (from product in data
                                where product.Status == false
                                select product).ToList();
                    }
                    if (state == (int)StateFilterEnum.PriceEmpty)
                    {
                        data = (from product in data
                                where ((product.Price.HasValue == false) || (product.Price.Value == 0))
                                select product).ToList();
                    }

                }

                if (manufact > 0)
                {
                    data = (from product in data
                            where product.Manufacturer == manufact
                            select product
                     ).ToList();
                }
                if (brand > 0)
                {
                    data = (from product in data
                            where product.BrandId == brand
                            select product
                     ).ToList();
                }
                if (provider > 0)
                {
                    data = (from product in data
                            where product.AccountId == provider
                            select product
                     ).ToList();
                }
                totalRecord = data.Count();
                return (from product in data
                        orderby product.Date.Value descending
                        select new ProductItem
                        {
                            Id = product.ID,
                            Name = product.Name,
                            CategoryId = product.Type,
                            CategoryName = product.ProductType.Name,
                            Quantity = product.Quantity,
                            Code = product.Code,
                            CreatedDate = product.Date.Value,
                            Status = product.Status,
                            Image = product.Image,
                            Price = product.Price,
                            Npp = product.Npp,
                            Order = product.Number.HasValue ? product.Number.Value : 0
                        }
                    )
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }
        }
    }
}
