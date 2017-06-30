using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data.Helpers;

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
                            where comment.status == true
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

        public int limit = 10;
        public List<Testimonial> GetList(string site = "") {
            List<Testimonial> mList = null;
            try
            {
                var items = from comment in entities.Testimonial
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

        public Testimonial Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from c in entities.Testimonial
                        where c.id == id
                        select c).FirstOrDefault();
            }
        }

        public string Update(Testimonial data)
        {
            using (var entities = new V308CMSEntities())
            {
                var itemUpdate = (from c in entities.Testimonial
                                    where c.id == data.id
                                    select c).FirstOrDefault();
                if (itemUpdate != null)
                {
                    itemUpdate.taget = data.taget;
                    itemUpdate.fullname = data.fullname;
                    itemUpdate.avartar = data.avartar;
                    itemUpdate.mobile = data.mobile;
                    itemUpdate.content = data.content;
                    itemUpdate.status = data.status;
                    
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;
            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from c in entities.Testimonial
                                    where c.id == id
                                    select c).FirstOrDefault();
                if (item != null)
                {
                    entities.Testimonial.Remove(item);
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;
            }
        }

        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from c in entities.Testimonial
                                    where c.id == id
                                    select c
               ).FirstOrDefault();
                if (item != null)
                {
                    item.status = !item.status;
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;
            }

        }
    }
}