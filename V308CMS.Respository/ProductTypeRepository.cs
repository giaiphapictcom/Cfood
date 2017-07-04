using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Respository
{
    public interface IProductTypeRepository
    {
       List<ProductType> GetList(int rootId = 0, int parentId =0,int childId =0);
       List<ProductType> GetAll(bool state = true);

        string Insert(string name, int parentId, string icon, string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status, bool isHome);

        string Update(int id,string name, int parentId, string icon, string description, string image,
            string imageBanner, int number,
            DateTime createdDate, bool status, bool isHome);

        List<ProductType> GetListByType(int level, int categoryId = 0);
    }


    public class ProductTypeRepository : IProductTypeRepository
    {
       

        public ProductTypeRepository( )
        {
           
        }

        public async Task<List<ProductType>> GetListHome()
        {
            using (var entities = new V308CMSEntities())
            {
                return await entities.ProductType
                     .Where(productType => productType.IsHome && productType.Status == true)
                     .OrderBy(productType => productType.Number)
                     .Select(productType => productType)
                     .ToListAsync();
            }
          
        }
        public List<ProductType> GetAllWeb()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from category in entities.ProductType
                        orderby category.Number ascending
                        select category
                ).ToList();
            }
            
        }

        public void GetListByParent(int level,ref List<ProductType> result,List<ProductType> allCategory,int categoryId=0)
        {
            if (result == null)
            {
                result = new List<ProductType>();
            }
            allCategory = level == 0
                ? allCategory
                    .Select(category => category)
                    .ToList()
                : allCategory
                    .Select(category => category)
                    .Where(cate => cate.Parent.HasValue && cate.Parent.Value == categoryId)
                    .ToList();

            if (allCategory.Count > 0)
            {
                foreach (var category in allCategory)
                {
                    result.Add(category);
                    if (category.Parent == categoryId)
                    {
                        GetListByParent(level, ref result, allCategory
                            .Where(cate => cate.Parent.HasValue && cate.Parent.Value == categoryId)
                            .Select(cate => category)
                            .ToList(), category.ID);
                        result.AddRange(result);
                    }
                }
            }
        }

        public List<ProductType> GetList(int rootId = 0, int parentId = 0, int childId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                //Tat ca danh muc
                var allCategory = (from category in entities.ProductType
                                   orderby category.Parent
                                   select category).ToList();
                if (rootId == 0 && parentId == 0 && childId == 0)
                {
                    return allCategory;
                }
                //Danh muc goc

                //Loc theo danh muc con
                var result = new List<ProductType>();


                if (childId > 0)
                {

                    GetListByParent(2, ref result, allCategory, childId);
                    return result.Distinct().ToList();
                }
                if (parentId > 0)
                {

                    GetListByParent(1, ref result, allCategory, parentId);
                    return result.Distinct().ToList();
                }
                if (rootId > 0)
                {

                    GetListByParent(0, ref result, allCategory, rootId);
                    return result.Distinct().ToList();
                }

                return (from category in result
                        orderby category.Date.Value descending
                        select category).ToList();

            }
                
        }

        public List<ProductType> GetAll(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from category in entities.ProductType
                                where category.Status == true
                                orderby category.Date.Value descending
                                select category).ToList() :
                   (from category in entities.ProductType
                    orderby category.Date.Value descending
                    select category).ToList();
            }
           
        }

        public string Insert(
            string name, 
            int parentId, 
            string icon, 
            string description,
            string image, 
            string imageBanner,
            int number, 
            DateTime createdDate, 
            bool status,
            bool isHome
            )
        {
            using (var entities = new V308CMSEntities())
            {
                var checkProductType = (from type in entities.ProductType
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
                        Status = status,
                        IsHome = isHome
                    };
                    entities.ProductType.Add(newProductType);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }
        public string ChangeState(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var productTypeItem = (
               from item in entities.ProductType.Include("ListProduct")
               where item.ID == id
               select item
              ).FirstOrDefault();
                if (productTypeItem != null)
                {
                    productTypeItem.Status = !productTypeItem.Status;
                    entities.SaveChanges();
                    if (productTypeItem.ListProduct != null && productTypeItem.ListProduct.Count > 0)
                    {
                        foreach (var product in productTypeItem.ListProduct)
                        {
                            product.Status = productTypeItem.Status;
                            entities.SaveChanges();
                        }
                    }
                    return "ok";
                }
                return "not_exists";
            }
            
        }


        public string Update(int id, string name, int parentId, string icon, string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status, bool isHome)
        {
            using (var entities = new V308CMSEntities())
            {
                var productType = (from type in entities.ProductType
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
                    productType.IsHome = isHome;
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";
            }
            

        }

        public List<ProductType> GetListByType(int level, int categoryId =0)
        {
            using (var entities = new V308CMSEntities())
            {
                if (level == (int)ProductCategoryLevelEnum.Root)
                {
                    return (from category in entities.ProductType
                            where category.Parent == 0
                            orderby category.Date.Value descending
                            select category).ToList();
                }
                if (level == (int)ProductCategoryLevelEnum.Parent ||
                    level == (int)ProductCategoryLevelEnum.Child)
                {
                    if (categoryId > 0)
                    {
                        return (from category in entities.ProductType
                                where category.Parent == categoryId
                                orderby category.Date.Value descending
                                select category).ToList();
                    }

                }
                return new List<ProductType>();
            }
           

        }


        public ProductType Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.ProductType
                        where item.ID == id
                        select item
                ).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var productTypeItem = (
                from item in entities.ProductType.Include("ListProduct")
                where item.ID == id
                select item
               ).FirstOrDefault();
                if (productTypeItem != null)
                {

                    if (productTypeItem.ListProduct != null && productTypeItem.ListProduct.Count > 0)
                    {
                        foreach (var product in productTypeItem.ListProduct)
                        {
                            entities.Product.Remove(product);
                            entities.SaveChanges();
                        }
                    }
                    entities.ProductType.Remove(productTypeItem);
                    entities.SaveChanges();
                    var listSubType = (
                         from item in entities.ProductType.Include("ListProduct")
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
                                    entities.Product.Remove(product);
                                    entities.SaveChanges();
                                }
                            }
                            entities.ProductType.Remove(subType);
                            entities.SaveChanges();
                        }
                    }
                    return "ok";
                }
                return "not_exists";
            }
            
        }
    }
}