namespace V308CMS.Helpers.Discount
{
    public class Discount
    {
        public string Code { get; set; }
        public double Amount { get; set; }
        public IIDiscountRule DiscountRule { get; set; }
    }
}