using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IRoleRepository
    {
        List<Role> GetList();
        int InsertAndReturnId(string name, string description, byte status);
        List<Permission> Update(int id , string name, string description, byte status);
        Role FindToModify(int id);
    }
    public class RoleRespository: IBaseRespository<Role>, IRoleRepository
    {
        

        public RoleRespository()
        {
           
        }

        public List<Permission> Update(int id, string name, string description, byte status)
        {
            using (var entities = new V308CMSEntities())
            {
                var roleUpdate = (from role in entities.Role.Include("Permissions")
                                  where role.Id == id
                                  select role
               ).FirstOrDefault();
                if (roleUpdate != null)
                {
                    roleUpdate.Name = name;
                    roleUpdate.Description = description;
                    roleUpdate.Status = status;
                    entities.SaveChanges();
                    return entities.Permission.ToList();
                }
                return null;
            }
        }

        public Role FindToModify(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from role in entities.Role.Include("Permissions")
                        where role.Id == id
                        select role
               ).FirstOrDefault();
            }
        }
        public Role Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from role in entities.Role
                        where role.Id == id
                        select role
               ).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var roleDelete = (from role in entities.Role
                                  where role.Id == id
                                  select role
               ).FirstOrDefault();
                if (roleDelete != null)
                {
                    entities.Role.Remove(roleDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           

        }

        public string Update(Role data)
        {
            using (var entities = new V308CMSEntities())
            {
                var roleUpdate = (from role in entities.Role
                                  where role.Id == data.Id
                                  select role
               ).FirstOrDefault();
                if (roleUpdate != null)
                {
                    roleUpdate.Name = data.Name;
                    roleUpdate.Description = data.Description;
                    roleUpdate.Status = data.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public int InsertAndReturnId(string name, string description,  byte status)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkRole = (from role in entities.Role
                                 where role.Name == name
                                 select role
                ).FirstOrDefault();
                if (checkRole == null)
                {
                    var role = new Role
                    {
                        Name = name,
                        Description = description,
                        Status = status
                    };
                    entities.Role.Add(role);
                    entities.SaveChanges();
                    return role.Id;

                }
                return 0;
            }
        }

       

        public string Insert(Role data)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkRole = (from role in entities.Role
                                 where role.Name == data.Name
                                 select role
                ).FirstOrDefault();
                if (checkRole == null)
                {
                    entities.Role.Add(data);
                    entities.SaveChanges();
                    return "ok";

                }
                return "exists";
            }
            
        }

        public List<Role> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from role in entities.Role
                        where role.Status == 1
                        select role
              ).ToList();
            }
          
        }

        public List<Role> GetList()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from role in entities.Role
                        orderby role.Id descending
                        select role
                   ).ToList();

            }
           
        }
    }
}
