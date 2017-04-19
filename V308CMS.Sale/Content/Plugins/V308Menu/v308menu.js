$(document).ready(function () {
    $(".tang1 li").hover(function () {
        $(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(100);
    }, function () {
        $(this).find('ul:first').css({ visibility: "hidden" });
    });
}
);