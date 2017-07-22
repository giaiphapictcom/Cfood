using System.ComponentModel;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Sale.Helpers
{
    public static class OrderStatusHelper
    {
        public static string ToOrderStatusText(this byte state)
        {            
            var statusText = "";           
            switch (state)
            {
                case (byte)OrderStatusEnum.Pending:
                    statusText = OrderStatusEnum.Pending.GetAttributeOfType<DescriptionAttribute>().Description;                
                    break;
                case (byte)OrderStatusEnum.Processing:
                    statusText = OrderStatusEnum.Processing.GetAttributeOfType<DescriptionAttribute>().Description;                    
                    break;
                case (byte)OrderStatusEnum.Delivering:
                    statusText = OrderStatusEnum.Delivering.GetAttributeOfType<DescriptionAttribute>().Description;                    
                    break;
                case (byte)OrderStatusEnum.Complete:
                    statusText = OrderStatusEnum.Complete.GetAttributeOfType<DescriptionAttribute>().Description;                
                    break;
                case (byte)OrderStatusEnum.CanceelledPayment:
                    statusText = OrderStatusEnum.CanceelledPayment.GetAttributeOfType<DescriptionAttribute>().Description;                  
                    break;
                case (byte)OrderStatusEnum.CancelledOrder:
                    statusText = OrderStatusEnum.CancelledOrder.GetAttributeOfType<DescriptionAttribute>().Description;                   
                    break;
                case (byte)OrderStatusEnum.Refund:
                    statusText = OrderStatusEnum.CancelledOrder.GetAttributeOfType<DescriptionAttribute>().Description;                  
                    break;
            }
            return statusText;
        }
        public static MvcHtmlString ToOrderStateTextHtml(this int? state)
        {
            if (!state.HasValue)
            {
                return MvcHtmlString.Empty;
            }
            var statusText = "";
            var colorClass = "";
            switch (state)
            {
                case (byte)OrderStatusEnum.Pending:
                    statusText = OrderStatusEnum.Pending.GetAttributeOfType<DescriptionAttribute>().Description;

                    colorClass = "yellow";
                    break;
                case (byte)OrderStatusEnum.Processing:
                    statusText = OrderStatusEnum.Processing.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "blue";
                    break;
                case (byte)OrderStatusEnum.Delivering:
                    statusText = OrderStatusEnum.Delivering.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "yellow";
                    break;
                case (byte)OrderStatusEnum.Complete:
                    statusText = OrderStatusEnum.Complete.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "green";
                    break;
                case (byte)OrderStatusEnum.CanceelledPayment:
                    statusText = OrderStatusEnum.CanceelledPayment.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "red";
                    break;
                case (byte)OrderStatusEnum.CancelledOrder:
                    statusText = OrderStatusEnum.CancelledOrder.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "red";
                    break;
                case (byte)OrderStatusEnum.Refund:
                    statusText = OrderStatusEnum.CancelledOrder.GetAttributeOfType<DescriptionAttribute>().Description;
                    colorClass = "red";
                    break;
            }
            return new MvcHtmlString(string.Format("<span class='grid-report-item {0}'>{1}</span>", colorClass, statusText));
            
        }

    }
}