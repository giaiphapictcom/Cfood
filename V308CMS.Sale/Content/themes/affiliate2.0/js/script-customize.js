window.ajax_cart = true;
window.money_format = "<span class=money>${{amount}}</span>";
window.shop_currency = "USD";
window.show_multiple_currencies = true;
window.loading_url = "//cdn.shopify.com/s/files/1/1498/2346/t/12/assets/loading.gif?12126481974382985035";
window.use_color_swatch = true;
window.product_image_resize = false;
window.enable_sidebar_multiple_choice = true;
window.dropdowncart_type = "hover";
window.file_url = "//cdn.shopify.com/s/files/1/1498/2346/files/?12126481974382985035";
window.asset_url = "";
window.images_size = {
    is_crop: false,
    ratio_width: 1,
    ratio_height: 1.35,

};
window.multi_lang = false;

//<![CDATA[
var Shopify = Shopify || {};
Shopify.shop = "bigsale-01.myshopify.com";
Shopify.theme = { "name": "bigsale-01", "id": 158713545, "theme_store_id": null, "role": "main" };
Shopify.theme.handle = 'null';
Shopify.theme.style = { "id": null, "handle": null };

//]]>

//<![CDATA[
(function () {
    function asyncLoad() {
        var urls = ["\/\/productreviews.shopifycdn.com\/assets\/v4\/spr.js?shop=bigsale-01.myshopify.com"];
        for (var i = 0; i < urls.length; i++) {
            var s = document.createElement('script');
            s.type = 'text/javascript';
            s.async = true;
            s.src = urls[i];
            var x = document.getElementsByTagName('script')[0];
            x.parentNode.insertBefore(s, x);
        }
    };
    if (window.attachEvent) {
        window.attachEvent('onload', asyncLoad);
    } else {
        window.addEventListener('load', asyncLoad, false);
    }

    
    
})();

//]]>


