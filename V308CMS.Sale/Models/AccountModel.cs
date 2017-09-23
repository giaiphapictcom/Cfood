using System;
using System.Web;

namespace V308CMS.Sale.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
           
        }

        public int id { get; set; }
        public int Manage { get; set; }
        public string AdminAffiliateCode { get; set; }
        public string affiliate_code { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool gender { get; set; }

        public string bank_account { get; set; }
        public string bank_number { get; set; }
        public string bank_name { get; set; }
        public string bank_address { get; set; }

        public string cmt_front { get; set; }
        public string cmt_back { get; set; }

        public HttpPostedFileBase FileFront { get; set; }
        public HttpPostedFileBase FileBack { get; set; }

    }
}