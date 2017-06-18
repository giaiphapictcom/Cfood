using System.Security.Principal;

namespace V308CMS.Admin.Helpers
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
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
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
     
    }
}