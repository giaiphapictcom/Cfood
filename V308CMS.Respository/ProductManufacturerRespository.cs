using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductManufacturerRespository
    {
        string Insert(string name,string image, string detail, bool status,int order, DateTime createdDate);
        string Update(int id, string name, string image, string detail, bool status, int order, DateTime createdDate);

    }
    public  class ProductManufacturerRespository:IBaseRespository<ProductManufacturer>, IProductManufacturerRespository
    {
        private readonly V308CMSEntities _entities;
        public ProductManufacturerRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public ProductManufacturer Find(int id)
        {
            return (from manufacturer in _entities.ProductManufacturer
                    where manufacturer.ID == id
                    select manufacturer).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var manufacturerItem = (from manufacturer in _entities.ProductManufacturer
                              where manufacturer.ID == id
                              select manufacturer).FirstOrDefault();
            if (manufacturerItem != null)
            {
                _entities.ProductManufacturer.Remove(manufacturerItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductManufacturer data)
        {
            var manufacturerItem = (from manufacturer in _entities.ProductManufacturer
                              where manufacturer.ID == data.ID
                              select manufacturer).FirstOrDefault();
            if (manufacturerItem != null)
            {
                manufacturerItem.Name = data.Name;
                manufacturerItem.Image = data.Image;
                manufacturerItem.Detail = data.Detail;
                manufacturerItem.Status = data.Status;
                manufacturerItem.Number = data.Number;
                manufacturerItem.Date = data.Date;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductManufacturer data)
        {
            var manufacturerItem = (from manufacturer in _entities.ProductManufacturer
                              where manufacturer.Name == data.Name
                              select manufacturer).FirstOrDefault();
            if (manufacturerItem == null)
            {
                _entities.ProductManufacturer.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<ProductManufacturer> GetAll()
        {
            return (from manufacturer in _entities.ProductManufacturer
                    orderby manufacturer.Date.Value descending
                    select manufacturer
                 ).ToList();
        }


        public string Insert( string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            var manufacturerItem = (from manufacturer in _entities.ProductManufacturer
                                    where manufacturer.Name == name
                                    select manufacturer).FirstOrDefault();
            if (manufacturerItem == null)
            {
                var manufacturer = new ProductManufacturer
                {
                    Name = name,
                    Image = image,
                    Detail = detail,
                    Status = status,
                    Number = order,
                    Date = createdDate
                };
                _entities.ProductManufacturer.Add(manufacturer);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public string Update(int id, string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            var manufacturerItem = (from manufacturer in _entities.ProductManufacturer
                                    where manufacturer.ID == id
                                    select manufacturer).FirstOrDefault();
            if (manufacturerItem != null)
            {
                manufacturerItem.Name = name;
                manufacturerItem.Image = image;
                manufacturerItem.Detail = detail;
                manufacturerItem.Status = status;
                manufacturerItem.Number = order;
                manufacturerItem.Date = createdDate;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public List<ProductManufacturer> GetAllByState(bool state = true)
        {
            return state ? (from manufacturer in _entities.ProductManufacturer
                            orderby manufacturer.Date.Value descending
                            where manufacturer.Status == true
                            select manufacturer).ToList() :
                     (from manufacturer in _entities.ProductManufacturer
                      orderby manufacturer.Date.Value descending
                      select manufacturer).ToList();
        }
    }
}
