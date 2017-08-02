using System.Linq;
using V308CMS.Data.Models;

namespace V308CMS.Data
{
    public interface IProductWishlistRepositry
    {
        string AddItemToWishlist(int productId, int userId);
        string RemoveItemFromWishlist(int productId, int userId);
        string GetListWishlist(int userId);
    }
    public class ProductWishlistRepositry : IProductWishlistRepositry
    {
        public ProductWishlistRepositry()
        {

        }

        public string GetListWishlist(int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                var wishlistItem = (from item in entities.ProductWishlist
                                    where item.UserId == userId
                                    select item
                  ).FirstOrDefault();
                return wishlistItem != null ? wishlistItem.ListProduct : "";

            }

        }


        public string AddItemToWishlist(int productId, int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                var wishlistItem = (from item in entities.ProductWishlist
                                    where item.UserId == userId
                                    select item
               ).FirstOrDefault();
                if (wishlistItem != null)
                {
                    if (string.IsNullOrWhiteSpace(wishlistItem.ListProduct))
                    {
                        wishlistItem.ListProduct = productId.ToString();
                        entities.SaveChanges();
                        return "ok";
                    }
                    if (wishlistItem.ListProduct.Contains(";"))
                    {
                        if (wishlistItem.ListProduct.Contains(";" + productId)
                            || wishlistItem.ListProduct.Contains(productId + ";"))
                        {
                            return "exist";
                        }
                    }
                    if (wishlistItem.ListProduct.Contains(productId.ToString()))
                    {
                        return "exist";
                    }

                    wishlistItem.ListProduct = wishlistItem.ListProduct + ";" + productId;
                    entities.SaveChanges();
                    return "ok";
                }
                else
                {
                    var newWishList = new ProductWishlist
                    {
                        ListProduct = productId.ToString(),
                        UserId = userId
                    };
                    entities.ProductWishlist.Add(newWishList);
                    entities.SaveChanges();
                    return "ok";

                }
            }

        }

        public string RemoveItemFromWishlist(int productId, int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                var wishlistItem = (from item in entities.ProductWishlist
                                    where item.UserId == userId
                                    select item
                    ).FirstOrDefault();
                if (wishlistItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(wishlistItem.ListProduct))
                    {
                        if (wishlistItem.ListProduct.Contains(";"))
                        {
                            if ((wishlistItem.ListProduct.Contains(";" + productId) || wishlistItem.ListProduct.Contains(productId + ";")))
                            {
                                wishlistItem.ListProduct = wishlistItem.ListProduct.Replace(";" + productId, "").Replace(productId + ";", "");
                                entities.SaveChanges();
                                return "ok";
                            }
                            return "invalid";

                        }
                        else
                        {
                            wishlistItem.ListProduct = wishlistItem.ListProduct.Replace(productId.ToString(), "");
                            entities.SaveChanges();
                            return "ok";
                        }
                    }
                    return "not_exist";
                }
                return "userid_invalid";

            }

        }
    }
}