using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class SupportRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public SupportRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public SupportRepository(V308CMSEntities mEntities)
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

        public Support LayTheoId(int pId)
        {
            Support mSupport = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mSupport = (from p in entities.Support
                          where p.ID == pId
                          select p).FirstOrDefault();
                return mSupport;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Support> LayTheoTrang(int pcurrent, int psize)
        {
            List<Support> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Support
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
        public List<Support> GetSupportByTypeId(int pTypeId, int pTake)
        {
            List<Support> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Support
                         where p.TypeID == pTypeId
                         orderby p.ID descending
                         select p).Take(pTake).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Support> LayAll()
        {
            List<Support> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Support
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SupportType> LaySupportTypeAll()
        {
            List<SupportType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.SupportType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SupportType> LaySupportTypeTheoTrang(int pcurrent, int psize)
        {
            List<SupportType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.SupportType
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
        public SupportType LaySupportTypeTheoId(int pId)
        {
            SupportType mSupportType = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mSupportType = (from p in entities.SupportType
                              where p.ID == pId
                              select p).FirstOrDefault();
                return mSupportType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}