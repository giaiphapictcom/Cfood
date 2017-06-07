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
        private readonly V308CMSEntities _entities;
        public SizeRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public Size Find(int id)
        {
            return (from size in _entities.Size
                    where size.Id == id
                    select size).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var sizeItem = (from size in _entities.Size
                               where size.Id == id
                               select size).FirstOrDefault();
            if (sizeItem != null)
            {
                _entities.Size.Remove(sizeItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Size data)
        {
            var sizeItem = (from size in _entities.Size
                            where size.Id == data.Id
                               select size).FirstOrDefault();
            if (sizeItem != null)
            {
                sizeItem.Name = data.Name;
                sizeItem.Description = data.Description;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Size data)
        {
            var sizeItem = (from size in _entities.Size
                               where size.Name == data.Name
                               select size).FirstOrDefault();
            if (sizeItem == null)
            {
                _entities.Size.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

       
        public List<Size> GetAll()
        {
            return (from size in _entities.Size
                orderby size.Id descending
                select size).ToList();
        }
    }

}
