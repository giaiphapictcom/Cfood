using V308CMS.Data;

namespace V308CMS.Helpers
{
    public static class ProductExtension
    {

        public static string ToProductSaleOffPrice(this Product product)
        {

            return ToPriceString(product.Price.Value - (product.Price.Value/100)*product.SaleOff.Value);
        }
       
        public static string ToProductPrice(this Product product )
        {
            return ToPriceString(product.Price ?? 0);
        }

        public static string ToSaleOffPriceString(this double price, int saleOff)
        {
            return ToPriceString(price - (price / 100) * saleOff);
        }
        public static string ToPriceString(this double price)
        {
            return $"{$"{price: 0,0}"} {ConfigHelper.MoneyShort}".Replace(",",".");
          
        }

        public static string ToSaleOffLabel(this int saleOff)
        {
            return saleOff>0 ? $"{saleOff} %" :"";

        }
    }

}