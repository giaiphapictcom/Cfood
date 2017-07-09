using System;
using System.Linq;
using V308CMS.Data.Helpers;
using System.Collections.Generic;
using V308CMS.Data;

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

        public CouponRepository(V308CMSEntities mEntities = null)
        {
            if (mEntities == null) {
                mEntities = new V308CMSEntities();
            }
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

        public string InsertObject(Counpon data)
        {
            try
            {
                var check = (from c in entities.CounponTbl
                             where c.code.ToLower() == data.code.ToLower()
                             select c
                    ).FirstOrDefault();
                if (check != null)
                {
                    return Result.Exists;
                }


                if (data.productcode.Length > 0)
                {
                    data.type = "product";
                }
                else {
                    data.type = "order";
                }
                data.created = DateTime.Now;
                entities.CounponTbl.Add(data);
                entities.SaveChanges();

                return Result.Ok;

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


        /*
         * 20170708 add by QuanNH
         */
        public List<Counpon> GetList(int page = 1, int pageSize = 10, int status = -1)
        {
            List<Counpon> vouchers = new List<Counpon>();
            using (var entities = new V308CMSEntities())
            {

                var items = entities.CounponTbl.Select(c => c);

                if (status > 0)
                {
                    items = items.Where(c => c.status == 1);
                }
                else if (status == 0)
                {
                    items = items.Where(c => c.status == 0);
                }

                vouchers = items.OrderBy(c=>c.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            return vouchers;

        }

        public Counpon Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.CounponTbl.FirstOrDefault(c => c.ID == id);
            }

        }

        public string UpdateObject(Counpon data)
        {
            using (var entities = new V308CMSEntities())
            {
                var check = (from c in entities.CounponTbl
                             where c.code.ToLower() == data.code.ToLower() && c.ID != data.ID
                             select c
                    ).FirstOrDefault();
                if (check != null)
                {
                    return Result.Exists;
                }

                var item = (from c in entities.CounponTbl
                            where c.ID == c.ID
                            select c).FirstOrDefault();
                if (item != null)
                {

                    item.title = data.title;
                    item.productcode = data.productcode;
                    item.image = data.image;
                    item.site = data.site;
                    item.code = data.code;
                    item.type = data.type;
                    item.summary = data.summary;
                    item.content = data.content;
                    item.start_date = DateTime.Parse(data.start_date.ToString());
                    item.end_date = DateTime.Parse(data.end_date.ToString());
                    item.status = data.status;

                    if (item.productcode.Length > 0)
                    {
                        item.type = "product";
                    }
                    else
                    {
                        item.type = "order";
                    }
                    item.update_date = DateTime.Now;

                    entities.SaveChanges();
                    return Data.Helpers.Result.Ok;
                }
                return Data.Helpers.Result.NotExists;
            }


        }
        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var item = (from v in entities.CounponTbl where v.ID == id select v).FirstOrDefault();
                if (item != null)
                {
                    item.status = item.status > 1 ? 0 : 1;
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;
            }


        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var voucher = (from c in entities.CounponTbl
                                where c.ID == id
                                select c).FirstOrDefault();
                if (voucher != null)
                {
                    entities.CounponTbl.Remove(voucher);
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;

            }

        }
    }
}
