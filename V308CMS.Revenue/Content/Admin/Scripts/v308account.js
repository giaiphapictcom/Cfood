/// <reference path="jquery-1.10.2.min.js" />

function Admin_Account_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Account_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Account_HienThiPopUpAn(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Account_An(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_Account_HienThiPopUpHien(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Account_Hien(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Account_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Account_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_Account_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Account_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Account_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_Account_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Account_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/ThucHienXoaAccount",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Account?pPage=" + pPage + "";
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

function Admin_Account_An(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/AccountThucHienAn",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Account?pPage=" + pPage + "";
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
function Admin_Account_Hien(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/AccountThucHienHien",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/Account?pPage=" + pPage + "";
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
function Admin_Account_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mFullName = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mpAccount = $('#pAccountId').val();
    var mpPassword1 = $('#pPassword1Id').val();
    var mpPassword2 = $('#pPassword2Id').val();
    var mpEmail = $('#pEmailId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTitle': mFullName, 'pGroupId': mNhomTin, 'pAccount': mpAccount, 'pPassword1': mpPassword1, 'pPassword2': mpPassword2, 'pEmail': mpEmail },
        dataType: 'json',
        url: "/Admin/AccountThucHienThemMoi/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Account_PopUpKetQuaTaoMoi(data.message);
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

function Admin_Account_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mFullName = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mpAccount = $('#pAccountId').val();
    var mpActive = $('#pActiveId').val();
    var mpEmail = $('#pEmailId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTitle': mFullName, 'pGroupId': mNhomTin, 'pAccount': mpAccount, 'pActive': mpActive, 'pEmail': mpEmail },
        dataType: 'json',
        url: "/Admin/ThucHienSuaAccount/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Account_PopUpKetQuaSua(data.message, mId);
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

function Admin_Product_ThayDoiPassWord(pId) {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mpPassword1 = $('#pPassword1Id').val();
    var mpPassword2 = $('#pPassword2Id').val();
    var mpPassword3 = $('#pPassword3Id').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pPassword1': mpPassword1, 'pPassword2': mpPassword2, 'pPassword3': mpPassword3 },
        dataType: 'json',
        url: "/Admin/ThucHienDoiMatKhau/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                HienThiPopUp(data.message, mId);
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
function Admin_Account_Reset() {

    $('#pTitleId').val("");
    $('#pLinkId').val("");
    $('#pSummaryId').val("");
    $('#ImageUrl').val("");
    ClosePopup();
}

function Admin_Account_ComeHome() {

    window.location = "/Admin/Account";
}

function Admin_Account_ComeSua(pId) {

    window.location = "/Admin/Account?pId=" + pId + "";
}
function ThayDoiKieuNhomAccount(pPage) {
    var pType = $("#pGroupId").val();
    window.location = "/Admin/Account?pPage=1&pType=" + pType + "";
}