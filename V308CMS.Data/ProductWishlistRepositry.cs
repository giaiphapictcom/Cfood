using System.Linq;

namespace V308CMS.Data
{
    public class ProductWishlistRepositry
    {
        private readonly V308CMSEntities _entities;
        public ProductWishlistRepositry(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public string AddItemToWishlist(int productId, string userId)
        {
            var checkItemInWishlist = (from item in _entities.ProductWishlist
                where item.UserId == userId && item.ProductId == productId
                                       select item
                ).FirstOrDefault();
            if (checkItemInWishlist != null)
            {
                return "exist";

            }
            else
            {
                var newWishList = new ProductWishlist
                {
                    ProductId = productId,
                    UserId = userId
                };
                _entities.ProductWishlist.Add(newWishList);
                _entities.SaveChanges();
                return "ok";

            }

        }
    }
}