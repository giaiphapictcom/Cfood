using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductSaleOffRespository
    {
        
    }

    public class ProductSaleOffRespository: IBaseRespository<ProductSaleOff>, IProductSaleOffRespository
    {
        
        public ProductSaleOffRespository()
        {
           
        }
        public ProductSaleOff Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from sale in entities.ProductSaleOff
                        where sale.ID == id
                        select sale).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var saleItem = (from sale in entities.ProductSaleOff
                                where sale.ID == id
                                select sale).FirstOrDefault();
                if (saleItem != null)
                {
                    entities.ProductSaleOff.Remove(saleItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(ProductSaleOff data)
        {
            using (var entities = new V308CMSEntities())
            {
                var saleItem = (from sale in entities.ProductSaleOff
                                where sale.ID == data.ID
                                select sale).FirstOrDefault();
                if (saleItem != null)
                {

                    saleItem.ProductID = data.ProductID;
                    saleItem.StartTime = data.StartTime;
                    saleItem.EndTime = data.EndTime;
                    saleItem.Percent = data.Percent;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(ProductSaleOff data)
        {
            using (var entities = new V308CMSEntities())
            {
                entities.ProductSaleOff.Add(data);
                entities.SaveChanges();
                return "ok";
            }
            
        }
        public List<ProductSaleOff> GetAllByProductId(int productId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from sale in entities.ProductSaleOff
                        where sale.ProductID == productId
                        orderby sale.ID descending
                        select sale).ToList();
            }
            
        }
        public List<ProductSaleOff> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from sale in entities.ProductSaleOff
                        orderby sale.ID descending
                        select sale).ToList();
            }
         
        }
    }
}
