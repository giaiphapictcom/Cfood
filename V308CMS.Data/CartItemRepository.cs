using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace V308CMS.Data
{
    public interface IShoppingCartItemRepository
    {
        string Insert(int orderId, int itemId, string itemName, string itemPicture, double price, int quantity);
    }

    public class CartItemRepository : IShoppingCartItemRepository
    {
        public CartItemRepository()
        {

        }


        public string Insert(int orderId, int itemId, string itemName, string itemPicture, double price, int quantity)
        {
            
            using (var entities = new V308CMSEntities())
            {
                var checkOrderDetail = entities.ProductOrderItem.FirstOrDefault(order => order.order_id == orderId
                                    && order.item_id == itemId
                                    && order.item_name == itemName
                                    && order.item_picture == itemPicture
                                    && order.item_price == price
                                    && order.item_qty == quantity);

                var item = new productorder_detail
                {
                    order_id = orderId,
                    item_id = itemId,
                    item_name = itemName,
                    item_price = price,
                    item_qty = quantity,
                    item_picture = itemPicture
                };
                entities.ProductOrderItem.Add(item);
                entities.SaveChanges();
                return item.ID.ToString();

            }
        }

    }
}
