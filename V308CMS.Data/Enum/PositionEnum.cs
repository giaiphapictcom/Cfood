using System;
using System.ComponentModel;

namespace V308CMS.Data.Enum
{
  
    public enum PositionEnum
    {
        [Description("Trang chủ")]
        Home = 10,
        [Description("Trang chủ - Top")]
        HomeTop =11,
        [Description("Trang chủ - Giữa")]
        HomeCenter = 12,
        [Description("Trang chủ - Trái")]
        HomeLeft = 13,
        [Description("Trang chủ - Phải")]
        HomeRight = 14,
        [Description("Trang chủ - Dưới")]
        HomeBottom = 15,
        [Description("Trang chủ - Slider")]
        HomeSlider = 16,


        [Description("Trang chuyên mục")]
        Category = 20,
        [Description("Trang chuyên mục - Top")]
        CategoryTop = 21,
        [Description("Trang chuyên mục - Giữa")]
        CategoryCenter = 22,
        [Description("Trang chuyên mục - Trái")]
        CategoryLeft = 23,
        [Description("Trang chuyên mục - Phải")]
        CategoryRight = 24,
        [Description("Trang chuyên mục - Dưới")]
        CategoryBottom = 25,

        [Description("Trang BigSale")]
        BigSale = 30,
        [Description("Trang BigSale - Top")]
        BigSaleTop = 31,
        [Description("Trang BigSale - Giữa")]
        BigSaleCenter = 32,
        [Description("Trang BigSale - Trái")]
        BigSaleLeft = 33,
        [Description("Trang BigSale - Phải")]
        BigSaleRight = 34,
        [Description("Trang BigSale - Dưới")]
        BigSaleBottom = 35,

        [Description("Trang Tin tức")]
        News = 40,
        [Description("Trang Tin tức - Top")]
        NewsTop = 41,
        [Description("Trang Tin tức - Giữa")]
        NewsCenter = 42,
        [Description("Trang Tin tức - Trái")]
        NewsLeft = 43,
        [Description("Trang Tin tức - Phải")]
        NewsRight = 44,
        [Description("Trang Tin tức - Dưới")]
        NewsBottom = 45,

        [Description("Trang Chi tiết tin tức")]
        NewsDetail = 50,
        [Description("Trang Chi tiết tin tức - Top")]
        NewsDetailTop = 51,
        [Description("Trang Chi tiết tin tức - Giữa")]
        NewsDetailCenter = 52,
        [Description("Trang Chi tiết tin - Trái")]
        NewsDetailLeft = 53,
        [Description("Trang Chi tiết tin - Phải")]
        NewsDetailRight = 54,
        [Description("Trang Chi tiết tin - Dưới")]
        NewsDetailBottom = 55,


        [Description("Trang Video")]
        Video = 60,
        [Description("Trang Video - Top")]
        VideoTop = 61,
        [Description("Trang Video - Giữa")]
        VideoCenter =62,
        [Description("Trang Video - Trái")]
        VideoLeft = 63,
        [Description("Trang Video - Phải")]
        VideoRight = 64,
        [Description("Trang Video - Dưới")]
        VideoBottom = 65,

        [Description("Trang Chi tiết video")]
        VideoDetail = 70,
        [Description("Trang Chi tiết video - Top")]
        VideoDetailTop = 71,
        [Description("Trang Chi tiết video - Giữa")]
        VideoDetailCenter = 72,
        [Description("Trang Chi tiết video - Trái")]
        VideoDetailLeft = 73,
        [Description("Trang Chi tiết video - Phải")]
        VideoDetailRight = 74,
        [Description("Trang Chi tiết video - Dưới")]
        VideoDetailBottom = 75,

        [Description("Trang liên hệ")]
        Contact = 80,
        [Description("Trang liên hệ - Top")]
        ContactTop = 81,
        [Description("Trang liên hệ - Giữa")]
        ContactCenter = 82,
        [Description("Trang liên hệ - Trái")]
        ContactLeft = 83,
        [Description("Trang liên hệ - Phải")]
        ContactRight = 84,
        [Description("Trang liên hệ - Dưới")]
        ContactBottom = 85,


        [Description("Trang chi tiết sản phẩm")]
        ProductDetail = 90,
        [Description("Trang chi tiết sản phẩm - Top")]
        ProductDetailTop = 91,
        [Description("Trang chi tiết sản phẩm - Giữa")]
        ProductDetailCenter = 92,
        [Description("Trang chi tiết sản phẩm - Trái")]
        ProductDetailLeft = 93,
        [Description("Trang chi tiết sản phẩm - Phải")]
        ProductDetailRight = 94,
        [Description("Trang chi tiết sản phẩm - Dưới")]
        ProductDetailBottom = 95,

        [Description("Trang tìm kiếm")]
        Search = 100,
        [Description("Trang tìm kiếm - Top")]
        SearchTop = 101,
        [Description("Trang tìm kiếm - Giữa")]
        SearchCenter = 102,
        [Description("Trang tìm kiếm - Trái")]
        SearchLeft = 103,
        [Description("Trang tìm kiếm - Phải")]
        SearchRight = 104,
        [Description("Trang tìm kiếm - Dưới")]
        SearchBottom = 105
    }
}