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
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ArticleItems", "bai-viet/{alias}", new { Controller = "Affiliate", action = "Articles", alias= "" });

            routes.MapRoute("Login", "dang-nhap", new { Controller = "Partner", action = "Login"});
            routes.MapRoute("Login-html", "dang-nhap.html", new { Controller = "Partner", action = "Login" });
            routes.MapRoute("LogoutRoute", "dang-xuat", new { Controller = "Partner", action = "Logout" });

            routes.MapRoute("Register", "dang-ky", new { Controller = "Partner", action = "Register" });
            routes.MapRoute("Register-html", "dang-ky.html", new { Controller = "Partner", action = "Register" });

            routes.MapRoute("Dashboard", "dashboard", new { Controller = "Partner", action = "index" });

            routes.MapRoute("ShortLinks", "link", new { Controller = "Partner", action = "Links" });
            routes.MapRoute("ShortLink-Create", "link/tao-moi", new { Controller = "Partner", action = "LinkForm" });
            routes.MapRoute("ShortLink-Report", "link/report", new { Controller = "Partner", action = "LinkReport" });
            
            routes.MapRoute("ShortLink-Edit", "link/chinh-sua/{id}", new { Controller = "Partner", action = "LinkForm" }, new { id = @"\d+" });

            routes.MapRoute("ShortBanners", "baner", new { Controller = "Partner", action = "Banners" });
            routes.MapRoute("ShortBanners-Create", "baner/tao-moi", new { Controller = "Partner", action = "BannerForm" });
            routes.MapRoute("ShortBanners-Edit", "baner/chinh-sua/{id}", new { Controller = "Partner", action = "BannerForm" }, new { id = @"\d+" });

            routes.MapRoute("Product", "san-pham", new { Controller = "Partner", action = "Products" });
            routes.MapRoute("Coupon", "ma-giam-gia", new { Controller = "Partner", action = "Coupons" });
            routes.MapRoute("CouponCreate", "tao-ma-giam-gia", new { Controller = "Partner", action = "CouponForm" });

            routes.MapRoute("Orders", "order", new { Controller = "Partner", action = "Orders" });
            routes.MapRoute("OrderReport", "order/report", new { Controller = "Partner", action = "OrderReport" });
            routes.MapRoute("OrderSearch", "order/search", new { Controller = "Partner", action = "OrderSearch" });
            
            

            routes.MapRoute("NewsThucDay", "chuong-trinh-thuc-day", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "chuong-trinh-thuc-day", PageTitle ="Chương Trình Thúc Đẩy"});
            routes.MapRoute("NewsBaiVietThucDay", "chuong-trinh-thuc-day/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsHuongDan", "huong-dan", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-huong-dan", PageTitle="Hướng Dẫn" });
            routes.MapRoute("NewsBaiVietHuongDan", "huong-dan/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsQuyDinh", "quy-dinh", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-quy-dinh", PageTitle="Quy Định" });
            routes.MapRoute("NewsBaiVietQuyDinh", "quy-dinh/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            routes.MapRoute("NewsChinhSach", "chinh-sach", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "affiliate-chinh-sach", PageTitle="Chính Sách" });
            routes.MapRoute("NewsBaiVietChinhSach", "chinh-sach/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });
            
            
            routes.MapRoute("NewsHeThong", "he-thong", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "he-thong", PageTitle = "Hỗ Trợ" });
            routes.MapRoute("NewsBaiVietHeThong", "he-thong/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });

            routes.MapRoute("NewsVinhDanhCaNhan", "vinh-danh-ca-nhan", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "vinh-danh-ca-nhan", PageTitle = "Hỗ Trợ" });
            routes.MapRoute("NewsBaiVietVinhDanhCaNhan", "vinh-danh-ca-nhan/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });

            routes.MapRoute("NewsTopXuatSac", "top-xuat-sac", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "top-xuat-sac", PageTitle = "Hỗ Trợ" });
            routes.MapRoute("NewsBaiVietTopXuatSac", "top-xuat-sac/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });

            routes.MapRoute("NewsHoTro", "ho-tro", new { Controller = "Affiliate", action = "NewsList", CategoryAlias = "ho-tro", PageTitle = "Hỗ Trợ" });
            routes.MapRoute("NewsBaiVietHoTro", "ho-tro/{alias}", new { Controller = "Affiliate", action = "News" }, new { NewsAlias = @"\d+" });

            routes.MapRoute("PartnerSupportRequest", "yeu-cau-kien-nghi", new { Controller = "Partner", action = "SupportRequest" });
            

            routes.MapRoute("NewsKhuyenMaiMoi", "khuyen-mai-moi", new { Controller = "Affiliate", action = "NewsTable", CategoryAlias = "khuyen-mai", PageTitle = "Tin Tức khuyến mại tổng hợp" });
            

            routes.MapRoute("NewsAboutUs", "ve-affiliate", new { Controller = "Affiliate", action = "News", NewsAlias = "ve-affiliate", PageTitle="Về Affiliate" });
            routes.MapRoute("ArticleItemRoute", "{title}-n{id}.html", new { Controller = "Affiliate", action = "Article" }, new { id = @"\d+" });
            

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Affiliate", action = "Home", id = UrlParameter.Optional });
            

        }
    }
}