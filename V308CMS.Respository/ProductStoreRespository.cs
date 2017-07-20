using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface IStoreRespository
    {
        string Insert(string name, string phone,
            string manager, string address, DateTime createdAt,
            DateTime updatedAt, byte state);
        string Update(int id, string name, string phone,
            string manager, string address, DateTime createdAt,
            DateTime updatedAt, byte state);

    }

    public class StoreRespository : IBaseRespository<Store>, IStoreRespository
    {
      

        public StoreRespository()
        {
           
        }

        public Store Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from store in entities.Store
                        where store.Id == id
                        select store).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var storeItem = (from store in entities.Store
                                 where store.Id == id
                                 select store).FirstOrDefault();
                if (storeItem != null)
                {
                    entities.Store.Remove(storeItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
          
        }

        public string Update(Store data)
        {
            using (var entities = new V308CMSEntities())
            {
                var storeItem = (from store in entities.Store
                                 where store.Id == data.Id
                                 select store).FirstOrDefault();
                if (storeItem != null)
                {
                    storeItem.Name = data.Name;
                    storeItem.Phone = data.Phone;
                    storeItem.Manager = data.Manager;
                    storeItem.Address = data.Address;
                    storeItem.UpdatedAt = data.UpdatedAt;
                    storeItem.State = data.State;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Store data)
        {
            using (var entities = new V308CMSEntities())
            {
                var storeItem = (from store in entities.Store
                                 where store.Name == data.Name
                                 select store).FirstOrDefault();
                if (storeItem == null)
                {
                    entities.Store.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

        public List<Store> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from store in entities.Store
                        orderby store.UpdatedAt descending
                        select store
               ).ToList();
            }
           
        }


        public string Insert(
            string name, string phone,
            string manager,
            string address, DateTime createdAt,
            DateTime updatedAt, byte state
            )
        {
            using (var entities = new V308CMSEntities())
            {
                var storeItem = (from store in entities.Store
                                 where store.Name == name
                                 select store).FirstOrDefault();
                if (storeItem == null)
                {
                    var store = new Store
                    {
                        Name = name,
                        Phone = phone,
                        Manager = manager,
                        Address = address,
                        CreatedAt = createdAt,
                        UpdatedAt = updatedAt,
                        State = state
                    };
                    entities.Store.Add(store);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }

        public string Update(int id, string name, string phone,
            string manager,
            string address, DateTime createdAt,
            DateTime updatedAt, byte state)
        {
            using (var entities = new V308CMSEntities())
            {
                var storeItem = (from store in entities.Store
                                 where store.Id == id
                                 select store).FirstOrDefault();
                if (storeItem != null)
                {
                    storeItem.Name = name;
                    storeItem.Phone = phone;
                    storeItem.Manager = manager;
                    storeItem.Address = address;
                    storeItem.CreatedAt = createdAt;
                    storeItem.UpdatedAt = updatedAt;
                    storeItem.State = state;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
            
        }
        public List<Store> GetAllByState(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from store in entities.Store
                                orderby store.CreatedAt descending
                                where store.State == 1
                                select store).ToList() :
                    (from store in entities.Store
                     orderby store.CreatedAt descending
                     select store).ToList();
            }
           
        }
    }
}
