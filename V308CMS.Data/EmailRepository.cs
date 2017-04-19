using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class EmailRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public EmailRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public EmailRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }

        #endregion
        #region["Vung cac thao tac Dispose"]
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

        public VEmail LayTheoId(int pId)
        {
            VEmail mVEmail = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mVEmail = (from p in entities.VEmail
                           where p.ID == pId
                           select p).FirstOrDefault();
                return mVEmail;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<VEmail> LayTheoTrang(int pcurrent, int psize)
        {
            List<VEmail> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.VEmail
                         orderby p.ID descending
                         select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}