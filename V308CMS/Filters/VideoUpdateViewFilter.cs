using System.Web.Mvc;
using V308CMS.Respository;

namespace V308CMS.Filters
{
    public class VideoUpdateView: ActionFilterAttribute
    {

        private readonly string _idParamName;
        public VideoUpdateView(string idParamName = "id")
        {
            _idParamName = idParamName;

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.ActionParameters[_idParamName] != null ? filterContext.ActionParameters[_idParamName].ToString() : "";
            if (!string.IsNullOrEmpty(id))
            {
                int videoId;
                int.TryParse(id, out videoId);
                if (videoId > 0)
                {
                    new VideoRespository().IncrementView(videoId);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}