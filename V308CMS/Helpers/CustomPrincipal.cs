﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

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
    }
}