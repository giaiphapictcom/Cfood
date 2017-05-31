using System;

namespace V308CMS.Data
{
    public interface ShoppingCartItemRepository
    {
        string Insert(int order_id, int item_id, string item_name, double price, int quantity);
    }

    public class CartItemRepository : ShoppingCartItemRepository
    {
        private V308CMSEntities entities;
        public CartItemRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }


        public string Insert(int OrderID, int ItemID, string ItemName, double price, int quantity)
        {
            try
            {
                var item = new productorder_detail
                {
                    order_id = OrderID,
                    item_id = ItemID,
                    item_name = ItemName,
                    item_price = price,
                    item_qty = quantity
                };
                entities.ProductOrderItem.Add(item);
                entities.SaveChanges();

                return item.ID.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }

    }
}
