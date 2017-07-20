using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface AffiliateCategoryInterface
    {

    }
    public class AffiliateCategoryRespository : IBaseRespository<Categorys>, AffiliateCategoryInterface
    {

        public List<Categorys> GetList(int page = 1, int pageSize = 10, int status = -1)
        {
            using (var entities = new V308CMSEntities())
            {

                var menus = entities.Categorys.Select(c => c);


                if (status > 0)
                {
                    menus = menus.Where(c => c.status == true);
                } else if(status==0) {
                    menus = menus.Where(c => c.status == false);
                }


                return menus.OrderBy(c => c.order).Skip((page - 1) * pageSize).Take(pageSize).ToList();


            }

        }

        public Categorys Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Categorys.FirstOrDefault(c => c.id == id);
            }

        }

        public string Insert(Categorys data)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from c in entities.Categorys
                                where c.id == data.id
                                select c).FirstOrDefault();
                if (newsItem == null)
                {
                    entities.Categorys.Add(data);
                    entities.SaveChanges();
                    return Data.Helpers.Result.Ok;
                }
                return Data.Helpers.Result.Exists;
            }


        }

        public List<Categorys> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from c in entities.Categorys
                        where c.status == true
                        orderby c.id descending
                        select c).ToList();
            }
        }

        public List<Categorys> GetAll(string site = Site.home)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from c in entities.Categorys
                        where c.status == true
                        orderby c.id descending
                        select c).ToList();
            }
        }

        public string Update(Categorys data)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from c in entities.Categorys
                            where c.id == c.id
                                select c).FirstOrDefault();
                if (item != null)
                {

                    item.name = data.name;
                    item.image = data.image;
                    item.link = data.link;
                    item.summary = data.summary;

                    entities.SaveChanges();
                    return Data.Helpers.Result.Ok;
                }
                return Data.Helpers.Result.NotExists;
            }


        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var newsItem = (from color in entities.Categorys
                                where color.id == id
                                select color).FirstOrDefault();
                if (newsItem != null)
                {
                    entities.Categorys.Remove(newsItem);
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;

            }

        }

        public string ChangeState(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var categoryItem = (from item in entities.Categorys where item.id == id select item ).FirstOrDefault();
                if (categoryItem != null)
                {
                    categoryItem.status = !categoryItem.status;
                    entities.SaveChanges();
                    
                    return Result.Ok;
                }
                return Result.NotExists;
            }


        }

    }
}
