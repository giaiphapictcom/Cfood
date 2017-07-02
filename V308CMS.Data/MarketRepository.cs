using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class MarketRepository
    {
        public MarketRepository()
        {

        }

        public Market LayTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Market
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public Market LayTheoUserId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Market
                        where p.UserId == pId
                        select p).FirstOrDefault();
            }
        }
        public Market getByName(string pName)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Market
                        where p.UserName.Equals(pName)
                        select p).FirstOrDefault();
            }
        }
        public List<Market> getAll(int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Market
                        where p.Status == true
                        select p).Take(psize).ToList();
            }
        }
        public List<Market> getMarketByType(int psize, int ptype)
        {
            using (var entities = new V308CMSEntities())
            {
                if (ptype == 0)
                {
                    return (from p in entities.Market
                            where p.Status == true
                            select p).Take(psize).ToList();
                }
                return (from p in entities.Market
                        where p.Status == true && p.Role == ptype
                        select p).Take(psize).ToList();

            }
        }
        public List<Market> LayMarketTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.Market
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return (from p in entities.Market
                        where p.Role == pType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                          .Take(psize).ToList();
            }
        }
        public List<Market> SearchMarketTheoTrangAndType(int pcurrent, int psize, string pkey)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Market
                        where p.UserName.ToLower().Trim().Contains(pkey.Trim())
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
            }
        }
        public List<MarketProductType> getAllMarketProductType(int pMarketId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.MarketProductType
                        where p.MarketId == pMarketId
                        select p).ToList();
            }
        }
        public List<MarketProductType> getMarketProductTypeByProductType(int pProductType)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.MarketProductType
                        where p.Parent == pProductType
                        select p).ToList();
            }
        }
        public List<MarketProductType> getMarketProductTypeByProductType2(int pProductType)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.MarketProductType
                        where p.Parent == pProductType
                        select p).ToList();
            }
        }
    }
}