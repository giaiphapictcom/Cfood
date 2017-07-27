using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data.Models;
using V308CMS.Common;
using System.Data.Entity;



namespace V308CMS.Data
{
    public interface LinkInterfaceRepository
    {
        string Insert(string url, int uid, string source, string taget, string name, string summary);
    }

    public class LinkRepository : LinkInterfaceRepository
    {
        private V308CMSEntities entities;
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
        public LinkRepository(V308CMSEntities mEntities = null)
        {
            if (mEntities == null) {
                mEntities = new V308CMSEntities();
            }
            this.entities = mEntities;
        }


        public string Insert(string url, int uid, string source = "", string taget = "", string name = "", string summary = "")
        {
            try
            {
                var link = new AffiliateLink
                {
                    url = url,
                    code = HashCode(),
                    source = source,
                    taget = taget,
                    name = name,
                    summary = summary,
                    created = DateTime.Now,
                    created_by = uid
                };
                entities.AffiliateLink.Add(link);
                entities.SaveChanges();

                return link.ID.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        private string HashCode(string code = "") {
            if (code.Length < 1) {
                code = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToUpper();
            }
            var check_exit = from l in entities.AffiliateLink
                             where l.code.ToUpper().Equals(code)
                             select l;
            return check_exit.Count() < 1 ? code : HashCode();
        }


        public int PageSize = 20;
        public int itemTotal = 0;
        public List<AffiliateLink> GetItems(int pcurrent = 1, int Account = 0, int limit = 10)
        {
            List<AffiliateLink> mList = null;

            //try
            //{
            var links = entities.AffiliateLink.OrderBy(l => l.ID).Select(l => l);
            links = links.Where(l => l.created_by == Account);
            itemTotal = links.Count();
            if (limit > 0)
            {
                    
                mList = links.Skip((pcurrent - 1) * PageSize).Take(PageSize).ToList();
            }
            else {
                mList = links.ToList();
            }

                
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex);
            //    throw;
            //}
            return mList;
        }

        public int GetItemsTotal()
        {
            try
            {
                var products = from p in entities.AffiliateLink
                               orderby p.ID descending
                               select p;
                return products.Count();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public static int LinkCount(int uid = 0) {
            int count = 0;
            using (var entities = new V308CMSEntities())
            {
                var links = entities.AffiliateLink.Where(l => l.created_by == uid);
                count = links.Count();
            }
            return count;
        }

        static int MinuteLimitUpdateView = 3;
        public void UpdateClick(int link_id)
        {
            using (var entiry = new V308CMSEntities())
            {
                DateTime now = DateTime.Now;
                var click_time = entiry.AffilateLinkClickTbl
                            .Where(t => t.updated.Year == now.Year && t.updated.Month == now.Month && t.updated.Day == now.Day)
                            .Where(t => t.link_id == link_id)
                            .OrderByDescending(t => t.updated)
                            .Select(t => t).FirstOrDefault();
                if (click_time != null)
                {
                    long time_diff = now.toUnixTime() - click_time.updated.toUnixTime();
                    if (time_diff > 60 * MinuteLimitUpdateView)
                    {
                        click_time.count++;
                        click_time.updated = DateTime.Now;
                        entiry.SaveChanges();
                    }
                }
                else
                {
                    AddClick(link_id);
                }
            }
        }

        public void AddClick(int link_id)
        {
            using (var entiry = new V308CMSEntities())
            {
                var click_new = new AffilateLinkClick
                {
                    updated = DateTime.Now,
                    created = DateTime.Now,
                    link_id = link_id,
                    count = 1
                };
                entiry.AffilateLinkClickTbl.Add(click_new);
                entiry.SaveChanges();
            }
        }
    }
}
