using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
       
        public ProductManufacturerRespository()
        {
            
        }

        public async Task<List<ProductManufacturer>> GetAllAsync()
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from manufacturer in entities.ProductManufacturer
                       orderby  manufacturer.Date descending 
                        select manufacturer).ToListAsync();
            }
        } 
        public ProductManufacturer Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from manufacturer in entities.ProductManufacturer
                        where manufacturer.ID == id
                        select manufacturer).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var manufacturerItem = (from manufacturer in entities.ProductManufacturer
                                        where manufacturer.ID == id
                                        select manufacturer).FirstOrDefault();
                if (manufacturerItem != null)
                {
                    entities.ProductManufacturer.Remove(manufacturerItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }

           
        }

        public string Update(ProductManufacturer data)
        {
            using (var entities = new V308CMSEntities())
            {
                var manufacturerItem = (from manufacturer in entities.ProductManufacturer
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

            
        }

        public string Insert(ProductManufacturer data)
        {
            using (var entities = new V308CMSEntities())
            {
                var manufacturerItem = (from manufacturer in entities.ProductManufacturer
                                        where manufacturer.Name == data.Name
                                        select manufacturer).FirstOrDefault();
                if (manufacturerItem == null)
                {
                    entities.ProductManufacturer.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }

            
        }

        public List<ProductManufacturer> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from manufacturer in entities.ProductManufacturer
                        orderby manufacturer.Date.Value descending
                        select manufacturer
                   ).ToList();

            }
           
        }
        public string Insert( string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var manufacturerItem = (from manufacturer in entities.ProductManufacturer
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
                    entities.ProductManufacturer.Add(manufacturer);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }
            
        }

        public string Update(int id, string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var manufacturerItem = (from manufacturer in entities.ProductManufacturer
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }
        public List<ProductManufacturer> GetAllByState(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from manufacturer in entities.ProductManufacturer
                                orderby manufacturer.Date.Value descending
                                where manufacturer.Status == true
                                select manufacturer).ToList() :
                     (from manufacturer in entities.ProductManufacturer
                      orderby manufacturer.Date.Value descending
                      select manufacturer).ToList();
            }
            
        }
    }
}
