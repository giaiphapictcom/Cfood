using System;
using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Data
{
    public interface IContactRepository
    {
        string Insert
            (
                string name,
                string email,
                string phone, 
                string message,
                DateTime createdDate
            );
        string Update(int id, string name, string email, string phone, string message, DateTime createdDate);
        string Delete(int id);
        List<Contact> GetAll();
        Contact Find(int id);
    }
    public class ContactRepository: IContactRepository
    {
        private V308CMSEntities entities;
        public ContactRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }
        public List<Contact> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from contact in entities.Contact
                        orderby contact.CreatedDate descending
                        select contact
                    ).ToList();
            }
                
        }
        public Contact Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from contact in entities.Contact
                        where contact.ID == id
                        select contact
                   ).FirstOrDefault();

            }
            
        }

        public string Insert(string fullName, string email, string phone, string message, DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var contact = new Contact
                {
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    Message = message,
                    CreatedDate = createdDate
                };
                entities.Contact.Add(contact);
                entities.SaveChanges();
                return "ok";

            }
                
            
        }

        public string Update(int id, string fullName, string email, string phone, string message, DateTime createdDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var contactUpdate = (from contact in entities.Contact
                                     where contact.ID == id
                                     select contact
                 ).FirstOrDefault();
                if (contactUpdate != null)
                {
                    contactUpdate.FullName = fullName;
                    contactUpdate.Email = email;
                    contactUpdate.Phone = phone;
                    contactUpdate.Message = message;
                    contactUpdate.CreatedDate = createdDate;
                    entities.SaveChanges();
                    return "ok";

                }
                return "not_exists";
            }

           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var contactDelete = (from contact in entities.Contact
                                     where contact.ID == id
                                     select contact
                  ).FirstOrDefault();
                if (contactDelete != null)
                {
                    entities.Contact.Remove(contactDelete);
                    return "ok";
                }
                return "not_exists";

            }
          
        }
    }
}