using System;
using V308CMS.Data;

namespace V308CMS.Models
{
    public class CartItem : IEquatable<CartItem>
    {
        public CartItem(Product productItem)
        {
            ProductItem = productItem;           
        }
        //San pham
        public Product ProductItem { get; set; }    
        //So luong
        public int Quantity { get; set; }
        //Don gia
        public double UnitPrice
        {
            get {
                if (ProductItem.Price != null)
                    return ProductItem.Price.Value;
                return 0;
            }
        }
        public int Voucher { get; set; }
        public int SaleOff => ProductItem.SaleOff ?? 0;


        public double TotalPrice => SaleOff > 0 ? 
            Quantity * ((UnitPrice - ((UnitPrice / 100) * SaleOff))) : 
            Quantity * UnitPrice;

        public bool Equals(CartItem other)
        {
            return other != null && (other.ProductItem.ID == this.ProductItem.ID);
        }
    }
}