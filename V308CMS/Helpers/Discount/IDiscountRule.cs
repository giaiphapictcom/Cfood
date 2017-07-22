namespace V308CMS.Helpers.Discount
{
    
   public interface IIDiscountRule
    {
        double ApplyDiscount(dynamic item,double amount);

    }
}
