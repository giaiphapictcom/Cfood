using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductSaleOffRespository
    {
        
    }

    public class ProductSaleOffRespository: IBaseRespository<ProductSaleOff>, IProductAttributeRespository
    {
        private readonly V308CMSEntities _entities;
        public ProductSaleOffRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public ProductSaleOff Find(int id)
        {
            return (from sale in _entities.ProductSaleOff
                    where sale.ID == id
                    select sale).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var saleItem = (from sale in _entities.ProductSaleOff
                            where sale.ID == id
                                 select sale).FirstOrDefault();
            if (saleItem != null)
            {
                _entities.ProductSaleOff.Remove(saleItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductSaleOff data)
        {
            var saleItem = (from sale in _entities.ProductSaleOff
                                 where sale.ID == data.ID
                                 select sale).FirstOrDefault();
            if (saleItem != null)
            {

                saleItem.ProductID = data.ProductID;
                saleItem.StartTime = data.StartTime;
                saleItem.EndTime = data.EndTime;               
                saleItem.Percent = data.Percent;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductSaleOff data)
        {
            _entities.ProductSaleOff.Add(data);
            _entities.SaveChanges();
            return "ok";
        }
        public List<ProductSaleOff> GetAllByProductId(int productId)
        {
            return (from sale in _entities.ProductSaleOff
                    where sale.ProductID == productId
                    orderby sale.ID descending
                    select sale).ToList();
        }
        public List<ProductSaleOff> GetAll()
        {
            return (from sale in _entities.ProductSaleOff
                    orderby sale.ID descending
                    select sale).ToList();
        }
    }
}
