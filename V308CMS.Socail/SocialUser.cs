namespace V308CMS.Socail
{
    public  sealed class SocialUser
    {
       
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string  Avatar { get; set; }
        public string FullName { get; set; }
        public  string DisplayName { get; set; }

        public override string ToString()
        {
          return  $"{FirstName} {LastName}";
        }
    }
}