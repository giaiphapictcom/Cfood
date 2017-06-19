using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IProductColorRespository
    {
        string DeleteByProductId(int productId);
        string Insert(int productId, string[] listColor);
        List<ProductColor> GetAllByProductId(int productId);
    }
    public class ProductColorRespository: IBaseRespository<ProductColor>, IProductColorRespository
    {
       

        public ProductColorRespository( )
        {
          

        }
        public ProductColor Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.ProductColor
                        where color.Id == id
                        select color).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.ProductColor
                                 where color.Id == id
                                 select color).FirstOrDefault();
                if (colorItem != null)
                {
                    entities.ProductColor.Remove(colorItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(ProductColor data)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.ProductColor
                                 where color.Id == data.Id
                                 select color).FirstOrDefault();
                if (colorItem != null)
                {
                    colorItem.ColorCode = data.ColorCode;
                    colorItem.ProductId = data.ProductId;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(ProductColor data)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.ProductColor
                                 where color.ColorCode == data.ColorCode
                                 && color.ProductId == data.ProductId
                                 select color).FirstOrDefault();
                if (colorItem == null)
                {
                    entities.ProductColor.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }
            
        }

        public List<ProductColor> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.ProductColor
                        orderby color.Id descending
                        select color).ToList();
            }
           
        }

      
        public string DeleteByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                var listColor = (from color in entities.ProductColor
                                 where color.ProductId == productId
                                 select color).ToList();
                if (listColor.Any())
                {
                    foreach (var color in listColor)
                    {
                        entities.ProductColor.Remove(color);
                        entities.SaveChanges();
                        return "ok";
                    }
                }
                return "not_exists";
            }
          
        }

        public string Insert(int productId, string[] listColor)
        {
            foreach (var color in listColor)
            {
                if (!string.IsNullOrWhiteSpace(color))
                {
                    Insert(new ProductColor
                    {
                        ColorCode = color,
                        ProductId = productId
                    });

                }
            }
            return "ok";
        }

        public List<ProductColor> GetAllByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.ProductColor
                        where color.ProductId == productId
                        orderby color.Id descending
                        select color).ToList();
            }
          
        }
    }
}
