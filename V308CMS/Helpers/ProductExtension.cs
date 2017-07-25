using V308CMS.Data;

namespace V308CMS.Helpers
{
    public static class ProductExtension
    {
        public static string ToProductSaleOffPrice(this Product product)
        {

            return $"{product.Price - (product.Price/100)*product.SaleOff: 0,0} {ConfigHelper.MoneyShort}";
        }
       
        public static string ToProductPrice(this Product product )
        {
            return ToPriceString(product.Price ?? 0);
        }

        public static string ToPriceString(this double price)
        {
            return $"{$"{price: 0,0}"} {ConfigHelper.MoneyShort}".Replace(",",".");
          
        }
    }

}