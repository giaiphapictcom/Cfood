using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface IWebsiteRequestRespository
    {
        string ChangeStatus(int id, int status=0);
        List<WebsiteRequest> GetList(bool status, int limit = 1);
    }
    public class WebsiteRequestRepository : IBaseRespository<WebsiteRequest>, IWebsiteRequestRespository
    {
        public WebsiteRequest Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from s in entities.WebsiteRequestTbl
                        where s.id == id
                        select s).FirstOrDefault();
            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from s in entities.WebsiteRequestTbl
                            where s.id == id
                            select s).FirstOrDefault();
                if (item != null)
                {
                    entities.WebsiteRequestTbl.Remove(item);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Update(WebsiteRequest data)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from banner in entities.WebsiteRequestTbl
                                    where banner.id == data.id
                                    select banner).FirstOrDefault();
                if (item != null)
                {
                    item.domain = data.domain;

                    item.email = data.email;
                    item.mobile = data.mobile;
                    item.address = data.address;
                    item.content = data.content;
                    item.status = data.status;

                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Insert(WebsiteRequest data)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from c in entities.WebsiteRequestTbl
                                    where c.domain == data.domain
                                    select c
                    ).FirstOrDefault();
                if (item == null)
                {
                    try
                    {
                        entities.WebsiteRequestTbl.Add(data);
                        entities.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Console.Write(dbEx);
                    }

                    return Data.Helpers.Result.Ok;
                }
                return Data.Helpers.Result.Exists;
            }

        }
        public List<WebsiteRequest> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from banner in entities.WebsiteRequestTbl
                        select banner
                    ).ToList();
            }

        }

        public string ChangeStatus(int id, int status=0)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerStatus = (from banner in entities.WebsiteRequestTbl
                                    where banner.id== id
                                    select banner
               ).FirstOrDefault();
                if (bannerStatus != null)
                {
                    bannerStatus.status = status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }
        public List<WebsiteRequest> GetList(bool status = true, int limit = -1)
        {
            var sites = new List<WebsiteRequest>();
            using (var entities = new V308CMSEntities())
            {
                try
                {
                    IQueryable<WebsiteRequest> items = entities.WebsiteRequestTbl;

                    if (items.Count() > 0)
                    {
                        if (limit > 0)
                        {
                            sites = items.Take(limit).ToList();
                        }
                        else {
                            sites = items.ToList();
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Console.Write(dbEx);
                }

            }
            return sites;
        }
    }
}
