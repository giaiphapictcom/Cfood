using System;
using System.Linq;

namespace V308CMS.Data
{
    public interface IShoppingCartRepository
    {
        string Insert(string address, string email, string fullName, int itemCount, double totalAmount);
        int InsertOrUpdate(ProductOrder orderInfo);
    }

    public class CartRepository : IShoppingCartRepository
    {

        public CartRepository()
        {

        }


        public string Insert(string address, string email, string fullName, int itemCount = 0, double totalAmount = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                var order = new ProductOrder
                {
                    FullName = fullName,
                    Address = address,
                    Email = email,
                    Price = totalAmount,
                    Count = itemCount,
                    Date = DateTime.Now
                };
                entities.ProductOrder.Add(order);
                entities.SaveChanges();

                return order.ID.ToString();

            }



        }

        public int InsertOrUpdate(ProductOrder orderInfo)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkOrder = entities.ProductOrder.FirstOrDefault(order => order.FullName == orderInfo.FullName
                                    && order.Address == orderInfo.Address
                                    && order.AccountID == orderInfo.AccountID
                                    && order.Phone == orderInfo.Phone
                                    && order.Count == orderInfo.Count
                                    && order.Price == orderInfo.Price
                                    && order.Status == orderInfo.Status);
                if (checkOrder == null)
                {
                    entities.ProductOrder.Add(orderInfo);
                    entities.SaveChanges();
                    return orderInfo.ID;
                }
                else
                {
                    if (checkOrder.FullName != orderInfo.FullName)
                    {
                        checkOrder.FullName = orderInfo.FullName;
                    }
                    if (checkOrder.Address != orderInfo.Address)
                    {
                        checkOrder.Address = orderInfo.Address;
                    }
                    if (checkOrder.AccountID != orderInfo.AccountID)
                    {
                        checkOrder.AccountID = orderInfo.AccountID;
                    }
                    if (checkOrder.Phone != orderInfo.Phone)
                    {
                        checkOrder.Phone = orderInfo.Phone;
                    }
                    if (checkOrder.Count != orderInfo.Count)
                    {
                        checkOrder.Count = orderInfo.Count;
                    }
                    if (checkOrder.Price != orderInfo.Price)
                    {
                        checkOrder.Price = orderInfo.Price;
                    }
                    if (checkOrder.Status != orderInfo.Status)
                    {
                        checkOrder.Status = orderInfo.Status;
                    }
                    if (checkOrder.ShippingId != orderInfo.ShippingId)
                    {
                        checkOrder.ShippingId = orderInfo.ShippingId;
                    }
                    entities.SaveChanges();

                    return checkOrder.ID;
                }

            }
        }
    }
}
