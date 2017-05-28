using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            string keyword = "", int pType=0, 
            string pLevel="",int rootId = 0, int parentId = 0,
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
                return  (from p in _entities.ProductType
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
                        where productType.Parent >0 && ((productType.Parent == rootId) || (listParentString.Contains(productType.Parent + ",")
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
}