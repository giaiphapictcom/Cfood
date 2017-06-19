using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public interface ISiteConfigRespository
    {
        List<SiteConfig> GetList();
        SiteConfig GetById(int id);
        string Delete(int id);
        string Update(int id, string name, string content);
        string Insert(string name, string content );
    }
    public class SiteConfigRespository: ISiteConfigRespository
    {
        private readonly V308CMSEntities _entities;
        public SiteConfigRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public List<SiteConfig> GetList()
        {
            return (from item in _entities.SiteConfig
                orderby item.id descending
                select item
                ).ToList();
        }

        public SiteConfig GetById(int id)
        {
            return (from config in _entities.SiteConfig
                where config.id == id
                select config).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var configItem = (from config in _entities.SiteConfig
                    where config.id == id
                    select config).FirstOrDefault();
            if (configItem != null)
            {
                _entities.SiteConfig.Remove(configItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(int id, string name, string content)
        {
            var configItem = (from config in _entities.SiteConfig
                              where config.id == id
                              select config).FirstOrDefault();
            if (configItem != null)
            {
                configItem.name = name;
                configItem.content = content;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(string name, string content)
        {
            var configItem = (from config in _entities.SiteConfig
                              where config.name == name
                              select config).FirstOrDefault();
            if (configItem == null)
            {
                _entities.SiteConfig.Add(new SiteConfig
                {
                    name = name,
                    content = content
                });
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }
    }
}