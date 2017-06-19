/// <reference path="jquery-1.10.2.min.js" />

function Admin_Order_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Order_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_Order_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/ProductOrder/OnDelete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/ProductOrder/Index?pPage=" + pPage + "";
            }
            else {
                $("#wait").css("display", "none");
                HienThiPopUp(data.message);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}

function Admin_Product_XacNhan(pId) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/OrderXacNhan",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/ProductOrder/Index";
            }
            else {
                $("#wait").css("display", "none");
                HienThiPopUp(data.message);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}

function Admin_Order_Reset() {

    $('#pTitleId').val("");
    $('#ImageUrl').val("");
    $('#pSummaryId').val("");
    var editor = CKEDITOR.instances.editor1;
    editor.setData('');
    ClosePopup();
}

function Admin_Order_ComeHome() {

    window.location = "/ProductOrder/Index";
}

function ThayDoiKieuHoaDon(pPage) {
    var pType = $("#pGroupId").val();
    window.location = "/ProductOrder/Index?pPage=1&pType=" + pType + "";
}