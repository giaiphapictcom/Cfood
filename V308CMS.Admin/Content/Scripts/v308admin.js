/// <reference path="../assets/js/jquery-1.10.2.min.js" />
function ClosePopup() {
    $("#popupId").css('display', 'none');
    $("#popupId").empty();
}

function HienThiPopUp(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlno" href="javascript:ClosePopup();">OK</a></div></div>')
}
/*Cuon Windows*/
function scrollWindow(top) {
    $("html, body").animate({ scrollTop: top }, 400);
}