$(document).ready(function () {

    $('.tabs-product .nav-tabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })
    $(".productCateCarousel").owlCarousel({
        itemsCustom: [
          [320, 1],
          [360, 1],
          [450, 2],
          [600, 2],
          [700, 2],
          [800, 2],
          [1000, 3],
          [1200, 3],
          [1400, 3],
          [1600, 3]
        ],
        autoPlay: false,
        stopOnHover: false,

        lazyLoad: false,
        lazyFollow: true,
        lazyEffect: "fade",
        pagination: true,

        // Navigation
        navigation: false,
        navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        rewindNav: true,
        scrollPerPage: false,

    });

})




Shopify.doNotTriggerClickOnThumb = false;

var selectCallbackQuickview = function (variant, selector) {
    var productItem = jQuery('.product-quickview .product-item');
    addToCart = productItem.find('.add-to-cart-btn'),
    productPrice = productItem.find('.price'),
    comparePrice = productItem.find('.compare-price'),
    totalPrice = productItem.find('.total-price span');


    if (variant) {
        if (variant.available) {
            // We have a valid product variant, so enable the submit button
            addToCart.removeClass('disabled').removeAttr('disabled').text('Add to Cart');

        } else {
            // Variant is sold out, disable the submit button
            addToCart.val('Sold Out').addClass('disabled').attr('disabled', 'disabled');
        }

        // Regardless of stock, update the product price
        productPrice.html(Shopify.formatMoney(variant.price, "<span class=money>${{amount}}</span>"));

        // Also update and show the product's compare price if necessary
        if (variant.compare_at_price > variant.price) {
            comparePrice
              .html(Shopify.formatMoney(variant.compare_at_price, "<span class=money>${{amount}}</span>"))
              .show();
            productPrice.addClass('on-sale');
        } else {
            comparePrice.hide();
            productPrice.removeClass('on-sale');
        }


        // BEGIN SWATCHES
        var form = jQuery('#' + selector.domIdPrefix).closest('form');
        for (var i = 0, length = variant.options.length; i < length; i++) {
            var radioButton = form.find('.swatch[data-option-index="' + i + '"] :radio[value="' + variant.options[i] + '"]');
            if (radioButton.size()) {
                radioButton.get(0).checked = true;
            }
        }
        // END SWATCHES


        //update variant inventory

        var inventoryInfo = productItem.find('.product-inventory span');
        if (variant.available) {
            if (variant.inventory_management != null) {
                inventoryInfo.text(variant.inventory_quantity + " in stock");
            } else {
                inventoryInfo.text("Many in stock");
            }
        } else {
            inventoryInfo.text("Out of stock");
        }


        /*recaculate total price*/
        //try pattern one before pattern 2
        var regex = /([0-9]+[.|,][0-9]+[.|,][0-9]+)/g;
        var unitPriceTextMatch = jQuery('.product-quickview .price').text().match(regex);

        if (!unitPriceTextMatch) {
            regex = /([0-9]+[.|,][0-9]+)/g;
            unitPriceTextMatch = jQuery('.product-quickview .price').text().match(regex);
        }

        if (unitPriceTextMatch) {
            var unitPriceText = unitPriceTextMatch[0];
            var unitPrice = unitPriceText.replace(/[.|,]/g, '');
            var quantity = parseInt(jQuery('.product-quickview input[name=quantity]').val());
            var totalPrice = unitPrice * quantity;

            var totalPriceText = Shopify.formatMoney(totalPrice, window.money_format);

            totalPriceText = totalPriceText.match(regex)[0];

            var regInput = new RegExp(unitPriceText, "g");
            var totalPriceHtml = jQuery('.product-quickview .price').html().replace(regInput, totalPriceText);
            jQuery('.product-quickview .total-price span').html(totalPriceHtml);
        }/*end of price calculation*/


        Currency.convertAll('USD', jQuery('#currencies').val(), 'span.money', 'money_format');


        /*begin variant image*/
        if (variant && variant.featured_image) {
            var newImage = Shopify.resizeImage(variant.featured_image.src, 'small');
            newImage = newImage.replace(/https?:/, '');
            jQuery('.product-quickview .quickview-more-views img').each(function () {
                var grandSize = jQuery(this).attr('src');
                if (grandSize == newImage) {
                    jQuery(this).parent().trigger('click');
                    return false;
                }
            });
        }
        /*end of variant image*/

    } else {
        // The variant doesn't exist. Just a safegaurd for errors, but disable the submit button anyway
        addToCart.text('Unavailable').addClass('disabled').attr('disabled', 'disabled');
    }

};




// Pick your format here:  
// Can be 'money_format' or 'money_with_currency_format'
Currency.format = 'money_format';
var shopCurrency = 'USD';
var cookieCurrency = Currency.cookie.read();

// Fix for customer account pages 
jQuery('span.money span.money').each(function () {
    jQuery(this).parent('span.money').removeClass('money');
});

// Add precalculated shop currency to data attribute 
jQuery('span.money').each(function () {
    jQuery(this).attr('data-currency-USD', jQuery(this).html());
});

// Select all your currencies buttons.
var currencySwitcher = jQuery('#currencies');

// When the page loads.
if (cookieCurrency == null || cookieCurrency == shopCurrency) {
    Currency.currentCurrency = shopCurrency;
}
else {
    Currency.currentCurrency = cookieCurrency;
    currencySwitcher.val(cookieCurrency);
    Currency.convertAll(shopCurrency, cookieCurrency);
}
currencySwitcher.selectize();
jQuery('.selectize-input input').attr('disabled', 'disabled');

// When customer clicks on a currency switcher.
currencySwitcher.change(function () {
    var newCurrency = jQuery(this).val();
    Currency.cookie.write(newCurrency);
    Currency.convertAll(Currency.currentCurrency, newCurrency);
    //show modal
    jQuery("#currencies-modal span").text(newCurrency);
    if (jQuery("#cart-currency").length > 0) {
        jQuery("#cart-currency").text(newCurrency);
    }

});

// For product options.
var original_selectCallback = window.selectCallback;
var selectCallback = function (variant, selector) {
    original_selectCallback(variant, selector);
    Currency.convertAll(shopCurrency, jQuery('#currencies').val());
};