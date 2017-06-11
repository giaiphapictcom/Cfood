using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IProductSizeRespository
    {
        string DeleteByProductId(int productId);
        List<ProductSize> GetAllByProductId(int productId);
    }
    public class ProductSizeRespository: IBaseRespository<ProductSize>, IProductSizeRespository
    {
        private readonly V308CMSEntities _entities;

        public ProductSizeRespository(V308CMSEntities entities)
        {
            _entities = entities;

        }
        public ProductSize Find(int id)
        {
            return (from size in _entities.ProductSize
                    where size.Id == id
                    select size).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var sizeItem = (from size in _entities.ProductSize
                             where size.Id == id
                             select size).FirstOrDefault();
            if (sizeItem != null)
            {
                _entities.ProductSize.Remove(sizeItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductSize data)
        {
            var sizeItem = (from size in _entities.ProductSize
                             where size.Id == data.Id
                             select size).FirstOrDefault();
            if (sizeItem != null)
            {
                sizeItem.Size = data.Size;
                sizeItem.ProductId = data.ProductId;             
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductSize data)
        {
            var sizeItem = (from size in _entities.ProductSize
                             where size.Size == data.Size
                             && size.ProductId ==data.ProductId
                             select size).FirstOrDefault();
            if (sizeItem == null)
            {
                _entities.ProductSize.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<ProductSize> GetAll()
        {
            return (from size in _entities.ProductSize
                    orderby size.Id descending
                    select size).ToList();
        }

      
        public string DeleteByProductId(int productId)
        {
            var listSize = (from size in _entities.ProductSize
                             where size.ProductId == productId
                select size).ToList();
            if (listSize.Any())
            {
                foreach (var size in listSize)
                {
                    _entities.ProductSize.Remove(size);
                    _entities.SaveChanges();
                    return "ok";
                }
            }
            return "not_exists";
        }

        public List<ProductSize> GetAllByProductId(int productId)
        {
            return (from size in _entities.ProductSize
                    where size.ProductId == productId
                    orderby size.Id descending
                    select size).ToList();
        }
    }
}
