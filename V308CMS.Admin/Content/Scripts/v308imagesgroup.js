/// <reference path="jquery-1.10.2.min.js" />

function Admin_ImagesGroup_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ImagesGroup_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_ImagesGroup_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ImagesGroup_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_ImagesGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ImagesGroup_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ImagesGroup_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_ImagesGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ImagesGroup_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/ImageType/OnDelete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/ImageType/Index?pPage=" + pPage + "";
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

function Admin_ImagesGroup_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mThuTu = $('#pOrderId').val();
    var mKichCo = $('#pSizeId').val();
    var mAnh = $('#ImageUrl').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTieuDe': mTieuDe, 'pGroupId': mNhomTin, 'pUuTien': mThuTu, 'pKichCo': mKichCo, 'pImageUrl': mAnh },
        dataType: 'json',
        url: "/ImageType/OnCreate/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ImagesGroup_PopUpKetQuaTaoMoi(data.message);
                scrollWindow(10);
            }
            else {
                HienThiPopUp(data.message);
                scrollWindow(10);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}

function Admin_ImagesGroup_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mThuTu = $('#pOrderId').val();
    var mKichCo = $('#pSizeId').val();
    var mAnh = $('#ImageUrl').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTieuDe': mTieuDe, 'pGroupId': mNhomTin, 'pUuTien': mThuTu, 'pKichCo': mKichCo, 'pImageUrl': mAnh },
        dataType: 'json',
        url: "/ImageType/OnEdit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ImagesGroup_PopUpKetQuaSua(data.message, mId);
                scrollWindow(10);
            }
            else {
                HienThiPopUp(data.message);
                scrollWindow(10);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}


function Admin_ImagesGroup_Reset() {

    $('#pTitleId').val("");
    ClosePopup();
}

function Admin_ImagesGroup_ComeHome() {

    window.location = "/ImageType/Index";
}

function Admin_ImagesGroup_ComeSua(pId) {

    window.location = "/ImageType/Edit?pId=" + pId + "";
}
function ThayDoiKieuNhomAnh2(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/ImageType/Index?pPage=1&pType=" + pType + "";
}