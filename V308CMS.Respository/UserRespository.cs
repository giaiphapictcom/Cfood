using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface IUserRespository
    {
         List<Account> GetList(
            int state = 0,
            string site=""
            );

        string ChangeStatus(int id);
        
        int Count();
        List<Account> Take(int count = 10);
    }


    public class UserRespository: IBaseRespository<Account>, IUserRespository
    {
     
        public UserRespository()
        {
           
        }
        public Account Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from user in entities.Account
                        where user.ID == id
                        select user
              ).FirstOrDefault();
            }
          
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var userDelete = (from user in entities.Account
                                  where user.ID == id
                                  select user
                ).FirstOrDefault();
                if (userDelete != null)
                {
                    entities.Account.Remove(userDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            

        }

        public string Update(Account account)
        {
            using (var entities = new V308CMSEntities())
            {
                var userUpdate = (from user in entities.Account
                                  where user.ID == account.ID
                                  select user
               ).FirstOrDefault();
                if (userUpdate != null)
                {
                    userUpdate.UserName = account.UserName;
                    userUpdate.FullName = account.FullName;
                    userUpdate.Email = account.Email;
                    userUpdate.Address = account.Address;
                    userUpdate.Phone = account.Phone;
                    userUpdate.Gender = account.Gender;
                    userUpdate.Status = account.Status;
                    userUpdate.Avatar = account.Avatar;
                    userUpdate.Gender = account.Gender;
                    userUpdate.Date = account.Date;
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Account account)
        {
            using (var entities = new V308CMSEntities())
            {
                var userInsert = (from user in entities.Account
                                  where user.Email == account.Email
                                  select user
                 ).FirstOrDefault();
                if (userInsert == null)
                {
                    entities.Account.Add(account);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
            
        }

        public List<Account> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from user in entities.Account
                        orderby user.Date.Value descending
                        select user).ToList();
            }
           
        }

        public List<Account> GetList(int state = 0,string site="")
        {
            using (var entities = new V308CMSEntities())
            {
                var items = entities.Account.Select(a=>a);
                if (site == "affiliate")
                {
                    items = items.Where(a => a.Site.Equals(site.Trim()));
                }
                else {
                    items = items.Where(a => a.Site == "home" || a.Site =="" );
                }


                if (state > 0)
                {
                    if (state == 1)
                    {
                        items = items.Where(a => a.Status == true);
                    }
                    else {
                        items = items.Where(a => a.Status == false);
                    }

                }
                return items.OrderByDescending(a=>a.ID).ToList();

            }
            
        }
        private string HashPassword(string password, string salt)
        {
            return EncryptionMD5.ToMd5(password.ToString() + "|" + salt.ToString());
        }
        public string ChangePassword(int id, string currentPassword, string newPassword)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkAccount = (from account in entities.Account
                                    where account.ID == id
                                    select account
               ).FirstOrDefault();
                if (checkAccount == null)
                {
                    return "invalid";
                }
                var hashCurrentPassword = HashPassword(currentPassword, checkAccount.Salt);
                if (checkAccount.Password != hashCurrentPassword)
                {
                    return "current_wrong";
                }
                checkAccount.Password = HashPassword(newPassword, checkAccount.Salt);
                entities.SaveChanges();
                return "ok";
            }
           

        }
        public string ChangePassword(int id,string newPassword)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkAccount = (from account in entities.Account
                                    where account.ID == id
                                    select account
               ).FirstOrDefault();
                if (checkAccount == null)
                {
                    return "invalid";
                }
                if (checkAccount.Salt.Length > 0)
                {
                    checkAccount.Password = HashPassword(newPassword, checkAccount.Salt);
                }
                else {
                    checkAccount.Password = EncryptionMD5.ToMd5(newPassword.Trim());
                }

                
                entities.SaveChanges();
                return "ok";
            }

           

        }
        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var product = (from item in entities.Product
                               where item.ID == id
                               select item
               ).FirstOrDefault();
                if (product != null)
                {
                    product.Status = !product.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public int Count()
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Account.Count();
            }
        }

        public List<Account> Take(int count = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from user in entities.Account
                        orderby user.Date.Value descending
                        select user).Take(count).ToList();
            }
        }
    }
}
