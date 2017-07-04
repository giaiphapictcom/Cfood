
$(document).ready(function () {
    console.log("loaded");
    $(".showsubmenu").click(function () {
        $(".nav-item ul").hide();
        $(this).parents(".nav-item").find("ul").css({"display":"block"});
        console.log("call me");
        return false;
    });
    if (jQuery().owlCarousel) {
        $("#logo-brand").owlCarousel({
            itemsCustom: [
              [0, 1],
              [450, 2],
              [600, 3],
              [700, 4],
              [1000, 5],
              [1200, 6],
              [1400, 7],
              [1600, 7]
            ],
            autoPlay: true,
            stopOnHover: false,

            lazyLoad: false,
            lazyFollow: true,
            lazyEffect: "fade",
            pagination: false,

            // Navigation
            navigation: false,
            //navigationText : ['<i class="arrow_carrot-left"></i>','<i class="arrow_carrot-right"></i>'],
            //rewindNav : true,
            //scrollPerPage : false,

        });
    }

    jQuery('.btn-menu-canvas').click(function () {
        
        if (jQuery('#offcanvas').hasClass('active')) {
            jQuery('body').removeClass('off-canvas-active');
            jQuery('#offcanvas').removeClass('active');
            jQuery('.wrapper').removeClass('offcanvas-push');
        } else {
            jQuery('body').addClass('off-canvas-active');
            jQuery('#offcanvas').addClass('active');
            jQuery('.wrapper').addClass('offcanvas-push');
        }
    });
    
})



function ajaxPost(form, callback) {
    var xhr = new XMLHttpRequest();
    var params = [].filter.call(form.elements, function (el) { return !(el.type in ['checkbox', 'radio']) || el.checked; })
    .filter(function (el) { return !!el.name; }) //Nameless elements die.
    .filter(function (el) { return !el.disabled; }) //Disabled elements die.
    .map(function (el) {
        if (el.type == 'checkbox') return encodeURIComponent(el.name) + '=' + encodeURIComponent(el.checked);
        else return encodeURIComponent(el.name) + '=' + encodeURIComponent(el.value);
    }).join('&'); //Then join all the strings by &
    xhr.open("POST", form.action);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    if (typeof callback !="") {
        xhr.onload = callback.bind(xhr);
    }
    xhr.send(params);
}