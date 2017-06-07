using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductAttributeRespository
    {

    }
    public class ProductAttributeRespository: IBaseRespository<ProductAttribute>, IProductAttributeRespository
    {
        private readonly V308CMSEntities _entities;
        public ProductAttributeRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public ProductAttribute Find(int id)
        {
            return (from attribute in _entities.ProductAttribute
                    where attribute.ID == id
                    select attribute).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var attributeItem = (from attribute in _entities.ProductAttribute
                            where attribute.ID == id
                            select attribute).FirstOrDefault();
            if (attributeItem != null)
            {
                _entities.ProductAttribute.Remove(attributeItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductAttribute data)
        {
            var attributeItem = (from attribute in _entities.ProductAttribute
                            where attribute.ID == data.ID
                            select attribute).FirstOrDefault();
            if (attributeItem != null)
            {
                attributeItem.CateAttributeID = data.CateAttributeID;
                attributeItem.ProductID = data.ProductID;
                attributeItem.Name = data.Name;
                attributeItem.Value = data.Value;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductAttribute data)
        {
            var sizeItem = (from attribute in _entities.ProductAttribute
                            where attribute.Name == data.Name && attribute.ProductID  == data.ProductID
                            select attribute).FirstOrDefault();
            if (sizeItem == null)
            {
                _entities.ProductAttribute.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<ProductAttribute> GetAllByProductId(int productId)
        {
            return (from attribute in _entities.ProductAttribute
                    where  attribute.ProductID == productId
                    orderby attribute.ID descending
                    select attribute).ToList();
        }
        public List<ProductAttribute> GetAll()
        {
            return (from attribute in _entities.ProductAttribute
                    orderby attribute.ID descending
                    select attribute).ToList();
        }

    }
}
