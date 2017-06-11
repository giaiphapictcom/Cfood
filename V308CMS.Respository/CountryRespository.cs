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
        private readonly V308CMSEntities _entities;
        public CountryRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public Country Find(int id)
        {
            return (from country in _entities.Country
                    where country.Id == id
                    select country).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var countryItem = (from country in _entities.Country
                              where country.Id == id
                              select country).FirstOrDefault();
            if (countryItem != null)
            {
                _entities.Country.Remove(countryItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Country data)
        {
            var countryItem = (from country in _entities.Country
                              where country.Id == data.Id
                              select country).FirstOrDefault();
            if (countryItem != null)
            {
                countryItem.Name = data.Name;                
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Country data)
        {
            var countryItem = (from country in _entities.Country
                              where country.Name == data.Name
                              select country).FirstOrDefault();
            if (countryItem == null)
            {
                _entities.Country.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Country> GetList(int page = 1, int pageSize = 10)
        {
            return (from country in _entities.Country
                    orderby country.Id descending
                    select country
               ).ToList();
        }

        public List<Country> GetAll()
        {
            return (from country in _entities.Country
                orderby country.Id descending
                select country).ToList();
        }
    }
}
