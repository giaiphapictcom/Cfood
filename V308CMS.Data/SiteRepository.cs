using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Data
{
    public class SiteRepository
    {

        public SiteRepository()
        {
        }



        public string SiteConfig(string name)
        {
            using (var entities = new V308CMSEntities())
            {
                string content = "";
                var config = (from p in entities.SiteConfig
                              where p.Name == name
                              select p).FirstOrDefault();
                if (config != null) return config.Content;
                else return content;

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