using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Data.Models;


namespace V308CMS.Data
{
    public interface IProductTypeRepository
    {

        List<ProductType> GetListRoot();
        List<ProductType> GetListParent(int rootId = 0);

       List<ProductType> GetList(
            string keyword = "", int pType = 0,
            string pLevel = "", int rootId = 0, int parentId = 0,
            int childId = 0, int page = 1, int pageSize = 10);
    }



    //    List<ProductType> GetAll(bool state = true);

    //    string Insert(string name, int parentId, string icon, string description, string image, string imageBanner,
    //        int number, DateTime createdDate, bool status);

    //    string Update(int id,string name, int parentId, string icon, string description, string image,
    //        string imageBanner, int number,
    //        DateTime createdDate, bool status);
    //}



    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly V308CMSEntities _entities;

        public ProductTypeRepository(V308CMSEntities entities)
        {
            _entities = entities;
        }


        public List<ProductType> GetListRoot()
        {
            return (from item in _entities.ProductType
                where item.Parent == 0
                    orderby  item.ID descending 
                select item
                ).ToList();
        }
        public List<ProductType> GetListParent(int rootId = 0)
        {
            
            return rootId==0? 
                 new List<ProductType>() : 
                (from item in _entities.ProductType
                 where item.Parent == rootId
                 orderby item.ID descending
                    select item
                ).ToList();
        }
        public List<ProductType> GetList(
            string keyword = "", int pType = 0,
            string pLevel = "", int rootId = 0, int parentId = 0,
            int childId = 0, int page = 1, int pageSize = 10)
        {
            var listGroup = (from productType in _entities.ProductType.AsEnumerable()
                             orderby productType.ID descending
                             select productType);
            //Loc theo tu khoa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var keywordLower = keyword.ToLower();
                listGroup = (from productType in listGroup
                             where productType.Name.ToLower().Contains(keywordLower)
                             orderby productType.ID descending
                             select productType);
            }
            if (pType > 0)
            {
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in _entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in _entities.ProductType
                        where mIdGroup.Contains(p.ID)
                        orderby p.ID descending
                        select p).Skip((page - 1) * pageSize)
                         .Take(pageSize).ToList();
            }
            //Loc theo childId
            if (childId > 0)
            {
                return (from productType in listGroup
                        where productType.ID == childId
                        orderby productType.ID descending
                        select productType).ToList();
            }
            //Loc theo ParentId
            if (parentId > 0)
            {
                return (from productType in listGroup
                        where productType.Parent == parentId
                        orderby productType.ID descending
                        select productType).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            //Loc theo RootId
            if (rootId > 0)
            {
                var listParent = (from productType in listGroup
                                  where productType.Parent == rootId
                                  orderby productType.ID descending
                                  select productType.ID).ToList();
                if (listParent.Count > 0)
                {
                    var listParentString = string.Join(",", listParent.ToArray());
                    listGroup = (from productType in listGroup.AsEnumerable()
                                 where productType.Parent > 0 && ((productType.Parent == rootId) || (listParentString.Contains(productType.Parent + ",")
                                       || listParentString.Contains("," + productType.Parent)))
                                 orderby productType.ID descending
                                 select productType);
                }
                else
                {
                    return new List<ProductType>();
                }

            }
            return listGroup.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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