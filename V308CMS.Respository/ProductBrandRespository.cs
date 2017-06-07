using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductBrandRespository
    {
        string Insert(string name,int categoryId, string image,byte status);
        string Update(int id, string name, int categoryId, string image, byte status);
    }

    public class ProductBrandRespository : IBaseRespository<Brand>, IProductBrandRespository
    {

        private readonly V308CMSEntities _entities;

        public ProductBrandRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }


        public Brand Find(int id)
        {
            return (from brand in _entities.Brand
                where brand.id == id
                select brand).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var brandItem = (from brand in _entities.Brand
                where brand.id == id
                select brand).FirstOrDefault();
            if (brandItem != null)
            {
                _entities.Brand.Remove(brandItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Brand data)
        {
            var brandItem = (from brand in _entities.Brand
                where brand.id == data.id
                select brand).FirstOrDefault();
            if (brandItem != null)
            {
                brandItem.category_default = data.category_default;
                brandItem.name = data.name;
                brandItem.image = data.image;
                brandItem.status = data.status;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Brand data)
        {
            var brandItem = (from brand in _entities.Brand
                where brand.name == data.name
                select brand).FirstOrDefault();
            if (brandItem == null)
            {
                _entities.Brand.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Brand> GetAll()
        {
            return (from brand in _entities.Brand
                    orderby brand.id descending
                    select brand
                 ).ToList();
        }


        public List<Brand> GetListWithProductType(int page = 1, int pageSize = 10)
        {
            return
                (from brand in _entities.Brand.Include("ProductType")
                    orderby brand.id descending
                    select brand
                    ).ToList();

        }

        public string Update(int id, string name, int categoryId, string image, byte status)
        {
            var brandItem = (from brand in _entities.Brand
                where brand.id == id
                select brand).FirstOrDefault();
            if (brandItem != null)
            {
                brandItem.name = name;
                brandItem.category_default = categoryId;
                brandItem.image = image;
                brandItem.status = status;                
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(string name, int categoryId, string image, byte status)
        {
            var brandItem = (from brand in _entities.Brand
                where brand.name == name
                select brand).FirstOrDefault();
            if (brandItem == null)
            {
                var brand = new Brand
                {
                    name = name,
                    category_default = categoryId,
                    image = image,
                    status = status
                };
                _entities.Brand.Add(brand);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Brand> GetAll(bool state = true)
        {
            return state ? (from brand in _entities.Brand
                            orderby brand.id descending
                            where brand.status == 1
                            select brand).ToList() :
                     (from brand in _entities.Brand
                      orderby brand.id descending
                      select brand).ToList();
        }
    }
}
