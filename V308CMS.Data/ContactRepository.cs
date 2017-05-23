using System;

namespace V308CMS.Data
{
    public interface IContactRepository
    {
        string Insert(string name,string email,string phone,string message, DateTime createdDate);
    }
    public class ContactRepository: IContactRepository
    {
        private V308CMSEntities entities;
        public ContactRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
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
            entities.Contact.Add(contact);
            entities.SaveChanges();
            return "ok";
        }
    }
}