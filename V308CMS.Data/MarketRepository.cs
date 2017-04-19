using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class MarketRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public MarketRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public MarketRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }

        #endregion
        #region["Vung cac thao tac Dispose"]
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
        #endregion
        public Market LayTheoId(int pId)
        {
            Market mMarket = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mMarket = (from p in entities.Market
                           where p.ID == pId
                           select p).FirstOrDefault();
                return mMarket;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Market LayTheoUserId(int pId)
        {
            Market mMarket = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mMarket = (from p in entities.Market
                           where p.UserId == pId
                           select p).FirstOrDefault();
                return mMarket;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Market getByName(string pName)
        {
            Market mMarket = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mMarket = (from p in entities.Market
                           where p.UserName.Equals(pName)
                           select p).FirstOrDefault();
                return mMarket;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Market> getAll(int psize)
        {
            List<Market> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Market
                         where p.Status == true
                         select p).Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Market> getMarketByType(int psize, int ptype)
        {
            List<Market> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                if (ptype == 0)
                {
                    mList = (from p in entities.Market
                             where p.Status == true
                             select p).Take(psize).ToList();
                }
                else
                {
                    mList = (from p in entities.Market
                             where p.Status == true && p.Role == ptype
                             select p).Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Market> LayMarketTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            List<Market> mList = null;
            try
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Market
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else
                {
                    mList = (from p in entities.Market
                             where p.Role == pType
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Market> SearchMarketTheoTrangAndType(int pcurrent, int psize, string pkey)
        {
            List<Market> mList = null;
            try
            {
                mList = (from p in entities.Market
                         where p.UserName.ToLower().Trim().Contains(pkey.Trim())
                         orderby p.ID descending
                         select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<MarketProductType> getAllMarketProductType(int pMarketId)
        {
            List<MarketProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.MarketProductType
                         where p.MarketId == pMarketId
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<MarketProductType> getMarketProductTypeByProductType(int pProductType)
        {
            List<MarketProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.MarketProductType
                         where p.Parent == pProductType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MarketProductType> getMarketProductTypeByProductType2(int pProductType)
        {
            List<MarketProductType> mList = null;
            //ProductType mProductType;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.MarketProductType
                         where p.Parent == pProductType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}