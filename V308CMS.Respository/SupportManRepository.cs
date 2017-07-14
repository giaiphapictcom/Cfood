using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface ISupportManRespository
    {
        string ChangeStatus(int id);
        List<SupportMan> GetList(bool status, string site = "", int limit = 1);
    }
    public class SupportManRepository : IBaseRespository<SupportMan>, ISupportManRespository
    {
        public SupportMan Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from s in entities.SupportManTbl
                        where s.id == id
                        select s).FirstOrDefault();
            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from s in entities.SupportManTbl
                                    where s.id == id
                                    select s).FirstOrDefault();
                if (item != null)
                {
                    entities.SupportManTbl.Remove(item);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Update(SupportMan data)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerUpdate = (from banner in entities.SupportManTbl
                                    where banner.id == data.id
                                    select banner).FirstOrDefault();
                if (bannerUpdate != null)
                {
                    bannerUpdate.Name = data.Name;
                    
                    bannerUpdate.Status = data.Status;
                    bannerUpdate.Created = data.Created;
                    bannerUpdate.Updated = data.Updated;
                    bannerUpdate.Site = data.Site;
   
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Insert(SupportMan data)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerInsert = (from c in entities.SupportManTbl
                                    where c.Name == data.Name
                                    select c
                    ).FirstOrDefault();
                if (bannerInsert == null)
                {
                    try
                    {
                        entities.SupportManTbl.Add(data);
                        entities.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Console.Write(dbEx);
                    }

                    return "ok";
                }
                return "not_exists";
            }

        }
        public List<SupportMan> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from banner in entities.SupportManTbl
                        orderby banner.Updated descending
                        select banner
                    ).ToList();
            }

        }

        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerStatus = (from banner in entities.Banner
                                    where banner.Id == id
                                    select banner
               ).FirstOrDefault();
                if (bannerStatus != null)
                {
                    bannerStatus.Status = !bannerStatus.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }
        public List<SupportMan> GetList(bool status=true, string site = "", int limit = 1)
        {
            var banners = new List<SupportMan>();
            using (var entities = new V308CMSEntities())
            {
                try
                {
                    IQueryable<SupportMan> items = entities.SupportManTbl;
                    if (site == Data.Helpers.Site.home)
                    {
                        items = items.Where(b => b.Site == site || b.Site == "" || b.Site == null || b.Site == "1");
                    }
                    else
                    {
                        items = items.Where(b => b.Site == site.Trim());
                    }


                    if (items.Count() > 0)
                    {
                        banners = items.OrderBy(b => b.Order).ToList();
                        if (limit > 0)
                        {
                            banners = items.Take(limit).ToList();
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Console.Write(dbEx);
                }

            }
            return banners;
        }
    }
}
