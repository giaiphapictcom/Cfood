using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface IProductDistributorRespository
    {
        string Insert(string name,string image, string detail, bool status,int order, DateTime createdDate);
        string Update(int id, string name, string image, string detail, bool status, int order, DateTime createdDate);

    }
    public  class ProductDistributorRespository : IBaseRespository<ProductDistributor>, IProductDistributorRespository
    {
       
        public ProductDistributorRespository()
        {
           
        }
        public ProductDistributor Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from distributor in entities.ProductDistributor
                        where distributor.ID == id
                        select distributor).FirstOrDefault();
            }
          
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var distributorItem = (from distributor in entities.ProductDistributor
                                       where distributor.ID == id
                                       select distributor).FirstOrDefault();
                if (distributorItem != null)
                {
                    entities.ProductDistributor.Remove(distributorItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Update(ProductDistributor data)
        {
            using (var entities = new V308CMSEntities())
            {
                var distributorItem = (from distributor in entities.ProductDistributor
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Insert(ProductDistributor data)
        {
            using (var entities = new V308CMSEntities())
            {
                var distributorItem = (from distributor in entities.ProductDistributor
                                       where distributor.Name == data.Name
                                       select distributor).FirstOrDefault();
                if (distributorItem == null)
                {
                    entities.ProductDistributor.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

        public List<ProductDistributor> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from distributor in entities.ProductDistributor
                        orderby distributor.Date.Value descending
                        select distributor
                 ).ToList();
            }
            
        }


        public string Insert( string name, string image, string detail, bool status, int order, DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var distributorItem = (from distributor in entities.ProductDistributor
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
                    entities.ProductDistributor.Add(distributor);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }

            
        }

        public string Update(
            int id, string name, 
            string image, string detail, 
            bool status, int order, 
            DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var distributorItem = (from distributor in entities.ProductDistributor
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }
        public List<ProductDistributor> GetAll(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from distributor in entities.ProductDistributor
                                orderby distributor.Date.Value descending
                                where distributor.Status == true
                                select distributor).ToList() :
                   (from distributor in entities.ProductDistributor
                    orderby distributor.Date.Value descending
                    select distributor).ToList();
            }
          
        }
    }
}
