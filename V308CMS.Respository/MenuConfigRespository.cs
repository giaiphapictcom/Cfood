﻿using System;
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

        private V308CMSEntities entities;
        public MenuConfigRespository(V308CMSEntities mEntities = null)
        {
            if (mEntities == null){
                mEntities = new V308CMSEntities();
            }
            this.entities = mEntities;
        }

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
                if (this.entities != null)
                {
                    this.entities.Dispose();
                    this.entities = null;
                }
            }
        }
        #endregion

        public MenuConfig Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.MenuConfig
                        where item.Id == id
                        select item
               ).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var menuConfig = (from item in entities.MenuConfig
                                  where item.Id == id
                                  select item
              ).FirstOrDefault();
                if (menuConfig != null)
                {
                    entities.MenuConfig.Remove(menuConfig);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Update(MenuConfig config)
        {
            using (var entities = new V308CMSEntities())
            {
                var menuConfig = (from item in entities.MenuConfig
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
                    menuConfig.Target = config.Target;
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";
            }
            
        }

        public string Insert(MenuConfig config)
        {
            using (var entities = new V308CMSEntities())
            {
                var menuConfig = (from item in entities.MenuConfig
                                  where item.Name == config.Name
                                  select item
                ).FirstOrDefault();
                if (menuConfig == null)
                {

                    try
                    {
                        entities.MenuConfig.Add(config);
                        entities.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Console.Write(dbEx);
                    }
                    return "ok";
                }
                return "exists";
            }
            
        }

        public string Insert
            (
                string name, string description, string code,
                string link, byte state, DateTime createdAt,
                DateTime updatedAt
            )
        {
            using (var entities = new V308CMSEntities())
            {
                var menuConfig = (from item in entities.MenuConfig
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
                    entities.MenuConfig.Add(newMenuConfig);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

        public List<MenuConfig> GetList(int page = 1, int pageSize = 10,string site="",byte status = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                var items = from m in entities.MenuConfig
                            where m.Site == site 

                        orderby m.Order ascending
                        select m;
                if (status > 0) { 
                     items = from m in entities.MenuConfig
                                 where m.Site == site && m.State == status

                        orderby m.Order ascending
                        select m;
                }
                
                return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            
        }
        public List<MenuConfig> GetAll(string site="")
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.MenuConfig
                        orderby item.Order
                        select item
               ).ToList();
            }
           
        }


        public string ChangeState(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var menuConfig = (from item in entities.MenuConfig
                                  where item.Id == id
                                  select item
                  ).FirstOrDefault();
                if (menuConfig != null)
                {
                    menuConfig.State = (byte)(menuConfig.State == 1 ? 0 : 1);
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";
            }
            
        }
    }
}