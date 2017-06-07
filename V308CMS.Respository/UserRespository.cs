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


    }


    public class UserRespository: IBaseRespository<Account>, IUserRespository
    {
        private readonly V308CMSEntities _entities;
        public UserRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public Account Find(int id)
        {
            return (from user in _entities.Account
                where user.ID == id
                select user
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
           var userDelete = (from user in _entities.Account
                       where user.ID == id
                       select user
                ).FirstOrDefault();
            if (userDelete != null)
            {
                _entities.Account.Remove(userDelete);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";

        }

        public string Update(Account account)
        {
            var userUpdate = (from user in _entities.Account
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

        public string Insert(Account account)
        {
            var userInsert = (from user in _entities.Account
                              where user.Email == account.Email
                              select user
                 ).FirstOrDefault();
            if (userInsert == null)
            {
                _entities.Account.Add(account);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public List<Account> GetAll()
        {
            return (from user in _entities.Account
                    orderby user.Date.Value descending
                    select user).ToList();
        }

        public List<Account> GetList(int state = 0)
        {
            IEnumerable<Account> data = (from user in _entities.Account
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
        private string HashPassword(string password, string salt)
        {
            return EncryptionMD5.ToMd5($"{password}|{salt}");
        }
        public string ChangePassword(int id, string currentPassword, string newPassword)
        {
            var checkAccount = (from account in _entities.Account
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
            _entities.SaveChanges();
            return "ok";

        }
    }
}
