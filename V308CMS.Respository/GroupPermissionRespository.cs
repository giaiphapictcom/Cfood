using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IGroupPermissionRespository
    {
        List<GroupPermission> GetList();
    }
    public class GroupPermissionRespository: IBaseRespository<GroupPermission>, IGroupPermissionRespository
    {
        private readonly V308CMSEntities _entities;

        public GroupPermissionRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public GroupPermission Find(int id)
        {
            return (from permission in _entities.GroupPermission
                where permission.Id == id
                select permission
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
          var permissionDelete = (from permission in _entities.GroupPermission
                                  where permission.Id == id
                                  select permission
                ).FirstOrDefault();
            if (permissionDelete != null)
            {
               
                _entities.GroupPermission.Remove(permissionDelete);
                return "ok";
            }
            return "not_exists";

        }

        public string Update(GroupPermission data)
        {
            var permissionUpdate = (from permission in _entities.GroupPermission
                                    where permission.Id == data.Id
                                    select permission
               ).FirstOrDefault();
            if (permissionUpdate != null)
            {
                permissionUpdate.Name = data.Name;
                permissionUpdate.Code = data.Code;
                permissionUpdate.Description = data.Description;
                permissionUpdate.Status = data.Status;
                permissionUpdate.UpdatedAt = data.UpdatedAt;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(GroupPermission data)
        {
            var checkGroupPermission = (from permission in _entities.GroupPermission
                where permission.Id == data.Id
                select permission
                ).FirstOrDefault();
            if (checkGroupPermission == null)
            {
                _entities.GroupPermission.Add(data);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

        public List<GroupPermission> GetAll()
        {
            return (from permission in _entities.GroupPermission
                    where permission.Status ==1
                orderby permission.UpdatedAt
                select permission
                ).ToList();
        }

        public List<GroupPermission> GetList()
        {
            return (from permission in _entities.GroupPermission
                    orderby permission.UpdatedAt
                    select permission
                ).ToList();
        }
    }
}
