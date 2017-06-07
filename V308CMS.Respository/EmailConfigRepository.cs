using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IEmailConfigRepository
    {
        string ChangeState(int id);
    }
    public class EmailConfigRepository: IBaseRespository<EmailConfig>,IEmailConfigRepository
    {
        private readonly V308CMSEntities _entities;

        public EmailConfigRepository(V308CMSEntities entities)
        {
            _entities = entities;
        }
        public EmailConfig Find(int id)
        {
            return (from item in _entities.EmailConfig
                where item.Id == id
                select item
                ).FirstOrDefault();
        }


        public string Delete(int id)
        {
           var emailConfig =(from item in _entities.EmailConfig
                            where item.Id == id
                            select item
                ).FirstOrDefault();
            if (emailConfig != null)
            {
                _entities.EmailConfig.Remove(emailConfig);
                return "ok";
            }
            return "not_exists";
        }

        public string Update(EmailConfig config)
        {
            var emailConfig = (from item in _entities.EmailConfig
                               where item.Id == config.Id
                               select item
                  ).FirstOrDefault();
            if (emailConfig != null)
            {
                emailConfig.Name = config.Name;
                emailConfig.Type = config.Type;
                emailConfig.Host = config.Host;
                emailConfig.Port = config.Port;
                emailConfig.UserName = config.UserName;
                emailConfig.Password = config.Password;
                emailConfig.State = config.State;
                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";
        }

        public string Insert(EmailConfig config)
        {
            var emailConfig = (from item in _entities.EmailConfig
                               where item.Name == config.Name
                               select item
               ).FirstOrDefault();
            if (emailConfig == null)
            {
                var newEmailConfig = new EmailConfig
                {
                    Name = config.Name,
                    Type = config.Type,
                    Host = config.Host,
                    Port = config.Port,
                    UserName = config.UserName,
                    Password = config.UserName,
                    CreatedDate = config.CreatedDate
                };
                _entities.EmailConfig.Add(newEmailConfig);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }

       
      

        public List<EmailConfig> GetAll()
        {
            return (from item in _entities.EmailConfig
                orderby item.CreatedDate descending
                select item
                ).ToList();
        }

        public string ChangeState(int id)
        {
            var emailConfig = (from item in _entities.EmailConfig
                    where item.Id == id
                    select item
                  ).FirstOrDefault();
            if (emailConfig != null)
            {
                emailConfig.State = (byte)(emailConfig.State == 1 ? 0 : 1);
                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";
        }
    }
}
