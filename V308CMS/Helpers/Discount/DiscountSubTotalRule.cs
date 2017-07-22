using System;

namespace V308CMS.Helpers.Discount
{
    public class DiscountSubTotalRule:IIDiscountRule
    {
        public double ApplyDiscount(dynamic item, double amount)
        {
            if (!(item is double))
            {
                throw  new Exception("Item must be a double.");
            }
            var subTotal = item;
            if (subTotal == 0)
            {
                return subTotal;
            }
            if (amount == 0)
            {
                return subTotal;
            }
            return subTotal - subTotal * amount / 100;
        }
    }
}