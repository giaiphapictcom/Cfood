using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Data
{
    public interface CouponInterfaceRepository
    {
        string Insert(string image, string title, string summary, string url);
    }

    public class CouponRepository : CouponInterfaceRepository
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

        public CouponRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }


        public string Insert(string image, string title, string summary, string url)
        {
            try
            {
                var banner = new AffiliateBanner
                {
                    image = image,
                    title = title,
                    summary = summary,
                    url = url,

                    created = DateTime.Now
                };
                entities.AffiliateBanner.Add(banner);
                entities.SaveChanges();

                return banner.ID.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }

        public int PageSize = 20;
        public CouponsPage GetItemsPage(int PageCurrent = 0)
        {
            CouponsPage ModelPage = new CouponsPage();
            try
            {
                var items = from p in entities.CounponTbl
                               orderby p.ID descending
                               select p;

                ModelPage.Total = items.Count();
                ModelPage.Page = PageCurrent;
                ModelPage.Coupons = items.Skip((PageCurrent - 1) * PageSize).Take(PageSize).ToList();
                return ModelPage;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

    }
}
