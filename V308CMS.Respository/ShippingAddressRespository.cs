using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IShippingAddressRespository
    {
        List<ShippingAddress> GetListAddressByUserId(int userId);
        ShippingAddress Find(int id);

        ShippingAddress FistByEmail(string email);
        ShippingAddress FistByIpAddress(string ipAddress);
        string Update(ShippingAddress customerAddress);
        int InsertOrUpdate(ShippingAddress customerAddress);
    }
    public class ShippingAddressRespository: IShippingAddressRespository
    {
        public List<ShippingAddress> GetListAddressByUserId(int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.ShippingAddress
                    .Where(address => address.UserId == userId)
                    .OrderByDescending(address => address.Id)
                    .ToList();                 
            }
        }


        public ShippingAddress Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.ShippingAddress.FirstOrDefault(address => address.Id == id);
            }
        }

        public ShippingAddress FistByEmail(string email)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.ShippingAddress.FirstOrDefault(address => address.Email == email);
            }
        }

        public ShippingAddress FistByIpAddress(string ipAddress)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.ShippingAddress.FirstOrDefault(address => address.IpAddress == ipAddress);
            }
        }

        public string Update(ShippingAddress data)
        {
            using (var entities = new V308CMSEntities())
            {
                var addressUpdate = entities.ShippingAddress.FirstOrDefault(address => address.Id == data.Id);
                if (addressUpdate != null)
                {
                    addressUpdate.UserId = data.UserId;
                    addressUpdate.FullName = data.FullName;
                    addressUpdate.Phone = data.Phone;
                    addressUpdate.Region = data.Region;
                    addressUpdate.City = data.City;
                    addressUpdate.Ward = data.Ward;
                    addressUpdate.Address = data.Address;
                    addressUpdate.Default = data.Default;
                    addressUpdate.UpdatedAt = data.UpdatedAt;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
           
        }

        public int InsertOrUpdate(ShippingAddress shippingAddress)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkShipping = entities.ShippingAddress.FirstOrDefault(shipping =>
                    shipping.UserId == shippingAddress.UserId
                    && shipping.FullName == shippingAddress.FullName
                    && shipping.Phone == shippingAddress.Phone
                    && shipping.Region == shippingAddress.Region
                    && shipping.City == shippingAddress.City
                    && shipping.Ward == shippingAddress.Ward
                    && shipping.Address == shippingAddress.Address);
                if (checkShipping == null)
                {
                    entities.ShippingAddress.Add(shippingAddress);
                    entities.SaveChanges();
                    return shippingAddress.Id;
                }
                checkShipping.UserId = shippingAddress.UserId;
                checkShipping.FullName = shippingAddress.FullName;
                checkShipping.Phone = shippingAddress.Phone;
                checkShipping.Region = shippingAddress.Region;
                checkShipping.City = shippingAddress.City;
                checkShipping.Ward = shippingAddress.Ward;
                checkShipping.Address = shippingAddress.Address;
                entities.SaveChanges();
                return shippingAddress.Id;
            }
        }
    }
}
