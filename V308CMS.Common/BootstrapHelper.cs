using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Common
{
    public class BootstrapHelper : Controller
    {
        //
        // GET: /Bootstrap/

        public ActionResult Index()
        {
            return View();
        }

        private static MvcHtmlString InputGroup(string inputID, string title, TagBuilder input, Boolean request = false, string help = "",string button_affter="")
        {
            TagBuilder group = new TagBuilder("div");
            group.MergeAttribute("class", "form-group");

            //string inputID = "input-" + EncryptionMD5.ToMd5(inputName);

            TagBuilder label = new TagBuilder("label");
            label.InnerHtml = title;
            if (request){
                label.InnerHtml += " <span class=\"notification-red\"></span>";
            }
            label.MergeAttribute("for", inputID);
            group.InnerHtml += label.ToString();

            group.InnerHtml += input.ToString();

            //group.InnerHtml += "<div class=\"input-group\">" + input.ToString();

            
            //if (button_affter.Length > 0) {
            //    group.InnerHtml += "<span class=\"input-group-btn\"><span class=\"glyphicon glyphicon-" + button_affter + "\"></span></span>";
            //}
            //group.InnerHtml += "</div>";
            if (help.Length > 0)
            {
                group.InnerHtml += "<small class=\"form-text text-muted text-note\">" + help + "</small>";
            }
            return MvcHtmlString.Create(group.ToString());
        }

        public static MvcHtmlString InputText(string inputName, string title = "", string placeholder = "", string value = "", bool Readonly = false)
        {
            string inputID = "input-" + EncryptionMD5.ToMd5(inputName);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("value", value);
            input.MergeAttribute("name", inputName);
            input.MergeAttribute("id", inputID);
            input.MergeAttribute("class", "form-control");
            if (placeholder !=null && placeholder.Length > 0) {
                input.MergeAttribute("placeholder", placeholder);
            }

            if (Readonly) {
                input.MergeAttribute("readonly", "true");
            }

            return InputGroup(inputID, title, input);
        }

        public static MvcHtmlString InputNumber(string inputName, string title = "", string placeholder = "", string value = "", bool Readonly = false)
        {
            string inputID = "input-" + EncryptionMD5.ToMd5(inputName);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "number");
            input.MergeAttribute("value", value);
            input.MergeAttribute("name", inputName);
            input.MergeAttribute("id", inputID);
            input.MergeAttribute("class", "form-control");
            if (placeholder != null && placeholder.Length > 0)
            {
                input.MergeAttribute("placeholder", placeholder);
            }

            if (Readonly)
            {
                input.MergeAttribute("readonly", "true");
            }

            return InputGroup(inputID, title, input);
        }

        public static MvcHtmlString InputDate(string inputName, string title = "", string placeholder = "", string value = "", bool Readonly = false)
        {
            string inputID = "input-" + EncryptionMD5.ToMd5(inputName);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("value", value);
            input.MergeAttribute("name", inputName);
            input.MergeAttribute("id", inputID);
            input.MergeAttribute("class", "form-control datepicker");
            if (placeholder != null && placeholder.Length > 0)
            {
                input.MergeAttribute("placeholder", placeholder);
            }

            if (Readonly)
            {
                input.MergeAttribute("readonly", "true");
            }

            //input.InnerHtml += new HtmlString("<span class=\"input-group-addon\">< span class=\"glyphicon glyphicon-calendar\"></span></span>");
            return InputGroup(inputID, title, input, false, "", "glyphicon-calendar");
        }

        public static MvcHtmlString InputTextarea(string inputName, string title = "", string placeholder = "",string value = "", Boolean request = false, string help = "")
        {
            string inputID = "input-" + EncryptionMD5.ToMd5(inputName);
            TagBuilder input = new TagBuilder("textarea");
            input.MergeAttribute("name", inputName);
            input.MergeAttribute("id", inputID);
            input.MergeAttribute("rows", "3");
            input.MergeAttribute("class", "form-control");
            if (placeholder !=null && placeholder.Length > 0)
            {
                input.MergeAttribute("placeholder", placeholder);
            }

            input.InnerHtml = value;
            return InputGroup(inputID, title, input, request, help);
        }

    }
}
