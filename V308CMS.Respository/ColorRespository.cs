using System;
using System.Collections.Generic;
using System.Linq;
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
        public Color Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.Color
                        where color.Id == id
                        select color).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.Color
                                 where color.Id == id
                                 select color).FirstOrDefault();
                if (colorItem != null)
                {
                    entities.Color.Remove(colorItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public string Update(Color data)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.Color
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Color data)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.Color
                                 where color.Name == data.Name
                                 select color).FirstOrDefault();
                if (colorItem == null)
                {
                    entities.Color.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }

        public List<Color> GetList(int page = 1, int pageSize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.Color
                        orderby color.UpdatedAt descending
                        select color
               ).ToList();
            }
           
        }


        public string Insert(
            string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state
            )
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.Color
                                 where color.Name == name
                                 select color).FirstOrDefault();
                if (colorItem == null)
                {
                    var color = new Color
                    {
                        Name = name,
                        Code = code,
                        Description = description,
                        CreatedAt = createdAt,
                        UpdatedAt = updatedAt,
                        State = state
                    };
                    entities.Color.Add(color);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

        public string Update(int id, string name, string code,
            string description, DateTime createdAt,
            DateTime updatedAt, byte state)
        {
            using (var entities = new V308CMSEntities())
            {
                var colorItem = (from color in entities.Color
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
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
            
        }
        public List<Color> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from color in entities.Color
                        orderby color.UpdatedAt descending
                        select color).ToList();
            }
           
        }
        public List<Color> GetAllByState(bool state = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return state ? (from color in entities.Color
                                orderby color.UpdatedAt descending
                                where color.State == 1
                                select color).ToList() :
                     (from color in entities.Color
                      orderby color.UpdatedAt descending
                      select color).ToList();
            }
            
        }
    }
}
