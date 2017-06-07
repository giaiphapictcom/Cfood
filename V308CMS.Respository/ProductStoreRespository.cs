using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

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
        private readonly V308CMSEntities _entities;

        public StoreRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public Store Find(int id)
        {
            return (from store in _entities.Store
                    where store.Id == id
                select store).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var storeItem = (from store in _entities.Store
                             where store.Id == id
                select store).FirstOrDefault();
            if (storeItem != null)
            {
                _entities.Store.Remove(storeItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Store data)
        {
            var storeItem = (from store in _entities.Store
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
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Store data)
        {
            var storeItem = (from store in _entities.Store
                             where store.Name == data.Name
                select store).FirstOrDefault();
            if (storeItem == null)
            {
                _entities.Store.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Store> GetAll()
        {
            return (from store in _entities.Store
                    orderby store.UpdatedAt descending
                select store
                ).ToList();
        }


        public string Insert(
            string name, string phone,
            string manager,
            string address, DateTime createdAt,
            DateTime updatedAt, byte state
            )
        {
            var storeItem = (from store in _entities.Store
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
                _entities.Store.Add(store);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public string Update(int id, string name, string phone,
            string manager,
            string address, DateTime createdAt,
            DateTime updatedAt, byte state)
        {
            var storeItem = (from store in _entities.Store
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
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public List<Store> GetAllByState(bool state = true)
        {
            return state ? (from store in _entities.Store
                            orderby store.CreatedAt descending
                            where store.State == 1
                            select store).ToList() :
                     (from store in _entities.Store
                      orderby store.CreatedAt descending
                      select store).ToList();
        }
    }
}
