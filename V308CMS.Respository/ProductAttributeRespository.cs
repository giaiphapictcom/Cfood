using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductAttributeRespository
    {
        string Insert(int productId, string[] listKey, string[] listValue);

    }
    public class ProductAttributeRespository: IBaseRespository<ProductAttribute>, IProductAttributeRespository
    {
       
        public ProductAttributeRespository()
        {
           
        }
        public ProductAttribute Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from attribute in entities.ProductAttribute
                        where attribute.ID == id
                        select attribute).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var attributeItem = (from attribute in entities.ProductAttribute
                                     where attribute.ID == id
                                     select attribute).FirstOrDefault();
                if (attributeItem != null)
                {
                    entities.ProductAttribute.Remove(attributeItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(ProductAttribute data)
        {
            using (var entities = new V308CMSEntities())
            {
                var attributeItem = (from attribute in entities.ProductAttribute
                                     where attribute.ID == data.ID
                                     select attribute).FirstOrDefault();
                if (attributeItem != null)
                {
                    attributeItem.CateAttributeID = data.CateAttributeID;
                    attributeItem.ProductID = data.ProductID;
                    attributeItem.Name = data.Name;
                    attributeItem.Value = data.Value;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(ProductAttribute data)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from attribute in entities.ProductAttribute
                                where attribute.Name == data.Name && attribute.ProductID == data.ProductID
                                select attribute).FirstOrDefault();
                if (sizeItem == null)
                {
                    entities.ProductAttribute.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }

            
        }

        public List<ProductAttribute> GetAllByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from attribute in entities.ProductAttribute
                        where attribute.ProductID == productId
                        orderby attribute.ID descending
                        select attribute).ToList();
            }
            
        }
        public List<ProductAttribute> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from attribute in entities.ProductAttribute
                        orderby attribute.ID descending
                        select attribute).ToList();
            }
            
        }

        public string Insert(int productId, string[] listKey, string[] listValue)
        {
            int i = 0;
            foreach (var attribute in listKey)
            {
                if (!string.IsNullOrEmpty(attribute))
                {
                    Insert(new ProductAttribute
                    {
                        Name = listKey[i],
                        Value = listValue[i],
                        ProductID = productId
                    });
                }
                i++;
            }
            return "ok";

        }
    }
}
