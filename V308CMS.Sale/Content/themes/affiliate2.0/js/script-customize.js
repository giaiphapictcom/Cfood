
$(document).ready(function () {

   

    jQuery("#search-top-sm .icon-search").click(function () {
        jQuery("#search-top-sm").addClass("active");
    });
    jQuery("#search-top-sm .search-close").click(function () {
        jQuery("#search-top-sm").removeClass("active");
    });

    jQuery("button.btn-menu-canvas").click(function () {
        jQuery("#offcanvas").addClass("active");
    });

    jQuery("#offcanvas .off-canvas-nav").click(function () {
        jQuery("#offcanvas").removeClass("active");
    });
});
