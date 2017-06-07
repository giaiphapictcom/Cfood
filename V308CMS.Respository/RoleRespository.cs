using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IRoleRepository
    {
        List<Role> GetList();
    }
    public class RoleRespository: IBaseRespository<Role>, IRoleRepository
    {
        private readonly V308CMSEntities _entities;

        public RoleRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public Role Find(int id)
        {
            return (from role in _entities.Role
                    where role.Id == id
                    select role
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var roleDelete = (from role in _entities.Role
                where role.Id == id
                select role
                ).FirstOrDefault();
            if (roleDelete != null)
            {
                _entities.Role.Remove(roleDelete);
                return "ok";
            }
            return "not_exists";

        }

        public string Update(Role data)
        {
            var roleUpdate = (from role in _entities.Role
                              where role.Id == data.Id
                              select role
                ).FirstOrDefault();
            if (roleUpdate != null)
            {
                roleUpdate.Name = data.Name;
                roleUpdate.Description = data.Description;
                roleUpdate.Status = data.Status;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Role data)
        {
            var checkRole = (from role in _entities.Role
                where role.Name == data.Name
                select role
                ).FirstOrDefault();
            if (checkRole == null)
            {
                _entities.Role.Add(data);
                return "ok";

            }
            return "exists";
        }

        public List<Role> GetAll()
        {
            return (from role in _entities.Role
                where role.Status == 1
                select role
                ).ToList();
        }

        public List<Role> GetList()
        {
            return (from role in _entities.Role
                    orderby role.Id descending                 
                    select role
                 ).ToList();
        }
    }
}
