using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductOrderDetailRespository
    {
         List<productorder_detail> GetListOrderDetailByOrderId(int orderId);

    }
    public class ProductOrderDetailRespository: IProductOrderDetailRespository
    {
        public List<productorder_detail> GetListOrderDetailByOrderId(int orderId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from orderItem in entities.ProductOrderItem
                    orderby orderItem.ID descending
                    select orderItem
                    ).ToList();
            }
        }
    }
}
