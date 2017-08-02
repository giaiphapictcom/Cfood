using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Data
{
    public class FileRepository
    {

        public FileRepository()
        {

        }

        public File LayFileTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.File
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<File> LayFileTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.File
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<File> GetFileByTypeId(int pTypeId, int pTake)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.File
                        where p.TypeID == pTypeId
                        orderby p.ID descending
                        select p).Take(pTake).ToList();
            }
        }
        public List<File> GetFileByTypeIdAndName(int pTypeId, string pName, int pTake)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.File
                        where p.TypeID == pTypeId && p.FileName.Equals(pName)
                        orderby p.ID descending
                        select p).Take(pTake).ToList();
            }
        }
        public List<FileType> LayNhomFileAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.FileType
                        select p).ToList();

            }
        }
        public List<FileType> LayNhomFileTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.FileType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();
            }
        }
        public FileType LayTheLoaiFileTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.FileType
                        where p.ID == pId
                        select p).FirstOrDefault();

            }
        }
        public List<File> LayFileTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeID == 0)
                {
                    return (from p in entities.File
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                              .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in entities.FileType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.File
                        where mIdGroup.Contains(p.TypeID.Value)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();

            }
        }
        public List<FileType> LayFileTypeTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.FileType
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                int[] mIdGroup = (from p in entities.FileType
                                  where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                  select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.FileType
                        where mIdGroup.Contains(p.ID)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
    }
}