using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

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
        public LinkRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }


        public string Insert(string url, int uid , string source = "", string taget = "", string name = "", string summary = "")
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
            var check_exit =  from l in entities.AffiliateLink
                              where l.code.ToUpper().Equals(code)
                             select l;
            return check_exit.Count() < 1 ? code : HashCode();
        }


        public int PageSize = 20;
        public List<AffiliateLink> GetItems(int pcurrent = 1)
        {
            List<AffiliateLink> mList = null;
            try
            {
                var links = from l in entities.AffiliateLink
                               orderby l.ID descending
                               select l;
                mList = links.Skip((pcurrent - 1) * PageSize)
                         .Take(PageSize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
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


    }
}
