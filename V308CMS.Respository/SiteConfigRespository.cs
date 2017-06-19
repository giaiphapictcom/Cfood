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
        private V308CMSEntities _entities;
        public SiteConfigRespository(V308CMSEntities entities)
        {
           this._entities = entities;
        }
     
        public SiteConfig Find(int id)
        {

            return (from config in _entities.SiteConfig
                where config.id == id
                select config).FirstOrDefault();

            using (var entities = new V308CMSEntities())
            {
                return (from config in entities.SiteConfig
                        where config.id == id
                        select config).FirstOrDefault();
            }
            

        }

        public string Delete(int id)
        {

            //var configItem = (from config in _entities.SiteConfig
            //        where config.id == id
            //        select config).FirstOrDefault();
            //if (configItem != null)

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