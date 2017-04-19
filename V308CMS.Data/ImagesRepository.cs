using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class ImagesRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public ImagesRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public ImagesRepository(V308CMSEntities mEntities)
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

        public Image LayAnhTheoId(int pId)
        {
            Image mImage = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mImage = (from p in entities.Image
                         where p.ID == pId
                         select p).FirstOrDefault();
                return mImage;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Image> LayAnhTheoTrang(int pcurrent, int psize)
        {
            List<Image> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Image
                         orderby p.ID descending
                         select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Image> GetImageByTypeId(int pTypeId,int pTake)
        {
            List<Image> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Image
                         where p.TypeID == pTypeId
                         orderby p.ID descending
                         select p).Take(pTake).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ImageType> LayNhomAnhAll()
        {
            List<ImageType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ImageType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ImageType> LayNhomAnhTheoTrang(int pcurrent, int psize)
        {
            List<ImageType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ImageType
                         orderby p.ID descending
                         select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public ImageType LayTheLoaiAnhTheoId(int pId)
        {
            ImageType mImageType = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mImageType = (from p in entities.ImageType
                               where p.ID == pId
                               select p).FirstOrDefault();
                return mImageType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Image> LayImageTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            List<Image> mList = null;
            int[] mIdGroup;
            try
            {
                if (pTypeID > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.ImageType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Image
                             where mIdGroup.Contains(p.TypeID.Value)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Image
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ImageType> LayImageTypeTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            List<ImageType> mList = null;
            int[] mIdGroup;
            try
            {
                if (pTypeID > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.ImageType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.ImageType
                             where mIdGroup.Contains(p.ID)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.ImageType
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

        #region Adv
        public List<Image> GetImagesByGroupAlias(string ImgTypeAlias="",int Limit=2)
        {
            try
            {
                var images = from img in entities.Image
                             join type in entities.ImageType on img.TypeID equals type.ID
                             where type.Alias.Equals(ImgTypeAlias)
                             select img;
                 return images.Take(Limit).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        #endregion Adv
    }
}