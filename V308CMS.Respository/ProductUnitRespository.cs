using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IUnitRespository
    {
        string Insert(
            string name, 
            DateTime createdAt, 
            DateTime updatedAt,
            byte state);
        string Update(
            int id, 
            string name,
            DateTime createdAt, 
            DateTime updatedAt,
            byte state);

    }

    public class UnitRespository : IBaseRespository<Unit>, IUnitRespository
    {
       

        public UnitRespository()
        {
           
        }

        public Unit Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from unit in entities.Unit
                        where unit.Id == id
                        select unit).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var unitItem = (from unit in entities.Unit
                                where unit.Id == id
                                select unit).FirstOrDefault();
                if (unitItem != null)
                {
                    entities.Unit.Remove(unitItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(Unit data)
        {
            using (var entities = new V308CMSEntities())
            {
                var unitItem = (from unit in entities.Unit
                                where unit.Id == data.Id
                                select unit).FirstOrDefault();
                if (unitItem != null)
                {
                    unitItem.Name = data.Name;
                    unitItem.CreatedAt = data.CreatedAt;
                    unitItem.UpdatedAt = data.UpdatedAt;
                    unitItem.State = data.State;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Unit data)
        {
            using (var entities = new V308CMSEntities())
            {
                var unitItem = (from unit in entities.Unit
                                where unit.Name == data.Name
                                select unit).FirstOrDefault();
                if (unitItem == null)
                {
                    entities.Unit.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
          
        }

        public List<Unit> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from unit in entities.Unit
                        orderby unit.UpdatedAt descending
                        select unit
               ).ToList();
            }
           
        }


        public string Insert(
            string name, DateTime createdAt, 
            DateTime updatedAt, byte state
            )
        {
            using (var entities = new V308CMSEntities())
            {
                var unitItem = (from unit in entities.Unit
                                where unit.Name == name
                                select unit).FirstOrDefault();
                if (unitItem == null)
                {
                    var unit = new Unit
                    {
                        Name = name,
                        CreatedAt = createdAt,
                        UpdatedAt = updatedAt,
                        State = state
                    };
                    entities.Unit.Add(unit);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }

        public string Update(int id, string name,
            DateTime createdAt, DateTime updatedAt,
            byte state)
        {
            using (var entities = new V308CMSEntities())
            {
                var unitItem = (from unit in entities.Unit
                                where unit.Id == id
                                select unit).FirstOrDefault();
                if (unitItem != null)
                {
                    unitItem.Name = name;
                    unitItem.CreatedAt = createdAt;
                    unitItem.UpdatedAt = updatedAt;
                    unitItem.State = state;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }
        public List<Unit> GetAllByState(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from unit in entities.Unit
                                orderby unit.CreatedAt descending
                                where unit.State == 1
                                select unit).ToList() :
                     (from unit in entities.Unit
                      orderby unit.CreatedAt descending
                      select unit).ToList();
            }
            
        }
    }
}
