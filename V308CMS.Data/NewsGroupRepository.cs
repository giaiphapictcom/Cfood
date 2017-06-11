﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;


namespace V308CMS.Data
{
    public interface INewsGroupRepository
    {
        List<NewsGroups> GetListRoot();
        List<NewsGroups> GetListParent(int rootId = 0);


    //    List<NewsGroups> GetList(string keyword = "", int rootId = 0, int parentId = 0, int childId = 0, int page = 1,
    //        int pageSize = 10);
    }

    public class NewsGroupRepository: INewsGroupRepository
    {
        private readonly V308CMSEntities _entities;


        List<NewsGroups> GetList(
            string keyword = "", int rootId = 0, int parentId = 0, int childId = 0, int page = 1,
            int pageSize = 10);

        List<NewsGroups> GetAll(bool state = true);
        string ChangeState(int id);
        string Insert(string name,int parentId,int number,bool state,DateTime createdDate);

        string Update(int id,string name, int parentId, int number, bool state, DateTime createdDate);
    }


    public class NewsGroupRepository : IBaseRespository<NewsGroups>, INewsGroupRepository
    {
        //private readonly V308CMSEntities _entities;



        public NewsGroupRepository(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public List<NewsGroups> GetAll(bool state=true)
        {
            return state?(from category in _entities.NewsGroups
                        orderby category.Date.Value descending
                        where category.Status == true
                        select category).ToList(): 
                    (from category in _entities.NewsGroups
                        orderby category.Date.Value descending                                                
                        select category).ToList();
        }

        public string ChangeState(int id)
        {
            var categoryItem = (
               from item in _entities.NewsGroups.Include("ListNews")
               where item.ID == id
               select item
              ).FirstOrDefault();
            if (categoryItem != null)
            {
                categoryItem.Status = !categoryItem.Status;
                _entities.SaveChanges();
                if (categoryItem.ListNews != null && categoryItem.ListNews.Count > 0)
                {
                    foreach (var news in categoryItem.ListNews)
                    {
                        news.Status = categoryItem.Status;
                        _entities.SaveChanges();
                    }
                }
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(string name, int parentId,int number, bool state, DateTime date)
        {
            var checkCategory = (from item in _entities.NewsGroups
                                 where item.Parent == parentId && item.Name == name
                                 select item
               ).FirstOrDefault();
            if (checkCategory == null)
            {
                var newsCategory = new NewsGroups
                {
                    Name = name,
                    Parent = parentId,
                    Number = number,
                    Status = state,
                    Date = date
                };
                _entities.NewsGroups.Add(newsCategory);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }
        public string Update(NewsGroups category)
        {
            var newsCategory = (from item in _entities.NewsGroups
                                where item.ID == category.ID
                                select item
               ).FirstOrDefault();
            if (newsCategory != null)
            {
                newsCategory.Name = category.Name;
                newsCategory.Parent = category.Parent;
                newsCategory.Number = category.Number;
                newsCategory.Status = category.Status;
                newsCategory.Date = category.Date;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }
        public string Update(int id, string name, int parentId, int number, bool state, DateTime createdDate)
        {
            var newsCategory = (from item in _entities.NewsGroups
                                where item.ID == id
                                select item
             ).FirstOrDefault();
            if (newsCategory != null)
            {
                newsCategory.Name = name;
                newsCategory.Parent = parentId;
                newsCategory.Number = number;
                newsCategory.Status = state;
                newsCategory.Date = createdDate;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";

        }


        public string Insert(NewsGroups category)
        {
            var checkCategory = (from item in _entities.NewsGroups
                where item.Parent == category.Parent && item.Name == category.Name
                                 select item
                ).FirstOrDefault();
            if (checkCategory == null)
            {
                var newsCategory = new NewsGroups
                {
                    Name = category.Name,
                    Parent = category.Parent,
                    Number = category.Number,
                    Status = category.Status,
                    Date = category.Date
                };
                _entities.NewsGroups.Add(newsCategory);
                _entities.SaveChanges();
                return "ok";
            }
            return "exists";
        }


        public List<NewsGroups> GetListRoot()
        {
            return (from item in _entities.NewsGroups
                where item.Parent == 0
                    orderby  item.ID descending 
                select item
                ).ToList();
        }
        public List<NewsGroups> GetListParent(int rootId = 0)
        {
            
            return rootId==0? 
                 new List<NewsGroups>() : 
                (from item in _entities.NewsGroups
                    where item.Parent == rootId
                 orderby item.ID descending
                    select item
                ).ToList();
        }
        public List<NewsGroups> GetList(string keyword = "", int rootId = 0, int parentId = 0, int childId = 0, int page = 1, int pageSize = 10)
        {
            var listGroup = (from newsGroup in _entities.NewsGroups.AsEnumerable()
                             orderby newsGroup.ID descending
                             select newsGroup);
            //Loc theo tu khoa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var keywordLower = keyword.ToLower();
                listGroup = (from newsGroup in listGroup
                             where newsGroup.Name.ToLower().Contains(keywordLower)
                             orderby newsGroup.ID descending
                             select newsGroup);
            }
            //Loc theo childId
            if (childId > 0)
            {
                return (from newsGroup in listGroup
                    where newsGroup.ID == childId
                    orderby newsGroup.ID descending
                    select newsGroup).ToList();
            }
            //Loc theo ParentId
            if (parentId > 0)
            {
              return (from newsGroup in listGroup
                             where newsGroup.Parent == parentId
                             orderby newsGroup.ID descending
                             select newsGroup).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            //Loc theo RootId
            if (rootId > 0)
            {
                var listParent = (from newsGroup in listGroup
                                  where newsGroup.Parent == rootId
                                  orderby newsGroup.ID descending
                                  select newsGroup.ID).ToList();
                if (listParent.Count > 0)
                {
                    var listParentString = string.Join(",", listParent.ToArray());
                    listGroup = (from newsGroup in listGroup.AsEnumerable()
                        where newsGroup.Parent>0 &&((newsGroup.Parent == rootId) ||(listParentString.Contains(newsGroup.Parent + ",")
                              || listParentString.Contains("," + newsGroup.Parent)) ) 
                        orderby newsGroup.ID descending
                        select newsGroup);
                }
                else
                {
                    return new List<NewsGroups>();
                }
             
            }
            return listGroup.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }


        public NewsGroups Find(int id)
        {
            return (from item in _entities.NewsGroups
                where item.ID == id
                select item
                ).FirstOrDefault();
        }
       
        
        public string Delete(int id)
        {
            var categoryItem =  (
                 from item in _entities.NewsGroups.Include("ListNews")
                 where item.ID == id
                 select item
                ).FirstOrDefault();
            if (categoryItem != null)
            {
               
                if (categoryItem.ListNews != null && categoryItem.ListNews.Count > 0)
                {
                    foreach (var news in categoryItem.ListNews)
                    {
                        _entities.News.Remove(news);
                        _entities.SaveChanges();
                    }
                }
                _entities.NewsGroups.Remove(categoryItem);
                _entities.SaveChanges();
                var listSubCategory = (
                     from item in _entities.NewsGroups.Include("ListNews")
                     where item.ID == categoryItem.ID
                     select item
                ).ToList();
                if (listSubCategory.Count >0)
                {

                    foreach (var subCategory in listSubCategory)
                    {
                        if (subCategory.ListNews != null && subCategory.ListNews.Count > 0)
                        {
                            foreach (var news in subCategory.ListNews)
                            {
                                _entities.News.Remove(news);
                                _entities.SaveChanges();
                            }
                        }
                        _entities.NewsGroups.Remove(subCategory);
                        _entities.SaveChanges();
                    }
                }
                return "ok";
            }
            return "not_exists";

        }

       

       
        public List<NewsGroups> GetList(int page = 1, int pageSize = 10)
        {
           return (from category in _entities.NewsGroups
             orderby category.Date descending
             select category).ToList();
        }

    }
}