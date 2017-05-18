using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace V308CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("VideoIndexRoute", "video.html", new { Controller = "MyShopify", action = "HomeYoutube" });
            routes.MapRoute("NewsIndexRoute", "tin-tuc.html", new { Controller = "News", action = "Index", page =1, type = 58 });
            routes.MapRoute("NewsIndexPagingRoute", "tin-tuc/trang-{page}.html", new { Controller = "News", action = "Index",type = 58 });
            routes.MapRoute("NewsDetailRoute", "tin-tuc/{slug}.{id}.html", new { Controller = "News", action = "Detail" }, new { id = @"\d+" });
            routes.MapRoute("LoginRoute", "dang-nhap.html", new { Controller = "Account", action = "Login"});
            routes.MapRoute("LogoutRoute", "dang-xuat.html", new { Controller = "Account", action = "Logout" });
            routes.MapRoute("ProfileRoute", "profile.html", new { Controller = "Account", action = "ProfileUser" });
            routes.MapRoute("RegisterRoute", "dang-ky.html", new { Controller = "Account", action = "Register" });
            routes.MapRoute("MarketRegisterRoute", "open-shop.html", new { Controller = "Home", action = "MarketRegister" });
            routes.MapRoute("ShopCartDetailRoute", "chi-tiet-don-hang.html", new { Controller = "Home", action = "ShopCartDetail" });
            routes.MapRoute("MarketListRoute", "danh-sach-sieu-thi.html", new { Controller = "Home", action = "MarketList" });

            routes.MapRoute("AddCartRoute", "them-san-pham", new { Controller = "Home", action = "addToShopCart" });
            ///
            routes.MapRoute("MarketCategoryRoute", "{pMarketName}-m{pGroupId}.html", new { Controller = "Home", action = "MarketCategory" }, new { pGroupId = @"\d+" });
          
            routes.MapRoute("YoutubeDetailRoute", "{title}-youtube{pId}.html", new { Controller = "Home", action = "YoutubeDetail" }, new { pId = @"\d+" });
          
            routes.MapRoute("CategoryRoute", "{title}-t{pGroupId}.html", new { Controller = "Home", action = "Category" }, new { pGroupId = @"\d+" });
            routes.MapRoute("DetailRoute", "{title}-d{pId}.html", new { Controller = "Home", action = "Detail" }, new { pId = @"\d+" });
            ///
            routes.MapRoute("SearchRoute", "tim-kiem.html", new { Controller = "Home", action = "Search" });
            routes.MapRoute("MarketRoute", "{pMarketName}", new { Controller = "Home", action = "Market" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            
        }
    }
}