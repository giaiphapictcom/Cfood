using System.Web.Mvc;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Helpers.UI
{
    public static class StateLabelHelper
    {
      
        public static MvcHtmlString ToStateLabelHtml(this bool? state, bool isSwitchState = false)
        {
            string stateLabel;
            if (isSwitchState)
            {
                stateLabel = state.HasValue && state.Value
                    ? "<span style='background-color:#d9534f;color:#FFFFFF;padding: 5px;border-radius: 5px'>Ẩn</span>"
                    : "<span style='background-color:#5cb85c;color:#FFFFFF;padding: 5px;border-radius: 5px'>Hiển thị</span>";

            }
            else
            {
                stateLabel = state.HasValue && state.Value ?
              "<span style='background-color:#5cb85c;color:#FFFFFF;padding: 5px;border-radius: 5px'>Hiển thị</span>" :
                   "<span style='background-color:#d9534f;color:#FFFFFF;padding: 5px;border-radius: 5px'>Ẩn</span>";
            }
           
            return new MvcHtmlString(stateLabel);
          
        }
        public static MvcHtmlString ToStateLabelHtml(this byte state)
        {
            var stateLabel = state == (byte)StateEnum.Active ?
                "<span style='background-color:#5cb85c;color:#FFFFFF;padding: 5px;border-radius: 5px'>Hiển thị</span>" :
                "<span style='background-color:#d9534f;color:#FFFFFF;padding: 5px;border-radius: 5px'>Ẩn</span>";
            return new MvcHtmlString(stateLabel);
        }
        public static MvcHtmlString ToStateLabel(this int state)
        {
            var stateLabel = state == (byte)StateEnum.Active ?
                "<span style='background-color:#5cb85c;color:#FFFFFF;padding: 5px;border-radius: 5px'>Hiển thị</span>" : 
                "<span style='background-color:#d9534f;color:#FFFFFF;padding: 5px;border-radius: 5px'>Ẩn</span>";
            return new MvcHtmlString(stateLabel);
        }

        public static MvcHtmlString ToStateLabel(this byte? state)
        {
            var stateLabel = state == (byte) StateEnum.Active ? 
                "<span style='background-color:#5cb85c;color:#FFFFFF;padding: 5px;border-radius: 5px'>Hiển thị</span>" :
                "<span style='background-color:#d9534f;color:#FFFFFF;padding: 5px;border-radius: 5px'>Ẩn</span>";
            return new MvcHtmlString(stateLabel);
        }
        
    }
}