using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IProductDistributorRespository
    {
        string Insert(string name,string image, string detail, bool status,int order, DateTime createdDate);
        string Update(int id, string name, string image, string detail, bool status, int order, DateTime createdDate);

    }
    public  class ProductDistributorRespository : IBaseRespository<ProductDistributor>, IProductDistributorRespository
    {
        private readonly V308CMSEntities _entities;
        public ProductDistributorRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public ProductDistributor Find(int id)
        {
            return (from distributor in _entities.ProductDistributor
                    where distributor.ID == id
                    select distributor).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var distributorItem = (from distributor in _entities.ProductDistributor
                                    where distributor.ID == id
                              select distributor).FirstOrDefault();
            if (distributorItem != null)
            {
                _entities.ProductDistributor.Remove(distributorItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(ProductDistributor data)
        {
            var distributorItem = (from distributor in _entities.ProductDistributor
                                   where distributor.ID == data.ID
                              select distributor).FirstOrDefault();
            if (distributorItem != null)
            {
                distributorItem.Name = data.Name;
                distributorItem.Image = data.Image;
                distributorItem.Detail = data.Detail;
                distributorItem.Status = data.Status;
                distributorItem.Number = data.Number;
                distributorItem.Date = data.Date;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(ProductDistributor data)
        {
            var distributorItem = (from distributor in _entities.ProductDistributor
                                   where distributor.Name == data.Name
                              select distributor).FirstOrDefault();
            if (distributorItem == null)
            {
                _entities.ProductDistributor.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<ProductDistributor> GetAll()
        {
            return (from distributor in _entities.ProductDistributor
                    orderby distributor.Date.Value descending
                    select distributor
                 ).ToList();
        }


        public string Insert( string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            var distributorItem = (from distributor in _entities.ProductDistributor
                                    where distributor.Name == name
                                    select distributor).FirstOrDefault();
            if (distributorItem == null)
            {
                var distributor = new ProductDistributor
                {
                    Name = name,
                    Image = image,
                    Detail = detail,
                    Status = status,
                    Number = order,
                    Date = createdDate
                };
                _entities.ProductDistributor.Add(distributor);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public string Update(
            int id, string name, 
            string image, string detail, 
            bool status, int order, 
            DateTime createdDate)
        {
            var distributorItem = (from distributor in _entities.ProductDistributor
                                    where distributor.ID == id
                                    select distributor).FirstOrDefault();
            if (distributorItem != null)
            {
                distributorItem.Name = name;
                distributorItem.Image = image;
                distributorItem.Detail = detail;
                distributorItem.Status = status;
                distributorItem.Number = order;
                distributorItem.Date = createdDate;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public List<ProductDistributor> GetAll(bool state = true)
        {
            return state ? (from distributor in _entities.ProductDistributor
                            orderby distributor.Date.Value descending
                            where distributor.Status == true
                            select distributor).ToList() :
                     (from distributor in _entities.ProductDistributor
                      orderby distributor.Date.Value descending
                      select distributor).ToList();
        }
    }
}
