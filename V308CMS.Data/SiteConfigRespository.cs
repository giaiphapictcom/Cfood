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
        string Insert(string name, string content);
    }
    public class SiteConfigRespository : ISiteConfigRespository
    {

        public SiteConfigRespository()
        {

        }
        public List<SiteConfig> GetList()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.SiteConfig
                        orderby item.Id descending
                        select item
               ).ToList();
            }

        }

        public SiteConfig GetById(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from config in entities.SiteConfig
                        where config.Id == id
                        select config).FirstOrDefault();
            }

        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Id == id
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

        public string Update(int id, string name, string content)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Id == id
                                  select config).FirstOrDefault();
                if (configItem != null)
                {
                    configItem.Name = name;
                    configItem.Content = content;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }

        public string Insert(string name, string content)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Name == name
                                  select config).FirstOrDefault();
                if (configItem == null)
                {
                    entities.SiteConfig.Add(new SiteConfig
                    {
                        Name = name,
                        Content = content
                    });
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }

        }
        public List<SiteConfig> LoadSiteConfig()
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.SiteConfig.ToList();

            }

        }

        public string ReadSiteConfig(List<SiteConfig> siteConfigs, string config, string defaultValue = "")
        {
            var siteConfig = siteConfigs.FirstOrDefault(item => item.Name == config);
            return siteConfig != null ? siteConfig.Content : defaultValue;

        }
    }
}