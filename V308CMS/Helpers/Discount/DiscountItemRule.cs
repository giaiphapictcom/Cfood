using System;
using V308CMS.Data;

namespace V308CMS.Helpers.Discount
{
 
    
    public class DiscountItemRule: IIDiscountRule
    {
      
        public double ApplyDiscount(dynamic item, double amount)
        {

            if (!(item is Product))
            {
                throw new Exception("Item must be a product.");
            }
          
            if (!item.Price.HasValue)
            {
                return 0;
            }
            if (amount == 0)
            {
                return (double)item.Price;

            }
            return item.Price - item.Price * amount / 100;

        }
    }
}
