using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Data
{
    public class SiteRepository
    {

        public SiteRepository()
        {
        }

        public string SiteConfig(string name,string site = Data.Helpers.Site.home)
        {
            using (var entities = new V308CMSEntities())
            {
                var siteconfig = entities.SiteConfig.Where(c => c.Name.ToLower() == name.ToLower());
                if (site == Data.Helpers.Site.home)
                {
                    siteconfig = siteconfig.Where(m => m.Site == site || m.Site == "" || m.Site == "1" || m.Site == string.Empty);
                }
                else {
                    siteconfig = siteconfig.Where(m => m.Site == site);
                }

                var config = siteconfig.FirstOrDefault();


                if (config != null)
                    return config.Content;
                else
                    return "";

            }


        }

        public string SiteConfig()
        {
            using (var entities = new V308CMSEntities())
            {
                string content = "";
                var config = (from p in entities.SiteConfig
                              select p).FirstOrDefault();
                if (config != null)
                    return config.Content;
                else return content;


            }

        }

    }
}