using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class ImagesRepository
    {

        public ImagesRepository()
        {
        }

        public Image LayAnhTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Image
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<Image> LayAnhTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Image
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<Image> GetImageByTypeId(int pTypeId, int pTake)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Image
                        where p.TypeID == pTypeId
                        orderby p.ID descending
                        select p).Take(pTake).ToList();
            }
        }
        public List<ImageType> LayNhomAnhAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ImageType
                        select p).ToList();
            }
        }
        public List<ImageType> LayNhomAnhTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ImageType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
            }
        }
        public ImageType LayTheLoaiAnhTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ImageType
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<Image> LayImageTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeId, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeId == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.Image
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                              .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                int[] mIdGroup = (from p in entities.ImageType
                                  where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                  select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.Image
                        where mIdGroup.Contains(p.TypeID.Value)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                          .Take(psize).ToList();
            }
        }
        public List<ImageType> LayImageTypeTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeId, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeId == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.ImageType
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                              .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                int[] mIdGroup = (from p in entities.ImageType
                                  where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                  select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.ImageType
                        where mIdGroup.Contains(p.ID)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                          .Take(psize).ToList();
            }
        }

        #region Adv
        public List<Image> GetImagesByGroupAlias(string imgTypeAlias = "", int limit = 2)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from img in entities.Image
                        join type in entities.ImageType on img.TypeID equals type.ID
                        where type.Alias.Equals(imgTypeAlias)
                        select img).Take(limit).ToList();

            }
        }
        #endregion Adv
    }
}