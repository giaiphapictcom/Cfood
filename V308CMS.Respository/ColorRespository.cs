using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IColorRespository
    {        

        string Insert(
            string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state
            );

        string Update(int id, string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state);

    }

    public class ColorRespository : IBaseRespository<Color>, IColorRespository
    {
        private readonly V308CMSEntities _entities;

        public ColorRespository(V308CMSEntities entities)
        {
            this._entities = entities;
        }

        public Color Find(int id)
        {
            return (from color in _entities.Color
                    where color.Id == id
                    select color).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var colorItem = (from color in _entities.Color
                             where color.Id == id
                            select color).FirstOrDefault();
            if (colorItem != null)
            {
                _entities.Color.Remove(colorItem);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Color data)
        {
            var colorItem = (from color in _entities.Color
                             where color.Id == data.Id
                            select color).FirstOrDefault();
            if (colorItem != null)
            {
                colorItem.Name = data.Name;
                colorItem.Code = data.Code;
                colorItem.Description = data.Description;
                colorItem.CreatedAt = data.CreatedAt;
                colorItem.UpdatedAt = data.UpdatedAt;
                colorItem.State = data.State;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Color data)
        {
            var colorItem = (from color in _entities.Color
                             where color.Name == data.Name
                            select color).FirstOrDefault();
            if (colorItem == null)
            {
                _entities.Color.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<Color> GetList(int page = 1, int pageSize = 10)
        {
            return (from color in _entities.Color
                    orderby color.UpdatedAt descending
                    select color
                ).ToList();
        }


        public string Insert(
            string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state
            )
        {
            var colorItem = (from color in _entities.Color
                             where color.Name == name
                            select color).FirstOrDefault();
            if (colorItem == null)
            {
                var color = new Color
                {
                    Name = name,
                    Code = code,
                    Description =  description,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt,
                    State = state
                };
                _entities.Color.Add(color);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public string Update(int id, string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state)
        {
            var colorItem = (from color in _entities.Color
                             where color.Id == id
                            select color).FirstOrDefault();
            if (colorItem != null)
            {
                colorItem.Name = name;
                colorItem.Code = code;
                colorItem.Description = description;
                colorItem.CreatedAt = createdAt;
                colorItem.UpdatedAt = updatedAt;
                colorItem.State = state;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public List<Color> GetAll()
        {
            return (from color in _entities.Color
                orderby color.UpdatedAt descending
                select color).ToList();
        }
        public List<Color> GetAllByState(bool state = true)
        {
            return state ? (from color in _entities.Color
                            orderby color.UpdatedAt descending
                            where color.State == 1
                            select color).ToList() :
                     (from color in _entities.Color
                      orderby color.UpdatedAt descending
                      select color).ToList();
        }
    }
}
