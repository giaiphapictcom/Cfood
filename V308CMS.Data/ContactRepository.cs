using System;

namespace V308CMS.Data
{
    public interface IContactRepository
    {
        string Insert(string name,string email,string phone,string message, DateTime createdDate);
    }
    public class ContactRepository: IContactRepository
    {
        private readonly V308CMSEntities _entities;
        public ContactRepository(V308CMSEntities mEntities)
        {
            this._entities = mEntities;
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
    }
}