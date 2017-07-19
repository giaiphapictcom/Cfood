using System.ComponentModel;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Data;

namespace V308CMS.Admin.Helpers.UI
{
    public static class AffiliateStatus
    {
        public static MvcHtmlString StatusTextHtml(this Account Account)
        {
            var statusText = "";
            var colorClass = "";

            if (!Account.Status)
            {
                statusText = AccountStatusEnum.Processing.GetAttributeOfType<DescriptionAttribute>().Description;
                colorClass = "yellow";
            } else {
                if (Account.affiliate_code.Length < 1) {
                    statusText = AccountStatusEnum.Processing.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "yellow";
                } else {
                    statusText = AccountStatusEnum.Complete.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "green";
                }
            }
            
            return new MvcHtmlString(string.Format("<span class='grid-report-item {0}'>{1}</span>", colorClass, statusText));

        }
    }
}