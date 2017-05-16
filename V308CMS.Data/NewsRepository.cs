using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
        public class NewsRepository
        {
            private V308CMSEntities entities;
            #region["Contructor"]

            public NewsRepository()
            {
                this.entities = new V308CMSEntities();
            }

            public NewsRepository(V308CMSEntities mEntities)
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

            public News LayTinTheoId(int pId)
            {
                News mNews = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mNews = (from p in entities.News
                             where p.ID == pId
                             select p).FirstOrDefault();
                    return mNews;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public News getFirstNewsWithType(int pId)
            {
                News mNews = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mNews = (from p in entities.News
                             where p.TypeID == pId
                             select p).FirstOrDefault();
                    return mNews;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayTinTheoTrang(int pcurrent, int psize)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
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
            public List<News> LayTinTheoTrangAndGroupId(int pcurrent, int psize, int pTypeID)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.TypeID==pTypeID
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
            public List<News> LayTinTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
            {
                List<News> mList = null;
                int[] mIdGroup;
                try
                {
                    if (pTypeID > 0)
                    {
                        //lay tat ca cac ID cua group theo Level
                        mIdGroup = (from p in entities.NewsGroups
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.News
                                 where mIdGroup.Contains(p.TypeID.Value)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList();
                    }
                    else if (pTypeID == 0)
                    {
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.News
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
            public List<News> LayTinTheoTrangAndGroupIdAndLevel(int pcurrent, int psize, int pTypeID, string pLevel)
            {
                List<News> mList = null;
                int[] mIdGroup;
                try
                {
                    if (pTypeID > 0)
                    {
                        //lay tat ca cac ID cua group theo Level
                        mIdGroup = (from p in entities.NewsGroups
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.News
                                 where mIdGroup.Contains(p.TypeID.Value)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList();
                    }
                    else if (pTypeID == 0)
                    {
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.News
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
            public List<NewsGroups> LayNewsGroupsTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
            {
                List<NewsGroups> mList = null;
                int[] mIdGroup;
                try
                {
                    if (pTypeID > 0)
                    {
                        //lay tat ca cac ID cua group theo Level
                        mIdGroup = (from p in entities.NewsGroups
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.NewsGroups
                                 where mIdGroup.Contains(p.ID)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList();
                    }
                    else if (pTypeID == 0)
                    {
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.NewsGroups
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
            public List<News> LayTinTucLienQuan(int pId,int pTypeID,int pSize)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.ID!=pId && p.TypeID == pTypeID
                             orderby p.ID descending
                             select p)
                             .Take(pSize).ToList();
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayTinTheoGroupId(int pTypeID)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.TypeID == pTypeID
                             orderby p.ID descending
                             select p).ToList();
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayTinSlider(int pTake)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Slider==true && p.Status==true
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
            public List<NewsGroups> LayNhomTinAll()
            {
                List<NewsGroups> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             where p.Status==true
                             select p).ToList();
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<NewsGroups> getNewsGroupAffterParent(int pId)
            {
                List<NewsGroups> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             where p.Parent == pId
                             select p).ToList();
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<NewsGroups> LayNhomTinTheoTrang(int pcurrent, int psize)
            {
                List<NewsGroups> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
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
            public NewsGroups LayTheLoaiTinTheoId(int pId)
            {
                NewsGroups mNewsGroups = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mNewsGroups = (from p in entities.NewsGroups
                             where p.ID == pId
                             select p).FirstOrDefault();
                    return mNewsGroups;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinHot(int pSoLuongTin)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.Hot==true
                             orderby p.Date descending
                             select p).Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinNhanh(int pSoLuongTin)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.Fast == true
                             orderby p.Date descending
                             select p).Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinTheoGroupId(int pSoLuongTin, int pType)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.TypeID == pType
                             orderby p.Date descending
                             select p).Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinMoiNhatTheoGroupId(int pSoLuongTin, int pType)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.TypeID == pType
                             orderby p.ID descending
                             select p).Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinDangDocTheoGroupId(int pSoLuongTin, int pType)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.Featured==true && p.TypeID == pType
                             orderby p.ID descending
                             select p).Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinTheoGroupIdWithPage(int pSoLuongTin, int pType, int pcurrent=1)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.TypeID == pType
                             orderby p.Date descending
                             select p).Skip((pcurrent - 1) * pSoLuongTin)
                                 .Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayDanhSachTinTheoKey(int pSoLuongTin, string pkey, int pcurrent)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.Status == true && p.Keyword.Contains(pkey)
                             orderby p.Date descending
                             select p).Skip((pcurrent - 1) * pSoLuongTin)
                                 .Take(pSoLuongTin).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<NewsGroups> LayDanhSachNhomTin()
            {
                List<NewsGroups> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             where p.Status == true && p.Parent == 1
                             orderby p.Number ascending
                             select p).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<NewsGroups> LayDanhSachTatCaNhomTin()
            {
                List<NewsGroups> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             orderby p.Number ascending
                             select p).ToList();


                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public NewsGroups LayDanhNhomTin(int pId)
            {
                NewsGroups mGroup = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mGroup = (from p in entities.NewsGroups
                              where p.ID == pId && p.Status == true
                              select p).FirstOrDefault();
                    return mGroup;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public List<News> LayTatCaTinTheoNhom(int pNewsGroup)
            {
                List<News> mList = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.News
                             where p.TypeID == pNewsGroup
                             select p).ToList();
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            public NewsGroups LayNhomTinAn(int pId)
            {
                NewsGroups mGroup = null;
                try
                {
                    //lay danh sach tin moi dang nhat
                    mGroup = (from p in entities.NewsGroups
                              where p.ID == pId
                              select p).FirstOrDefault();
                    return mGroup;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }

            public List<NewsGroups> GetNewsGroup(int Parent = 0, Boolean Status = true, int Limit = 5)
            {
                try
                {
                    var newGroups = from g in entities.NewsGroups
                             where g.Status == true & g.Parent == Parent
                             orderby g.Number ascending
                             select g;
                    return newGroups.Take(Limit).ToList();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }

            public NewsGroups SearchNewsGroup(string name)
            {
                try
                {
                    var NewsGroups = from p in entities.NewsGroups
                                     where p.Name.ToLower().Contains(name.ToLower())
                                     select p;
                    return NewsGroups.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }

            public NewsGroups SearchNewsGroupByAlias(string alias)
            {
                try
                {
                    string[] words = alias.Split();
                    var NewsGroups = from p in entities.NewsGroups
                                     where p.Alias.ToLower().Contains(alias.ToLower())
                                     //where  ( words.Any(r=> p.Alias.Contains(r)) || words.Any(r => p.Name.Contains(r)) )
                                     select p;
                    return NewsGroups.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }

        }
}