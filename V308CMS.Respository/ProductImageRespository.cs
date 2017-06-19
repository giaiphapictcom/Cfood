using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductImageRespository
    {
        List<ProductImage> GetByProductId(int productId);
        string Insert(int productId, string productName, string[] imageUrl, int number);
    }
    public class ProductImageRespository : IBaseRespository<ProductImage>, IProductImageRespository
    {
      
      
        public ProductImageRespository()
        {
           
        }
        public ProductImage Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from image in entities.ProductImage
                        where image.ID == id
                        select image
                ).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var imageItem = (from image in entities.ProductImage
                                 where image.ID == id
                                 select image).FirstOrDefault();
                if (imageItem != null)
                {
                    entities.ProductImage.Remove(imageItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Update(ProductImage data)
        {
            using (var entities = new V308CMSEntities())
            {
                var imageItem = (from image in entities.ProductImage
                                 where image.ID == data.ID
                                 select image).FirstOrDefault();
                if (imageItem != null)
                {
                    imageItem.ProductID = data.ProductID;
                    imageItem.Title = data.Title;
                    imageItem.Number = data.Number;
                    imageItem.Name = data.Name;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(ProductImage data)
        {
            using (var entities = new V308CMSEntities())
            {

                var imageItem = (from image in entities.ProductImage
                                 where image.ID == data.ID && image.Title == data.Title
                                 select image).FirstOrDefault();
                if (imageItem == null)
                {
                    entities.ProductImage.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }


        }

        public List<ProductImage> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from image in entities.ProductImage
                        orderby image.ID descending
                        select image
                 ).ToList();
            }
            
        }

        public List<ProductImage> GetByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from image in entities.ProductImage
                        where image.ProductID == productId
                        orderby image.ID descending
                        select image
                ).ToList();
            }
            
        }

        public string Insert(int productId, string productName, string[] listImage, int number =1)
        {
            foreach (var image in listImage)
            {
                if (!string.IsNullOrEmpty(image))
                {
                     Insert(new ProductImage
                     {
                        Name = image,
                        Number = number,
                        ProductID = productId,
                        Title = productName
                     });
                }
            }
            return "ok";
        }
    }
}
