$(document).ready(function () {
    jQuery('a.buy').click(function () {
        var taget = jQuery(this).attr('add-cart');
        if (taget.length > 0) {
            shopping.addCart(taget);
        }
    });
    
});

var shopping = {
    addCart: function (prodID) {
        var quatity = 1;
        var mUnit = 1;

        $.ajax({
            type: 'POST',
            data: { 'pNumber': quatity, 'pId': prodID, 'pUnit': mUnit },
            dataType: 'json',
            url: "/them-san-pham",
            //url: "Home/addToShopCart",
            timeout: 6000,
            success: function (data) {
                console.log(data);
                if (data.code == 1) {
                    $("#shopping-cart .value").html(data.totalprice);
                }
            },
            error: function (x, t, m) {
                $("#wait").css("display", "none");
            }
        });
    }
}