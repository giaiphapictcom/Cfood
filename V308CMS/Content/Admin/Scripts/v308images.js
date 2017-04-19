/// <reference path="jquery-1.10.2.min.js" />

function Admin_Images_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Images_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_Images_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Images_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_Images_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Images_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Images_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_Images_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Images_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/ThucHienXoaAnh",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Anh?pPage=" + pPage + "";
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

function Admin_Images_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mLink = $('#pLinkId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageLink = $('#ImageUrl').val();

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTitle': mTieuDe, 'pGroupId': mNhomTin, 'pLink': mLink, 'pSummary': mSummary, 'pImageUrl': mImageLink },
        dataType: 'json',
        url: "/Admin/AnhThucHienThemMoi/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Images_PopUpKetQuaTaoMoi(data.message);
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

function Admin_Images_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mLink = $('#pLinkId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageLink = $('#ImageUrl').val();

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTitle': mTieuDe, 'pGroupId': mNhomTin, 'pLink': mLink, 'pSummary': mSummary, 'pImageUrl': mImageLink },
        dataType: 'json',
        url: "/Admin/ThucHienSuaAnh/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Images_PopUpKetQuaSua(data.message, mId);
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


function Admin_Images_Reset() {

    $('#pTitleId').val("");
    $('#pLinkId').val("");
    $('#pSummaryId').val("");
    $('#ImageUrl').val("");
    ClosePopup();
}

function Admin_Images_ComeHome() {

    window.location = "/Admin/Anh";
}

function Admin_Images_ComeSua(pId) {

    window.location = "/Admin/AnhSua?pId=" + pId + "";
}
function ThayDoiKieuNhomAnh(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/Admin/Anh?pPage=1&pType=" + pType + "";
}