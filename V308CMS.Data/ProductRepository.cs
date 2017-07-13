using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Data
{
    public class ProductRepository
    {
        public ProductRepository()
        {
        }

        public List<Product> GetListByCategoryId(int categoryId, string[] listFilter, int sort, out int totalRecord, int page = 1,
            int pageSize = 25)
        {
            using (var entities = new V308CMSEntities())
            {
                var listProduct = (from product in entities.Product
                    where product.Status == true && product.Type == categoryId
                    orderby  product.Number, product.Date descending 
                    select product
                    );
                if (listFilter != null && listFilter.Length>0)
                {
                    for (var i = 0; i < listFilter.Length; i += 2)
                    {
                        var typeFilter = int.Parse(listFilter[i]);
                        var valueFilter = listFilter[i + 1];                     
                        switch (typeFilter)
                        {
                            case (int)FilterEnum.ByBrand:
                                int brandIdFilter;
                                int.TryParse(valueFilter, out brandIdFilter);                             
                                listProduct = (from product in listProduct
                                               where product.BrandId == brandIdFilter
                                               orderby product.Number, product.Date descending
                                               select product
                               );
                                break;
                            case (int)FilterEnum.ByManufacturer:                                
                                int manufacturerIdFilter;
                                int.TryParse(valueFilter, out manufacturerIdFilter);
                                listProduct = (from product in listProduct
                                               where product.Manufacturer == manufacturerIdFilter
                                               orderby product.Number, product.Date descending
                                               select product
                                );
                                break;
                            case (int)FilterEnum.ByFromPrice:
                                double fromPriceValue;
                                double.TryParse(valueFilter, out fromPriceValue);
                                if (fromPriceValue > 0)
                                {
                                    listProduct = (from product in listProduct
                                                   where product.Price >= fromPriceValue
                                                   orderby product.Number, product.Date descending
                                                   select product
                                                   );
                                }
                                break;
                            case (int)FilterEnum.ByToPrice:
                                double toPriceValue;
                                double.TryParse(valueFilter, out toPriceValue);
                                if (toPriceValue > 0)
                                {
                                    listProduct = (from product in listProduct
                                                   where product.Price <= toPriceValue
                                                   orderby product.Number, product.Date descending
                                                   select product
                                                   );
                                }
                                break;

                        }
                    }
                }

                switch (sort)
                {
                    case (int)SortEnum.PriceAsc:
                        listProduct = (from product in listProduct                                    
                                       orderby product.Price
                                       select product
                        );
                        break;
                    case (int)SortEnum.PriceDesc:
                        listProduct = (from product in listProduct
                                       orderby product.Number,  product.Price descending 
                                       select product
                        );
                        break;
                    case (int)SortEnum.NameAsc:
                        listProduct = (from product in listProduct
                                       orderby product.Number, product.Name
                                       select product
                        );
                        break;
                    case (int)SortEnum.NameDesc:
                        listProduct = (from product in listProduct
                                       orderby product.Number, product.Name descending 
                                       select product
                        );
                        break;
                    case (int)SortEnum.DateAsc:
                        listProduct = (from product in listProduct
                                       orderby product.Number, product.Date
                                       select product
                        );
                        break;
                    case (int)SortEnum.DateDesc:
                        listProduct = (from product in listProduct
                                       orderby product.Number, product.Date descending 
                                       select product
                        );
                        break;
                    case (int)SortEnum.BestSelling:
                        listProduct = (from product in listProduct
                                       orderby product.Number, product.Buy descending
                                       select product
                        );
                        break;                        
                }
                totalRecord = listProduct.Count();
                return (from product in listProduct.Include("ProductImages")                                                       
                             select product
                   ).Skip((page-1)*pageSize).Take(pageSize).ToList();

            }
        }

        public async Task<List<Product>> GetListByCategoryWithImagesAsync(int categoryId, int limit)
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from product in entities.Product.Include("ProductImages")
                              where product.Status == true && product.Type == categoryId
                              orderby product.Number
                              select product
                    ).Take(limit).ToListAsync();
            }

        }


        #region get Product by
        public List<Product> LaySanPhamKhuyenMai(int pcurrent = 1, int psize = 5)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.SaleOff > 0 & p.Status == true
                        orderby p.SaleOff descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<Product> getProductsRecommend(int psize = 5)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.SaleOff > 0 & p.Status == true
                        orderby p.SaleOff descending
                        select p)
                         .Take(psize).ToList();
            }
        }
        public async Task<List<Product>>  GetProductsRandomAsync(int limit = 5, int categoryId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var products = from p in entities.Product
                               where p.Status == true

                               select p;
                if (categoryId > 0)
                {
                    products = from p in products where p.Type == categoryId select p;
                }
                return await products.OrderBy(x => Guid.NewGuid()).Take(limit).ToListAsync();

            }
        }

        public List<Product> GetProductsBestSeller(int categoryId = 0,int limit = 5)
        {
            using (var entities = new V308CMSEntities())
            {
                if (categoryId == 0)
                {
                    return  (from product in entities.Product
                            where product.Status == true
                            orderby product.Buy
                            select product).Take(limit).ToList();
                }

                return  (from product in entities.Product
                        where product.Status == true && product.Type == categoryId
                        orderby product.Buy
                        select product).Take(limit).ToList();
            }
        }

        public async Task<List<Product>> GetProductsBestSellerAsync(int categoryId = 0, int limit = 5)
        {
            using (var entities = new V308CMSEntities())
            {
                if (categoryId == 0)
                {
                    return await (from product in entities.Product
                            where product.Status == true
                            orderby product.Buy
                            select product).Take(limit).ToListAsync();
                }

                return await (from product in entities.Product
                        where product.Status == true && product.Type == categoryId
                        orderby product.Buy
                        select product).Take(limit).ToListAsync();
            }
        }

        public List<Product> getProductsRandom(int psize = 5, int category_id = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var products = from p in entities.Product
                               where p.Status == true

                               select p;
                if (category_id > 0)
                {
                    products = from p in products where p.Type == category_id select p;
                }
                return products.ToList().OrderBy(x => Guid.NewGuid()).Take(psize).ToList();

            }
        }

        public int getProductTotal(int pType, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                int[] mIdGroup;
                int countTotal = 0;
                if (pType > 0)
                {
                    mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();

                    var mList = from p in entities.Product
                                where mIdGroup.Contains(p.Type.Value)

                                select p;

                    countTotal = mList.Count();
                }
                return countTotal;
            }

        }
        public List<Product> LayTheoTrangAndType(int pcurrent, int psize, int pType, string pLevel, bool includeProductImages = false)
        {
            using (var entities = new V308CMSEntities())
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
                        mList =
                            includeProductImages ?

                                (from p in entities.Product.Include("ProductImages")
                                 where mIdGroup.Contains(p.Type.Value)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                 .Take(psize).ToList() :

                                (from p in entities.Product
                                 where mIdGroup.Contains(p.Type.Value)
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                .Take(psize).ToList();
                    }
                    else if (pType == 0)
                    {
                        mList = includeProductImages ?
                                (from p in entities.Product.Include("ProductImages")
                                 orderby p.ID descending
                                 select p).Skip((pcurrent - 1) * psize)
                                    .Take(psize).ToList()
                                :
                                 (from p in entities.Product
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

        }

        #endregion

        public async Task<Product> FindAsync(int id, bool includeProductImages = false)
        {
            using (var entities = new V308CMSEntities())
            {
                
                return await ( includeProductImages ?(from p in entities.Product.Include("ProductImages")
                        where p.ID == id
                              select p).FirstOrDefaultAsync() :
                              (from p in entities.Product
                               where p.ID == id
                               select p).FirstOrDefaultAsync());
            }
        }
        public Product Find(int pId)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.Product
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public Product LayTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.Product
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public Product GetById(int id, bool includeDetail = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return includeDetail
                 ? (from p in entities.Product.Include("ProductManufacturer").Include("ProductImages")
                    where p.ID == id
                    select p).FirstOrDefault()
                 : (from p in entities.Product
                    where p.ID == id
                    select p).FirstOrDefault();

            }




        }
        public List<Product> LayTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }
        public List<Product> getProductWithSizePageMarketId(int pcurrent, int psize, int pMarketId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.MarketId == pMarketId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }

        public List<Product> getProductByIdList(int[] pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where pId.Contains(p.ID)
                        orderby p.ID descending
                        select p).ToList();
            }
        }
        public List<Product> getByTypeAndNameAndMarket(int pcurrent, int psize, int pType, int pMarket, string pName, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                List<Product> mList = null;
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    var mIdGroup = (from p in entities.ProductType
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
        }
        public List<Product> getByPageSizeMarketId(int pcurrent, int psize, int pType, int pMarketId, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                List<Product> mList = null;
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    var mIdGroup = (from p in entities.ProductType
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

        }

        public List<Product> LayTheoTrangAndTypeAndMarketId(int pcurrent, int psize, int pType, string pLevel,
            int pMarketId)
        {
            using (var entities = new V308CMSEntities())
            {
                List<Product> mList = null;
                int[] mIdGroup;
                if (pType == 0)
                {
                    return (from p in entities.Product
                            where p.MarketId == pMarketId
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
                }
                //lay tat ca cac ID cua group theo Level
                mIdGroup = (from p in entities.ProductType
                            where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                            select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.Product
                        where p.MarketId == pMarketId && mIdGroup.Contains(p.Type.Value)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }

        public List<Product> getByPageSizeMarketId(int pcurrent, int psize, int pMarketId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.MarketId == pMarketId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();

            }
        }
        public List<Product> LayTheoTrangVaType(int pcurrent, int psize, int pType)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.Product
                        where p.Type == pType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
            }
        }

        public async Task<List<Product>> GetListRelativedAsync(
            int id, int categoryId, int limit = 12,
            bool includeProductImages = false)
        {
            using (var entities = new V308CMSEntities())
            {

                string mGuid = Guid.NewGuid().ToString();
                //lay danh sach tin moi dang nhat
                return includeProductImages ? await 
                         (from p in entities.Product.Include("ProductImages")
                          where p.Type == categoryId && p.ID != id
                          orderby mGuid
                          select p)
                              .Take(limit).ToListAsync() :
                              await
                          (from p in entities.Product
                           where p.Type == categoryId && p.ID != id
                           orderby mGuid
                           select p)
                          .Take(limit).ToListAsync();

            }
        }
        public List<Product> LayDanhSachSanPhamLienQuan(int pType, int pSize, bool includeProductImages = false)
        {
            using (var entities = new V308CMSEntities())
            {

                string mGuid = Guid.NewGuid().ToString();
                //lay danh sach tin moi dang nhat
                return includeProductImages ?
                         (from p in entities.Product.Include("ProductImages")
                          where p.Type == pType
                          orderby mGuid
                          select p)
                              .Take(pSize).ToList() :
                          (from p in entities.Product
                           where p.Type == pType
                           orderby mGuid
                           select p)
                          .Take(pSize).ToList();

            }

        }
        public List<Product> TimSanPhamTheoTen(int pcurrent, int psize, string pValue)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Name.ToLower().Trim().Contains(pValue.Trim())
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                          .Take(psize).ToList();
            }
        }

        public List<Product> Search(string keyword,out int totalRecord, int page = 1, int pageSize = 30)
        {
           
            using (var entities = new V308CMSEntities())
            {
                var keywordSearch = keyword.Trim().ToLower();
                var listProduct = (from product in entities.Product
                    where product.Name.ToLower().Trim().Contains(keywordSearch)
                    orderby product.ID descending
                    select product);
                totalRecord = listProduct.Count();
                return listProduct.Include("ProductImages")
                    .OrderByDescending(product=>product.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public List<Product> TimSanPhamTheoGia(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Price > pValue1 && p.Price < pValue2 && p.Type == pGroupId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }
        public List<Product> TimSanPhamTheoHangSanXuat(int pcurrent, int psize, int pValue1, int pGroupId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Manufacturer == pValue1 && p.Type == pGroupId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }
        public List<Product> TimSanPhamTheoCoManHinh(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Size > pValue1 && p.Size < pValue2 && p.Type == pGroupId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();
            }

        }
        public List<Product> TimSanPhamTheoCongSuat(int pcurrent, int psize, int pValue1, int pValue2, int pGroupId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Power > pValue1 && p.Power < pValue2 && p.Type == pGroupId
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();

            }
        }
        public List<Product> TimSanPhamTheoGroup(int pcurrent, int psize, int pValue1)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Group == pValue1
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }
        public async Task<List<Product>>  GetListProductMostAsync(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from p in entities.Product
                        where p.Buy > 0
                        orderby p.Buy descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToListAsync();
            }
        }

        public List<Product> LaySanPhamBanChay(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Buy > 0
                        orderby p.Buy descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();
            }
        }
        public List<Product> getBestBuyWithType(int pcurrent, int psize, int pType, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                List<Product> mList = null;
                if (pType > 0)
                {
                    //lay tat ca cac ID cua group theo Level
                    var mIdGroup = (from p in entities.ProductType
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
        }

        public List<Product> LaySanPhamMoi(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Hot == true
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();
            }
        }
        public async Task<List<Product>>  GetProductsLastestAsync(int limit)
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from p in entities.Product
                        where p.Hot == true
                        orderby p.ID descending
                        select p).Take(limit).ToListAsync();
            }
        }

        public List<Product> getProductsLastest(int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Product
                        where p.Hot == true
                        orderby p.ID descending
                        select p).Take(psize).ToList();
            }
        }
        public List<Product> getProductsByCategory(int CategoryID, int Limit = 20, int Page = 1)
        {
            using (var entities = new V308CMSEntities())
            {             
                var categoryIDs = (from c in entities.ProductType
                                   where c.Parent == CategoryID
                                   select c.ID).ToList();
                categoryIDs.Add(CategoryID);

                return (from p in entities.Product
                        where categoryIDs.Any(c => c == p.Type)
                        orderby p.ID descending
                        select p).Skip((Page - 1) * Limit).Take(Limit).ToList();

            }

        }

        public int getProductTotalByCategory(int CategoryID)
        {
            using (var entities = new V308CMSEntities())
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

        }

        public List<ProductType> LayNhomSanPhamAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        orderby p.ID descending
                        select p).ToList();

            }
        }
        public List<ProductType> getProductTypeParent()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == 0
                        orderby p.Number ascending
                        select p).ToList();
            }
        }
        public List<ProductType> getProductTypeByParent(int pParent, int limit = 10)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.ProductType
                        where p.Parent == pParent
                        orderby p.Number descending
                        select p).Take(limit).ToList();
            }
        }
        public List<ProductType> getProductTypeByParentSize(int pParent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == pParent
                        select p).Take(psize).ToList();
            }
        }
        public List<ProductType> LayNhomSanPhamAllOrderBy()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        orderby p.Number descending
                        select p).ToList();
            }
        }
        public List<ProductType> LayNhomSanPhamTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();

            }
        }
        public List<ProductType> LayNhomSanPhamTheoTrangVaSeri(int pcurrent, int psize, int pParent)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == pParent
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<ProductType> getProductTypeByProductType(int pProductType, int Limit = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == pProductType
                        orderby p.Number ascending
                        select p).Take(Limit).ToList();

            }
        }
        public List<ProductType> getProductTypeByProductType2(int pProductType)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == pProductType
                        select p).ToList();
            }
        }

        public ProductType LayLoaiSanPhamTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public ProductDistributor LayProductDistributorTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductDistributor
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public List<ProductDistributor> LayProductDistributorTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductDistributor
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();

            }
        }
        public List<ProductDistributor> LayProductDistributorAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductDistributor
                        select p).ToList();
            }
        }

        public ProductManufacturer LayProductManufacturerTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductManufacturer
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<ProductManufacturer> LayProductManufacturerTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductManufacturer
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<ProductManufacturer> LayProductManufacturerAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductManufacturer
                        select p).ToList();
            }
        }

        public ProductType LayProductTypeTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.ProductType
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<ProductType> LayProductTypeTheoTrang(int pcurrent, int psize)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                       .Take(psize).ToList();
            }
        }
        public List<ProductType> LayProductTypeTheoTrangAndType(int pcurrent, int psize, int pType, string pLevel)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pType == 0)
                {
                    return (from p in entities.ProductType
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();


                }
                //lay tat ca cac ID cua group theo Level
                var mIdGroup = (from p in entities.ProductType
                                where p.Level.Substring(0, pLevel.Length).Equals(pLevel)
                                select p.ID).ToArray();
                //lay danh sach tin moi dang nhat
                return (from p in entities.ProductType
                        where mIdGroup.Contains(p.ID)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                         .Take(psize).ToList();
            }
        }
        public List<ProductType> LayProductTypeAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        select p).ToList();

            }
        }

        public List<ProductAttribute> LayProductAttributeTheoIDProduct(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductAttribute
                        where p.ProductID == pId
                        select p)
                       .ToList();
            }
        }
        public List<ProductImage> LayProductImageTheoIDProduct(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductImage
                        where p.ProductID == pId
                        select p)
                     .ToList();
            }
        }
        public List<ProductType> LayProductTypeTheoParentId(int ParentId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductType
                        where p.Parent == ParentId
                        orderby p.Number descending
                        select p).ToList();
            }

        }

        public ProductOrder LayProductOrderTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductOrder
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public List<ProductOrder> LayOrderTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    return (from p in entities.ProductOrder
                            where p.Status == 0
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                            .Take(psize).ToList();
                }
                if (pType == 1)
                {
                    return (from p in entities.ProductOrder
                            where p.Status == 1
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
                }
                return (from p in entities.ProductOrder
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }
        public List<ProductOrder> LayOrderTheoTrangAndType(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.ProductOrder
                        where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                        .Take(psize).ToList();
            }
        }
        public List<ProductOrder> LayOrderKhachMua1Lan(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            using (var entities = new V308CMSEntities())
            {
                List<ProductOrder> mList = new List<ProductOrder>();
                List<ProductOrder> listProductOrder = (from p in entities.ProductOrder
                                                       where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                                                       orderby p.ID
                                                       select p).ToList();
                var table = (from p in entities.ProductOrder
                             where p.AdminId == pUserId && (p.Date >= pDate1 && p.Date <= pDate2)
                             orderby p.ID descending
                             group p by p.AccountID into g//Nhóng theo Mã hàng
                             where g.Count() == 1
                             select new { ID = g.Key });
                foreach (var obj in table)
                {
                    int IDDistin = obj.ID.GetValueOrDefault();
                    ProductOrder mProductOrder = new ProductOrder();
                    mProductOrder = listProductOrder.Where(x => x.ID == IDDistin).FirstOrDefault();
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

        }

        public List<ProductOrder> LayOrderKhachVIP(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            using (var entities = new V308CMSEntities())
            {
                List<ProductOrder> mList = new List<ProductOrder>();
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
        }
        public List<ProductOrder> LayOrderKhachCu(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            using (var entities = new V308CMSEntities())
            {
                List<ProductOrder> mList = new List<ProductOrder>();
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
        }

        public List<ProductOrder> LayOrderKhachMoi(int pcurrent, int psize, int pType, DateTime pDate1, DateTime pDate2, int pUserId)
        {
            using (var entities = new V308CMSEntities())
            {
                List<ProductOrder> mList = new List<ProductOrder>();
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
                    int accountIdDistin = obj.AccountID.GetValueOrDefault();
                    var mProductOrder = listProductOrder.FirstOrDefault(x => x.AccountID == accountIdDistin);
                    mList.Add(mProductOrder);
                }

                return mList;
            }

        }



        public List<Brand> getRandomBrands(int categoryId = 0, int limit = 1)
        {
            using (var entities = new V308CMSEntities())
            {              
                var items = from b in entities.Brand
                            where b.status.Equals(1)
                            select b;
                if (categoryId > 0)
                {
                    items = items.Where(b => b.category_default == categoryId);
                }
                return items.ToList().OrderBy(x => Guid.NewGuid()).Take(limit).ToList();
            }
        }

        public List<Product> GetListProductWishlist(string listWishlist)
        {
            using (var entities = new V308CMSEntities())
            {
                if (!string.IsNullOrWhiteSpace(listWishlist))
                {
                    if (listWishlist.Contains(";"))
                    {
                        return (from item in entities.Product.AsEnumerable()
                                where listWishlist.Contains(item.ID + ";") || listWishlist.Contains(";" + item.ID)
                                orderby item.ID descending
                                select item
                            ).ToList();
                    }
                    else
                    {
                        var productId = Convert.ToInt32(listWishlist.Trim());
                        return (from item in entities.Product
                                where item.ID == productId
                                orderby item.ID descending
                                select item
                          ).ToList();
                    }
                }

                return default(List<Product>);
            }



        }


        public int PageSize = 20;
        public List<Product> GetItems(int pcurrent = 1)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Product
                          .OrderByDescending(p => p.ID)
                          .Skip((pcurrent - 1) * PageSize)
                          .Take(PageSize).ToList();
            }

        }
        public int GetItemsTotal()
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Product.Count();

            }

        }

        public OrdersPage GetOrdersAffiliatePage(int pageCurrent = 0, int partnerId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                OrdersPage modelPage = new OrdersPage();
                var items = from p in entities.ProductOrder
                                //join m in entities.ProductOrderMap on p.AccountID equals m.uid into map
                                //    from m in map.DefaultIfEmpty()

                                //where m.partner_id.Equals(PartnerID)
                            orderby p.ID descending
                            select p;

                modelPage.Total = items.Count();
                modelPage.Page = pageCurrent;
                modelPage.Items = items.Skip((pageCurrent - 1) * PageSize).Take(PageSize).ToList();
                return modelPage;

            }

        }
        public OrdersReportByDaysPage GetOrderReport7DayPage(int pageCurrent = 0, int partnerId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                OrdersReportByDaysPage ModelPage = new OrdersReportByDaysPage();
                List<OrdersReportByDay> ReportDays = new List<OrdersReportByDay>();
                DateTime today = DateTime.Today;
                DateTime begin = today.AddDays(-6);
                var dates = Enumerable.Range(0, 7).Select(days => begin.AddDays(days)).ToList();
                foreach (DateTime d in dates)
                {
                    OrdersReportByDay ReportDay = new OrdersReportByDay();

                    var items = from p in entities.ProductOrder
                                    //join m in entities.ProductOrderMap on p.AccountID equals m.uid into map
                                    //    from m in map.DefaultIfEmpty()

                                    //where m.partner_id.Equals(PartnerID)
                                where p.Date <= d
                                select p;


                    ReportDay.date = d;
                    ReportDay.Total = items.Count();

                    ReportDays.Add(ReportDay);
                }
                ModelPage.report = ReportDays;
                ModelPage.days = dates;
                return ModelPage;
            }
        }


        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var product = (from item in entities.Product
                               where item.ID == id
                               select item
              ).FirstOrDefault();
                if (product != null)
                {
                    product.Status = !product.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }

        }
        public List<Product> GetListProductInListId(string listId, bool includeData = true)
        {
            using (var entities = new V308CMSEntities())
            {
                return includeData ? (from item in entities.Product.
                     Include("ProductImages").
                     Include("ProductColor").
                     Include("ProductSize").
                     Include("ProductAttribute").
                     Include("ProductSaleOff").AsEnumerable()
                                      where listId.Contains(item.ID.ToString())
                                      select item
               ).ToList() :
               (from item in entities.Product.AsEnumerable()
                where listId.Contains(item.ID.ToString())
                select item
               ).ToList();
            }

        }
        public Product FindToModify(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from item in entities.Product.
                  Include("ProductImages").
                  Include("ProductColor").
                  Include("ProductSize").
                  Include("ProductAttribute").
                  Include("ProductSaleOff")
                        where item.ID == id
                        select item
              ).FirstOrDefault();
            }


        }
        public string UpdateQuantity(int productId, int quantity)
        {
            using (var entities = new V308CMSEntities())
            {
                var productQuantity = (from product in entities.Product
                                       where product.ID == productId
                                       select product
              ).FirstOrDefault();
                if (productQuantity != null)
                {
                    productQuantity.Quantity = quantity;
                    entities.SaveChanges();
                    return productQuantity.Name;
                }
                return "not_exists";
            }

        }
        public string UpdateCode(int productId, string code)
        {
            using (var entities = new V308CMSEntities())
            {
                var productCode = (from product in entities.Product
                                   where product.ID == productId
                                   select product
                  ).FirstOrDefault();
                if (productCode != null)
                {
                    productCode.Code = code;
                    entities.SaveChanges();
                    return productCode.Name;
                }
                return "not_exists";

            }

        }
        public string UpdateNpp(int productId, double npp)
        {
            using (var entities = new V308CMSEntities())
            {
                var productNpp = (from product in entities.Product
                                  where product.ID == productId
                                  select product
              ).FirstOrDefault();
                if (productNpp != null)
                {
                    productNpp.Npp = npp;
                    entities.SaveChanges();
                    return productNpp.Name;
                }
                return "not_exists";
            }

        }
        public string UpdatePrice(int productId, double price)
        {
            using (var entities = new V308CMSEntities())
            {
                var productPrice = (from product in entities.Product
                                    where product.ID == productId
                                    select product
             ).FirstOrDefault();
                if (productPrice != null)
                {
                    productPrice.Price = price;
                    entities.SaveChanges();
                    return productPrice.Name;
                }
                return "not_exists";
            }

        }

        public string UpdateOrder(int productId, int order)
        {
            using (var entities = new V308CMSEntities())
            {
                var productOrder = (from product in entities.Product
                                    where product.ID == productId
                                    select product
                ).FirstOrDefault();
                if (productOrder != null)
                {
                    productOrder.Number = order;
                    entities.SaveChanges();
                    return productOrder.Name;
                }
                return "not_exists";
            }

        }

        public List<ProductItem> GetList(
            out int totalRecord, int categoryId = 0,
            int quantity = 0, int state = 0,
            int brand = 0, int manufact = 0,
            int provider = 0,
            string keyword = "",
            int page = 1, int pageSize = 15)
        {
            using (var entities = new V308CMSEntities())
            {
                IEnumerable<Product> data = (from product in entities.Product.Include("ProductType")
                                             select product
                                      ).ToList();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var keywordLower = Ultility.LocDau(keyword.ToLower());
                    data = (from product in entities.Product.AsEnumerable()
                            where Ultility.LocDau(product.Code.ToLower()).Contains(keywordLower) ||
                                   Ultility.LocDau(product.Name.ToLower()).Contains(keywordLower)

                            select product
                        ).ToList();
                }
                if (categoryId > 0)
                {
                    data = (from product in data
                            where product.Type == categoryId
                            select product
                      ).ToList();

                }
                if (quantity > 0)
                {
                    data = quantity == 1 ? (from product in data
                                            where product.Quantity > 0
                                            select product
                     ).ToList() : (from product in data
                                   where product.Quantity == 0
                                   select product
                     ).ToList();
                }
                if (state > 0)
                {
                    if (state == (int)StateFilterEnum.Active)
                    {
                        data = (from product in data
                                where product.Status == true
                                select product
                            ).ToList();
                    }
                    if (state == (int)StateFilterEnum.Pending)
                    {
                        data = (from product in data
                                where product.Status == false
                                select product).ToList();
                    }
                    if (state == (int)StateFilterEnum.PriceEmpty)
                    {
                        data = (from product in data
                                where ((product.Price.HasValue == false) || (product.Price.Value == 0))
                                select product).ToList();
                    }

                }

                if (manufact > 0)
                {
                    data = (from product in data
                            where product.Manufacturer == manufact
                            select product
                     ).ToList();
                }
                if (brand > 0)
                {
                    data = (from product in data
                            where product.BrandId == brand
                            select product
                     ).ToList();
                }
                if (provider > 0)
                {
                    data = (from product in data
                            where product.AccountId == provider
                            select product
                     ).ToList();
                }
                totalRecord = data.Count();
                return (from product in data
                        orderby product.Date.Value descending
                        select new ProductItem
                        {
                            Id = product.ID,
                            Name = product.Name,
                            CategoryId = product.Type,
                            CategoryName = product.ProductType.Name,
                            Quantity = product.Quantity,
                            Code = product.Code,
                            CreatedDate = product.Date.Value,
                            Status = product.Status,
                            Image = product.Image,
                            Price = product.Price,
                            Npp = product.Npp,
                            Order = product.Number.HasValue ? product.Number.Value : 0
                        }
                    )
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }

        }



        public ProductItems GetItemsBySaleoff(int page = 0, float? saleOffValue = 0, string strOperator = ">")
        {
            using (var entities = new V308CMSEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                ProductItems returnValue = new ProductItems();
               
                var items = from p in entities.Product
                                //where (StrOperator == "<") ? float.Parse(p.SaleOff.ToString()) >= SaleOffValue : float.Parse(p.SaleOff.ToString()) >= SaleOffValue
                            where p.SaleOff >= saleOffValue
                            orderby p.SaleOff descending
                            select p;

                returnValue.Products = items.Skip((page - 1) * PageSize).Take(PageSize).ToList();
                returnValue.total = items.Count();
                returnValue.page = page;
                return returnValue;
            }


        }


        public class ProductItems
        {

            public List<Product> Products { get; set; }
            public int total { get; set; }
            public int page { get; set; }

        }

        public void IncrementView(int id)
        {

            using (var entities = new V308CMSEntities())
            {
                var productView = (from product in entities.Product
                                    where product.ID == id
                                   select product
                ).FirstOrDefault();
                if (productView != null)
                {

                    if (productView.View.HasValue)
                    {
                        productView.View = 1;
                    }
                    else
                    {
                        productView.View += 1;
                    }
                    entities.SaveChanges();
                 
                }
            
            }
        }
    }
}