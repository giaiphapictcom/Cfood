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
    public class MenuConfigRespository : Data.IBaseRespository<MenuConfig>, IMenuConfigRespository
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

        public List<MenuConfig> GetList(int page = 1, int pageSize = 10, string site = Data.Helpers.Site.home, byte status = 0)
        {
            using (var entities = new V308CMSEntities())
            {

                var menus = entities.MenuConfig.Select(m=>m);

                if (site == Data.Helpers.Site.home)
                {
                    menus = menus.Where(m=>m.Site==site || m.Site=="" || m.Site =="1" || m.Site == string.Empty);
                }
                else {
                    menus = menus.Where(m => m.Site == site);
                }

                if (status > 0)
                {
                    menus = menus.Where(m=>m.State==status);
                }
            
                
                return menus.OrderBy(m=>m.Order).Skip((page - 1) * pageSize).Take(pageSize).ToList();


            }

        }
        public List<MenuConfig> GetAll(string site = "")
        {
            using (var entities = new V308CMSEntities())
            {
                var menu = entities.MenuConfig.Select(m=>m);

                if (site == Data.Helpers.Site.home)
                {
                    menu = menu.Where(m => m.Site == site || m.Site == "" || m.Site == "1" || m.Site==null);
                }
                else {
                    menu = menu.Where(m => m.Site == site);
                }
                return menu.OrderBy(m => m.Order).ToList();
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