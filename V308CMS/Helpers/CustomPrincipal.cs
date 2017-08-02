using System;
using System.Collections.Generic;
using System.Security.Principal;
using DocumentFormat.OpenXml.Spreadsheet;
using V308CMS.Data.Models;

namespace V308CMS.Helpers
{
    public class CustomPrincipal: IPrincipal
    {
        public bool IsInRole(string role)
        {
            return false;
        }
        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public CustomPrincipal()
        {

        }
        public IIdentity Identity { get; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }      
        public KeyValuePair<string,int> Affilate { get; set; }
        public Tuple<string,int,int> Voucher { get; set; }
    }
}