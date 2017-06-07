using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Data
{
    public interface TicketInterfaceRepository
    {
        string Insert(string title, string content, int created_by);
    }

    public class TicketRepository : TicketInterfaceRepository
    {
        private V308CMSEntities entities;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.entities != null)
                {
                    this.entities.Dispose();
                    this.entities = null;
                }
            }
        }

        public TicketRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }


        public string Insert(string title, string content, int created_by)
        {
            try
            {
                var TicketNew = new Ticket
                {
                    title = title,
                    content = content,
                    created_by = created_by,
                    created = DateTime.Now
                };
                entities.TicketRepository.Add(TicketNew);
                entities.SaveChanges();

                return TicketNew.ID.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }

    }
}
