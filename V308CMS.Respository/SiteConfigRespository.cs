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
       
        public SiteConfigRespository()
        {
           
        }
     
        public SiteConfig Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from config in entities.SiteConfig
                        where config.id == id
                        select config).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.id == id
                                  select config).FirstOrDefault();
                if (configItem != null)
                {
                    entities.SiteConfig.Remove(configItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
          
        }

        public string Update(SiteConfig data)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.id == data.id
                                  select config).FirstOrDefault();
                if (configItem != null)
                {
                    configItem.name = data.name;
                    configItem.content = data.content;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(SiteConfig data)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.name == data.name
                                  select config).FirstOrDefault();
                if (configItem == null)
                {
                    entities.SiteConfig.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }

        public List<SiteConfig> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from config in entities.SiteConfig

                        orderby config.id descending
                        select config
                ).ToList();
            }
            
        }
    }
}