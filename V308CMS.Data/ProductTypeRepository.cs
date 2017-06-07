using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data.Models;

namespace V308CMS.Data
{
    public interface IProductTypeRepository
    {

        List<ProductType> GetAll(bool state = true);

        string Insert(string name, int parentId, string icon, string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status);

        string Update(int id,string name, int parentId, string icon, string description, string image,
            string imageBanner, int number,
            DateTime createdDate, bool status);
    }


    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly V308CMSEntities _entities;

        public ProductTypeRepository(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public List<ProductType> GetAllWeb()
        {
            return (from category in _entities.ProductType
                orderby category.Number ascending
                select category
                ).ToList();
        } 

        public List<ProductType> GetAll(bool state = true)
        {
            return state ? (from category in _entities.ProductType
                            orderby category.Date.Value descending
                            where category.Status == true
                            select category).ToList() :
                    (from category in _entities.ProductType
                     orderby category.Date.Value descending
                     select category).ToList();
        }

        public string Insert(
            string name, int parentId, string icon, 
            string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status)
        {
            var checkProductType = (from type in _entities.ProductType
                where type.Name == name && type.Parent == parentId
                select type
                ).FirstOrDefault();
            if (checkProductType == null)
            {
                var newProductType = new ProductType
                {
                    Name = name,
                    Parent = parentId,
                    Icon = icon,
                    Description = description,
                    Image = image,
                    ImageBanner = imageBanner,
                    Number = number,
                    Date = createdDate,
                    Status = status
                };
                _entities.ProductType.Add(newProductType);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }
        public string ChangeState(int id)
        {
            var productTypeItem = (
               from item in _entities.ProductType.Include("ListProduct")
               where item.ID == id
               select item
              ).FirstOrDefault();
            if (productTypeItem != null)
            {
                productTypeItem.Status = !productTypeItem.Status;
                _entities.SaveChanges();
                if (productTypeItem.ListProduct != null && productTypeItem.ListProduct.Count > 0)
                {
                    foreach (var product in productTypeItem.ListProduct)
                    {
                        product.Status = productTypeItem.Status;
                        _entities.SaveChanges();
                    }
                }
                return "ok";
            }
            return "not_exists";
        }


        public string Update(int id, string name, int parentId, string icon, string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status)
        {
            var productType = (from type in _entities.ProductType
                                    where type.ID == id
                                    select type
                 ).FirstOrDefault();
            if (productType != null)
            {
                productType.Name = name;
                productType.Parent = parentId;
                productType.Icon = icon;
                productType.Description = description;
                if (!string.IsNullOrWhiteSpace(image) && productType.Image != image)
                {
                    productType.Image = image;
                }
                if (!string.IsNullOrWhiteSpace(imageBanner) && productType.ImageBanner != imageBanner)
                {
                    productType.ImageBanner = imageBanner;
                }               
                productType.Number = number;
                productType.Date = createdDate;
                productType.Status = status;
                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";

        }


        public ProductType Find(int id)
        {
            return (from item in _entities.ProductType
                    where item.ID == id
                    select item
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var productTypeItem = (
                from item in _entities.ProductType.Include("ListProduct")
                where item.ID == id
                select item
               ).FirstOrDefault();
            if (productTypeItem != null)
            {

                if (productTypeItem.ListProduct != null && productTypeItem.ListProduct.Count > 0)
                {
                    foreach (var product in productTypeItem.ListProduct)
                    {
                        _entities.Product.Remove(product);
                        _entities.SaveChanges();
                    }
                }
                _entities.ProductType.Remove(productTypeItem);
                _entities.SaveChanges();
                var listSubType = (
                     from item in _entities.ProductType.Include("ListProduct")
                     where item.ID == productTypeItem.ID
                     select item
                ).ToList();
                if (listSubType.Count > 0)
                {

                    foreach (var subType in listSubType)
                    {
                        if (subType.ListProduct != null && subType.ListProduct.Count > 0)
                        {
                            foreach (var product in subType.ListProduct)
                            {
                                _entities.Product.Remove(product);
                                _entities.SaveChanges();
                            }
                        }
                        _entities.ProductType.Remove(subType);
                        _entities.SaveChanges();
                    }
                }
                return "ok";
            }
            return "not_exists";
        }
    }
}