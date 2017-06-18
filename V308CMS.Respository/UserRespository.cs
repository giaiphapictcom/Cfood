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
            int state = 0
            );

        string ChangeStatus(int id);


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
                    userUpdate.Avata = account.Avata;
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

        public List<Account> GetList(int state = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                IEnumerable<Account> data = (from user in entities.Account
                                             select user
                                           ).ToList();

                if (state > 0)
                {
                    data = state == 1 ?
                        (from user in data
                         where user.Status == true
                         select user
                     ).ToList() :
                     (from user in data
                      where user.Status == false
                      select user
                     ).ToList();
                }

                return (from user in data
                        orderby user.ID descending
                        select user
                    ).ToList();

            }
            
        }
        private string HashPassword(string password, string salt)
        {
            return EncryptionMD5.ToMd5($"{password}|{salt}");
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

                checkAccount.Password = HashPassword(newPassword, checkAccount.Salt);
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


    }
}
