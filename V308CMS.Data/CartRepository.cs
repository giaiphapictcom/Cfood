using System;

namespace V308CMS.Data
{
    public interface ShoppingCartRepository
    {
        string Insert(string Address, string Email, string FullName, int item_count, double totalAmount);
    }

    public class CartRepository : ShoppingCartRepository
    {
        private V308CMSEntities entities;
        public CartRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }


        public string Insert(string Address, string email, string fullName, int item_count = 0, double totalAmount = 0)
        {
            try{
                var order = new ProductOrder
                {
                    FullName = fullName,
                    Address = Address,
                    Email = email,
                    Price = totalAmount,
                    Count = item_count,
                    Date = DateTime.Now
                };
                entities.ProductOrder.Add(order);
                entities.SaveChanges();

                return order.ID.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            
        }

    }
}
