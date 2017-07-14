using System;
using System.Linq;
using V308CMS.Data.Helpers;

namespace V308CMS.Data
{
    public interface BannerInterfaceRepository
    {
        string Insert(string image, string title, string summary, string url);
    }

    public class BannerRepository : BannerInterfaceRepository
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
        public int PageSize = 10;

        public BannerRepository(V308CMSEntities mEntities)
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

        public string InsertObject(AffiliateBanner data)
        {
            try
            {
                using (var entities = new V308CMSEntities()) {
                    var check = (from b in entities.AffiliateBanner where b.ID == data.ID select b).FirstOrDefault();
                    if (check != null)
                    {
                        return Result.Exists;
                    }


                    data.created = DateTime.Now;
                    entities.AffiliateBanner.Add(data);
                    entities.SaveChanges();
                }

                    return Result.Ok;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public AffiliateBannerPage GetItemsPage(int PageCurrent = 0)
        {
            AffiliateBannerPage ModelPage = new AffiliateBannerPage();
            using (var entities = new V308CMSEntities())
            {
                try
                {
                    var items = from p in entities.AffiliateBanner
                                orderby p.ID descending
                                select p;

                    ModelPage.Total = items.Count();
                    ModelPage.Page = PageCurrent;
                    ModelPage.Banners = items.Skip((PageCurrent - 1) * PageSize).Take(PageSize).ToList();
                   
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }

            return ModelPage;

        }


    }
}
