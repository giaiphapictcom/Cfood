using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface ISizeRespository
    {

    }
    public  class SizeRespository: IBaseRespository<Size>, ISizeRespository
    {
       
        public SizeRespository()
        {
          
          
        }
        public Size Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from size in entities.Size
                        where size.Id == id
                        select size).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.Size
                                where size.Id == id
                                select size).FirstOrDefault();
                if (sizeItem != null)
                {
                    entities.Size.Remove(sizeItem);
                    entities.SaveChanges();
                    return  "ok";
                }
                return "not_exists";
            }
           
        }

        public string Update(Size data)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.Size
                                where size.Id == data.Id
                                select size).FirstOrDefault();
                if (sizeItem != null)
                {
                    sizeItem.Name = data.Name;
                    sizeItem.Description = data.Description;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Size data)
        {
            using (var entities = new V308CMSEntities())
            {
                var sizeItem = (from size in entities.Size
                                where size.Name == data.Name
                                select size).FirstOrDefault();
                if (sizeItem == null)
                {
                    entities.Size.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
        }
        public List<Size> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from size in entities.Size
                        orderby size.Id descending
                        select size).ToList();
            }
          
        }
    }

}
