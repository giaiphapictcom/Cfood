$(document).ready(function () {
    jQuery('a.buy').click(function () {
        var taget = jQuery(this).attr('add-cart');
        if (taget.length > 0) {
            shopping.addCart(taget);
        }
    });
    shopping.updateshop();
    
});

var shopping = {
    addCart: function (prodID) {
        var quatity = 1;
        var mUnit = 1;
        var form = jQuery("form[name=addcart]");
        console.log(form);
        $.ajax({
            type: 'POST',
            data: { 'quantity': quatity, 'id': prodID, 'pUnit': mUnit },
            dataType: 'json',
            url: (typeof form != "undefined" && form.length > 0) ? form.attr("action") : "/cart/add",
            timeout: 6000,
            success: function (data) {
                console.log(data);
                if (data.code == 1) {
                    shopping.success(data);
                }

            },
            error: function (x, t, m) {
                $("#wait").css("display", "none");
            }
        });
    },
    addCartAjax: function (form, gotocheckout) {
        
        jQuery.ajax({
            cache: false,
            url: form.attr("action"),
            type:"POST",
            data: form.serialize(),
            datatype: 'json',
            success: function (data) {
                if (gotocheckout==true) {
                    window.location.href = "/" + shopping.checkouturl
                }
                shopping.success(data);
                shopping.updateshop();
            }
        });
    },
    checkouturl: "cart/checkout",
    success: function (response) {
        if (typeof response == 'object') {
            var modal = jQuery("#addcart-success");
            modal.find(".modal-title").html("Thêm vào giỏ hàng thành công");
            modal.find(".modal-body p").html(response.message);
            modal.modal("show");
            shopping.updateshop();
        }
        
    },
    updateshop: function () {
        jQuery.ajax({
            cache: false,
            url: "/cart",
            datatype: 'json',
            success: function (data) {
                jQuery(".input-group.cart .money .value").html(data.total_price);
            }
        });
    }
}