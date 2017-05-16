using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class ProductRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public ProductRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public ProductRepository(V308CMSEntities mEntities)
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

        #region get Product by
        public List<Product> LaySanPhamKhuyenMai(int pcurrent = 1, int psize = 5)
        {
            List<Product> mList = null;
            try
            {
                mList = (from p in entities.Product
                         where p.SaleOff > 0 & p.Status == true
                         orderby p.SaleOff descending
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
        public List<Product> getProductsRecommend(int psize = 5)
        {
            List<Product> mList = null;
            try
            {
                mList = (from p in entities.Product
                         where p.SaleOff > 0 & p.Status == true
                         orderby p.SaleOff descending
                         select p)
                         .Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> getProductsRandom(int psize = 5,int category_id=0)
        {
            try
            {
                var products = from p in entities.Product
                               where p.Status == true
                               
                               select p;
                if (category_id > 0) { 
                    products = from p in products where p.Type == category_id select p;
                }
                return products.ToList().OrderBy(x => Guid.NewGuid()).Take(psize).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public int getProductTotal(int pType, string pLevel)
        {
            int[] mIdGroup;
            int countTotal = 0;
            if (pType > 0)
            {
                mIdGroup = (from p in entities.ProductType
                            where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                            select p.ID).ToArray();

                var  mList = from p in entities.Product
                         where mIdGroup.Contains(p.Type.Value)
                        
                         select p;

                countTotal = mList.Count();
            }
            return countTotal;
        }
        public List<Product> LayTheoTrangAndType(int pcurrent, int psize, int pType, string pLevel)
        {
            List<Product> mList = null;
            int[] mIdGroup;
            try
            {
                if (pType > 0)
                {
                    mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();

                    mList = (from p in entities.Product
                             where mIdGroup.Contains(p.Type.Value)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pType == 0)
                {
                    mList = (from p in entities.Product
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        #endregion


        public Product LayTheoId(int pId)
        {
            Product mProduct = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProduct = (from p in entities.Product
                            where p.ID == pId
                            select p).FirstOrDefault();
                return mProduct;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> LayTheoTrang(int pcurrent, int psize)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
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
        public List<Product> getProductWithSizePageMarketId(int pcurrent, int psize, int pMarketId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.MarketId == pMarketId
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
        
        public List<Product> getProductByIdList(int[] pId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where pId.Contains(p.ID)
                         orderby p.ID descending
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> getByTypeAndNameAndMarket(int pcurrent, int psize, int pType, int pMarket, string pName, string pLevel)
        {
            List<Product> mList = null;
            int[] mIdGroup;
            try
            {
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    if (pMarket == 0)
                    {
                        if (pName.Length > 0)
                        {
                            mList = (from p in entities.Product
                                     where mIdGroup.Contains(p.Type.Value) && p.Name.ToLower().Trim().Contains(pName.Trim())
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                    .Take(psize).ToList();
                        }
                        else
                        {
                            mList = (from p in entities.Product
                                     where mIdGroup.Contains(p.Type.Value)
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                        }
                    }
                    else
                    {

                        if (pName.Length > 0)
                        {
                            mList = (from p in entities.Product
                                     where mIdGroup.Contains(p.Type.Value) && p.MarketId == pMarket && p.Name.ToLower().Trim().Contains(pName.Trim())
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                        .Take(psize).ToList();
                        }
                        else
                        {
                            mList = (from p in entities.Product
                                     where mIdGroup.Contains(p.Type.Value) && p.MarketId == pMarket
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                           .Take(psize).ToList();
                        }
                    }
                    /////////////////////////////////////////////////////////
                }
                else if (pType == 0)
                {
                    if (pMarket == 0)
                    {
                        if (pName.Length > 0)
                        {
                            mList = (from p in entities.Product
                                     where p.Name.ToLower().Trim().Contains(pName.Trim())
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                    .Take(psize).ToList();
                        }
                        else
                        {
                            mList = (from p in entities.Product
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                       .Take(psize).ToList();
                        }
                    }
                    else
                    {

                        if (pName.Length > 0)
                        {
                            mList = (from p in entities.Product
                                     where p.MarketId == pMarket && p.Name.ToLower().Trim().Contains(pName.Trim())
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                        .Take(psize).ToList();
                        }
                        else
                        {
                            mList = (from p in entities.Product
                                     where p.MarketId == pMarket
                                     orderby p.ID descending
                                     select p).Skip((pcurrent - 1) * psize)
                                           .Take(psize).ToList();
                        }
                    }
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> getByPageSizeMarketId(int pcurrent, int psize, int pType, int pMarketId, string pLevel)
        {
            List<Product> mList = null;
            try
            {
                //
                int[] mIdGroup;
                try
                {
                    if (pType > 0)
                    {
                        //lay tat ca cac ID cua group theo Level
                        mIdGroup = (from p in entities.ProductType
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.Product
                                 where p.MarketId == pMarketId && mIdGroup.Contains(p.Type.Value)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList();
                    }
                    else if (pType == 0)
                    {
                        mList = (from p in entities.Product
                                 where p.MarketId == pMarketId
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                .Take(psize).ToList();
                    }
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> LayTheoTrangAndTypeAndMarketId(int pcurrent, int psize, int pType, string pLevel, int pMarketId)
        {
            List<Product> mList = null;
            int[] mIdGroup;
            try
            {
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Product
                             where p.MarketId == pMarketId && mIdGroup.Contains(p.Type.Value)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pType == 0)
                {
                    mList = (from p in entities.Product
                             where p.MarketId == pMarketId
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> getByPageSizeMarketId(int pcurrent, int psize, int pMarketId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.MarketId == pMarketId
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
        public List<Product> LayTheoTrangVaType(int pcurrent, int psize, int pType)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Type == pType
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

        public List<Product> LayDanhSachSanPhamLienQuan(int pType, int pSize)
        {
            List<Product> mList = null;
            string mGuid = Guid.NewGuid().ToString();
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Type == pType
                         orderby mGuid
                         select p)
                         .Take(pSize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> TimSanPhamTheoTen(int pcurrent, int psize, string pValue)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Name.ToLower().Trim().Contains(pValue.Trim())
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
        public List<Product> TimSanPhamTheoGia(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Price > pValue1 && p.Price < pValue2 && p.Type == pGroupId
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
        public List<Product> TimSanPhamTheoHangSanXuat(int pcurrent, int psize, int pValue1, int pGroupId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Manufacturer == pValue1 && p.Type == pGroupId
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
        public List<Product> TimSanPhamTheoCoManHinh(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Size > pValue1 && p.Size < pValue2 && p.Type == pGroupId
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
        public List<Product> TimSanPhamTheoCongSuat(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Power > pValue1 && p.Power < pValue2 && p.Type == pGroupId
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
        public List<Product> TimSanPhamTheoGroup(int pcurrent, int psize, int pValue1)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Group == pValue1
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
        public List<Product> LaySanPhamBanChay(int pcurrent, int psize)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Buy > 0
                         orderby p.Buy descending
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
        public List<Product> getBestBuyWithType(int pcurrent, int psize, int pType, string pLevel)
        {
            List<Product> mList = null;
            try
            {
                //
                int[] mIdGroup;
                try
                {
                    if (pType > 0)
                    {
                        //lay tat ca cac ID cua group theo Level
                        mIdGroup = (from p in entities.ProductType
                                    where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                    select p.ID).ToArray();
                        //lay danh sach tin moi dang nhat
                        mList = (from p in entities.Product
                                 where p.Buy > 0 && mIdGroup.Contains(p.Type.Value)
                                 orderby p.Buy descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList();
                    }
                    else if (pType == 0)
                    {
                        mList = (from p in entities.Product
                                 where p.Buy > 0
                                 orderby p.Buy descending
                                 select p).Skip((pcurrent - 1) * psize)
                                .Take(psize).ToList();
                    }
                    return mList;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        
        public List<Product> LaySanPhamMoi(int pcurrent, int psize)
        {
            List<Product> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.Product
                         where p.Hot == true
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

        public List<Product> getProductsLastest(int psize)
        {
            List<Product> mList = null;
            try
            {
                mList = (from p in entities.Product
                         where p.Hot == true
                         orderby p.ID descending
                         select p).Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Product> getProductsByCategory(int CategoryID, int Limit = 20, int Page =1)
        {
            List<Product> mList = null;
            try
            {
                var CategoryIDs = (from c in entities.ProductType
                                   where c.Parent == CategoryID
                                   select c.ID).ToList();
                CategoryIDs.Add(CategoryID);

                var items = (from p in entities.Product
                            where CategoryIDs.Any(c => c == p.Type)
                             orderby p.ID descending
                             select p).Skip((Page - 1) * Limit);
                mList = items.Take(Limit).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public int getProductTotalByCategory(int CategoryID)
        {
            int countTotal = 0;
            if (CategoryID > 0)
            {
                var CategoryIDs = (from c in entities.ProductType
                                   where c.Parent == CategoryID
                                   select c.ID).ToList();
                CategoryIDs.Add(CategoryID);

                var mList = from p in entities.Product
                            where CategoryIDs.Any(c => c == p.Type)
                            select p;

                countTotal = mList.Count();
            }
            return countTotal;
        }

        public List<ProductType> LayNhomSanPhamAll()
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         orderby p.ID descending
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> getProductTypeParent()
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == 0
                         orderby p.Number ascending
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> getProductTypeByParent(int pParent, int limit = 10)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == pParent
                         orderby p.Number descending
                         select p).Take(limit).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> getProductTypeByParentSize(int pParent, int psize)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == pParent
                         select p).Take(psize).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> LayNhomSanPhamAllOrderBy()
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         orderby p.Number descending
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> LayNhomSanPhamTheoTrang(int pcurrent, int psize)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
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
        public List<ProductType> LayNhomSanPhamTheoTrangVaSeri(int pcurrent, int psize, int pParent)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == pParent
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

        public List<ProductType> getProductTypeByProductType(int pProductType, int Limit=10)
        {
            List<ProductType> mList = null;
            try
            {
                var categorys = from p in entities.ProductType
                         where p.Parent == pProductType
                         orderby p.Number ascending
                         select p;
                mList = categorys.Take(Limit).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        
        
        public List<ProductType> getProductTypeByProductType2(int pProductType)
        {
            List<ProductType> mList = null;
            //ProductType mProductType;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == pProductType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public ProductType LayLoaiSanPhamTheoId(int pId)
        {
            ProductType mProductType = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProductType = (from p in entities.ProductType
                                where p.ID == pId
                                select p).FirstOrDefault();
                return mProductType;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public ProductDistributor LayProductDistributorTheoId(int pId)
        {
            ProductDistributor mProductDistributor = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProductDistributor = (from p in entities.ProductDistributor
                                       where p.ID == pId
                                       select p).FirstOrDefault();
                return mProductDistributor;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductDistributor> LayProductDistributorTheoTrang(int pcurrent, int psize)
        {
            List<ProductDistributor> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductDistributor
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
        public List<ProductDistributor> LayProductDistributorAll()
        {
            List<ProductDistributor> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductDistributor
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public ProductManufacturer LayProductManufacturerTheoId(int pId)
        {
            ProductManufacturer mProductManufacturer = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProductManufacturer = (from p in entities.ProductManufacturer
                                        where p.ID == pId
                                        select p).FirstOrDefault();
                return mProductManufacturer;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductManufacturer> LayProductManufacturerTheoTrang(int pcurrent, int psize)
        {
            List<ProductManufacturer> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductManufacturer
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
        public List<ProductManufacturer> LayProductManufacturerAll()
        {
            List<ProductManufacturer> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductManufacturer
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public ProductType LayProductTypeTheoId(int pId)
        {
            ProductType mProductType = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProductType = (from p in entities.ProductType
                                where p.ID == pId
                                select p).FirstOrDefault();
                return mProductType;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> LayProductTypeTheoTrang(int pcurrent, int psize)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
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
        public List<ProductType> LayProductTypeTheoTrangAndType(int pcurrent, int psize, int pType, string pLevel)
        {
            List<ProductType> mList = null;
            int[] mIdGroup;
            try
            {
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.ProductType
                             where mIdGroup.Contains(p.ID)
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pType == 0)
                {
                    mList = (from p in entities.ProductType
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> LayProductTypeAll()
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public List<ProductAttribute> LayProductAttributeTheoIDProduct(int pId)
        {
            List<ProductAttribute> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductAttribute
                         where p.ProductID == pId
                         select p)
                       .ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductImage> LayProductImageTheoIDProduct(int pId)
        {
            List<ProductImage> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductImage
                         where p.ProductID == pId
                         select p)
                       .ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductType> LayProductTypeTheoParentId(int ParentId)
        {
            List<ProductType> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductType
                         where p.Parent == ParentId
                         orderby p.Number descending
                         select p).ToList();
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public ProductOrder LayProductOrderTheoId(int pId)
        {
            ProductOrder mProductOrder = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mProductOrder = (from p in entities.ProductOrder
                                 where p.ID == pId
                                 select p).FirstOrDefault();
                return mProductOrder;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductOrder> LayOrderTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            List<ProductOrder> mList = null;
            try
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.ProductOrder
                             where p.Status == 0
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else if (pType == 1)
                {
                    mList = (from p in entities.ProductOrder
                             where p.Status == 1
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                }
                else
                {
                    mList = (from p in entities.ProductOrder
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductOrder> LayOrderTheoTrangAndType(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2,int pUserId)
        {
            List<ProductOrder> mList = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mList = (from p in entities.ProductOrder
                         where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
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
        public List<ProductOrder> LayOrderKhachMua1Lan(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            List<ProductOrder> mList = new List<ProductOrder>();
            try
            {
                List<ProductOrder> listProductOrder = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID
                             select p).ToList();
                var table = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID descending
                             group p by p.AccountID into g//Nhóng theo Mã hàng
                            where g.Count() == 1
                            select new {ID = g.Key});
                foreach (var obj in table)
                { 
                    int IDDistin = obj.ID.GetValueOrDefault();
                    ProductOrder mProductOrder = new ProductOrder();
                    mProductOrder = listProductOrder.Where(x=>x.ID == IDDistin).FirstOrDefault();
                    mList.Add(mProductOrder);
                }
                
                //lay danh sach tin moi dang nhat
                //mList = (from p in entities.ProductOrder
                //         where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                //         orderby p.ID descending
                //         select p).Skip((pcurrent - 1) * psize)
                //        .Take(psize).ToList();

                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public List<ProductOrder> LayOrderKhachVIP(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            List<ProductOrder> mList = new List<ProductOrder>();
            try
            {
                List<ProductOrder> listProductOrder = (from p in entities.ProductOrder
                                                       where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                                                       orderby p.ID
                                                       select p).ToList();
                var table = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID descending
                             group p by p.AccountID into g//Nhóng theo Mã hàng
                             where g.Count() >= 50
                             select new { AccountID = g.FirstOrDefault().AccountID });
                foreach (var obj in table)
                {
                    int AccountIDDistin = obj.AccountID.GetValueOrDefault();
                    ProductOrder mProductOrder = new ProductOrder();
                    mProductOrder = listProductOrder.Where(x => x.AccountID == AccountIDDistin).FirstOrDefault();
                    mList.Add(mProductOrder);
                }

                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductOrder> LayOrderKhachCu(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            List<ProductOrder> mList = new List<ProductOrder>();
            try
            {
                List<ProductOrder> listProductOrder = (from p in entities.ProductOrder
                                                       where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                                                       orderby p.ID
                                                       select p).ToList();
                var table = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID descending
                             group p by p.AccountID into g//Nhóng theo Mã hàng
                             where g.Count() >= 1
                             select new { AccountID = g.FirstOrDefault().AccountID });
                foreach (var obj in table)
                {
                    int AccountIDDistin = obj.AccountID.GetValueOrDefault();
                    ProductOrder mProductOrder = new ProductOrder();
                    mProductOrder = listProductOrder.Where(x => x.AccountID == AccountIDDistin).FirstOrDefault();
                    mList.Add(mProductOrder);
                }

                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<ProductOrder> LayOrderKhachMoi(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            List<ProductOrder> mList = new List<ProductOrder>();
            try
            {
                List<ProductOrder> listProductOrder = (from p in entities.ProductOrder
                                                       where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                                                       orderby p.ID
                                                       select p).ToList();
                var table = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID descending
                             group p by p.AccountID into g//Nhóng theo Mã hàng
                             where g.Count() >= 50
                             select new { AccountID = g.FirstOrDefault().AccountID });
                foreach (var obj in table)
                {
                    int AccountIDDistin = obj.AccountID.GetValueOrDefault();
                    ProductOrder mProductOrder = new ProductOrder();
                    mProductOrder = listProductOrder.Where(x => x.AccountID == AccountIDDistin).FirstOrDefault();
                    mList.Add(mProductOrder);
                }

                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public List<Brand> getRandomBrands(int CategoryID, int Limit = 1)
        {

            List<Brand> brands = new List<Brand>();
            try
            {

                var items = from b in entities.Brand where b.category_default == CategoryID & b.status.Equals(1)
                            select b;
                brands = items.ToList().OrderBy(x => Guid.NewGuid()).Take(Limit).ToList();


            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return brands;

        }
    }
}