using System;
using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Data
{
    public interface IContactRepository
    {
        string Insert(string name, string email, string phone, string message, DateTime createdDate);
        string Update(int id, string name, string email, string phone, string message, DateTime createdDate);
        string Delete(int id);
        List<Contact> GetAll();
        Contact Find(int id);
    }
    public class ContactRepository: IContactRepository
    {
        private readonly V308CMSEntities _entities;
        public ContactRepository(V308CMSEntities mEntities)
        {
            _entities = mEntities;
        }
        public List<Contact> GetAll()
        {
            return (from contact in _entities.Contact
                    orderby contact.CreatedDate descending
                    select contact
                ).ToList();
        }
        public Contact Find(int id)
        {
            return (from contact in _entities.Contact
                    where contact.ID == id
                    select contact
                ).FirstOrDefault();
        }

        public string Insert(string fullName, string email, string phone, string message, DateTime createdDate)
        {
            var contact = new Contact
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                Message = message,
                CreatedDate = createdDate
            };
            _entities.Contact.Add(contact);
            _entities.SaveChanges();
            return "ok";
        }

        public string Update(int id, string fullName, string email, string phone, string message, DateTime createdDate)
        {
            var contactUpdate = (from contact in _entities.Contact
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
                _entities.SaveChanges();
                return "ok";

            }
            return "not_exists";
        }

        public string Delete(int id)
        {
            var contactDelete = (from contact in _entities.Contact
                                 where contact.ID == id
                                 select contact
                 ).FirstOrDefault();
            if (contactDelete != null)
            {
                _entities.Contact.Remove(contactDelete);
                return "ok";
            }
            return "not_exists";
        }
    }
}