using System.Linq;
using System.Security.Principal;

namespace V308CMS.Admin.Helpers
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            //if (Roles.Any(role.Contains))
            //{
            //    return true;
            //}
            //return false;
            return true;
        }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public CustomPrincipal()
        {
            
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public Data.Admin Admin { get; set; }
        public string[] Roles { get; set; }
    }
}