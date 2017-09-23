using System.Web.Mvc;
using V308CMS.Data;
using System;
using System.Web;
using V308CMS.Respository;

namespace V308CMS.Admin.Helpers.Url
{
    public static class AffiliateUrlHelper
    {
        //public static string AffiliateIndexUrl(string controller = "affiliate", string action = "index")
        //{
        //    return string.Format("/{0}/{1}", controller, action);
        //}

        public static string AffiliateLinksUrl(this UrlHelper helper, object value = null, string controller = "Affiliate", string action = "Links")
        {
            return helper.Action(action, controller, value);
        }

        public static string AffiliateVoucherssUrl(this UrlHelper helper, object value = null, string controller = "Affiliate", string action = "Links")
        {
            return helper.Action(action, controller, value);
        }

        public static HtmlString AffiliateVouchersTotalLink(this UrlHelper helper, object value = null)
        {
            string link  = helper.Action("vouchers", "Affiliate", value);
            var VoucherTotal = 0;
            try {
                var uid = value.GetType().GetProperty("ID").GetValue(value, null);
                VoucherTotal = CouponRepository.VoucherCount((int)uid);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return new HtmlString("<a class=\"btn btn-link\" href=\"" + link + "\">"+ VoucherTotal + "</a>");
        }

        public static HtmlString AffiliateLinksTotalLink(this UrlHelper helper, object value = null)
        {
            string link = helper.Action("Links", "Affiliate", value);
            var VoucherTotal = 0;
            try
            {
                var uid = value.GetType().GetProperty("ID").GetValue(value, null);
                var LinkRepo = new LinkRepository();
                VoucherTotal = LinkRepo.LinkCount((int)uid);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return new HtmlString("<a class=\"btn btn-link\" href=\"" + link + "\">" + VoucherTotal + "</a>");
        }
        public static HtmlString AffiliateOrdersLink(this UrlHelper helper, object value = null)
        {
            string link = helper.Action("Orders", "Affiliate", value);
            var total = 0;
            try
            {
                var uid = value.GetType().GetProperty("ID").GetValue(value, null);
                var OrderRepo = new ProductOrderRespository();
                var UserRepo= new UserRespository();

                var AccountIds = UserRepo.GetMemberIdOfAffiliate((int)uid);
                total = OrderRepo.OrderCountByManagers(AccountIds);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return new HtmlString("<a class=\"btn btn-link\" href=\"" + link + "\">" + total + "</a>");
        }
        
    }
}