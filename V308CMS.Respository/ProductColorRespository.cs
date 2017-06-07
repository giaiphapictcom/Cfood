using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IProductColorRespository
    {
        string DeleteByProductId(int productId);
        List<ProductColor> GetAllByProductId(int productId);
    }
    public class ProductColorRespository: IBaseRespository<ProductColor>, IProductColorRespository
    {
        private readonly V308CMSEntities _entities;

        public ProductColorRespository(V308CMSEntities entities)
        {
            _entities = entities;

        }
        public ProductColor Find(int id)
        {
            return (from color in _entities.ProductColor
                    where color.Id == id
                    select color).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var colorItem = (from color in _entities.ProductColor
                             where color.Id == id
                             select color).FirstOrDefault();
            if (colorItem != null)
            {
                _entities.ProductColor.Remove(colorItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductColor data)
        {
            var colorItem = (from color in _entities.ProductColor
                             where color.Id == data.Id
                             select color).FirstOrDefault();
            if (colorItem != null)
            {
                colorItem.ColorCode = data.ColorCode;
                colorItem.ProductId = data.ProductId;             
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductColor data)
        {
            var colorItem = (from color in _entities.ProductColor
                             where color.ColorCode == data.ColorCode
                             && color.ProductId ==data.ProductId
                             select color).FirstOrDefault();
            if (colorItem == null)
            {
                _entities.ProductColor.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<ProductColor> GetAll()
        {
            return (from color in _entities.ProductColor
                    orderby color.Id descending
                    select color).ToList();
        }

      
        public string DeleteByProductId(int productId)
        {
            var listColor = (from color in _entities.ProductColor
                where color.ProductId == productId
                select color).ToList();
            if (listColor.Any())
            {
                foreach (var color in listColor)
                {
                    _entities.ProductColor.Remove(color);
                    _entities.SaveChanges();
                    return "ok";
                }
            }
            return "not_exists";
        }

        public List<ProductColor> GetAllByProductId(int productId)
        {
            return (from color in _entities.ProductColor
                    where  color.ProductId == productId
                    orderby color.Id descending
                    select color).ToList();
        }
    }
}
