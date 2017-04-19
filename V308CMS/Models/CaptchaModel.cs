using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel;

namespace V308CMS.Models
{
    public class Captcha
    {
        [Display(Name = "Mã bảo vệ", Order = 20)]
        [Remote("ValidateCaptcha", "Captcha", "", ErrorMessage = "ErrorMessage")]
        [Required(ErrorMessage = "Required")]
        public virtual string CaptchaValue { get; set; }
        public Captcha()
        {

        }
    }

    public class InvisibleCaptcha
    {
        [Display(Name = "InvisibleCaptcha", Order = 20)]
        [Remote("ValidateInvisibleCaptcha", "Captcha", "", ErrorMessage = "ErrorMessage")]
        public virtual string InvisibleCaptchaValue { get; set; }
        public InvisibleCaptcha()
        {

        }
    }
}