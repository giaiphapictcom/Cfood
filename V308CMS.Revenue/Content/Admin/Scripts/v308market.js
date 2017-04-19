/// <reference path="jquery-1.10.2.min.js" />

function Admin_Market_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Market_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Market_HienThiPopUpAn(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Market_An(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Market_HienThiPopUpHien(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Market_Hien(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Market_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Market_Reset();">Tiếp tục thêm</a><a class="controlno" href="javascript:Admin_Market_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ProductType_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Market_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_Market_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Market_Xoa(pId, pPage) {
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/MarketThucHienXoa",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Market?pPage=" + pPage + "";
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

function Admin_Market_An(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/MarketThucHienAn",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Market?pPage=" + pPage + "";
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
function Admin_Market_Hien(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/MarketThucHienHien",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Market?pPage=" + pPage + "";
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

function Admin_Market_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mUserName = $('#pUserNameId').val();
    var mpMarketType = $('#pMarketTypeId').val();
    var mImageUrl = $('#ImageUrl').val();
    var mActive = $('#pActiveId').val();
    var mEmail = $('#pEmailId').val();
    var mFullName = $('#pFullNameId').val();
    var mMobile = $('#pMobileId').val();
    var mSumary = $('#pSumaryId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pUserName': mUserName, 'pMarketType': mpMarketType, 'pAvata': mImageUrl, 'pActive': mActive, 'pEmail': mEmail, 'pFullName': mFullName, 'pMobile': mMobile, 'pSumary': mSumary },
        dataType: 'json',
        url: "/Admin/MarketThucHienThemMoi/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductType_PopUpKetQuaTaoMoi(data.message);
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

function Admin_Market_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mUserName = $('#pUserNameId').val();
    var mpMarketType = $('#pMarketTypeId').val();
    var mImageUrl = $('#ImageUrl').val();
    var mActive = $('#pActiveId').val();
    var mEmail = $('#pEmailId').val();
    var mFullName = $('#pFullNameId').val();
    var mMobile = $('#pMobileId').val();
    var mSumary = $('#pSumaryId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pUserName': mUserName,'pMarketType':mpMarketType, 'pAvata': mImageUrl, 'pActive': mActive, 'pEmail': mEmail, 'pFullName': mFullName, 'pMobile': mMobile, 'pSumary': mSumary },
        dataType: 'json',
        url: "/Admin/MarketThucHienSua/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductType_PopUpKetQuaSua(data.message);
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


function Admin_Market_Reset() {

    $('#pUserNameId').val("");
    ClosePopup();
}

function Admin_Market_ComeHome() {

    window.location = "/Admin/Market";
}

function Admin_Market_ComeSua(pId) {

    window.location = "/Admin/MarketSua?pId=" + pId + "";
}
function ThayDoiKieuMarket2(pPage) {
    window.location = "/Admin/Market?pPage=1";
}