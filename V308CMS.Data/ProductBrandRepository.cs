using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public interface IProductBrandRepository
    {
        
    }
    public class ProductBrandRepository: IProductBrandRepository
    {
        private V308CMSEntities entities;

        public ProductBrandRepository(V308CMSEntities mEntities)
        {
            entities = mEntities;
        }

     
    }
}