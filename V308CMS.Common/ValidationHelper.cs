using System.Text.RegularExpressions;

namespace V308CMS.Common
{
    public static class ValidationHelper
    {
        public static bool IsEmailAddress(this string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }
            return Regex.IsMatch(emailAddress, @"^\s*[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\s*$", RegexOptions.Compiled);
        }

        public static bool IsPasswordValid(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return password.Length >= 6;
        }
        
    }
}