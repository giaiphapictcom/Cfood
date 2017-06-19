using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;

namespace V308CMS.Respository
{
    public interface INewsRespository
    {
        
    }
    public class NewsRespository:IBaseRespository<News>, INewsRespository
    {
        public News Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.News.FirstOrDefault(news => news.ID == id);
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
                    entities.News.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           

        }

        public List<News> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from news in entities.News
                        where news.Status == true
                    orderby news.ID descending
                    select news).ToList();
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
