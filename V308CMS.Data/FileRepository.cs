using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class FileRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public FileRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public FileRepository(V308CMSEntities mEntities)
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
        public File LayFileTheoId(int pId)
        {
            File mFile = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mFile = (from p in entities.File
                         where p.ID == pId
                         select p).FirstOrDefault();
                return mFile;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<File> LayFileTheoTrang(int pcurrent, int psize)
        {
            List<File> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.File
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
        public List<File> GetFileByTypeId(int pTypeId, int pTake)
        {
            List<File> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.File
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
        public List<File> GetFileByTypeIdAndName(int pTypeId,string pName, int pTake)
        {
            List<File> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.File
                         where p.TypeID == pTypeId && p.FileName.Equals(pName)
                         orderby p.ID descending
                         select p).Take(pTake).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<FileType> LayNhomFileAll()
        {
            List<FileType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.FileType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<FileType> LayNhomFileTheoTrang(int pcurrent, int psize)
        {
            List<FileType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.FileType
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
        public FileType LayTheLoaiFileTheoId(int pId)
        {
            FileType mFileType = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mFileType = (from p in entities.FileType
                               where p.ID == pId
                               select p).FirstOrDefault();
                return mFileType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<File> LayFileTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            List<File> mList = null;
            int[] mIdGroup;
            try
            {
                if (pTypeID > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.FileType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.File
                             where mIdGroup.Contains(p.TypeID.Value)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.File
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<FileType> LayFileTypeTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            List<FileType> mList = null;
            int[] mIdGroup;
            try
            {
                if (pTypeID > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.FileType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.FileType
                             where mIdGroup.Contains(p.ID)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.FileType
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}