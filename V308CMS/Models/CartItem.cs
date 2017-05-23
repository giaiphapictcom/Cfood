﻿using System;
using V308CMS.Data;

namespace V308CMS.Models
{
    public class CartItem : IEquatable<CartItem>
    {
        public CartItem(ProductModels productItem)
        {
            ProductItem = productItem;           
        }
        //San pham
        public ProductModels ProductItem { get; set; }    
        //So luong
        public int Quantity { get; set; }
        //Don gia
        public double UnitPrice
        {
            get { return ProductItem.Price; }
        }
        public int Voucher { get; set; }

        public int SaleOff
        {
            get { return ProductItem.SaleOff; }
        }
        public double TotalPrice
        {
            get
            {
                return SaleOff > 0 ?Quantity * ((UnitPrice - ((UnitPrice / 100) * SaleOff))) : Quantity * UnitPrice;
            }
        }

        public bool Equals(CartItem other)
        {
            return other != null && (other.ProductItem.Id == this.ProductItem.Id);
        }
    }
}