using System.Web.Mvc;

namespace V308CMS.Helpers.View
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CustomPrincipal User => base.User as CustomPrincipal;
    }
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User => base.User as CustomPrincipal;
    }
}