using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface ICountryRespository
    {
        
    }
    public class CountryRespository: IBaseRespository<Country>, ICountryRespository
    {
       
        public CountryRespository()
        {
           
        }
        public Country Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from country in entities.Country
                        where country.Id == id
                        select country).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var countryItem = (from country in entities.Country
                                   where country.Id == id
                                   select country).FirstOrDefault();
                if (countryItem != null)
                {
                    entities.Country.Remove(countryItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(Country data)
        {
            using (var entities = new V308CMSEntities())
            {
                var countryItem = (from country in entities.Country
                                   where country.Id == data.Id
                                   select country).FirstOrDefault();
                if (countryItem != null)
                {
                    countryItem.Name = data.Name;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(Country data)
        {
            using (var entities = new V308CMSEntities())
            {
                var countryItem = (from country in entities.Country
                                   where country.Name == data.Name
                                   select country).FirstOrDefault();
                if (countryItem == null)
                {
                    entities.Country.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

        public List<Country> GetList(int page = 1, int pageSize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from country in entities.Country
                        orderby country.Id descending
                        select country
              ).ToList();
            }
           
        }

        public List<Country> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from country in entities.Country
                        orderby country.Id descending
                        select country).ToList();
            }
            
        }
    }
}
