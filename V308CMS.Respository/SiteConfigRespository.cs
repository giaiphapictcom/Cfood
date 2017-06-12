using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface ISiteConfigRespository
    {
       
    }
    public class SiteConfigRespository: IBaseRespository<SiteConfig>, ISiteConfigRespository
    {
        private readonly V308CMSEntities _entities;
        public SiteConfigRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
     
        public SiteConfig Find(int id)
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

        public string Update(SiteConfig data)
        {
            var configItem = (from config in _entities.SiteConfig
                              where config.id == data.id
                              select config).FirstOrDefault();
            if (configItem != null)
            {
                configItem.name = data.name;
                configItem.content = data.content;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(SiteConfig data)
        {
            var configItem = (from config in _entities.SiteConfig
                              where config.name == data.name
                              select config).FirstOrDefault();
            if (configItem == null)
            {
                _entities.SiteConfig.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<SiteConfig> GetAll()
        {
            return (from config in _entities.SiteConfig
                    
                    orderby config.id descending
                    select config
                ).ToList();
        }
    }
}