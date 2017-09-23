
$(document).ready(function () {
    left_column_static.action();
    $(".showsubmenu").click(function () {
        $(".nav-item ul").hide();
        $(this).parents(".nav-item").find("ul").css({"display":"block"});
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
    
    jQuery(".main-content img").removeAttr("style").addClass("img-fluid");

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

var left_column_static = {
    headerSpace: 0,
    objHeight:0,
    action: function () {
        this.headerSpace = $(".main-content").offset().top;
        this.objHeight = $(".main-content").offset().top + $("#leftcol").height();
        //console.log("left height=" + $("#leftcol").height());
        left_column_static.setvalue();
        $(window).scroll(function () {
            left_column_static.objHeight = $(".main-content").offset().top + $("#leftcol").height();
            left_column_static.setvalue();
        });
    },
    setvalue: function () {
        if (window.innerWidth < 768) {
             return ;
        }
        var windowBottom2Top = $(window).scrollTop() + $(window).height();
            var contentBottom2Top = $(".main-content").height() + left_column_static.headerSpace;

            if (windowBottom2Top > left_column_static.objHeight) {
                if (windowBottom2Top < contentBottom2Top) {
                    $("#leftcol").css("top", windowBottom2Top - left_column_static.objHeight);
                }
                
            } else {
                $("#leftcol").css("top", 0);
            }

        //});
    }
};
