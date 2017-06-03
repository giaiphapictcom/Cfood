using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace V308CMS.Sale
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Login", "dang-nhap", new { Controller = "Partner", action = "Login"});
            routes.MapRoute("Login-html", "dang-nhap.html", new { Controller = "Partner", action = "Login" });

            routes.MapRoute("Register", "dang-ky", new { Controller = "Partner", action = "Register" });
            routes.MapRoute("Register-html", "dang-ky.html", new { Controller = "Partner", action = "Register" });

            routes.MapRoute("Dashboard", "dashboard", new { Controller = "Partner", action = "index" });

            routes.MapRoute("ShortLinks", "link", new { Controller = "Partner", action = "Links" });
            routes.MapRoute("ShortLink-Create", "link/tao-moi", new { Controller = "Partner", action = "LinkForm" });
            routes.MapRoute("ShortLink-Edit", "link/chinh-sua/{id}", new { Controller = "Partner", action = "LinkForm" }, new { id = @"\d+" });

            routes.MapRoute("Product", "san-pham", new { Controller = "Partner", action = "Products" });

            routes.MapRoute("NewsThucDay", "chuong-trinh-thuc-day", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "chuong-trinh-thuc-day", PageTitle ="Chương Trình Thúc Đẩy"});
            routes.MapRoute("NewsBaiVietThucDay", "chuong-trinh-thuc-day/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsHuongDan", "huong-dan", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-huong-dan", PageTitle="Hướng Dẫn" });
            routes.MapRoute("NewsBaiVietHuongDan", "huong-dan/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsQuyDinh", "quy-dinh", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-quy-dinh", PageTitle="Quy Định" });
            routes.MapRoute("NewsBaiVietQuyDinh", "quy-dinh/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsChinhSach", "chinh-sach", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-chinh-sach", PageTitle="Chính Sách" });
            routes.MapRoute("NewsBaiVietChinhSach", "chinh-sach/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsAboutUs", "ve-affiliate", new { Controller = "Affiliate", action = "News", NewsAlias = "ve-affiliate", PageTitle="Về Affiliate" });
            
            

            routes.MapRoute("LogoutRoute", "dang-xuat", new { Controller = "Partner", action = "Logout" });
            //routes.MapRoute("ProfileRoute", "profile.html", new { Controller = "Account", action = "ProfileUser" });
            //routes.MapRoute("RegisterRoute", "dang-ky.html", new { Controller = "Account", action = "MarketRegister" });
            //routes.MapRoute("MarketRegisterRoute", "open-shop.html", new { Controller = "Home", action = "MarketRegister" });
            //routes.MapRoute("ShopCartDetailRoute", "chi-tiet-don-hang.html", new { Controller = "Home", action = "ShopCartDetail" });
            //routes.MapRoute("MarketListRoute", "danh-sach-sieu-thi.html", new { Controller = "Home", action = "MarketList" });
            /////
            //routes.MapRoute("MarketCategoryRoute", "{pMarketName}-m{pGroupId}.html", new { Controller = "Home", action = "MarketCategory" }, new { pGroupId = @"\d+" });
            //routes.MapRoute("NewsDetailRoute", "{title}-n{pId}.html", new { Controller = "Home", action = "NewsDetail" }, new { pId = @"\d+" });
            //routes.MapRoute("YoutubeDetailRoute", "{title}-youtube{pId}.html", new { Controller = "Home", action = "YoutubeDetail" }, new { pId = @"\d+" });
            //routes.MapRoute("NewsRoute", "{title}-group{pType}.html", new { Controller = "Home", action = "News" }, new { pType = @"\d+" });
            //routes.MapRoute("CategoryRoute", "{title}-t{pGroupId}.html", new { Controller = "Home", action = "Category" }, new { pGroupId = @"\d+" });
            //routes.MapRoute("DetailRoute", "{title}-d{pId}.html", new { Controller = "Home", action = "Detail" }, new { pId = @"\d+" });
            /////
            //routes.MapRoute("SearchRoute", "tim-kiem.html", new { Controller = "Home", action = "Search" });
            //routes.MapRoute("MarketRoute", "{pMarketName}", new { Controller = "Home", action = "Market" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Affiliate", action = "Home", id = UrlParameter.Optional });
            
        }
    }
}