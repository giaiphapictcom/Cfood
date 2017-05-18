using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Data;

namespace V308CMS.Models
{
    /**
     * The ShoppingCart class
     * 
     * Holds the items that are in the cart and provides methods for their manipulation
     */
    public class ShoppingCart
    {
        public List<CartItem> Items { get; private set; }

        public static readonly ShoppingCart Instance;
        // The static constructor is called as soon as the class is loaded into memory
        static ShoppingCart()
        {
            // If the cart is not in the session, create one and put it there
            // Otherwise, get it from the session
            if (HttpContext.Current.Session["MPSTARTShoppingCart"] == null)
            {
                Instance = new ShoppingCart
                {
                    Items = new List<CartItem>()
                };
                HttpContext.Current.Session["MPSTARTShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCart)HttpContext.Current.Session["MPSTARTShoppingCart"];
            }
        }
        protected ShoppingCart() { }
        /**
             * AddItem() - Adds an item to the shopping 
        */
        public void AddItem(Product product)
        {
            // Create a new item to add to the cart
            CartItem newItem = new CartItem(product);

            // If this item already exists in our list of items, increase the quantity
            // Otherwise, add the new item to the list
            if (Items.Contains(newItem))
            {
                foreach (CartItem item in Items)
                {
                    if (item.Equals(newItem))
                    {
                        item.Quantity++;
                        return;
                    }
                }
            }
            else
            {
                newItem.Quantity = 1;
                Items.Add(newItem);
            }
        }
        /**
            * SetItemQuantity() - Changes the quantity of an item in the cart
        */
        public void SetItemQuantity(Product productItem, int quantity)
        {
            // If we are setting the quantity to 0, remove the item entirely
            if (quantity == 0)
            {
                RemoveItem(productItem);
                return;
            }

            // Find the item and update the quantity
            CartItem updatedItem = new CartItem(productItem);

            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.Quantity = quantity;
                    return;
                }
            }
        }
        /**
            * RemoveItem() - Removes an item from the shopping cart
        */
        public void RemoveItem(Product productItem)
        {
            CartItem removedItem = new CartItem(productItem);
            Items.Remove(removedItem);
        }
        /**
           * SubTotal- returns the total price of all of the items
           *                 before tax, shipping, etc.
        */

        public double SubTotal
        {
            get
            {
                return Items.Sum(item => item.TotalPrice);
            }
        }

        /**
         * SubTotalAfterService - returns the total price of all of the items
         *                 before tax, shipping, etc.
      */
        public double SubTotalAfterService
        {
            get
            {
                var subTotalAfterService = SubTotal;
                if (Voucher > 0){
                    subTotalAfterService = (subTotalAfterService - ((subTotalAfterService / 100) * Voucher));
                }
                if (ShipPrice > 0){
                    subTotalAfterService += ShipPrice;
                }
                return subTotalAfterService;
            }
        }
        public int Voucher { get; set; }
        public int ShipPrice { get; set; }
    

    }
}