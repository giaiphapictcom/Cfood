using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IMenuConfigRespository
    {
        string ChangeState(int id);
    }
    public class MenuConfigRespository: Data.IBaseRespository<MenuConfig>, IMenuConfigRespository
    {
        private V308CMSEntities _entities;
        #region["Vung cac thao tac Dispose"]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._entities != null)
                {
                    this._entities.Dispose();
                    this._entities = null;
                }
            }
        }
        #endregion

        public MenuConfigRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public MenuConfig Find(int id)
        {
            return (from item in _entities.MenuConfig
                    where item.Id == id
                    select item
               ).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var menuConfig = (from item in _entities.MenuConfig
                               where item.Id == id
                               select item
               ).FirstOrDefault();
            if (menuConfig != null)
            {
                _entities.MenuConfig.Remove(menuConfig);
                return "ok";
            }
            return "not_exists";
        }

        public string Update(MenuConfig config)
        {
            var menuConfig = (from item in _entities.MenuConfig
                              where item.Id == config.Id
                              select item
                ).FirstOrDefault();
            if (menuConfig != null)
            {
              
                menuConfig.Name = config.Name;
                menuConfig.Description = config.Description;
                menuConfig.Code = config.Code;
                menuConfig.Link = config.Link;
                menuConfig.State = config.State;
                menuConfig.CreatedAt = config.CreatedAt;
                menuConfig.UpdatedAt = config.UpdatedAt;
                menuConfig.Order = config.Order;

                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";
        }

        public string Insert(MenuConfig config)
        {
            var menuConfig = (from item in _entities.MenuConfig
                              where item.Name == config.Name
                              select item
                ).FirstOrDefault();
            if (menuConfig == null) { 
          
                _entities.MenuConfig.Add(config);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

       

        public string Insert
            (
                string name, string description, string code,
                string link, byte state, DateTime createdAt,
                DateTime updatedAt
            )
        {
            var menuConfig = (from item in _entities.MenuConfig
                               where item.Name == name
                               select item
               ).FirstOrDefault();
            if (menuConfig == null)
            {
                var newMenuConfig = new MenuConfig
                {
                    Name = name,
                    Description = description,
                    Code = code,
                    Link = link,
                    State = state,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt
                };
                _entities.MenuConfig.Add(newMenuConfig);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<MenuConfig> GetList(int page = 1, int pageSize = 10)
        {
            return (from item in _entities.MenuConfig
                    orderby item.CreatedAt descending
                    select item
                ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<MenuConfig> GetAll()
        {
            return (from item in _entities.MenuConfig
                orderby item.Order
                select item
                ).ToList();
        }


        public string ChangeState(int id)
        {
            var menuConfig = (from item in _entities.MenuConfig
                               where item.Id == id
                               select item
                  ).FirstOrDefault();
            if (menuConfig != null)
            {
                menuConfig.State = (byte)(menuConfig.State == 1 ? 0 : 1);
                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";
        }
    }
}