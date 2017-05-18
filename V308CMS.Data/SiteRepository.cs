using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Data
{
    public class SiteRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public SiteRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public SiteRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }

        #endregion

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

        public List<SiteConfig> LoadSiteConfig(string[] configs )
        {
            List <SiteConfig> listSiteConfig = new List<SiteConfig>();
            if (configs != null && configs.Length>0)
            {
                var configString = string.Join(";", configs);
                listSiteConfig = entities.SiteConfig.Where(item => configString.Contains(item.name)).ToList();
               
            }
            return listSiteConfig;
        }

        public string ReadSiteConfig(List<SiteConfig> siteConfigs, string config,  string defaultValue ="")
        {
            var siteConfig = siteConfigs.FirstOrDefault(item => item.name == config);
            return siteConfig != null ? siteConfig.content : defaultValue;

        }

        public string SiteConfig(string name)
        {
            string content = "";
            var config = (from p in entities.SiteConfig
                          where p.name == name
                          select p).FirstOrDefault();
            if (config != null) return config.content;
            else return content;

        }

        public string SiteConfig()
        {
            string content = "";
            var config = (from p in entities.SiteConfig
                          select p).FirstOrDefault();
            if (config != null) 
                return config.content;
            else return content;

        }

    }
}