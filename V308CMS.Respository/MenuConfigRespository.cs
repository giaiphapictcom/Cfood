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
      

        public MenuConfigRespository()
        {
            
        }
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

                    entities.MenuConfig.Add(config);
                    entities.SaveChanges();
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

        public List<MenuConfig> GetList(int page = 1, int pageSize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.MenuConfig
                        orderby item.CreatedAt descending
                        select item
                ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            
        }
        public List<MenuConfig> GetAll()
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