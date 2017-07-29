using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using V308CMS.Data.Enum;

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

        List<ProductType> GetList(int rootId = 0, int parentId = 0, int childId = 0);

        List<ProductType> GetAll(bool state = true);

        string Insert(string name, int parentId, string icon, string description, string image, string imageBanner,
            int number, DateTime createdDate, bool status, bool isHome);

        string Update(int id, string name, int parentId, string icon, string description, string image,
            string imageBanner, int number,
            DateTime createdDate, bool status, bool isHome);

        List<ProductType> GetListByType(int level, int categoryId = 0);
        List<ProductType> GetListIn(int[] listId);

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

        public ProductTypeRepository()
        {

        }

        public void IncrementView(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var productType = entities.ProductType.FirstOrDefault(product => product.ID == id);
                if (productType != null)
                {
                    productType.TotalView += 1;
                }
                entities.SaveChanges();
            }
        }

        public List<ProductType> GetListRoot()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.ProductType
                        where item.Parent == 0
                        orderby item.ID descending
                        select item
                 ).ToList();

            }

        }
        public List<ProductType> GetListParent(int rootId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                return rootId == 0 ?
                 new List<ProductType>() :
                (from item in entities.ProductType
                 where item.Parent == rootId
                 orderby item.ID descending
                 select item
                ).ToList();
            }


        }
        public List<ProductType> GetList(
            string keyword = "", int pType = 0,
            string pLevel = "", int rootId = 0, int parentId = 0,
            int childId = 0, int page = 1, int pageSize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                var listGroup = (from productType in entities.ProductType.AsEnumerable()
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
                    var mIdGroup = (from p in entities.ProductType
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.ProductType
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

        }


        public async Task<List<ProductType>> GetAllWeb()
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from category in entities.ProductType
                              where  category.Status == true
                        orderby category.Number ascending
                        select category
                ).ToListAsync();
            }

        }

        private static void GetCategoryParent(ref List<ProductType> result, List<ProductType> allCategory, int parentId)
        {
            var categoryParent = allCategory.FirstOrDefault(category => category.ID == parentId);
            if (categoryParent!= null &&  categoryParent.Parent> 0)
            {
                result.Add(categoryParent);
                GetCategoryParent(ref result, allCategory, categoryParent.Parent ?? 0);
            }
        }

        public List<ProductType> GetListCategoryPath(int categoryId)
        {
            using (var entities = new V308CMSEntities())

            {                                    
                var result = new List<ProductType>();
                GetCategoryParent(ref result, entities.ProductType
                       .Where(category => category.Status == true)
                       .OrderBy(category => category.Number)
                       .ToList(), categoryId);
                return result;
            }

        }

       
        public List<ProductType> GetAllByCategoryId(int categoryId)
        {
            using (var entities = new V308CMSEntities())

            {               
                var listProductType = entities.ProductType
                        .Where(category => category.Status == true)
                        .OrderBy(category => category.Number)
                        .ToList();
                var result = new List<ProductType>();
                GetListByParent(ref result, listProductType, categoryId);
                return result;
            }
        }

        public void GetListByParent(ref  List<ProductType> result, List<ProductType> allCategory,
            int categoryId = 0)
        {
            var listSub = allCategory
                     .Select(category => category)
                     .Where(cate => cate.Parent.HasValue && cate.Parent.Value == categoryId)
                     .ToList();

            foreach (var sub in listSub)
            {
                result.Add(sub);
                if (sub.Parent == categoryId)
                {
                    GetListByParent(ref result, allCategory, sub.ID);
                }
            }

        }

        public void GetListByParent(int level, ref List<ProductType> result, List<ProductType> allCategory, int categoryId = 0)
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
                        level += 1;
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

        public List<ProductType> GetListByType(int level, int categoryId = 0)
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

        public List<ProductType> GetListIn(int[] listId)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.ProductType.SqlQuery(
                                             "SELECT * FROM ProductType " +
                                             "WHERE ID IN (" + string.Join(",", listId) + ")").ToList();
            }

        }

        public List<ProductType>  GetListHomeAsync()
        {
            using (var entities = new V308CMSEntities())
            {
                return  entities.ProductType
                    .Where(productType => productType.IsHome && productType.Status == true).ToList();
            }
        }

        public async Task<List<ProductType>> GetListHot()
        {
            using (var entities = new V308CMSEntities())
            {
                return await entities.ProductType
                    .Where(productType => productType.Parent ==0 && productType.Status == true).ToListAsync();
            }
        }

        public async  Task<List<ProductType>> GetListSubByParentIdAsync(int parentId, int limit)
        {
            using (var entities = new V308CMSEntities())
            {
               return  await(from subCategory in entities.ProductType
                                            where subCategory.Parent == parentId && subCategory.Status == true
                                            orderby subCategory.Number
                                            select subCategory
                       ).Take(limit).ToListAsync();
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