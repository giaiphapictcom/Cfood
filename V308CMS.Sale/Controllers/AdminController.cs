using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ServiceStack.Text;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Sale.Models;
using System.Web.UI;
using System.Text;

namespace V308CMS.Sale.Controllers
{
    public class AdminController : Controller
    {
        #region VUNG CAC CONTROLER CHUNG
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult ChucNang()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckDangNhap(string pUserName, string pPassWord)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ETLogin mETLogin = null;
            try
            {

                mETLogin = accountRepository.CheckDangNhap(pUserName, pPassWord);

                if (mETLogin.code == 1)
                {
                    //SET session cho UserId
                    Session["UserId"] = mETLogin.Admin.ID;
                    Session["UserName"] = mETLogin.Admin.UserName;
                    Session["Role"] = mETLogin.Admin.Role;
                    Session["Admin"] = mETLogin.Admin;
                    //Thuc hien Authen cho User.
                    FormsAuthentication.SetAuthCookie(pUserName, true);
                    return Json(new { code = 1, message = "Đăng ký thành công. Tài khoản là : " + pUserName + "." });
                }
                else
                {
                    return Json(new { code = 0, message = mETLogin.message });
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return Redirect("/Admin/Login");
        }
        #endregion

        #region TIN TUC
        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TinTuc(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<News> mNewsList;
            List<NewsGroups> mNewsGroupsList;
            NewsPage mNewsPage = new NewsPage();
            NewsGroups mNewsGroups;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["TinTucType"] != null)
                        pType = (int)Session["TinTucType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["TinTucType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["TinTucPage"] != null)
                        pPage = (int)Session["TinTucPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["TinTucPage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mNewsGroups = newsRepository.LayTheLoaiTinTheoId((int)pType);
                    if (mNewsGroups != null)
                        mLevel = mNewsGroups.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mNewsList = newsRepository.LayTinTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                mNewsGroupsList = newsRepository.LayNhomTinAll();
                if (mNewsList.Count < 10)
                    mNewsPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mNewsPage.Html = V308HTMLHELPER.TaoDanhSachTinTuc(mNewsList, (int)pPage);
                //Tao danh sach cac nhom tin
                mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTinHome(mNewsGroupsList, (int)pPage, (int)pType);
                mNewsPage.Page = (int)pPage;
                mNewsPage.TypeId = (int)pType;
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

        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienXoaTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            News mNews;
            try
            {
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {
                    mEntities.DeleteObject(mNews);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienAnTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            News mNews;
            try
            {
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {
                    mNews.Status = false;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Ẩn thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần ẩn." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienHienTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            News mNews;
            try
            {
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {
                    mNews.Status = true;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Ẩn thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần ẩn." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TinTucThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsPage mNewsPage = new NewsPage();
            List<NewsGroups> mListNewsGroup;
            try
            {
                mListNewsGroup = newsRepository.LayNhomTinAll();
                //Tao danh sach cac nhom tin
                mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin(mListNewsGroup, 0);
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
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        public JsonResult TinTucThucHienThemMoi(string pTieuDe, string pImageUrl, int? pGroupId, string pMoTa, string pNoiDung, string pKichHoat, int? pUuTien, string pDescription, string pKeyWord, string pSlide, string pHot, string pFast, string pFeatured)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            News mNews;
            try
            {
                mNews = new News() { Date = DateTime.Now, Detail = pNoiDung, Image = pImageUrl, Order = pUuTien, Status = true, Summary = pMoTa, Title = pTieuDe, TypeID = pGroupId, Keyword = pKeyWord, Description = pDescription, Slider = ConverterUlti.ConvertStringToLogic2(pSlide), Hot = ConverterUlti.ConvertStringToLogic2(pHot), Fast = ConverterUlti.ConvertStringToLogic2(pFast), Featured = ConverterUlti.ConvertStringToLogic2(pFeatured) };
                mEntities.AddToNews(mNews);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu tin tức thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TinTucSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsPage mNewsPage = new NewsPage();
            List<NewsGroups> mListNewsGroup;
            News mNews;
            try
            {
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {
                    mListNewsGroup = newsRepository.LayNhomTinAll();
                    //Tao danh sach cac nhom tin
                    mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin(mListNewsGroup, (int)mNews.TypeID);
                    mNewsPage.pNews = mNews;
                }
                else
                {
                    mNewsPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
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
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        public JsonResult TinTucThucHienSuaTin(int pId, string pTieuDe, string pImageUrl, int? pGroupId, string pMoTa, string pNoiDung, string pKichHoat, int? pUuTien, string pDescription, string pKeyWord, string pSlide, string pHot, string pFast, string pFeatured)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            News mNews;
            try
            {
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {
                    mNews.Title = pTieuDe;
                    mNews.Date = DateTime.Now;
                    mNews.Detail = pNoiDung;
                    mNews.Image = pImageUrl;
                    mNews.Order = pUuTien;
                    mNews.Summary = pMoTa;
                    mNews.Keyword = pKeyWord;
                    mNews.Description = pDescription;
                    mNews.TypeID = pGroupId;
                    mNews.Status = ConverterUlti.ConvertStringToLogic2(pKichHoat);
                    mNews.Hot = ConverterUlti.ConvertStringToLogic2(pHot);
                    mNews.Slider = ConverterUlti.ConvertStringToLogic2(pSlide);
                    mNews.Fast = ConverterUlti.ConvertStringToLogic2(pFast);
                    mNews.Featured = ConverterUlti.ConvertStringToLogic2(pFeatured);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Lưu tin tức thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin tức để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }

        #endregion

        #region THE LOAI TIN TUC

        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TheLoaiTinTuc(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            List<NewsGroups> mNewsGroupsList;
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            List<NewsGroups> mNewsGroupsListAll;
            NewsGroups mNewsGroups;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["LoaiTinTucType"] != null)
                        pType = (int)Session["LoaiTinTucType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["LoaiTinTucType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["LoaiTinTucPage"] != null)
                        pPage = (int)Session["LoaiTinTucPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["LoaiTinTucPage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mNewsGroups = newsRepository.LayTheLoaiTinTheoId((int)pType);
                    if (mNewsGroups != null)
                        mLevel = mNewsGroups.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mNewsGroupsListAll = newsRepository.LayNhomTinAll();
                mNewsGroupsList = newsRepository.LayNewsGroupsTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                if (mNewsGroupsList.Count < 10)
                    mNewsGroupPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mNewsGroupPage.Html = V308HTMLHELPER.TaoDanhSachCacNhomTinTuc(mNewsGroupsList, (int)pPage);
                mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTinHome2(mNewsGroupsListAll, (int)pPage, (int)pType);
                mNewsGroupPage.Page = (int)pPage;
                mNewsGroupPage.TypeId = (int)pType;
                return View(mNewsGroupPage);
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
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienXoaTheLoaiTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroups mNewsGroups;
            try
            {
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pId);
                if (mNewsGroups != null)
                {
                    mEntities.DeleteObject(mNewsGroups);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienAnTheLoaiTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroups mNewsGroups;
            try
            {
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pId);
                if (mNewsGroups != null)
                {
                    mNewsGroups.Status = false;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Ẩn thể loại tin thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy thể loại tin cần ẩn." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [HttpPost]
        public JsonResult ThucHienHienTheLoaiTinTuc(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroups mNewsGroups;
            try
            {
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pId);
                if (mNewsGroups != null)
                {
                    mNewsGroups.Status = true;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Hiện thể loại tin thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy thể loại tin cần Hiện." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TheLoaiTinTucThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            List<NewsGroups> mListNewsGroup;
            try
            {
                mListNewsGroup = newsRepository.LayNhomTinAll();
                //Tao danh sach cac nhom tin
                mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin2(mListNewsGroup, 0);
                return View(mNewsGroupPage);
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
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        public JsonResult TheLoaiTinTucThucHienThemMoi(string pTieuDe, string pLink, int? pKichHoat, int? pUuTien, int? pGroupId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroups mNewsGroups;
            NewsGroups mNewsGroupsParent;
            string[] mLevelArray;
            long mLevel = 0;
            try
            {
                if (pGroupId == 0)
                {
                    //Tinh gia tri Level moi cho Group nay
                    //1- Lay tat ca cac Group me
                    //2- Convert gia tri Level de lay gia tri lon nhat
                    //3- Tao gia tri moi lon hon gia tri lon nhat
                    mLevelArray = (from p in mEntities.NewsGroups
                                   where p.Parent == 0
                                   select p.Level).ToArray();
                    mLevel = mLevelArray.Select(p => Convert.ToInt64(p)).ToArray().Max();
                    mLevel = (mLevel + 1);
                    mNewsGroups = new NewsGroups() { Link = pLink, Date = DateTime.Now, Number = pUuTien, Status = true, Name = pTieuDe, Parent = pGroupId, Level = mLevel.ToString() };
                    mEntities.AddToNewsGroups(mNewsGroups);
                    mEntities.SaveChanges();
                }
                else
                {

                    //lay level cua nhom me
                    mNewsGroupsParent = newsRepository.LayTheLoaiTinTheoId((int)pGroupId);
                    if (mNewsGroupsParent != null)
                    {
                        mLevelArray = (from p in mEntities.NewsGroups
                                       where (p.Level.Substring(0, mNewsGroupsParent.Level.Length).Equals(mNewsGroupsParent.Level)) && (p.Level.Length == (mNewsGroupsParent.Level.Length + 5))
                                       select p.Level).ToArray();
                        if (mLevelArray.Count() > 0)
                        {
                            mLevel = mLevelArray.Select(p => Convert.ToInt64(p)).ToArray().Max();
                            mLevel = (mLevel + 1);
                        }
                        else
                        {
                            mLevel = Convert.ToInt64(mNewsGroupsParent.Level.ToString().Trim() + "10001");
                        }
                        mNewsGroups = new NewsGroups() { Date = DateTime.Now, Number = pUuTien, Status = true, Name = pTieuDe, Parent = pGroupId, Level = mLevel.ToString() };
                        mEntities.AddToNewsGroups(mNewsGroups);
                        mEntities.SaveChanges();
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Không tìm thấy nhóm tin." });
                    }
                }
                return Json(new { code = 1, message = "Lưu tin tức thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        public ActionResult TheLoaiTinTucSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            List<NewsGroups> mListNewsGroup;
            NewsGroups mNewsGroups;
            try
            {
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pId);
                if (mNewsGroups != null)
                {
                    mListNewsGroup = newsRepository.LayNhomTinAll();
                    //Tao danh sach cac nhom tin
                    mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin3(mListNewsGroup, (int)mNewsGroups.Parent);
                    mNewsGroupPage.pNewsGroups = mNewsGroups;
                }
                else
                {
                    mNewsGroupPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mNewsGroupPage);
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
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        public JsonResult TheLoaiTinTucThucHienSuaTin(int pId, string pTieuDe, string pLink, int? pGroupId, int? pKichHoat, int? pUuTien)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsGroups mNewsGroups;
            try
            {
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pId);
                if (mNewsGroups != null)
                {
                    mNewsGroups.Name = pTieuDe;
                    mNewsGroups.Date = DateTime.Now;
                    mNewsGroups.Number = pUuTien;
                    mNewsGroups.Parent = pGroupId;
                    mNewsGroups.Link = pLink;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại tin thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại tin tức để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }

        }

        #endregion

        #region THE LOAI ANH
        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult TheLoaiAnh(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<ImageType> mmImagesList;
            ImagesPage mImagesPage = new ImagesPage();
            List<ImageType> mImageTypeAll;
            ImageType mImageType;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["LoaiAnhType"] != null)
                        pType = (int)Session["LoaiAnhType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["LoaiAnhType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["LoaiAnhPage"] != null)
                        pPage = (int)Session["LoaiAnhPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["LoaiAnhPage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mImageType = imagesRepository.LayTheLoaiAnhTheoId((int)pType);
                    if (mImageType != null)
                        mLevel = mImageType.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mImageTypeAll = imagesRepository.LayNhomAnhAll();
                /*Lay danh sach cac tin theo page*/
                mmImagesList = imagesRepository.LayImageTypeTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                if (mmImagesList.Count < 10)
                    mImagesPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mImagesPage.Html = V308HTMLHELPER.TaoDanhSachCacNhomAnh(mmImagesList, (int)pPage);
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnhHome2(mImageTypeAll, (int)pPage, (int)pType);
                mImagesPage.Page = (int)pPage;
                mImagesPage.TypeId = (int)pType;
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [HttpPost]
        public JsonResult ThucHienXoaTheLoaiAnh(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImageType mImageType;
            try
            {
                mImageType = imagesRepository.LayTheLoaiAnhTheoId(pId);
                if (mImageType != null)
                {
                    mEntities.DeleteObject(mImageType);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult TheLoaiAnhThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImagesPage mImagesPage = new ImagesPage();
            List<ImageType> mListImageType;
            try
            {
                mListImageType = imagesRepository.LayNhomAnhAll();
                //Tao danh sach cac nhom tin
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh2(mListImageType, 0);
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        public JsonResult TheLoaiAnhThucHienThemMoi(string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImageType mImageType;
            ImageType mImageTypeParent;
            string[] mLevelArray;
            int mLevel = 0;
            try
            {
                if (pGroupId == 0)
                {
                    //Tinh gia tri Level moi cho Group nay
                    //1- Lay tat ca cac Group me
                    //2- Convert gia tri Level de lay gia tri lon nhat
                    //3- Tao gia tri moi lon hon gia tri lon nhat
                    mLevelArray = (from p in mEntities.ImageType
                                   where p.Parent == 0
                                   select p.Level).ToArray();
                    mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                    mLevel = (mLevel + 1);
                    mImageType = new ImageType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, Parent = pGroupId, Size = pKichCo, Image = pImageUrl };
                    mEntities.AddToImageType(mImageType);
                    mEntities.SaveChanges();
                }
                else
                {

                    //lay level cua nhom me
                    mImageTypeParent = imagesRepository.LayTheLoaiAnhTheoId((int)pGroupId);
                    if (mImageTypeParent != null)
                    {
                        mLevelArray = (from p in mEntities.NewsGroups
                                       where (p.Level.Substring(0, mImageTypeParent.Level.Length).Equals(mImageTypeParent.Level)) && (p.Level.Length == (mImageTypeParent.Level.Length + 5))
                                       select p.Level).ToArray();
                        if (mLevelArray.Count() > 0)
                        {
                            mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                            mLevel = (mLevel + 1);
                        }
                        else
                        {
                            mLevel = Convert.ToInt32(mImageTypeParent.Level.ToString().Trim() + "10001");
                        }
                        mImageType = new ImageType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, Parent = pGroupId, Size = pKichCo, Image = pImageUrl };
                        mEntities.AddToImageType(mImageType);
                        mEntities.SaveChanges();
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Không tìm thấy nhóm ảnh." });
                    }
                }
                return Json(new { code = 1, message = "Lưu loại ảnh thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult TheLoaiAnhSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImagesPage mImagesPage = new ImagesPage();
            List<ImageType> mImageTypeList;
            ImageType mImageType;
            try
            {
                mImageType = imagesRepository.LayTheLoaiAnhTheoId(pId);
                if (mImageType != null)
                {
                    mImageTypeList = imagesRepository.LayNhomAnhAll();
                    //Tao danh sach cac nhom tin
                    mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh3(mImageTypeList, (int)mImageType.Parent);
                    mImagesPage.pImageType = mImageType;
                }
                else
                {
                    mImagesPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        public JsonResult TheLoaiAnhThucHienSuaAnh(int pId, string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImageType mImageType;
            try
            {
                mImageType = imagesRepository.LayTheLoaiAnhTheoId(pId);
                if (mImageType != null)
                {
                    mImageType.Name = pTieuDe;
                    mImageType.Date = DateTime.Now;
                    mImageType.Number = pUuTien;
                    mImageType.Parent = pGroupId;
                    mImageType.Size = pKichCo;
                    mImageType.Image = pImageUrl;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }
        #endregion

        #region ANH
        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult Anh(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            List<Image> mmImagesList;
            List<ImageType> mImageTypeList;
            ImagesPage mImagesPage = new ImagesPage();
            ImageType mImageType;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["AnhType"] != null)
                        pType = (int)Session["AnhType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["AnhType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["AnhPage"] != null)
                        pPage = (int)Session["AnhPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["AnhPage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mImageType = imagesRepository.LayTheLoaiAnhTheoId((int)pType);
                    if (mImageType != null)
                        mLevel = mImageType.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mmImagesList = imagesRepository.LayImageTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                mImageTypeList = imagesRepository.LayNhomAnhAll();
                if (mmImagesList.Count < 10)
                    mImagesPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mImagesPage.Html = V308HTMLHELPER.TaoDanhSachCacAnh(mmImagesList, (int)pPage);
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnhHome(mImageTypeList, (int)pPage, (int)pType);
                mImagesPage.Page = (int)pPage;
                mImagesPage.TypeId = (int)pType;
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [HttpPost]
        public JsonResult ThucHienXoaAnh(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            Image mImage;
            try
            {
                mImage = imagesRepository.LayAnhTheoId(pId);
                if (mImage != null)
                {
                    mEntities.DeleteObject(mImage);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult AnhThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImagesPage mImagesPage = new ImagesPage();
            List<ImageType> mListImageType;
            try
            {
                mListImageType = imagesRepository.LayNhomAnhAll();
                //Tao danh sach cac nhom tin
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh4(mListImageType, 0);
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        public JsonResult AnhThucHienThemMoi(string pTitle, int? pGroupId, string pLink, string pSummary, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            Image mImage;
            try
            {
                mImage = new Image() { Date = DateTime.Now, Link = pLink, LinkImage = pImageUrl, Name = pTitle, Summary = pSummary, TypeID = pGroupId };
                mEntities.AddToImage(mImage);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu  ảnh thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(4)]
        public ActionResult AnhSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            ImagesPage mImagesPage = new ImagesPage();
            List<ImageType> mImageTypeList;
            Image mImage;
            try
            {
                mImage = imagesRepository.LayAnhTheoId(pId);
                if (mImage != null)
                {
                    mImageTypeList = imagesRepository.LayNhomAnhAll();
                    //Tao danh sach cac nhom tin
                    mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh4(mImageTypeList, (int)mImage.TypeID);
                    mImagesPage.pImage = mImage;
                }
                else
                {
                    mImagesPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mImagesPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        public JsonResult ThucHienSuaAnh(int pId, string pTitle, int? pGroupId, string pLink, string pSummary, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ImagesRepository imagesRepository = new ImagesRepository(mEntities);
            Image mImage;
            try
            {
                mImage = imagesRepository.LayAnhTheoId(pId);
                if (mImage != null)
                {
                    mImage.Name = pTitle;
                    mImage.TypeID = pGroupId;
                    mImage.Link = pLink;
                    mImage.LinkImage = pImageUrl;
                    mImage.Summary = pSummary;
                    mImage.Date = DateTime.Now;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa ảnh thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy ảnh để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                imagesRepository.Dispose();
            }

        }
        #endregion

        #region NHA PHAN PHOI
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaPhanPhoi(int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductDistributor> mProductDistributor;
            ProductPage mProductPage = new ProductPage();
            try
            {
                #region SAVE SESSION

                if (pPage == null)
                {
                    if (Session["NhaPhanPhoiPage"] != null)
                        pPage = (int)Session["NhaPhanPhoiPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["NhaPhanPhoiPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mProductDistributor = productRepository.LayProductDistributorTheoTrang((int)pPage, 10);
                if (mProductDistributor.Count < 10)
                    mProductPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductDistributor(mProductDistributor, (int)pPage);
                mProductPage.Page = (int)pPage;
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamNhaPhanPhoiThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductDistributor mProductDistributor;
            try
            {
                mProductDistributor = productRepository.LayProductDistributorTheoId(pId);
                if (mProductDistributor != null)
                {
                    mEntities.DeleteObject(mProductDistributor);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaPhanPhoiThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductDistributor> mListProductDistributor;
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamNhaPhanPhoiThucHienThemMoi(string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductDistributor mProductDistributor;
            try
            {
                mProductDistributor = new ProductDistributor() { Date = DateTime.Now, Number = pUuTien, Name = pTieuDe, Detail = pSummary, Image = pUrlImage, Status = true, Visible = true };
                mEntities.AddToProductDistributor(mProductDistributor);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu loại ảnh thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaPhanPhoiSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage mProductPage = new ProductPage();
            ProductDistributor mProductDistributor;
            try
            {
                mProductDistributor = productRepository.LayProductDistributorTheoId(pId);
                if (mProductDistributor != null)
                {
                    mProductPage.pProductDistributor = mProductDistributor;
                }
                else
                {
                    mProductPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamNhaPhanPhoiThucHienSua(int pId, string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductDistributor mProductDistributor;
            try
            {
                mProductDistributor = productRepository.LayProductDistributorTheoId(pId);
                if (mProductDistributor != null)
                {
                    mProductDistributor.Name = pTieuDe;
                    mProductDistributor.Date = DateTime.Now;
                    mProductDistributor.Number = pUuTien;
                    mProductDistributor.Detail = pSummary;
                    mProductDistributor.Image = pUrlImage;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        #endregion

        #region NHA SAN XUAT
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaSanXuat(int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductManufacturer> mProductManufacturer;
            ProductPage mProductPage = new ProductPage();
            try
            {
                #region SAVE SESSION
                if (pPage == null)
                {
                    if (Session["NhaSanXuatPage"] != null)
                        pPage = (int)Session["NhaSanXuatPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["NhaSanXuatPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mProductManufacturer = productRepository.LayProductManufacturerTheoTrang((int)pPage, 10);
                if (mProductManufacturer.Count < 10)
                    mProductPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductManufacturer(mProductManufacturer, (int)pPage);
                mProductPage.Page = (int)pPage;
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamNhaSanXuatThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductManufacturer mProductManufacturer;
            try
            {
                mProductManufacturer = productRepository.LayProductManufacturerTheoId(pId);
                if (mProductManufacturer != null)
                {
                    mEntities.DeleteObject(mProductManufacturer);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaSanXuatThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductManufacturer> mListProductManufacturer;
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamNhaSanXuatThucHienThemMoi(string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductManufacturer mProductManufacturer;
            try
            {
                mProductManufacturer = new ProductManufacturer() { Date = DateTime.Now, Number = pUuTien, Name = pTieuDe, Detail = pSummary, Image = pUrlImage, Status = true, Visible = true };
                mEntities.AddToProductManufacturer(mProductManufacturer);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu loại ảnh thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamNhaSanXuatSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage mProductPage = new ProductPage();
            ProductManufacturer mProductManufacturer;
            try
            {
                mProductManufacturer = productRepository.LayProductManufacturerTheoId(pId);
                if (mProductManufacturer != null)
                {
                    mProductPage.pProductManufacturer = mProductManufacturer;
                }
                else
                {
                    mProductPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamNhaSanXuatThucHienSua(int pId, string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductManufacturer mProductManufacturer;
            try
            {
                mProductManufacturer = productRepository.LayProductManufacturerTheoId(pId);
                if (mProductManufacturer != null)
                {
                    mProductManufacturer.Name = pTieuDe;
                    mProductManufacturer.Date = DateTime.Now;
                    mProductManufacturer.Number = pUuTien;
                    mProductManufacturer.Detail = pSummary;
                    mProductManufacturer.Image = pUrlImage;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        #endregion

        #region LOAI SAN PHAM

        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamLoaiSanPham(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductType> mProductType;
            List<ProductType> mProductTypeAll;
            ProductPage mProductPage = new ProductPage();
            List<ProductType> mProductTypeChildList;
            ProductType mProductTypeDetail = new ProductType() { Parent = 0 };
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["LoaiSanPhamType"] != null)
                        pType = (int)Session["LoaiSanPhamType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["LoaiSanPhamType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["LoaiSanPhamPage"] != null)
                        pPage = (int)Session["LoaiSanPhamPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["LoaiSanPhamPage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mProductTypeDetail = productRepository.LayProductTypeTheoId((int)pType);
                    if (mProductTypeDetail != null)
                        mLevel = mProductTypeDetail.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                /*Lay danh sach cac tin theo page*/
                mProductType = productRepository.LayProductTypeTheoTrangAndType((int)pPage, 5, (int)pType, mLevel);
                //Lay tat ca cac nhom
                mProductTypeAll = productRepository.LayProductTypeAll();
                mProductTypeChildList = productRepository.LayProductTypeTheoParentId((int)pType);
                if (mProductType.Count < 5)
                    mProductPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductType(mProductType, (int)pPage);
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductTypeHome2(mProductTypeAll, (int)pPage, (int)pType);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;
                mProductPage.ProductTypeLt = mProductTypeChildList;
                mProductPage.pProductType = mProductTypeDetail;
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamLoaiSanPhamThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductType mProductType;
            try
            {
                mProductType = productRepository.LayProductTypeTheoId(pId);
                if (mProductType != null)
                {
                    mEntities.DeleteObject(mProductType);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamLoaiSanPhamThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage mProductPage = new ProductPage();
            List<ProductType> mProductType;
            try
            {
                mProductType = productRepository.LayProductTypeAll();
                //Tao danh sach cac nhom tin
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType2(mProductType, 0);
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamLoaiSanPhamThucHienThemMoi(string pTieuDe, int? pGroupId, string pSummary, int? pKichHoat, int? pUuTien, string pImageUrl, string pImageBanner, string pDescription)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductType mProductType;
            ProductType mProductTypeParent;
            string[] mLevelArray;
            int mLevel = 0;
            try
            {
                if (pGroupId == 0)
                {
                    //Tinh gia tri Level moi cho Group nay
                    //1- Lay tat ca cac Group me
                    //2- Convert gia tri Level de lay gia tri lon nhat
                    //3- Tao gia tri moi lon hon gia tri lon nhat
                    mLevelArray = (from p in mEntities.ProductType
                                   where p.Parent == 0
                                   select p.Level).ToArray();
                    mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                    mLevel = (mLevel + 1);
                    mProductType = new ProductType() { Date = DateTime.Now, Number = pUuTien, Level = mLevel.ToString(), Status = true, Name = pTieuDe, Parent = pGroupId, Detail = pSummary, Image = pImageUrl, ImageBanner = pImageBanner, Description = pDescription };
                    mEntities.AddToProductType(mProductType);
                    mEntities.SaveChanges();
                }
                else
                {

                    //lay level cua nhom me
                    mProductTypeParent = productRepository.LayProductTypeTheoId((int)pGroupId);
                    if (mProductTypeParent != null)
                    {
                        mLevelArray = (from p in mEntities.ProductType
                                       where (p.Level.Substring(0, mProductTypeParent.Level.Length).Equals(mProductTypeParent.Level)) && (p.Level.Length == (mProductTypeParent.Level.Length + 5))
                                       select p.Level).ToArray();
                        if (mLevelArray.Count() > 0)
                        {
                            mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                            mLevel = (mLevel + 1);
                        }
                        else
                        {
                            mLevel = Convert.ToInt32(mProductTypeParent.Level.ToString().Trim() + "10001");
                        }
                        mProductType = new ProductType() { Date = DateTime.Now, Number = pUuTien, Level = mLevel.ToString(), Status = true, Name = pTieuDe, Parent = pGroupId, Detail = pSummary, Image = pImageUrl, Description = pDescription };
                        mEntities.AddToProductType(mProductType);
                        mEntities.SaveChanges();
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Không tìm thấy nhóm sản phẩm." });
                    }
                }
                return Json(new { code = 1, message = "Lưu loại sản phẩm thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamLoaiSanPhamSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage mProductPage = new ProductPage();
            List<ProductType> mListProductType;
            ProductType mProductType;
            try
            {
                mProductType = productRepository.LayProductTypeTheoId(pId);
                if (mProductType != null)
                {
                    mListProductType = productRepository.LayProductTypeAll();
                    //Tao danh sach cac nhom tin
                    mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType3(mListProductType, (int)mProductType.Parent);
                    mProductPage.pProductType = mProductType;
                }
                else
                {
                    mProductPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamLoaiSanPhamThucHienSua(int pId, string pTieuDe, int? pGroupId, string pSummary, int? pKichHoat, int? pUuTien, string pImageUrl, string pImageBanner, string pDescription)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductType mProductType;
            try
            {
                mProductType = productRepository.LayProductTypeTheoId(pId);
                if (mProductType != null)
                {
                    mProductType.Name = pTieuDe;
                    mProductType.Date = DateTime.Now;
                    mProductType.Number = pUuTien;
                    mProductType.Parent = pGroupId;
                    mProductType.Detail = pSummary;
                    mProductType.Image = pImageUrl;
                    mProductType.ImageBanner = pImageBanner;
                    mProductType.Description = pDescription;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa loại sản phẩm thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại sản phẩm để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }

        #endregion

        #region SAN PHAM
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPham(int? pType, int? pPage, int? pMarket = 0, string pKey = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            AccountRepository accountRepository = new AccountRepository();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Product> mProductList;
            List<ProductType> mProductTypeList;
            List<ProductType> mProductTypeChildList;
            ProductPage mProductPage = new ProductPage();
            ProductType mProductType = new ProductType() { Parent = 0 };
            string mLevel = "";
            List<Market> mMarketList = new List<Market>();
            Market mMarket = null;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["SanPhamType"] != null)
                        pType = (int)Session["SanPhamType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["SanPhamType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["SanPhamPage"] != null)
                        pPage = (int)Session["SanPhamPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["SanPhamPage"] = pPage;
                }
                //
                if (pMarket == 0)
                {
                    if (Session["SanPhamMarket"] != null)
                        pMarket = (int)Session["SanPhamMarket"];
                    else
                        pMarket = 0;
                }
                else
                {
                    Session["SanPhamMarket"] = pMarket;
                }
                //
                if (pKey.Length == 0)
                {
                    if (Session["SanPhamKey"] != null)
                        pKey = (string)Session["SanPhamKey"];
                    else
                        pKey = "";
                }
                else
                {
                    Session["SanPhamKey"] = pKey;
                }
                #endregion

                //lay Level cua Type
                if (pType != 0)
                {
                    mProductType = productRepository.LayProductTypeTheoId((int)pType);
                    if (mProductType != null)
                        mLevel = mProductType.Level.Trim();
                }
                //
                mMarket = marketRepository.LayTheoUserId((int)Session["UserId"]);
                //
                mProductPage.Market = (int)pMarket;
                mProductPage.Key = pKey;
                //lay danh sach cac sieu thi
                mMarketList = marketRepository.getAll(1000);
                mProductPage.MarketList = mMarketList;
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.getByTypeAndNameAndMarket((int)pPage, 18, (int)pType, (int)mMarket.ID, pKey, mLevel);
                //lay danh sach cac kieu san pham
                mProductTypeList = productRepository.LayProductTypeAll();
                mProductTypeChildList = productRepository.LayProductTypeTheoParentId((int)pType);
                if (mProductList.Count < 18)
                    mProductPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachSanPham(mProductList, (int)pPage);
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductTypeHome(mProductTypeList, (int)pPage, (int)pType);
                mProductPage.ProductTypeLt = mProductTypeChildList;
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;
                mProductPage.pProductType = mProductType;
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamUpdate(int[] pId, int[] pNumber, bool[] pHome, bool[] pBestSale, bool[] pHide, bool[] pDelete)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<Product> mProductList;
            try
            {
                mProductList = productRepository.getProductByIdList(pId);
                foreach (Product it in mProductList)
                {
                    for (int i = 0; i < 18; i++)
                    {
                        if (pId.Length > i)
                        {
                            if (it.ID == pId[i])
                            {
                                if (pHide.Length > i)
                                {
                                    if (it.Status != pHide[i])
                                    {
                                        it.Status = pHide[i];
                                    }
                                }
                                if (pHome.Length > i)
                                {
                                    if (it.Hot != pHome[i])
                                    {
                                        it.Hot = pHome[i];
                                    }
                                }
                                if (pBestSale.Length > i)
                                {
                                    if (it.Visible != pBestSale[i])
                                    {
                                        it.Visible = pBestSale[i];
                                    }
                                }
                                if (pNumber.Length > i)
                                {
                                    if (it.Number != pNumber[i])
                                    {
                                        it.Number = pNumber[i];
                                    }
                                }
                                if (pDelete.Length > i)
                                {
                                    if (pDelete[i] == true)
                                    {
                                        mEntities.DeleteObject(it);
                                    }
                                }
                            }
                        }
                    }
                }
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." + ex.Message });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            Product mProduct;
            List<ProductImage> mProductImageList;
            List<ProductAttribute> mProductAttribute;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mEntities.DeleteObject(mProduct);
                    //Tim danh sach anh
                    mProductImageList = productRepository.LayProductImageTheoIDProduct(pId);
                    mProductAttribute = productRepository.LayProductAttributeTheoIDProduct(pId);
                    if (mProductAttribute.Count > 0)
                    {
                        foreach (ProductAttribute it in mProductAttribute)
                        {
                            mEntities.DeleteObject(it);
                        }
                    }
                    if (mProductImageList.Count > 0)
                    {
                        foreach (ProductImage it in mProductImageList)
                        {
                            mEntities.DeleteObject(it);
                        }
                    }
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamThucHienAn(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            Product mProduct;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProduct.Status = false;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Ẩn thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm cần ẩn." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [HttpPost]
        public JsonResult SanPhamThucHienHien(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            Product mProduct;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProduct.Status = true;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Hiện thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm cần ẩn." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Market> mMarketList;
            ProductPage mProductPage = new ProductPage();
            List<ProductType> mListProductType;
            List<ProductDistributor> mListProductDistributor;
            List<ProductManufacturer> mListProductManufacturer;

            try
            {
                mListProductType = productRepository.LayProductTypeAll();
                mListProductDistributor = productRepository.LayProductDistributorAll();
                mListProductManufacturer = productRepository.LayProductManufacturerAll();
                mMarketList = marketRepository.LayMarketTheoTrangAndType(1, 1000, 0);
                //Tao danh sach cac nhom tin
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType4(mListProductType, 0);
                mProductPage.HtmlNhom2 = V308HTMLHELPER.TaoDanhSachProductDistributor4(mListProductDistributor, 0);
                mProductPage.HtmlNhom3 = V308HTMLHELPER.TaoDanhSachProductManufacturer4(mListProductManufacturer, 0);
                mProductPage.HtmlNhom4 = V308HTMLHELPER.TaoDanhSachMarket(mMarketList, 0);
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamThucHienThemMoi(int? pTransport1, int? pTransport2, int? pTransport12, int? pTransport22, string pTitle, string pDapAn1, string pDapAn2, string pDapAn3, string pDapAn4, int? pKetQua, int? pGroup, int? pProductDistributor, int? pProductManufacturer, string pImage, int? pPrice, int? pPrice2, int? pPrice3, string pUnit1, string pUnit2, string pUnit3, string pSeri, string pSummary, string[] imageslide, string[] pNamePro, string[] pValuePro, string editor1, int? pOrder, int? pSaleOff, string pActive, int? pBaoHanh, int? pSize, int? pPower, int? pMotoGroup, int? pMarket, string pDescription, string pKeyWord)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            Product mProduct;
            ProductImage mProductImage;
            ProductAttribute mProductAttribute;
            int i = 0;
            try
            {
                mProduct = new Product()
                {
                    Name = pTitle,
                    Choice1 = pDapAn1,
                    Choice2 = pDapAn2,
                    Choice3 = pDapAn3,
                    Choice4 = pDapAn4,
                    Price2 = pPrice2,
                    Price3 = pPrice3,
                    Unit1 = pUnit1,
                    Unit2 = pUnit2,
                    Unit3 = pUnit3,
                    Answer = pKetQua,
                    Type = pGroup,
                    Distributor = pProductDistributor,
                    Manufacturer = pProductManufacturer,
                    AccountId = 0,
                    Date = DateTime.Now,
                    Detail = editor1,
                    Image = pImage,
                    Number = pOrder,
                    Price = pPrice,
                    MarketId = pMarket,
                    SeriNumber = pSeri,
                    Status = true,
                    Summary = pSummary,
                    Visible = true,
                    SaleOff = pSaleOff,
                    BaoHanh = pBaoHanh,
                    Size = pSize,
                    Power = pPower,
                    Group = pMotoGroup,
                    Description = pDescription,
                    Keyword = pKeyWord,
                    Transport1 = pTransport1,
                    Transport2 = pTransport2,
                    Transport12 = pTransport12,
                    Transport22 = pTransport22
                };
                mEntities.AddToProduct(mProduct);
                mEntities.SaveChanges();
                //Tao cac anh Slide va properties
                if (imageslide != null)
                {
                    foreach (string it in imageslide)
                    {
                        if (!String.IsNullOrEmpty(it))
                        {
                            mProductImage = new ProductImage() { Name = it, Number = 1, ProductID = mProduct.ID, Title = pTitle };
                            mEntities.AddToProductImage(mProductImage);
                        }
                    }
                }
                //Tao cac gia tri thuoc tinh
                if (pNamePro != null)
                {
                    foreach (string it in pNamePro)
                    {
                        if (!String.IsNullOrEmpty(it))
                        {
                            mProductAttribute = new ProductAttribute() { Name = pNamePro[i], Value = pValuePro[i], ProductID = mProduct.ID };
                            mEntities.AddToProductAttribute(mProductAttribute);
                        }
                        i++;
                    }
                }
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu sản phẩm thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult SanPhamSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Market> mMarketList;
            Product mProduct;
            ProductPage mProductPage = new ProductPage();
            List<ProductType> mListProductType;
            List<ProductDistributor> mListProductDistributor;
            List<ProductManufacturer> mListProductManufacturer;
            List<ProductImage> mProductImageList;
            List<ProductAttribute> mProductAttributeList;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mListProductType = productRepository.LayProductTypeAll();
                    mListProductDistributor = productRepository.LayProductDistributorAll();
                    mListProductManufacturer = productRepository.LayProductManufacturerAll();
                    mMarketList = marketRepository.LayMarketTheoTrangAndType(1, 1000, 0);
                    //lay cac anh cu va xoa di
                    mProductImageList = productRepository.LayProductImageTheoIDProduct(mProduct.ID);
                    //lay danh sach cac thuoc tinh cu
                    mProductAttributeList = productRepository.LayProductAttributeTheoIDProduct(mProduct.ID);
                    //Tao danh sach cac nhom tin
                    mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType4(mListProductType, (int)mProduct.Type);
                    mProductPage.HtmlNhom2 = V308HTMLHELPER.TaoDanhSachProductDistributor4(mListProductDistributor, (int)mProduct.Distributor);
                    mProductPage.HtmlNhom3 = V308HTMLHELPER.TaoDanhSachProductManufacturer4(mListProductManufacturer, (int)mProduct.Manufacturer);
                    mProductPage.HtmlNhom4 = V308HTMLHELPER.TaoDanhSachMarket(mMarketList, (int)mProduct.MarketId);
                    mProductPage.HtmlProductAttribute = V308HTMLHELPER.TaoDanhSachAnhAttributeSanPham(mProductAttributeList);
                    mProductPage.HtmlProductImage = V308HTMLHELPER.TaoDanhSachAnhSlideSanPham(mProductImageList);
                    mProductPage.pProduct = mProduct;
                }
                else
                {
                    mProductPage.Html = "Không tìm thấy sản phẩm cần sửa.";
                }
                return View(mProductPage);

            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult SanPhamThucHienSua(int pId, int? pTransport1, int? pTransport2, int? pTransport12, int? pTransport22, string pTitle, string pDapAn1, string pDapAn2, string pDapAn3, string pDapAn4, int? pKetQua, int? pGroup, int? pProductDistributor, int? pProductManufacturer, string pImage, int? pPrice, int? pPrice2, int? pPrice3, string pUnit1, string pUnit2, string pUnit3, string pSeri, string pSummary, string[] imageslide, string[] pNamePro, string[] pValuePro, string editor1, int? pOrder, int? pSaleOff, string pActive, int? pBaoHanh, int? pSize, int? pPower, int? pMotoGroup, int? pMarket, string pDescription, string pKeyWord)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            Product mProduct;
            ProductImage mProductImage;
            ProductAttribute mProductAttribute;
            List<ProductImage> mProductImageList;
            List<ProductAttribute> mProductAttributeList;
            int i = 0;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProduct.Name = pTitle;
                    mProduct.Choice1 = pDapAn1;
                    mProduct.Choice2 = pDapAn2;
                    mProduct.Choice3 = pDapAn3;
                    mProduct.Choice4 = pDapAn4;
                    mProduct.Price2 = pPrice2;
                    mProduct.Price3 = pPrice3;
                    mProduct.Unit1 = pUnit1;
                    mProduct.Unit2 = pUnit2;
                    mProduct.Unit3 = pUnit3;
                    mProduct.Unit3 = pUnit3;
                    mProduct.Answer = pKetQua;
                    mProduct.Date = DateTime.Now;
                    mProduct.Detail = editor1;
                    mProduct.Image = pImage;
                    mProduct.Number = pOrder;
                    mProduct.Summary = pSummary;
                    mProduct.Type = pGroup;
                    mProduct.SeriNumber = pSeri;
                    mProduct.Manufacturer = pProductManufacturer;
                    mProduct.Distributor = pProductDistributor;
                    mProduct.SaleOff = pSaleOff;
                    mProduct.MarketId = pMarket;
                    mProduct.BaoHanh = pBaoHanh;
                    mProduct.Size = pSize;
                    mProduct.Power = pPower;
                    mProduct.Price = pPrice;
                    mProduct.Group = pMotoGroup;
                    mProduct.Description = pDescription;
                    mProduct.Keyword = pKeyWord;
                    mProduct.Transport1 = pTransport1;
                    mProduct.Transport2 = pTransport2;
                    mProduct.Transport12 = pTransport12;
                    mProduct.Transport22 = pTransport22;
                    //Tao cac anh Slide va properties
                    if (imageslide != null)
                    {
                        if (imageslide.Count() > 0)
                        {
                            //lay cac anh cu va xoa di
                            mProductImageList = productRepository.LayProductImageTheoIDProduct(mProduct.ID);
                            //xoa cac anh cu nay
                            foreach (ProductImage it in mProductImageList)
                            {
                                mEntities.DeleteObject(it);
                            }
                            //luu ket qua
                            mEntities.SaveChanges();
                            //them cac anh moi
                            foreach (string it in imageslide)
                            {
                                if (!String.IsNullOrEmpty(it))
                                {
                                    mProductImage = new ProductImage() { Name = it, Number = 1, ProductID = mProduct.ID, Title = pTitle };
                                    mEntities.AddToProductImage(mProductImage);
                                }
                            }
                            //luu ket qua
                            mEntities.SaveChanges();
                        }
                    }
                    //Tao cac gia tri thuoc tinh
                    if (pNamePro != null)
                    {
                        if (pNamePro.Count() > 0)
                        {
                            //lay danh sach cac thuoc tinh cu
                            mProductAttributeList = productRepository.LayProductAttributeTheoIDProduct(mProduct.ID);
                            //xoa cac anh cu nay
                            foreach (ProductAttribute it in mProductAttributeList)
                            {
                                mEntities.DeleteObject(it);
                            }
                            //luu ket qua
                            mEntities.SaveChanges();
                            foreach (string it in pNamePro)
                            {
                                if (!String.IsNullOrEmpty(it))
                                {
                                    mProductAttribute = new ProductAttribute() { Name = pNamePro[i], Value = pValuePro[i], ProductID = mProduct.ID };
                                    mEntities.AddToProductAttribute(mProductAttribute);
                                }
                                i++;
                            }
                            //luu ket qua
                            mEntities.SaveChanges();
                        }
                    }
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sửa sản phẩm thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        #endregion

        #region LOAI HO TRO
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTroLoaiHoTro(int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportPage mSupportPage = new SupportPage();
            List<SupportType> mSupportTypeList;
            try
            {
                #region SAVE SESSION

                if (pPage == null)
                {
                    if (Session["LoaiHoTroPage"] != null)
                        pPage = (int)Session["LoaiHoTroPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["LoaiHoTroPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mSupportTypeList = supportRepository.LaySupportTypeTheoTrang((int)pPage, 10);
                if (mSupportTypeList.Count < 10)
                    mSupportPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mSupportPage.Html = V308HTMLHELPER.TaoDanhSachSupportType(mSupportTypeList, (int)pPage);
                mSupportPage.Page = (int)pPage;
                return View(mSupportPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [HttpPost]
        public JsonResult HoTroLoaiHoTroThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportType mSupportType;
            try
            {
                mSupportType = supportRepository.LaySupportTypeTheoId(pId);
                if (mSupportType != null)
                {
                    mEntities.DeleteObject(mSupportType);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy kiểu cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTroLoaiHoTroThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            List<ProductType> mProductType;
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        public JsonResult HoTroLoaiHoTroThucHienThemMoi(string pTieuDe, int? pUuTien)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportType mSupportType;
            try
            {
                mSupportType = new SupportType() { Date = DateTime.Now, Number = pUuTien, Name = pTieuDe };
                mEntities.AddToSupportType(mSupportType);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu loại hỗ trợ thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTroLoaiHoTroSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportPage mSupportPage = new SupportPage();
            SupportType mSupportType;
            try
            {
                mSupportType = supportRepository.LaySupportTypeTheoId(pId);
                if (mSupportType != null)
                {
                    mSupportPage.pSupportType = mSupportType;
                }
                else
                {
                    mSupportPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mSupportPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        public JsonResult HoTroLoaiHoTroThucHienSua(int pId, string pTieuDe, int? pUuTien)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportType mSupportType;
            try
            {
                mSupportType = supportRepository.LaySupportTypeTheoId(pId);
                if (mSupportType != null)
                {
                    mSupportType.Name = pTieuDe;
                    mSupportType.Date = DateTime.Now;
                    mSupportType.Number = pUuTien;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại hỗ trợ thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy hỗ trợ để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }

        }
        #endregion

        #region HO TRO
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTro(int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportPage mSupportPage = new SupportPage();
            List<Support> mSupportList;
            try
            {
                if (pPage == null)
                {
                    if (Session["HoTroPage"] != null)
                        pPage = (int)Session["HoTroPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["HoTroPage"] = pPage;
                }
                /*Lay danh sach cac tin theo page*/
                mSupportList = supportRepository.LayTheoTrang((int)pPage, 10);
                if (mSupportList.Count < 10)
                    mSupportPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mSupportPage.Html = V308HTMLHELPER.TaoDanhSachSupport(mSupportList, (int)pPage);
                mSupportPage.Page = (int)pPage;
                return View(mSupportPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [HttpPost]
        public JsonResult HoTroThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            Support mSupport;
            try
            {
                mSupport = supportRepository.LayTheoId(pId);
                if (mSupport != null)
                {
                    mEntities.DeleteObject(mSupport);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy kiểu cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTroThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportPage mSupportPage = new SupportPage();
            List<SupportType> mSupportType;
            try
            {
                mSupportType = supportRepository.LaySupportTypeAll();
                mSupportPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachSupportType2(mSupportType, 0);
                return View(mSupportPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        public JsonResult HoTroThucHienThemMoi(string pTieuDe, int? pUuTien, int? pGroupId, string pNick, string pMobile, string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            Support mSupport;
            try
            {
                mSupport = new Support() { Date = DateTime.Now, Name = pTieuDe, TypeID = pGroupId, Phone = pMobile, Nick = pNick, Email = pEmail };
                mEntities.AddToSupport(mSupport);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu loại hỗ trợ thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(7)]
        public ActionResult HoTroSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            SupportPage mSupportPage = new SupportPage();
            Support mSupport;
            List<SupportType> mSupportType;
            try
            {
                mSupport = supportRepository.LayTheoId(pId);
                if (mSupport != null)
                {
                    mSupportType = supportRepository.LaySupportTypeAll();
                    mSupportPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachSupportType3(mSupportType, (int)mSupport.TypeID);
                    mSupportPage.pSupport = mSupport;
                }
                else
                {
                    mSupportPage.Html = "Không tìm thấy tin tức cần sửa.";
                }
                return View(mSupportPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        public JsonResult HoTroThucHienSua(int pId, string pTieuDe, int? pUuTien, int? pGroupId, string pNick, string pMobile, string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SupportRepository supportRepository = new SupportRepository(mEntities);
            Support mSupport;
            try
            {
                mSupport = supportRepository.LayTheoId(pId);
                if (mSupport != null)
                {
                    mSupport.Name = pTieuDe;
                    mSupport.Date = DateTime.Now;
                    mSupport.TypeID = pGroupId;
                    mSupport.Nick = pNick;
                    mSupport.Phone = pMobile;
                    mSupport.Email = pEmail;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể hỗ trợ thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy hỗ trợ để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                supportRepository.Dispose();
            }

        }
        #endregion

        #region THE LOAI FILE
        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult TheLoaiFile(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            List<FileType> mmFileList;
            FilePage mFilePage = new FilePage();
            List<FileType> mFileTypeAll;
            FileType mFileType;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["LoaiFileType"] != null)
                        pType = (int)Session["LoaiFileType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["LoaiFileType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["LoaiFilePage"] != null)
                        pPage = (int)Session["LoaiFilePage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["LoaiFilePage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mFileType = fileRepository.LayTheLoaiFileTheoId((int)pType);
                    if (mFileType != null)
                        mLevel = mFileType.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mFileTypeAll = fileRepository.LayNhomFileAll();
                /*Lay danh sach cac tin theo page*/
                mmFileList = fileRepository.LayFileTypeTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                if (mmFileList.Count < 10)
                    mFilePage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mFilePage.Html = V308HTMLHELPER.TaoDanhSachCacNhomFile(mmFileList, (int)pPage);
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFileHome2(mFileTypeAll, (int)pPage, (int)pType);
                mFilePage.Page = (int)pPage;
                mFilePage.TypeId = (int)pType;
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [HttpPost]
        public JsonResult ThucHienXoaTheLoaiFile(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FileType mFileType;
            try
            {
                mFileType = fileRepository.LayTheLoaiFileTheoId(pId);
                if (mFileType != null)
                {
                    mEntities.DeleteObject(mFileType);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult TheLoaiFileThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FilePage mFilePage = new FilePage();
            List<FileType> mListFileType;
            try
            {
                mListFileType = fileRepository.LayNhomFileAll();
                //Tao danh sach cac nhom tin
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile2(mListFileType, 0);
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        public JsonResult TheLoaiFileThucHienThemMoi(string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FileType mFileType;
            FileType mFileTypeParent;
            string[] mLevelArray;
            int mLevel = 0;
            try
            {
                if (pGroupId == 0)
                {
                    //Tinh gia tri Level moi cho Group nay
                    //1- Lay tat ca cac Group me
                    //2- Convert gia tri Level de lay gia tri lon nhat
                    //3- Tao gia tri moi lon hon gia tri lon nhat
                    mLevelArray = (from p in mEntities.FileType
                                   where p.ParentID == 0
                                   select p.Level).ToArray();
                    mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                    mLevel = (mLevel + 1);
                    mFileType = new FileType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, ParentID = pGroupId };
                    mEntities.AddToFileType(mFileType);
                    mEntities.SaveChanges();
                }
                else
                {

                    //lay level cua nhom me
                    mFileTypeParent = fileRepository.LayTheLoaiFileTheoId((int)pGroupId);
                    if (mFileTypeParent != null)
                    {
                        mLevelArray = (from p in mEntities.NewsGroups
                                       where (p.Level.Substring(0, mFileTypeParent.Level.Length).Equals(mFileTypeParent.Level)) && (p.Level.Length == (mFileTypeParent.Level.Length + 5))
                                       select p.Level).ToArray();
                        if (mLevelArray.Count() > 0)
                        {
                            mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                            mLevel = (mLevel + 1);
                        }
                        else
                        {
                            mLevel = Convert.ToInt32(mFileTypeParent.Level.ToString().Trim() + "10001");
                        }
                        mFileType = new FileType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, ParentID = pGroupId };
                        mEntities.AddToFileType(mFileType);
                        mEntities.SaveChanges();
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Không tìm thấy nhóm file." });
                    }
                }
                return Json(new { code = 1, message = "Lưu loại file thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult TheLoaiFileSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FilePage mFilePage = new FilePage();
            List<FileType> mFileTypeList;
            FileType mFileType;
            try
            {
                mFileType = fileRepository.LayTheLoaiFileTheoId(pId);
                if (mFileType != null)
                {
                    mFileTypeList = fileRepository.LayNhomFileAll();
                    //Tao danh sach cac nhom tin
                    mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile3(mFileTypeList, (int)mFileType.ParentID);
                    mFilePage.pFileType = mFileType;
                }
                else
                {
                    mFilePage.Html = "Không tìm thấy loại file cần sửa.";
                }
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        public JsonResult TheLoaiFileThucHienSua(int pId, string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FileType mFileType;
            try
            {
                mFileType = fileRepository.LayTheLoaiFileTheoId(pId);
                if (mFileType != null)
                {
                    mFileType.Name = pTieuDe;
                    mFileType.Date = DateTime.Now;
                    mFileType.Number = pUuTien;
                    mFileType.ParentID = pGroupId;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa thể loại File thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy loại File để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }
        #endregion

        #region FILE
        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult File(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            List<File> mmFileList;
            List<FileType> mFileTypeList;
            FilePage mFilePage = new FilePage();
            FileType mFileType;
            string mLevel = "";
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["FileType"] != null)
                        pType = (int)Session["FileType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["FileType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["FilePage"] != null)
                        pPage = (int)Session["FilePage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["FilePage"] = pPage;
                }
                #endregion
                //lay Level cua Type
                if (pType != 0)
                {
                    mFileType = fileRepository.LayTheLoaiFileTheoId((int)pType);
                    if (mFileType != null)
                        mLevel = mFileType.Level.Trim();
                }
                /*Lay danh sach cac tin theo page*/
                mmFileList = fileRepository.LayFileTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
                mFileTypeList = fileRepository.LayNhomFileAll();
                if (mmFileList.Count < 10)
                    mFilePage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mFilePage.Html = V308HTMLHELPER.TaoDanhSachCacFile(mmFileList, (int)pPage);
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFileHome(mFileTypeList, (int)pPage, (int)pType);
                mFilePage.Page = (int)pPage;
                mFilePage.TypeId = (int)pType;
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [HttpPost]
        public JsonResult ThucHienXoaFile(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            File mFile;
            try
            {
                mFile = fileRepository.LayFileTheoId(pId);
                if (mFile != null)
                {
                    mEntities.DeleteObject(mFile);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy File cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult FileThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FilePage mFilePage = new FilePage();
            List<FileType> mListFileType;
            try
            {
                mListFileType = fileRepository.LayNhomFileAll();
                //Tao danh sach cac nhom tin
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile4(mListFileType, 0);
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        public JsonResult FileThucHienThemMoi(string pTitle, int? pGroupId, string pLink, string pKichHoat, int? pValue, string pSummary, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            File mFile;
            try
            {
                mFile = new File() { Date = DateTime.Now, LinkFile = pImageUrl, Status = ConverterUlti.ConvertStringToLogic2(pKichHoat), FileName = pTitle, Summary = pSummary, TypeID = pGroupId, Value = pValue };
                mEntities.AddToFile(mFile);
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu  File thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(5)]
        public ActionResult FileSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            FilePage mFilePage = new FilePage();
            List<FileType> mFileTypeList;
            File mFile;
            try
            {
                mFile = fileRepository.LayFileTheoId(pId);
                if (mFile != null)
                {
                    mFileTypeList = fileRepository.LayNhomFileAll();
                    //Tao danh sach cac nhom tin
                    mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile4(mFileTypeList, (int)mFile.TypeID);
                    mFilePage.pFile = mFile;
                }
                else
                {
                    mFilePage.Html = "Không tìm thấy File cần sửa.";
                }
                return View(mFilePage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        public JsonResult ThucHienSuaFile(int pId, string pTitle, int? pGroupId, int? pValue, string pLink, string pKichHoat, string pSummary, string pImageUrl)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            FileRepository fileRepository = new FileRepository(mEntities);
            File mFile;
            try
            {
                mFile = fileRepository.LayFileTheoId(pId);
                if (mFile != null)
                {
                    mFile.FileName = pTitle;
                    mFile.TypeID = pGroupId;
                    mFile.LinkFile = pImageUrl;
                    mFile.Summary = pSummary;
                    mFile.Date = DateTime.Now;
                    mFile.Value = pValue;
                    mFile.Status = ConverterUlti.ConvertStringToLogic2(pKichHoat);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa File thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy File để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                fileRepository.Dispose();
            }

        }
        #endregion

        #region ORDER
        [CustomAuthorize]
        [CheckAdminAuthorize(3)]
        public ActionResult Order(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 6, (int)pType);
                //lay danh sach cac kieu san pham
                if (mProductList.Count < 6)
                    mProductPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachHoaDon(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(3)]
        [HttpPost]
        public JsonResult OrderThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductOrder mProductOrder;
            try
            {
                mProductOrder = productRepository.LayProductOrderTheoId(pId);
                if (mProductOrder != null)
                {
                    mEntities.DeleteObject(mProductOrder);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy hóa đơn cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(3)]
        public ActionResult OrderChiTiet(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductOrder mProductOrder;
            ProductPage mProductPage = new ProductPage();
            ShopCart mShopCart;
            try
            {
                mProductOrder = productRepository.LayProductOrderTheoId(pId);
                if (mProductOrder != null)
                {
                    mShopCart = JsonSerializer.DeserializeFromString<ShopCart>(mProductOrder.ProductDetail);
                    mProductPage.pProductOrder = mProductOrder;
                    mProductPage.Voucher = mShopCart.Voucher;
                    mProductOrder.ProductDetail = V308HTMLHELPER.TaoDanhSachSanPhamGioHangAdmin(mShopCart.List);
                    mProductPage.ShopCart = mShopCart;
                }
                else
                {
                    mProductPage.pProductOrder = new ProductOrder();
                    mProductPage.Html = "Không tìm thấy sản phẩm.";
                }
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra trên hệ thống. Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(3)]
        public ActionResult OrderExportToExcel(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductOrder mProductOrder;
            ProductPage mProductPage = new ProductPage();
            ShopCart mShopCart = new ShopCart();
            StringBuilder Str = new StringBuilder();
            try
            {
                mProductOrder = productRepository.LayProductOrderTheoId(pId);
                if (mProductOrder != null)
                {
                    mShopCart = JsonSerializer.DeserializeFromString<ShopCart>(mProductOrder.ProductDetail);
                }
                else
                {
                    mProductPage.pProductOrder = new ProductOrder();
                    mProductPage.Html = "Không tìm thấy sản phẩm.";
                }
                //
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachement; filename=data.xls");
                Response.ContentType = "application/excel";

                Str.Append("<table>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\" style=\"text-align:center;font-size:16px;\">Công ty CP Thương Mại & Thực Phẩm Clean Food Việt Nam</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\">Số 42, Thái Thịnh 1, Thịnh Quang, Đống Đa, Hà Nội</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\"  style=\"text-align:center;font-size:16px;font-weight:bold;\">ĐẶC SẢN VÙNG CAO TÂM VIỆT</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"3\">Tel: 04.668.866.15</td>");
                Str.Append("<td colspan=\"3\">Hotline: 093.94.88883</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\">Web site : dacsanvungcaovn.com - dacsantamviet.com</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\">Facebook: Đặc Sản Vùng Cao</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"3\">Số Hóa Đơn: CF" + mProductOrder.ID + "</td>");
                Str.Append("<td colspan=\"3\">Ngày: " + mProductOrder.Date.ToString() + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"3\">Khách hàng:" + mProductOrder.FullName + "</td>");
                Str.Append("<td colspan=\"3\">Nợ trước:100%</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"3\">Địa chỉ:" + mProductOrder.Address + "</td>");
                Str.Append("<td colspan=\"3\">ĐT:" + mProductOrder.Phone + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\"  style=\"text-align:center;font-size:16px;font-weight:bold;\">PHIẾU THANH TOÁN</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td>ID</td>");
                Str.Append("<td>Tên Hàng</td>");
                Str.Append("<td>SL</td>");
                Str.Append("<td>Giá Bán</td>");
                Str.Append("<td>%</td>");
                Str.Append("<td>Thành tiền</td>");
                Str.Append("</tr>");
                foreach (Product it in mShopCart.List)
                {
                    Str.Append("<tr>");
                    Str.Append("<td>" + it.ID + "</td>");
                    Str.Append("<td>" + it.Name + "</td>");
                    Str.Append("<td>" + it.Number + "</td>");
                    Str.Append("<td>" + String.Format("{0: 0,0}", (it.Price)) + "</td>");
                    Str.Append("<td>" + it.SaleOff + " %</td>");
                    Str.Append("<td>" + String.Format("{0: 0,0}", (it.Price - ((it.Price / 100) * it.SaleOff))) + "</td>");
                    Str.Append("</tr>");
                }
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Tổng tiền hàng</td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalBefore())) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Giảm giá</td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.Voucher)) + "  %</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Thực thu</td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalPrice())) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Phí ship</td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.ServicePrice)) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Dịch vụ sơ chế </td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (0)) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Dịch vụ Tẩm ướp</td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (0)) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Tổng tiền phải thanh toán </td>");
                Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalPrice())) + "</td>");
                Str.Append("</tr>");
                Str.Append("<tr>");
                Str.Append("<td colspan=\"6\">Đặc sản vùng cao Tâm Việt cảm ơn quý khách đã ủng hộ</td>");
                Str.Append("</tr>");
                Str.Append("</table>");

                Response.Output.Write(Str.ToString());
                Response.Flush();
                Response.End();
                return View();
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra trên hệ thống. Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(3)]
        [ValidateInput(false)]
        public JsonResult OrderXacNhan(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductOrder mProductOrder;
            try
            {
                mProductOrder = productRepository.LayProductOrderTheoId(pId);
                if (mProductOrder != null)
                {
                    mProductOrder.Status = 1;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xác nhận thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy hóa đơn cần Xác nhận." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }

        }
        #endregion

        #region AdminAccount
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult AdminAccount(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            List<Admin> mmAccountList;
            AccountPage mAccountPage = new AccountPage();
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["AdminAccountType"] != null)
                        pType = (int)Session["AdminAccountType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["AdminAccountType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["AdminAccountPage"] != null)
                        pPage = (int)Session["AdminAccountPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["AdminAccountPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mmAccountList = accountRepository.LayAdminTheoTrangAndType((int)pPage, 10, (int)pType);
                if (mmAccountList.Count < 10)
                    mAccountPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mAccountPage.Html = V308HTMLHELPER.TaoDanhSachCacAdminAccount(mmAccountList, (int)pPage);
                mAccountPage.Page = (int)pPage;
                mAccountPage.TypeId = (int)pType;
                return View(mAccountPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult ThucHienXoaAdminAccount(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            try
            {
                mAdmin = accountRepository.LayAdminTheoId(pId);
                if (mAdmin != null)
                {
                    mEntities.DeleteObject(mAdmin);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult AdminAccountThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [CheckDelete]
        [ValidateInput(false)]
        public JsonResult AdminAccountThucHienThemMoi(bool? phethong, bool? psanpham, bool? ptintuc, bool? pkhachhang, bool? phinhanh, bool? pupload, bool? ptaikhoan, bool? pthungrac, string pTitle, int pGroupId, string pAccount, string pPassword1, string pPassword2, string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            try
            {
                if (pPassword1.Trim().Equals(pPassword2.Trim()))
                {
                    if (pAccount.Length > 5 && pPassword1.Length > 5)
                    {
                        mAdmin = accountRepository.LayAdminTheoUserName(pAccount);
                        if (!(mAdmin != null))
                        {
                            mAdmin = new Admin()
                            {
                                Date = DateTime.Now,
                                Role = pGroupId,
                                FullName = pTitle,
                                Email = pEmail,
                                UserName = pAccount,
                                Password = EncryptionMD5.ToMd5(pPassword1.Trim()),
                                //PSanPham = (psanpham),
                                //PFileUpload = (pupload),
                                //PHeThong = (phethong),
                                //PHinhAnh = (phinhanh),
                                //PKhachHang = (pkhachhang),
                                //PTaiKhoan = (ptaikhoan),
                                //PThungRac = (pthungrac),
                                //PTinTuc = (ptintuc)
                            };
                            mEntities.AddToAdmin(mAdmin);
                            mEntities.SaveChanges();
                            return Json(new { code = 1, message = "Lưu  tài khoản thành công." });
                        }
                        else
                        {
                            return Json(new { code = 0, message = "Tài khoản đã tồn tại. Vui lòng tại tài khoản mới." });
                        }
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Mật khẩu và tài khoản và có độ dài tối thiểu 6 kí tự." });
                    }
                }
                else
                {
                    return Json(new { code = 0, message = "Mật khẩu xác nhận không trùng khớp." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult AdminAccountSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            AccountPage mAccountPage = new AccountPage();
            try
            {
                mAdmin = accountRepository.LayAdminTheoId(pId);
                if (mAdmin != null)
                {
                    mAccountPage.pAdmin = mAdmin;
                    mAccountPage.TypeId = (int)mAdmin.Role;
                }
                else
                {
                    mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                    mAccountPage.pAdmin = new Admin();
                }
                return View(mAccountPage);
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra trên hệ thống");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [CheckDelete]
        [ValidateInput(false)]
        public JsonResult ThucHienSuaAdminAccount(bool? phethong, bool? psanpham, bool? ptintuc, bool? pkhachhang, bool? phinhanh, bool? pupload, bool? ptaikhoan, bool? pthungrac, int pId, string pTitle, int pGroupId, string pAccount, bool? pActive, string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            try
            {
                mAdmin = accountRepository.LayAdminTheoId(pId);
                if (mAdmin != null)
                {
                    mAdmin.FullName = pTitle;
                    mAdmin.Role = pGroupId;
                    mAdmin.Email = pEmail;
                    mAdmin.Status = (pActive);
                    //mAdmin.PSanPham = (psanpham);
                    //mAdmin.PFileUpload = (pupload);
                    //mAdmin.PHeThong = (phethong);
                    //mAdmin.PHinhAnh = (phinhanh);
                    //mAdmin.PKhachHang = (pkhachhang);
                    //mAdmin.PTaiKhoan = (ptaikhoan);
                    //mAdmin.PThungRac = (pthungrac);
                    //mAdmin.PTinTuc = (ptintuc);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Hoàn thành sửa thông tin." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy thông tin tài khoản." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Lỗi hệ thống. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }


        [CustomAuthorize]
        public ActionResult AccountInfo()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            AccountPage mAccountPage = new AccountPage();
            try
            {
                int pId = (int)Session["UserId"];
                mAdmin = accountRepository.LayAdminTheoId(pId);
                if (mAdmin != null)
                {
                    mAccountPage.pAdmin = mAdmin;
                }
                else
                {
                    mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                    mAccountPage.pAdmin = new Admin();
                }
                return View(mAccountPage);
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra trên hệ thống");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        public JsonResult ThucHienDoiMatKhau(int pId, string pPassword1, string pPassword2, string pPassword3)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Admin mAdmin;
            try
            {
                int mId = (int)Session["UserId"];
                mAdmin = accountRepository.LayAdminTheoId(mId);
                if (mAdmin != null)
                {
                    if (mAdmin.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword1.Trim())))
                    {
                        if (pPassword2.Trim().Equals(pPassword3.Trim()))
                        {
                            if (pPassword2.Length > 5)
                            {
                                mAdmin.Password = EncryptionMD5.ToMd5(pPassword2.Trim());
                                mEntities.SaveChanges();
                                return Json(new { code = 1, message = "thay đổi mật khẩu thành công." });
                            }
                            else
                            {
                                return Json(new { code = 0, message = "Mật khẩu và tài khoản và có độ dài tối thiểu 6 kí tự." });
                            }
                        }
                        else
                        {
                            return Json(new { code = 0, message = "Mật khẩu mới và Mật khẩu xác nhận không trùng khớp." });
                        }
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Mật khẩu cũ không chính xác." });
                    }
                }
                else
                {
                    return Json(new { code = 0, message = "Tài khoản không tồn tại." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }


        }
        #endregion

        #region Account
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult Account(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            List<Account> mmAccountList;
            AccountPage mAccountPage = new AccountPage();
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["AccountType"] != null)
                        pType = (int)Session["AccountType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["AccountType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["AccountPage"] != null)
                        pPage = (int)Session["AccountPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["AccountPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mmAccountList = accountRepository.LayAccountTheoTrangAndType((int)pPage, 10, (int)pType);
                if (mmAccountList.Count < 10)
                    mAccountPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mAccountPage.Html = V308HTMLHELPER.TaoDanhSachCacAccount(mmAccountList, (int)pPage);
                mAccountPage.Page = (int)pPage;
                mAccountPage.TypeId = (int)pType;
                return View(mAccountPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult ThucHienXoaAccount(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Account mAccount;
            try
            {
                mAccount = accountRepository.LayTinTheoId(pId);
                if (mAccount != null)
                {
                    mEntities.DeleteObject(mAccount);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult AccountThucHienAn(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Account mAccount;
            try
            {
                mAccount = accountRepository.LayTinTheoId(pId);
                if (mAccount != null)
                {
                    mAccount.Status = false;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Khóa tài khoản thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult AccountThucHienHien(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Account mAccount;
            try
            {
                mAccount = accountRepository.LayTinTheoId(pId);
                if (mAccount != null)
                {
                    mAccount.Status = true;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Kích hoạt thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult AccountChiTiet(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            Account mAccount;
            AccountPage mAccountPage = new AccountPage();
            try
            {
                mAccount = accountRepository.LayTinTheoId(pId);
                if (mAccount != null)
                {
                    mAccountPage.pAccount = mAccount;
                }
                else
                {
                    mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                    mAccountPage.pAccount = new Account();
                }
                return View(mAccountPage);
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra trên hệ thống");
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }
        }
        #endregion

        #region Market
        [CustomAuthorize]
        [CheckAdminAuthorize(6)]
        public ActionResult Market(int? pType, int? pPage)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Market> mMarketList;
            MarketPage mMarketPage = new MarketPage();
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["MarketType"] != null)
                        pType = (int)Session["MarketType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["MarketType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["MarketPage"] != null)
                        pPage = (int)Session["MarketPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["MarketPage"] = pPage;
                }
                #endregion
                /*Lay danh sach cac tin theo page*/
                mMarketList = marketRepository.LayMarketTheoTrangAndType((int)pPage, 10, (int)pType);
                if (mMarketList.Count < 10)
                    mMarketPage.IsEnd = true;
                //Tao Html cho danh sach tin nay
                mMarketPage.Html = V308HTMLHELPER.TaoDanhSachCacMarket(mMarketList, (int)pPage);
                mMarketPage.Page = (int)pPage;
                mMarketPage.TypeId = (int)pType;
                return View(mMarketPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult MarketThemMoi()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult MarketThucHienThemMoi(string pUserName, string pAvata, int pMarketType, string pEmail, string pFullName, string pMobile, string pSumary, bool pActive = true)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductType> mList;
            Market mMarket;
            try
            {

                mMarket = new Market() { Date = DateTime.Now, BirthDay = DateTime.Now, UserName = Ultility.LocDau2(pUserName.Trim()), Avata = pAvata, Email = pEmail, FullName = pFullName, Gender = true, Phone = pMobile, Role = pMarketType, Status = pActive, Sumary = pSumary };
                mEntities.AddToMarket(mMarket);
                mEntities.SaveChanges();
                //lay danh sách nhom san pham
                mList = productRepository.getProductTypeParent();
                foreach (ProductType it in mList)
                {
                    MarketProductType mMarketProductType = new MarketProductType() { Date = DateTime.Now, Name = it.Name, Detail = it.Name, Parent = it.ID, Status = true, Visible = true, Number = 1, MarketId = mMarket.ID, MarketName = mMarket.UserName, ImageBanner = it.ImageBanner };
                    mEntities.AddToMarketProductType(mMarketProductType);
                }
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu cửa hàng thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(1)]
        public ActionResult MarketSua(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            MarketPage mMarketPage = new MarketPage();
            try
            {
                mMarket = marketRepository.LayTheoId(pId);
                if (mMarket != null)
                {
                    mMarketPage.Market = mMarket;
                }
                else
                {
                    mMarketPage.Html = "Không tìm thấy cửa hàng cần sửa.";
                }
                return View(mMarketPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        public JsonResult MarketThucHienSua(int pId, string pUserName, string pAvata, string pEmail, int pMarketType, string pFullName, string pMobile, string pSumary, bool pActive = true)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            MarketPage mMarketPage = new MarketPage();
            try
            {
                mMarket = marketRepository.LayTheoId(pId);
                if (mMarket != null)
                {
                    mMarket.UserName = pUserName;
                    mMarket.Date = DateTime.Now;
                    mMarket.Avata = pAvata;
                    mMarket.Email = pEmail;
                    mMarket.FullName = pFullName;
                    mMarket.Phone = pMobile;
                    mMarket.Sumary = pSumary;
                    mMarket.Status = pActive;
                    mMarket.Role = pMarketType;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Sủa cửa hàng thành công." });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy cửa hàng để sửa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }

        }


        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult MarketThucHienXoa(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            Market mMarket;
            List<Product> mList = new List<Product>();
            try
            {
                mMarket = marketRepository.LayTheoId(pId);
                if (mMarket != null)
                {
                    //lay danh sach cac san pham cua sieu thi
                    mList = productRepository.getByPageSizeMarketId(1, 1000, mMarket.ID);
                    //xoa cac san pham nay
                    foreach (Product it in mList)
                    {
                        mEntities.DeleteObject(it);
                    }
                    mEntities.DeleteObject(mMarket);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Xóa thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }

        }

        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult MarketThucHienAn(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            try
            {
                mMarket = marketRepository.LayTheoId(pId);
                if (mMarket != null)
                {
                    mMarket.Status = false;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Khóa tài khoản thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }

        }
        [CustomAuthorize]
        [CheckAdminJson(6)]
        [HttpPost]
        public JsonResult MarketThucHienHien(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            try
            {
                mMarket = marketRepository.LayTheoId(pId);
                if (mMarket != null)
                {
                    mMarket.Status = true;
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Kích hoạt thành công!" });
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
                }
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }

        }
        #endregion

        #region Bao cao
        [CustomAuthorize]
        [CheckAdminAuthorize(10)]
        public ActionResult DonHangNgay(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            int mCountOrder1 = 0;
            int mCountOrder2 = 0;
            int mCountOrderHuy = 0;
            int mCountOrderGiaoMotPhan = 0;
            int mMoney = 0;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                foreach (ProductOrder it in mProductList)
                {
                    if (it.Status == 0)
                    {
                        mCountOrder1 = (mCountOrder1 + 1);
                    }
                    else if (it.Status == -1)
                    {
                        mCountOrderHuy = (mCountOrderHuy + 1);
                    }
                    else if (it.Status == 2)
                    {
                        mCountOrderGiaoMotPhan = (mCountOrderGiaoMotPhan + 1);
                    }
                    else
                    {
                        mCountOrder2 = (mCountOrder2 + 1);
                    }
                    mMoney = (mMoney + (int)it.Price);
                }
                //
                mProductPage.CountOrder1 = mCountOrder1;
                mProductPage.CountOrder2 = mCountOrder2;
                mProductPage.CountOrderHuy = mCountOrderHuy;
                mProductPage.CountOrderGiaoMotPhan = mCountOrderGiaoMotPhan;
                mProductPage.Money = mMoney;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachThongKeDongHang(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult DoanhSoNgay(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            int mCountOrder1 = 0;
            int mCountOrder2 = 0;
            int mMoney = 0;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                foreach (ProductOrder it in mProductList)
                {
                    if (it.Status == 0)
                    {
                        mCountOrder1 = (mCountOrder1 + 1);
                    }
                    else 
                    {
                        mCountOrder2 = (mCountOrder2 + 1);
                    }
                    mMoney = (mMoney + (int)it.Price);
                }
                //
                mProductPage.CountOrder1 = mCountOrder1;
                mProductPage.CountOrder2 = mCountOrder2;
                mProductPage.Money = mMoney;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachThongKeDongHang(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }

        public ActionResult ShareLink() {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage page = new ProductPage();
            try
            {
                page.Money = 0;
                return View(page);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }

        public ActionResult Voucher()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            ProductPage VoucherPage = new ProductPage();
            try
            {
                VoucherPage.Money = 0;
                return View(VoucherPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }

        [CustomAuthorize]
        [CheckAdminAuthorize(10)]
        public ActionResult DonHangTungNgay(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            int mCountOrder1 = 0;
            int mCountOrder2 = 0;
            int mCountOrderHuy = 0;
            int mCountOrderGiaoMotPhan = 0;
            int mMoney = 0;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                foreach (ProductOrder it in mProductList)
                {
                    if (it.Status == 0)
                    {
                        mCountOrder1 = (mCountOrder1 + 1);
                    }
                    else if (it.Status == -1)
                    {
                        mCountOrderHuy = (mCountOrderHuy + 1);
                    }
                    else if (it.Status == 2)
                    {
                        mCountOrderGiaoMotPhan = (mCountOrderGiaoMotPhan + 1);
                    }
                    else
                    {
                        mCountOrder2 = (mCountOrder2 + 1);
                    }
                    mMoney = (mMoney + (int)it.Price);
                }
                //
                mProductPage.CountOrder1 = mCountOrder1;
                mProductPage.CountOrder2 = mCountOrder2;
                mProductPage.CountOrderHuy = mCountOrderHuy;
                mProductPage.CountOrderGiaoMotPhan = mCountOrderGiaoMotPhan;
                mProductPage.Money = mMoney;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachThongKeThang(mProductList, (int)pPage, mDate1, mDate2);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        #endregion 
        
        #region Thong Ke
        [CustomAuthorize]
        [CheckAdminAuthorize(11)]
        public ActionResult KhachMua1Lan(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.AddYears(-3).Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderKhachMua1Lan((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachKHMuaMotLan(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult DonHangVoucher(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            int mCountOrder1 = 0;
            int mCountOrder2 = 0;
            int mCountOrderHuy = 0;
            int mCountOrderGiaoMotPhan = 0;
            int mMoney = 0;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                foreach (ProductOrder it in mProductList)
                {
                    if (it.Status == 0)
                    {
                        mCountOrder1 = (mCountOrder1 + 1);
                    }
                    else if (it.Status == -1)
                    {
                        mCountOrderHuy = (mCountOrderHuy + 1);
                    }
                    else if (it.Status == 2)
                    {
                        mCountOrderGiaoMotPhan = (mCountOrderGiaoMotPhan + 1);
                    }
                    else
                    {
                        mCountOrder2 = (mCountOrder2 + 1);
                    }
                    mMoney = (mMoney + (int)it.Price);
                }
                //
                mProductPage.CountOrder1 = mCountOrder1;
                mProductPage.CountOrder2 = mCountOrder2;
                mProductPage.CountOrderHuy = mCountOrderHuy;
                mProductPage.CountOrderGiaoMotPhan = mCountOrderGiaoMotPhan;
                mProductPage.Money = mMoney;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachThongKeDongHang(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(11)]
        public ActionResult DonHangLink(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            int mCountOrder1 = 0;
            int mCountOrder2 = 0;
            int mCountOrderHuy = 0;
            int mCountOrderGiaoMotPhan = 0;
            int mMoney = 0;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                foreach (ProductOrder it in mProductList)
                {
                    if (it.Status == 0)
                    {
                        mCountOrder1 = (mCountOrder1 + 1);
                    }
                    else if (it.Status == -1)
                    {
                        mCountOrderHuy = (mCountOrderHuy + 1);
                    }
                    else if (it.Status == 2)
                    {
                        mCountOrderGiaoMotPhan = (mCountOrderGiaoMotPhan + 1);
                    }
                    else
                    {
                        mCountOrder2 = (mCountOrder2 + 1);
                    }
                    mMoney = (mMoney + (int)it.Price);
                }
                //
                mProductPage.CountOrder1 = mCountOrder1;
                mProductPage.CountOrder2 = mCountOrder2;
                mProductPage.CountOrderHuy = mCountOrderHuy;
                mProductPage.CountOrderGiaoMotPhan = mCountOrderGiaoMotPhan;
                mProductPage.Money = mMoney;
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachThongKeDongHang(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(11)]
        public ActionResult SoKhachCu(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachKHCu(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(11)]
        public ActionResult SoKhachMoi(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderTheoTrangAndType((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                //lay danh sach cac kieu san pham
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachKHMoi(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        [CustomAuthorize]
        [CheckAdminAuthorize(11)]
        public ActionResult TopKhachVip(int? pType, int? pPage, string pDate1 = "", string pDate2 = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            List<ProductOrder> mProductList;
            ProductPage mProductPage = new ProductPage();
            DateTime mDate1;
            DateTime mDate2;
            try
            {
                #region SAVE SESSION
                if (pType == null)
                {
                    if (Session["OrderType"] != null)
                        pType = (int)Session["OrderType"];
                    else
                        pType = 0;
                }
                else
                {
                    Session["OrderType"] = pType;
                }
                if (pPage == null)
                {
                    if (Session["OrderPage"] != null)
                        pPage = (int)Session["OrderPage"];
                    else
                        pPage = 1;
                }
                else
                {
                    Session["OrderPage"] = pPage;
                }
                #endregion
                //lay ngay băt dau và ngay ket thuc!
                if (pDate1.Length < 1 || pDate2.Length < 1)
                {
                    mDate2 = DateTime.Now;
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate2.Month + "/01" + "/" + mDate2.AddYears(-5).Year;
                    mDate1 = DateTime.Parse(ViewBag.Date1);
                    mDate2 = DateTime.Parse(ViewBag.Date2);
                }
                else
                {
                    mDate1 = DateTime.Parse(pDate1);
                    mDate2 = DateTime.Parse(pDate2);
                    ViewBag.Date2 = mDate2.ToString("MM/dd/yyyy");
                    ViewBag.Date1 = mDate1.ToString("MM/dd/yyyy");
                }
                /*Lay danh sach cac tin theo page*/
                mProductList = productRepository.LayOrderKhachVIP((int)pPage, 1000, (int)pType, mDate1, mDate2, (int)Session["UserId"]);
                
                //Tao Html cho danh sach tin nay
                mProductPage.Html = V308HTMLHELPER.TaoDanhSachKHVip(mProductList, (int)pPage);
                mProductPage.Page = (int)pPage;
                mProductPage.TypeId = (int)pType;

                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content("Xảy ra lỗi hệ thống ! Vui lòng thử lại.");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        #endregion

        
    }
}
