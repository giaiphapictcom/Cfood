namespace V308CMS.Helpers
{
    public class WishListLocalStorage
    {
        private static readonly CookieHelper CookieHelper = CookieHelper.Instance;
        private const string WishlistCookie = "63a693i0h80783t4e5a22g3n690587";

        public static string GetWishList()
        {
            var wishListCookie = CookieHelper.Get(WishlistCookie);
            return wishListCookie != null ? wishListCookie.Value : "";
        }

        public static void AddToWishList(string productId)
        {
            var wishList = GetWishList();
            if (wishList.Contains(";" + productId) || wishList.Contains(productId + ";"))
            {
                return;
            }
            if (string.IsNullOrEmpty(wishList)){
                wishList = productId;
            }
            else
            {
                wishList = wishList + ";" + productId;
            }
            CookieHelper.Add(WishlistCookie, wishList);
        }

        public static void RemoveFromWishList(string productId)
        {
            var wishList = GetWishList();
            if (!string.IsNullOrWhiteSpace(wishList) 
                && (wishList.Contains(";" + productId) || wishList.Contains(productId + ";")
                ))
            {
                if (wishList.Contains(";" + productId)){
                    wishList = wishList.Replace(";" + productId, "");

                }
                else if (wishList.Contains(productId + ";")){
                    wishList = wishList.Replace( productId + ";", "");
                }
                CookieHelper.Save(WishlistCookie, wishList);
            }
        }
    }
}