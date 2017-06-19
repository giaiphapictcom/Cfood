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
        public EmailConfigRepository( )
        {
           
        }
        public EmailConfig Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.EmailConfig
                        where item.Id == id
                        select item
                ).FirstOrDefault();
            }
            
        }
        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var emailConfig = (from item in entities.EmailConfig
                                   where item.Id == id
                                   select item
               ).FirstOrDefault();
                if (emailConfig != null)
                {
                    entities.EmailConfig.Remove(emailConfig);
                    return "ok";
                }
                return "not_exists";
            }
        }
        public string Update(EmailConfig config)
        {
            using (var entities = new V308CMSEntities())
            {
                var emailConfig = (from item in entities.EmailConfig
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
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";

            }
            
        }

        public string Insert(EmailConfig config)
        {
            using (var entities = new V308CMSEntities())
            {
                var emailConfig = (from item in entities.EmailConfig
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
                    entities.EmailConfig.Add(newEmailConfig);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
            
        }

       
      

        public List<EmailConfig> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.EmailConfig
                        orderby item.CreatedDate descending
                        select item
                   ).ToList();

            }

            
        }

        public string ChangeState(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var emailConfig = (from item in entities.EmailConfig
                                   where item.Id == id
                                   select item
                  ).FirstOrDefault();
                if (emailConfig != null)
                {
                    emailConfig.State = (byte)(emailConfig.State == 1 ? 0 : 1);
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";
            }
            
        }
    }
}
