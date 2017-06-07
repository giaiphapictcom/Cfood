using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IUnitRespository
    {
        string Insert(string name, DateTime createdAt, 
            DateTime updatedAt,byte state);
        string Update(int id, string name,
            DateTime createdAt, DateTime updatedAt,
            byte state);

    }

    public class UnitRespository : IBaseRespository<Unit>, IUnitRespository
    {
        private readonly V308CMSEntities _entities;

        public UnitRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public Unit Find(int id)
        {
            return (from unit in _entities.Unit
                    where unit.Id == id
                select unit).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var unitItem = (from unit in _entities.Unit
                            where unit.Id == id
                select unit).FirstOrDefault();
            if (unitItem != null)
            {
                _entities.Unit.Remove(unitItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Unit data)
        {
            var unitItem = (from unit in _entities.Unit
                            where unit.Id == data.Id
                select unit).FirstOrDefault();
            if (unitItem != null)
            {
                unitItem.Name = data.Name;
                unitItem.CreatedAt = data.CreatedAt;
                unitItem.UpdatedAt = data.UpdatedAt;
                unitItem.State = data.State;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Unit data)
        {
            var unitItem = (from unit in _entities.Unit
                            where unit.Name == data.Name
                select unit).FirstOrDefault();
            if (unitItem == null)
            {
                _entities.Unit.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Unit> GetAll()
        {
            return (from unit in _entities.Unit
                    orderby unit.UpdatedAt descending
                select unit
                ).ToList();
        }


        public string Insert(
            string name, DateTime createdAt, 
            DateTime updatedAt, byte state
            )
        {
            var unitItem = (from unit in _entities.Unit
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
                _entities.Unit.Add(unit);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public string Update(int id, string name,
            DateTime createdAt, DateTime updatedAt,
            byte state)
        {
            var unitItem = (from unit in _entities.Unit
                where unit.Id == id
                select unit).FirstOrDefault();
            if (unitItem != null)
            {
                unitItem.Name = name;             
                unitItem.CreatedAt = createdAt;
                unitItem.UpdatedAt = updatedAt;
                unitItem.State = state;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public List<Unit> GetAllByState(bool state = true)
        {
            return state ? (from unit in _entities.Unit
                            orderby unit.CreatedAt descending
                            where unit.State == 1
                            select unit).ToList() :
                     (from unit in _entities.Unit
                      orderby unit.CreatedAt descending
                      select unit).ToList();
        }
    }
}
