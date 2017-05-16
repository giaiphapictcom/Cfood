using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class CategoryRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public CategoryRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public CategoryRepository(V308CMSEntities mEntities)
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

        public List<Categorys> GetItems(int limit = 5)
        {
            List<Categorys> mList = null;
            try
            {
                var items = from comment in entities.Categorys
                            where comment.status == 1
                            select comment;
                mList = items.Take(limit).ToList();
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