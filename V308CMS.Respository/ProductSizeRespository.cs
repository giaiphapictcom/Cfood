using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface IProductSizeRespository
    {
        string DeleteByProductId(int productId);
        string Insert(int productId, string[] listSize);
        List<ProductSize> GetAllByProductId(int productId);
    }
    public class ProductSizeRespository: IBaseRespository<ProductSize>, IProductSizeRespository
    {     

        public ProductSizeRespository()
        {
         

        }
        public ProductSize Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from size in entities.ProductSize
                        where size.Id == id
                        select size).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.ProductSize
                                where size.Id == id
                                select size).FirstOrDefault();
                if (sizeItem != null)
                {
                    entities.ProductSize.Remove(sizeItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(ProductSize data)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.ProductSize
                                where size.Id == data.Id
                                select size).FirstOrDefault();
                if (sizeItem != null)
                {
                    sizeItem.Size = data.Size;
                    sizeItem.ProductId = data.ProductId;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(int productId,string[] listSize)
        {
            foreach (var size in listSize)
            {
                if (!string.IsNullOrWhiteSpace(size))
                {
                    Insert(new ProductSize
                    {
                        Size = size,
                        ProductId = productId
                    });
                }
            }
            return "ok";
        }
        public string Insert(ProductSize data)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.ProductSize
                                where size.Size == data.Size
                                && size.ProductId == data.ProductId
                                select size).FirstOrDefault();
                if (sizeItem == null)
                {
                    entities.ProductSize.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }
          
        }

        public List<ProductSize> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from size in entities.ProductSize
                        orderby size.Id descending
                        select size).ToList();
            }
           
        }

      
        public string DeleteByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                var listSize = (from size in entities.ProductSize
                                where size.ProductId == productId
                                select size).ToList();
                if (listSize.Any())
                {
                    foreach (var size in listSize)
                    {
                        entities.ProductSize.Remove(size);
                        entities.SaveChanges();
                        return "ok";
                    }
                }
                return "not_exists";
            }
            
        }

        public List<ProductSize> GetAllByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from size in entities.ProductSize
                        where size.ProductId == productId
                        orderby size.Id descending
                        select size).ToList();
            }
           
        }
    }
}
