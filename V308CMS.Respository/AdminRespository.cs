﻿using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IAdminRespository
    {
        string ChangeStatus(int id);
        string CheckUserName(string userName);

    }
    public  class AdminRespository:IBaseRespository<Admin>,IAdminRespository
    {
        public Admin CheckAccount(string userName, string password)
        {
            using (var entities = new V308CMSEntities())
            {
               var checkAdmin = (from admin in entities.Admin
                          where admin.UserName.Equals(userName) || admin.Email.Equals(userName)
                          select admin).FirstOrDefault();
                if (checkAdmin == null)
                {
                    return null;
                }
                var hashPassword = EncryptionMD5.ToMd5(password.Trim());            
                if (checkAdmin.Password.Trim().Equals(hashPassword))
                {
                    
                    return checkAdmin;
                }
                return null;

            }
             
        }
        public Admin Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from admin in entities.Admin
                    where admin.ID == id
                    select admin
                    ).FirstOrDefault();
            }
           
        }
        public string ChangePassword(int id, string newPassword)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkAdmin = (from account in entities.Admin
                                    where account.ID == id
                                    select account
               ).FirstOrDefault();
                if (checkAdmin == null)
                {
                    return "invalid";
                }

                checkAdmin.Password = EncryptionMD5.ToMd5(newPassword);
                entities.SaveChanges();
                return "ok";
            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var adminDelete = (from admin in entities.Admin
                                   where admin.ID == id
                                   select admin
                    ).FirstOrDefault();
                if (adminDelete != null)
                {
                    entities.Admin.Remove(adminDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Update(Admin data)
        {
            using (var entities = new V308CMSEntities())
            {
                var adminUpdate = (from admin in entities.Admin
                                  where admin.ID == data.ID
                                  select admin
                    ).FirstOrDefault();
                if (adminUpdate != null)
                {
                    adminUpdate.Password = EncryptionMD5.ToMd5(data.Password.Trim());
                    adminUpdate.Email = data.Email;
                    adminUpdate.FullName = data.FullName;
                    adminUpdate.Role = data.Role;
                    adminUpdate.Date = data.Date;
                    adminUpdate.Status = data.Status;
                    adminUpdate.Type = data.Type;
                    adminUpdate.Avatar = data.Avatar;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exist";
            }
            
        }

        public string Insert(Admin data)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkAdmin = (from admin in entities.Admin
                    where admin.UserName == data.UserName
                    select admin
                    ).FirstOrDefault();
                if (checkAdmin == null)
                {
                    data.Password = EncryptionMD5.ToMd5(data.Password.Trim());
                    entities.Admin.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
        }
        public List<Admin> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from admin in entities.Admin.Include("RoleInfo")                  
                    orderby admin.Date.Value descending
                    select admin
                    ).ToList();
            }
         
        }

        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var adminStatus = (from admin in entities.Admin
                             where admin.ID == id
                             select admin
                   ).FirstOrDefault();
                if (adminStatus != null)
                {
                    adminStatus.Status = !adminStatus.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }
        public string CheckUserName(string userName)
        {
            using (var entities = new V308CMSEntities())
            {
               var adminAccount =  (from admin in entities.Admin
                                   where admin.UserName == userName
                                   select admin
                 ).FirstOrDefault();
                return adminAccount != null ? "exists" : "ok";
            }
        }
    }
}
