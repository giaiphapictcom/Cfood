using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class WidgetController : Controller
    {
        public ActionResult Header()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            AccountRepository accountRepository = new AccountRepository(mEntities);
            HeaderPage mHeaderPage = new HeaderPage();
            List<Market> mList;
            Account mAccount;
            try
            {
                //Check xem dang nhap chưa thi hien thi menu khac
                // && Session["ShopCart"] != null
                if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                {
                    //lay thong tin chi tiet user
                    mAccount = accountRepository.LayTinTheoId(Int32.Parse(Session["UserId"].ToString()));
                    mHeaderPage.IsAuthenticated = true;
                    mHeaderPage.Account = mAccount;
                }
                if (Session["ShopCart"] != null)
                    mHeaderPage.ShopCart = (ShopCart)Session["ShopCart"];
                else
                    mHeaderPage.ShopCart = null;
                //lay danh sach nhom tin
                mList = marketRepository.getAll(8);
                mHeaderPage.Market = mList;
                //lay danh sach cac tin theo nhom
                //
                //
                //
                if (Request.Cookies["location"] != null && Request.Cookies["locationname"] != null)
                {
                    ViewBag.location = Int32.Parse(Request.Cookies["location"].Value);
                    ViewBag.locationname = Ultility.convertValueToDistrict(Int32.Parse(Request.Cookies["location"].Value));
                }
                else
                {
                    ViewBag.location = 0;
                    ViewBag.locationname = "Hà Nội";
                }
                //
                return View(mHeaderPage);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        public ActionResult Menu()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<ProductType> mProductTypeParent = null;
            List<ProductTypePage> mProductTypePage = new List<ProductTypePage>();
            News mNews;
            ProductTypePageContainer mProductTypePageContainer = new ProductTypePageContainer();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<Image> mListImage;
            try
            {
                //lay danh sach nhom tin
                mProductTypeParent = productRepository.getProductTypeParent();
                //lay danh sach cac tin theo nhom
                foreach (ProductType it in mProductTypeParent)
                {
                    ProductTypePage vProductTypePage = new ProductTypePage();
                    vProductTypePage.Id = it.ID;
                    vProductTypePage.Image = it.ImageBanner;
                    vProductTypePage.Name = it.Name;
                    //lay danh sach cac nhom con
                    List<ProductType> mProductTypeChild = productRepository.LayNhomSanPhamTheoTrangVaSeri(1, 10, it.ID);
                    vProductTypePage.List = mProductTypeChild;
                    mProductTypePage.Add(vProductTypePage);
                    //
                    mProductTypePageContainer.List = mProductTypePage;
                    mNews = newsRepository.getFirstNewsWithType(16);
                    mProductTypePageContainer.mNews = mNews;
                    //lay danh sach
                    mListImage = imagesRepository.GetImageByTypeId(1,4);
                    mProductTypePageContainer.ImageList = mListImage;

                }
                return View(mProductTypePageContainer);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult Menu2()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<ProductType> mProductTypeParent = null;
            List<ProductTypePage> mProductTypePage = new List<ProductTypePage>();
            News mNews;
            ProductTypePageContainer mProductTypePageContainer = new ProductTypePageContainer();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<Image> mListImage;
            try
            {
                //lay danh sach nhom tin
                mProductTypeParent = productRepository.getProductTypeParent();
                //lay danh sach cac tin theo nhom
                foreach (ProductType it in mProductTypeParent)
                {
                    ProductTypePage vProductTypePage = new ProductTypePage();
                    vProductTypePage.Id = it.ID;
                    vProductTypePage.Image = it.ImageBanner;
                    vProductTypePage.Name = it.Name;
                    //lay danh sach cac nhom con
                    List<ProductType> mProductTypeChild = productRepository.LayNhomSanPhamTheoTrangVaSeri(1, 10, it.ID);
                    vProductTypePage.List = mProductTypeChild;
                    mProductTypePage.Add(vProductTypePage);
                    //
                    mProductTypePageContainer.List = mProductTypePage;
                    mNews = newsRepository.getFirstNewsWithType(16);
                    mProductTypePageContainer.mNews = mNews;
                    //lay danh sach
                    mListImage = imagesRepository.GetImageByTypeId(1, 4);
                    mProductTypePageContainer.ImageList = mListImage;
                }
                return View(mProductTypePageContainer);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult MobileMenu()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<ProductType> mProductTypeParent = null;
            List<ProductTypePage> mProductTypePage = new List<ProductTypePage>();
            News mNews;
            ProductTypePageContainer mProductTypePageContainer = new ProductTypePageContainer();
            try
            {
                //lay danh sach nhom tin
                mProductTypeParent = productRepository.getProductTypeParent();
                //lay danh sach cac tin theo nhom
                foreach (ProductType it in mProductTypeParent)
                {
                    ProductTypePage vProductTypePage = new ProductTypePage();
                    vProductTypePage.Id = it.ID;
                    vProductTypePage.Image = it.ImageBanner;
                    vProductTypePage.Name = it.Name;
                    //lay danh sach cac nhom con
                    List<ProductType> mProductTypeChild = productRepository.LayNhomSanPhamTheoTrangVaSeri(1, 10, it.ID);
                    vProductTypePage.List = mProductTypeChild;
                    mProductTypePage.Add(vProductTypePage);
                    //
                    mProductTypePageContainer.List = mProductTypePage;
                    mNews = newsRepository.getFirstNewsWithType(16);
                    mProductTypePageContainer.mNews = mNews;
                }
                return View(mProductTypePageContainer);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult HeadMenu()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<NewsGroups> mNewsGroupsList;
            NewsPage mNewsPage = new NewsPage();
            try
            {
                //lay cac menu của MENU
                mNewsGroupsList = newsRepository.LayNhomTinAll();
                //Tao danh sach cac nhom tin
                mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachMenu(mNewsGroupsList);
                return View(mNewsPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult Email()
        {
            return View();
        }
        public ActionResult Footer()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<NewsGroups> mNewsGroupsList;
            List<NewsGroupPage> mNewsGroupPageList = new List<NewsGroupPage>();
            try
            {
                //lay cac nhom con cua: 14 la nhom cha Footer
                mNewsGroupsList = newsRepository.getNewsGroupAffterParent(14);
                foreach (NewsGroups it in mNewsGroupsList)
                {
                    NewsGroupPage mNewsGroupPage = new NewsGroupPage();
                    //lay danh sach cac tin ben trong
                    List<News> mList = newsRepository.LayTinTheoGroupId(it.ID);
                    mNewsGroupPage.Name = it.Name;
                    mNewsGroupPage.TypeId = it.ID;
                    mNewsGroupPage.NewsList = mList;
                    mNewsGroupPageList.Add(mNewsGroupPage);
                }
                return View(mNewsGroupPageList);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult Footer2()
        {
            return View();
        }
        public ActionResult Right()
        {
            return View();
            /*
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<Image> mList;
            StringBuilder mStr = new StringBuilder();
            try
            {
                mList = imagesRepository.GetImageByTypeId(2, 10);
                foreach (Image it in mList)
                {
                   
                    mStr.Append("<a href=\"" + it.Link + "\" target=\"_blank\" title=\"" + it.Name + "\"> <img src=\"" + it.LinkImage + "\" alt=\"" + it.Name + "\"> </a>");
                   
                }
                return View((object)mStr.ToString());
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
             */
        }
        public ActionResult Banner()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            BannerPage mBannerPage = new BannerPage();
            try
            {
                mList = newsRepository.LayDanhSachTinTheoGroupId(10, 26);
                mBannerPage.List = mList;
                return View(mBannerPage);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult MarketList()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Market> mList = new List<Market>();
            try
            {
                //lay danh sach nhom tin
                mList = marketRepository.getAll(10);
                //lay danh sach cac tin theo nhom
                return View(mList);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        public ActionResult SlideSide()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImagesPage mImagesPage = new ImagesPage();
            Image AnhTrai;
            Image AnhPhai;
            StringBuilder mStr = new StringBuilder();
            CommonModel mCommonModel = new CommonModel();
            try
            {
                AnhTrai = imagesRepository.GetImageByTypeId(3, 1).FirstOrDefault();
                AnhPhai = imagesRepository.GetImageByTypeId(2, 1).FirstOrDefault();
                mImagesPage.Html = AnhTrai.LinkImage;
                mImagesPage.HtmlNhom = AnhPhai.LinkImage;
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        public ActionResult ChooseDistrict()
        {
            //
            if (Request.Cookies["location"] != null && Request.Cookies["locationname"] != null)
            {
                ViewBag.location = Int32.Parse(Request.Cookies["location"].Value);
                ViewBag.locationname = Ultility.convertValueToDistrict(Int32.Parse(Request.Cookies["location"].Value));
            }
            else
            {
                ViewBag.location = 0;
                ViewBag.locationname = "Hà Nội";
            }
            return View();
        }
        public ActionResult ChooseUnit()
        {
            //
            return View();
        }



        public ActionResult TinNhanh()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            string str = "";
            try
            {
                mList = newsRepository.LayDanhSachTinNhanh(5);
                str = V308HTMLHELPER.TaoDanhSachTinHot(mList);
                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult PhoBien()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<Image> mImageList = null;
            StringBuilder mStr = new StringBuilder();
            CommonModel mCommonModel = new CommonModel();
            try
            {
                //lay thong tin chi tiet ve tin can doc
                mImageList = imagesRepository.GetImageByTypeId(2, 6);
                if (mImageList.Count > 0)
                {
                    foreach (Image it in mImageList)
                    {
                        mStr.Append("<li>");
                        mStr.Append("<a href=\"" + it.Link + "\" target=\"_blank\">");
                        mStr.Append("<img src=\"" + it.LinkImage + "\" >");
                        mStr.Append("</a>");
                        mStr.Append("</li>");
                    }
                    mCommonModel.Html = mStr.ToString();
                }
                return View(mCommonModel);
            }
            catch (Exception ex)
            {
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        public ActionResult DangNhap()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            string str = "";
            try
            {

                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult Logo()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            string str = "";
            try
            {

                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult ChuyenBiet()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            TinTucChuyenBiet Str = new TinTucChuyenBiet();
            List<News> mKhuyenMai = null;
            List<News> mCauDo = null;
            List<News> mSalon = null;
            List<News> mTamsu = null;
            try
            {
                mKhuyenMai = newsRepository.LayDanhSachTinTheoGroupId(5, 9);
                mCauDo = newsRepository.LayDanhSachTinTheoGroupId(5, 10);
                mSalon = newsRepository.LayDanhSachTinTheoGroupId(5, 11);
                mTamsu = newsRepository.LayDanhSachTinTheoGroupId(5, 12);
                Str.khuyenmai = V308HTMLHELPER.TaoDanhSachTinCauDo(mKhuyenMai, "Khuyến Mại", 9);
                Str.caudo = V308HTMLHELPER.TaoDanhSachTinCauDo(mCauDo, "Câu Đố", 10);
                Str.tamsu = V308HTMLHELPER.TaoDanhSachTinCauDo(mTamsu, "Tâm Sự", 11);
                Str.salon = V308HTMLHELPER.TaoDanhSachTinCauDo(mSalon, "Salon", 12);
                return View(Str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult Search()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            string str = "";
            try
            {

                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult LienHe()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            string str = "";
            try
            {

                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult TinHot()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            StringBuilder str = new StringBuilder();
            StringBuilder str2 = new StringBuilder();
            NewsPage mNewsPage = new NewsPage();
            int i = 0;
            try
            {
                //lay danh sach tin lien quan
                mList = newsRepository.LayDanhSachTinTheoGroupId(5, 12);
                foreach (News it in mList)
                {
                    if (i == 0)
                    {

                        str2.Append("<div class=\"item-image\">");
                        str2.Append("<a href=\"javascript:void(0);\">");
                        str2.Append("<span class=\"image-inset\">");
                        str2.Append("<img src=\"" + it.Image + "\" alt=\"" + it.Title + "\">");
                        str2.Append("</span>");
                        str2.Append("</a>");
                        str2.Append("</div>");


                        str2.Append("<div class=\"item-title\"><a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">" + it.Title + "</a></div>");
                        str2.Append("<div class=\"item-post-read\">");
                        str2.Append("<div class=\"item-post\">" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</div>");
                        str2.Append("</div>");
                        str2.Append("<div class=\"item-desc\">");
                        str2.Append("<p>" + it.Summary + "</p>");
                        str2.Append("</div>");

                        str2.Append("<div class=\"item-readmore\"><a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">CHI TIẾT</a></div>");

                    }
                    else
                    {
                        str.Append("<li>");
                        str.Append("<div class=\"left\">");
                        str.Append("<div class=\"item-post-read\">");
                        str.Append("<div class=\"item-post\">" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</div>");
                        str.Append("</div>");
                        str.Append("<div class=\"item-comments\">");
                        //str.Append("<a class=\"item-comments\" href=\"javascript:void(0);\">0 comment	</a>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("<div class=\"right\">");
                        str.Append("<a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">" + it.Title + "</a>");

                        str.Append("<div class=\"item-desc\">");
                        str.Append("<p>" + it.Summary + "</p>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("</li>");

                    }
                    i++;
                }
                mNewsPage.Html = str.ToString();
                mNewsPage.HtmlNhomTin = str2.ToString();
                return View(mNewsPage);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult Comment()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            StringBuilder str = new StringBuilder();
            NewsPage mNewsPage = new NewsPage();
            int i = 0;
            try
            {
                //lay danh sach tin lien quan
                mList = newsRepository.LayDanhSachTinTheoGroupId(4, 11);
                foreach (News it in mList)
                {
                    str.Append("<div class=\"line\">");
                    str.Append("<div class=\"item-wrap style1 last \">");
                    str.Append("<div class=\"item-image\">");
                    str.Append("<img src=\"" + it.Image + "\" alt=\"" + it.Image + "\">");
                    str.Append("</div>");
                    str.Append("<div class=\"item-info\">");
                    str.Append(" <div class=\"item-content\">");
                    str.Append("<div class=\"item-description\">" + it.Summary + "");
                    str.Append("</div>");
                    str.Append("<div class=\"name_school\">");
                    str.Append("<strong>" + it.Title + "</strong>");
                    str.Append("</div>");
                    str.Append("<div class=\"tooltip-meta\">");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("<div class=\"Testimonials_field\">");
                    str.Append("<strong>" + it.Title + "</strong>");
                    str.Append("</div>");
                    str.Append("</div>");

                }
                mNewsPage.Html = str.ToString();
                return View(mNewsPage);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult DanhSachNhomTin()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            List<NewsGroups> mListNhom = null;
            string str = "";
            DanhSachNhomTin mDanhSachNhomTin = new DanhSachNhomTin();
            NhomTin mNhomTin = null;
            try
            {
                //lay danh sach nhom tin
                mListNhom = newsRepository.LayDanhSachNhomTin();
                //lay danh sach cac tin theo nhom
                foreach (NewsGroups it in mListNhom)
                {
                    mNhomTin = new NhomTin();
                    mNhomTin.ID = it.ID;
                    mNhomTin.GroupName = it.Name;
                    mNhomTin.Order = (int)it.Number;
                    //lay danh sach cac tin hot nhat theo nhom
                    mList = newsRepository.LayDanhSachTinTheoGroupId(6, it.ID);
                    mNhomTin.NewsList = mList;
                    mDanhSachNhomTin.List.Add(mNhomTin);
                }
                str = V308HTMLHELPER.TaoDanhSachTinTrangChu(mDanhSachNhomTin);
                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult DanhSachTinLienQuan(int pGroupId, int pNewsId)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mList = null;
            string str = "";
            try
            {
                //lay danh sach tin lien quan
                mList = newsRepository.LayDanhSachTinTheoGroupId(6, pGroupId);
                //tao HTML
                str = V308HTMLHELPER.TaoDanhSachNhomTinLienQuan(mList, pNewsId);
                return View((object)str);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
    }
}
