﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class NewsController : BaseController
    {
        public NewsController()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new V308CMS.Helpers.MpStartViewEngine(false));
        }
        private const int PageSize = 10;
        private const int NewsType = 58;

        private int GetTotalPage(int totalItem)
        {
            int totalPage = totalItem / PageSize;           
            if (totalItem % PageSize != 0)
            {
                totalPage += 1;
            }
            return totalPage;
        }

       
        public ActionResult Index(int type = NewsType, int page = 1)
        {
            var newsIndexViewModel = new NewsIndexPageContainer();
            var newsGroup = NewsService.LayTheLoaiTinTheoId(type);
            string level = string.Empty;     
            if (newsGroup != null)
            {
                newsIndexViewModel.NewsGroups = newsGroup;
                level = newsGroup.Level;
            }
            newsIndexViewModel.ListNews = NewsService.LayTinTheoTrangAndGroupIdAndLevel(page, 10, type, level);
            newsIndexViewModel.Page = page;
            newsIndexViewModel.TotalPage = GetTotalPage(newsIndexViewModel.ListNews.Count);
            newsIndexViewModel.ListNewsMostView = NewsService.GetListNewsMostView(type, level);
            return View("News.Index", newsIndexViewModel);            
        }
    
        public ActionResult Detail(int id)
        {
            var newsItem = NewsService.GetById(id, NewsType);
            if (newsItem == null)
            {
                return HttpNotFound("Tin này không tồn tại trên hệ thống");
            }
            var newsDetailViewModel = new NewsDetailPageContainer
            {
                NewsItem = newsItem,
                NextNewsItem = NewsService.GetNext(id, NewsType),
                ListNewsMostView = NewsService.GetListNewsMostView(NewsType, ""),
                 PreviousNewsItem = NewsService.GetPrevious(id, NewsType)
            };

            return View("News.Detail", newsDetailViewModel);
        }

        public ActionResult ListByTag(string tag, int page = 1)
        {
            return Content("ok");
        }
    }
}
