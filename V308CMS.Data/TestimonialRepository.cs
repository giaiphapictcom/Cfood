using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class TestimonialRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public TestimonialRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public TestimonialRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }

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

        #endregion

        public List<Testimonial> GetRandom(int limit = 5)
        {
            List<Testimonial> mList = null;
            try
            {
                var items = from comment in entities.Testimonial
                            where comment.status == 1
                            select comment;
                mList = items.ToList().OrderBy(x => Guid.NewGuid()).Take(limit).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}