using System.Collections.Generic;
using V308CMS.Data;

namespace V308CMS
{
    public class SiteConfig
    {
        public static List<Data.SiteConfig> ConfigTable;
        public static void RegisterConfigs( )
        {            
            ConfigTable  = new SiteConfigRespository().LoadSiteConfig();

        }
    }
}