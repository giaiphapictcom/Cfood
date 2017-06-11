using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductImageRespository
    {
        List<ProductImage> GetByProductId(int productId);
    }
    public class ProductImageRespository : IBaseRespository<ProductImage>, IProductImageRespository
    {
        private readonly V308CMSEntities _entities;
      
        public ProductImageRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public ProductImage Find(int id)
        {
            return (from image in _entities.ProductImage
                where image.ID == id
                select image
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var imageItem = (from image in _entities.ProductImage
                            where image.ID == id
                            select image).FirstOrDefault();
            if (imageItem != null)
            {
                _entities.ProductImage.Remove(imageItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductImage data)
        {
            var imageItem = (from image in _entities.ProductImage
                             where image.ID == data.ID
                             select image).FirstOrDefault();
            if (imageItem != null)
            {
                imageItem.ProductID = data.ProductID;
                imageItem.Title = data.Title;
                imageItem.Number = data.Number;
                imageItem.Name = data.Name;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductImage data)
        {
            var imageItem = (from image in _entities.ProductImage
                             where image.ID == data.ID && image.Title == data.Title
                             select image).FirstOrDefault();
            if (imageItem == null)
            {
                _entities.ProductImage.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";

        }

        public List<ProductImage> GetAll()
        {
            return (from image in _entities.ProductImage
                    orderby image.ID descending
                    select image
                 ).ToList();
        }

        public List<ProductImage> GetByProductId(int productId)
        {
            return (from image in _entities.ProductImage
                where image.ProductID == productId
                orderby  image.ID descending 
                select image
                ).ToList();
        }
    }
}
