$(document).ready(function () {
    $(window).scroll(function () {

        var x = $(window).scrollTop();
        console.log(x);
        if (x > 170) {
            //$('#leftcol').scrollToFixed({ bottom: 0 });
            
        } else if (x < 170) {
            //$('.headerp').removeAttr("style");
        }
    });

    //$('#leftcol').scrollToFixed({
    //    bottom: 0

    //});
});