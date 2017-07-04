using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public interface INewsRepository
    {
        News LayTinTheoId(int pId);
        News getFirstNewsWithType(int pId);
        List<News> LayTinTheoTrang(int pcurrent, int psize);
        List<News> LayTinTheoTrangAndGroupId(int pcurrent, int psize, int pTypeID);
        List<News> LayTinTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel);
        List<News> GetListNewsMostView(int pTypeId, string pLevel, int psize = 10);
        List<News> GetListNewsLastest(int pTypeId, string pLevel, int psize = 10);
        List<News> LayTinTheoTrangAndGroupIdAndLevel(int pcurrent, int psize, int pTypeID, string pLevel);

    }
    public class NewsRepository
    {

        public NewsRepository()
        {

        }
        public News LayTinTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public void IncrementView(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from p in entities.News
                                where p.ID == id
                                select p).FirstOrDefault();
                if (newsItem != null)
                {
                    if (newsItem.Views.HasValue)
                    {
                        newsItem.Views += 1;
                    }
                    else
                    {
                        newsItem.Views = 1;
                    }

                    entities.SaveChanges();
                }

            }

        }

        public News GetById(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.ID == id
                        select p).FirstOrDefault();

            }

        }
        public News getFirstNewsWithType(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.TypeID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<News> LayTinTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();

            }
        }
        public List<News> LayTinTheoTrangAndGroupId(int pcurrent, int psize, int pTypeID)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.TypeID == pTypeID
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }

        public List<News> LayTinTheoTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeID == 0)
                {

                    return (from p in entities.News
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();

                }
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in entities.NewsGroups
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.News
                        where mIdGroup.Contains(p.TypeID.Value)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }
     

        public List<News> GetListNewsMostView(int pTypeId, string pLevel, int psize = 10)
        {
            using (var entities = new V308CMSEntities())
           
            {
                if (pTypeId == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.News
                            orderby p.Views, p.ID descending
                            select p).Take(psize).ToList();


                }               
                return (from p in entities.News
                        where p.TypeID == pTypeId
                        orderby p.Views, p.ID descending
                        select p).Take(psize).ToList();

            }

        }

        public List<News> GetListNewsLastest(int pTypeId, string pLevel, int psize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeId == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.News
                            orderby p.ID descending
                            select p).Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in entities.NewsGroups
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.News
                        where mIdGroup.Contains(p.TypeID.Value)
                        orderby p.ID descending
                        select p).Take(psize).ToList();
            }
        }
        public List<News> LayTinTheoTrangAndGroupIdAndLevel(int pcurrent, int psize, int pTypeID, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pTypeID == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.News
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in entities.NewsGroups
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.News
                        where mIdGroup.Contains(p.TypeID.Value)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();

            }
        }
        public List<NewsGroups> LayNewsGroupsTrangAndGroupIdAdmin(int pcurrent, int psize, int pTypeId, string pLevel, string keyword = "")
        {
            using (var entities = new V308CMSEntities())
            {
                List<NewsGroups> mList = null;
                if (pTypeId > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    var mIdGroup = (from p in entities.NewsGroups
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             where mIdGroup.Contains(p.ID)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        mList = (from p in mList
                                 where mIdGroup.Contains(p.ID) &&
                                 p.Name.ToLower().Contains(keyword.ToLower())
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                    }
                }
                else if (pTypeId == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.NewsGroups
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        mList = (from p in mList
                                 where p.Name.ToLower().Contains(keyword.ToLower())
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                    }
                }
                return mList;
            }
        }


        public List<News> LayTinTucLienQuan(int pId, int pTypeID, int pSize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.ID != pId && p.TypeID == pTypeID
                        orderby p.ID descending
                        select p)
                         .Take(pSize).ToList();
            }
        }
        public List<News> LayTinTheoGroupId(int typeId)
        {
            using (var entities = new V308CMSEntities())
            {
                //lay danh sach tin moi dang nhat
                return entities.News
                .Where(news => news.TypeID == typeId)
                .OrderByDescending(p => p.ID).ToList();
            }

        }
        public List<News> LayTinSlider(int pTake)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.News
                        where p.Slider == true && p.Status == true
                        orderby p.ID descending
                        select p).Take(pTake).ToList();
            }
        }
        public List<NewsGroups> LayNhomTinAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.Status == true
                        select p).ToList();
            }
        }
        public List<NewsGroups> getNewsGroupAffterParent(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.Parent == pId
                        select p).ToList();

            }
        }
        public List<NewsGroups> LayNhomTinTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }
        public NewsGroups LayTheLoaiTinTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.ID == pId
                        select p).FirstOrDefault();
            }

        }

        public List<NewsGroups> GetListNewsGroupRoot()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.Parent == 0
                        orderby p.ID descending
                        select p).ToList();

            }

        }
        public List<News> LayDanhSachTinHot(int pSoLuongTin)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.Hot == true
                        orderby p.Date descending
                        select p).Take(pSoLuongTin).ToList();
            }

        }
        public List<News> LayDanhSachTinNhanh(int pSoLuongTin)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.Fast == true
                        orderby p.Date descending
                        select p).Take(pSoLuongTin).ToList();
            }
        }
        public List<News> LayDanhSachTinTheoGroupId(int pSoLuongTin, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.TypeID == pType
                        orderby p.Date descending
                        select p).Take(pSoLuongTin).ToList();
            }
        }
        public List<News> LayDanhSachTinMoiNhatTheoGroupId(int pSoLuongTin, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                //lay danh sach tin moi dang nhat
                return (from p in entities.News
                        where p.Status == true && p.TypeID == pType
                        orderby p.ID descending
                        select p).Take(pSoLuongTin).ToList();


            }
        }
        public List<News> LayDanhSachTinDangDocTheoGroupId(int pSoLuongTin, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.Featured == true && p.TypeID == pType
                        orderby p.ID descending
                        select p).Take(pSoLuongTin).ToList();
            }
        }
        public List<News> LayDanhSachTinTheoGroupIdWithPage(int pSoLuongTin, int pType, int pcurrent = 1)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.TypeID == pType
                        orderby p.Date descending
                        select p).Skip((pcurrent - 1) * pSoLuongTin)
                            .Take(pSoLuongTin).ToList();
            }
        }
        public List<News> LayDanhSachTinTheoKey(int pSoLuongTin, string pkey, int pcurrent)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.Status == true && p.Keyword.Contains(pkey)
                        orderby p.Date descending
                        select p).Skip((pcurrent - 1) * pSoLuongTin)
                                 .Take(pSoLuongTin).ToList();
            }
        }
        public List<NewsGroups> LayDanhSachNhomTin()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.Status == true && p.Parent == 1
                        orderby p.Number ascending
                        select p).ToList();
            }
        }
        public List<NewsGroups> LayDanhSachTatCaNhomTin()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        orderby p.Number ascending
                        select p).ToList();
            }
        }
        public NewsGroups LayDanhNhomTin(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.ID == pId && p.Status == true
                        select p).FirstOrDefault();
            }
        }
        public List<News> LayTatCaTinTheoNhom(int pNewsGroup)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.News
                        where p.TypeID == pNewsGroup
                        select p).ToList();
            }
        }
        public NewsGroups LayNhomTinAn(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.NewsGroups
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public List<NewsGroups> GetNewsGroup(int Parent = 0, Boolean Status = true, int Limit = 5)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from g in entities.NewsGroups
                        where g.Status == Status & g.Parent == Parent
                        orderby g.Number ascending
                        select g).Take(Limit).ToList();
            }
        }

        public NewsGroups SearchNewsGroup(string name)
        {
            using (var cmsEntities = new V308CMSEntities())
            {

                return
                    cmsEntities.NewsGroups.FirstOrDefault(group => (group.Name.ToLower().Contains(name.ToLower()) ||
                                                                 group.Alias.ToLower().Contains(name.ToLower())));
            }

        }
        public NewsGroups SearchNewsGroupWithNews(string name)
        {
            using (var cmsEntities = new V308CMSEntities())
            {

                return cmsEntities.NewsGroups
                    .Include("ListNews")
                    .FirstOrDefault(group => (group.Name.ToLower().Contains(name.ToLower()) ||
                                              group.Alias.ToLower().Contains(name.ToLower())));
            }

        }

        public NewsGroups SearchNewsGroupByAlias(string alias, string site = "")
        {
            using (var entities = new V308CMSEntities())
            {
                string[] words = alias.Split();
                var NewsGroups = from p in entities.NewsGroups
                                 where p.Alias.ToLower().Contains(alias.ToLower())
                                 //where  ( words.Any(r=> p.Alias.Contains(r)) || words.Any(r => p.Name.Contains(r)) )
                                 select p;
                return NewsGroups.FirstOrDefault();
            }
        }
     
        public News GetNext(int id, int type = 58)
        {
            using (var entities = new V308CMSEntities())
            {
                var listNews = entities.News.Where(news => news.ID > id && news.TypeID == type).OrderByDescending(news=>news.ID).Take(1).ToList();
                return listNews.Count > 0 ? listNews[0] : null;
            }

        }
        public News GetPrevious(int id, int type = 58)
        {
            using (var entities = new V308CMSEntities())
            {
                var listNews = entities.News.Where(news => news.ID < id && news.TypeID == type).OrderByDescending(news => news.ID).Take(1).ToList();
                return listNews.Count > 0 ? listNews[0] : null;
            }
        }

        public List<News> GetListNewsByTag(string tag, out int totalRecord, int page = 1, int pageSize = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                var listNews = entities.News
                .Where(news => news.Keyword.Contains(tag))
                .OrderByDescending(news => news.ID)
                .Select(news => news)
                .ToList();
                totalRecord = listNews.Count;
                return listNews.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }

        }

        public News Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.News.FirstOrDefault(news => news.ID == id);

            }

        }

        public List<News> GetList(int categoryId = 0, string site = "")
        {

            using (var entities = new V308CMSEntities())
            {
                var listNews = from news in entities.News select news;

                if (categoryId > 0)
                {
                    listNews = (from news in listNews
                                where news.TypeID == categoryId
                                select news
                        );
                }
                if (site.Length > 0)
                {
                    listNews = (from news in listNews
                                join cate in entities.NewsGroups on news.TypeID equals cate.ID
                                where cate.Site == site
                                select news
                        );
                }
                return listNews.OrderByDescending(news => news.Date.Value).ToList();
            }


        }

        public List<News> GetVideos(int pcurrent, int psize, int pTypeID)
        {
            using (var entities = new V308CMSEntities())
            {
                List<News> mList = null;
                if (pTypeID > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    //mIdGroup = (from p in entities.NewsGroups

                    //            select p.ID).ToArray();

                    //lay danh sach tin moi dang nhat
                    return (from p in entities.News
                            where p.TypeID.Value.Equals(pTypeID) && p.Summary.Length > 0
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                              .Take(psize).ToList();
                }
                return mList;
            }
        }

        public string Insert(News data)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from news in entities.News
                                where news.Title == data.Title && news.TypeID == data.TypeID
                                select news).FirstOrDefault();
                if (newsItem == null)
                {

                    try
                    {
                        data.NewsGroup = new NewsGroups();
                        if (data.TypeID > 1)
                        {
                            data.NewsGroup = LayTheLoaiTinTheoId(int.Parse(data.TypeID.ToString()));
                        }
                        entities.News.Add(data);
                        entities.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Console.Write(dbEx);
                    }
                    return "ok";
                }
                return "exists";
            }

        }
        public News SearchNews(string name)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from n in entities.News
                        where n.Title.ToLower().Contains(name.ToLower()) || n.Alias.ToLower().Contains(name.ToLower())
                        select n).FirstOrDefault();

            }
        }

        public string Update(News data)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from news in entities.News
                    where news.ID == data.ID
                    select news).FirstOrDefault();
                if (newsItem != null)
                {
                    newsItem.Title = data.Title;
                    newsItem.Alias = data.Alias;
                    newsItem.TypeID = data.TypeID;
                    newsItem.Image = data.Image;
                    newsItem.Summary = data.Summary;
                    newsItem.Detail = data.Detail;
                    newsItem.Keyword = data.Keyword;
                    newsItem.Description = data.Description;
                    newsItem.Order = data.Order;
                    newsItem.Hot = data.Hot;
                    newsItem.Fast = data.Fast;
                    newsItem.Featured = data.Featured;
                    newsItem.Status = data.Status;
                    newsItem.Site = data.Site;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from color in entities.News
                                where color.ID == id
                                select color).FirstOrDefault();
                if (newsItem != null)
                {
                    entities.News.Remove(newsItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }

        }

    }
}