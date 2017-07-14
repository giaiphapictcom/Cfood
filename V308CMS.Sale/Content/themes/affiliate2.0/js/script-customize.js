
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

    //jQuery.fn.datepicker.defaults.format = "mm/dd/yyyy";

    $('.datepicker').each(function (index) {
        var dateval = new Date($(this).val());
        if (dateval == "Invalid Date" || dateval < new Date(2001, 1, 1)) {
            $(this).val("");
        } else {
            $(this).val($.datepicker.formatDate('dd/mm/yy', new Date()));
        }
        $(this).datepicker({ dateFormat: 'dd/mm/yy' });
    });

});
