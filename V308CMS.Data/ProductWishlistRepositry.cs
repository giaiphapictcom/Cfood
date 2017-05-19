﻿using System.Linq;

namespace V308CMS.Data
{
    public interface IProductWishlistRepositry
    {
        string AddItemToWishlist(int productId, string userId);
        string RemoveItemFromWishlist(int productId, string userId);
    }
    public class ProductWishlistRepositry : IProductWishlistRepositry
    {
        private readonly V308CMSEntities _entities;
        public ProductWishlistRepositry(V308CMSEntities entities)
        {
            _entities = entities;
        }


        public string AddItemToWishlist(int productId, string userId)
        {

            var wishlistItem = (from item in _entities.ProductWishlist
                where item.UserId == userId
                select item
                ).FirstOrDefault();
            if (wishlistItem != null)
            {
                if (string.IsNullOrWhiteSpace(wishlistItem.ListProduct))
                {
                    wishlistItem.ListProduct = productId.ToString();
                    _entities.SaveChanges();
                    return "ok";
                }
                if (wishlistItem.ListProduct.Contains(";" + productId) ||
                    wishlistItem.ListProduct.Contains(productId + ";"))
                {
                    return "exist";
                }
                wishlistItem.ListProduct = wishlistItem.ListProduct + ";" + productId;
                _entities.SaveChanges();
                return "ok";
            }
            else
            {
                var newWishList = new ProductWishlist
                {
                    ListProduct = productId.ToString(),
                    UserId = userId
                };
                _entities.ProductWishlist.Add(newWishList);
                _entities.SaveChanges();
                return "ok";

            }
        }

        public string RemoveItemFromWishlist(int productId, string userId)
        {
            var wishlistItem = (from item in _entities.ProductWishlist
                                where item.UserId == userId
                                select item
                 ).FirstOrDefault();
            if (wishlistItem != null)
            {
                if (!string.IsNullOrWhiteSpace(wishlistItem.ListProduct)
                    && (wishlistItem.ListProduct.Contains(";" + productId)
                        || wishlistItem.ListProduct.Contains(productId + ";")))
                {
                    wishlistItem.ListProduct =
                        wishlistItem.ListProduct.Replace(";" + productId, "").Replace(productId + ";", "");
                    _entities.SaveChanges();
                    return "ok";

                }
                return "none_exist";
            }
            return "userid_not_exist";
        }
    }
}