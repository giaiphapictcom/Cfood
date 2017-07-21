using System.Security.Principal;

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
        public string AffilateId { get; set; }
        public int AffilateAmount { get; set; }
    }
}