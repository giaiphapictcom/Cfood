/// <reference path="jquery-1.10.2.min.js" />

function Admin_NewsGroup_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_NewsGroup_XoaTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_NewsGroup_HienThiPopUpAnTinTuc(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_NewsGroup_AnTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_NewsGroup_HienThiPopUpHienTinTuc(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_NewsGroup_HienTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_NewsGroup_PopUpKetQuaTaoTin(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_NewsGroup_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_NewsGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_NewsGroup_PopUpKetQuaSuaTin(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_NewsGroup_ComeSuaTin(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_NewsGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_NewsGroup_XoaTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/NewsCategory/Delete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/NewsCategory/Index?pPage=" + pPage + "";
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

function Admin_NewsGroup_AnTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/NewsCategory/ChangeStatus",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/NewsCategory/Index?pPage=" + pPage + "";
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
function Admin_NewsGroup_HienTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/NewsCategory/ChangeStatus",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/NewsCategory/Index?pPage=" + pPage + "";
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

function Admin_NewsGroup_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mLink = $('#pLinkId').val();
    var mNhomTin = $('#pGroupId').val();
    var mThuTu = $('#pOrderId').val();
    var mKichHoat = $('#pActiveId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTieuDe': mTieuDe, 'pLink': mLink, 'pGroupId': mNhomTin, 'pKichHoat': mKichHoat, 'pUuTien': mThuTu },
        dataType: 'json',
        url: "/NewsCategory/Create/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_NewsGroup_PopUpKetQuaTaoTin(data.message);
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

function Admin_NewsGroup_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mLink = $('#pLinkId').val();
    var mNhomTin = $('#pGroupId').val();
    var mThuTu = $('#pOrderId').val();
    var mKichHoat = $('#pActiveId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTieuDe': mTieuDe, 'pLink': mLink, 'pGroupId': mNhomTin, 'pKichHoat': mKichHoat, 'pUuTien': mThuTu },
        dataType: 'json',
        url: "/NewsCategory/Edit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_NewsGroup_PopUpKetQuaSuaTin(data.message, mId);
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


function Admin_NewsGroup_Reset() {

    $('#pTitleId').val("");
    ClosePopup();
}

function Admin_NewsGroup_ComeHome() {

    window.location = "/NewsCategory/Index";
}

function Admin_NewsGroup_ComeSuaTin(pId) {

    window.location = "/NewsCategory/Edit?pId=" + pId + "";
}
function ThayDoiKieuTinTuc2(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/NewsCategory/Index?pPage=1&pType=" + pType + "";
}