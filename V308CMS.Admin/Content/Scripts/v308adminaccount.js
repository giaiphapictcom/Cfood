/// <reference path="jquery-1.10.2.min.js" />


function Admin_AdminAccount_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_AdminAccount_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_AdminAccount_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_AdminAccount_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_AdminAccount_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_AdminAccount_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_AdminAccount_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_AdminAccount_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_AdminAccount_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/AdminAccount/Delete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/AdminAccount/Index?pPage=" + pPage + "";
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

function Admin_AdminAccount_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mFullName = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mpAccount = $('#pAccountId').val();
    var mpPassword1 = $('#pPassword1Id').val();
    var mpPassword2 = $('#pPassword2Id').val();
    var mpEmail = $('#pEmailId').val();

    var mpsanpham = $('#psanpham').prop('checked');
    var mptintuc = $('#ptintuc').prop('checked');
    var mpkhachhang = $('#pkhachhang').prop('checked');
    var mphinhanh = $('#phinhanh').prop('checked');
    var mpupload = $('#pupload').prop('checked');
    var mptaikhoan = $('#ptaikhoan').prop('checked');
    var mphethong = $('#phethong').prop('checked');
    var mpthungrac = $('#pthungrac').prop('checked');

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data:{ 'psanpham': mpsanpham, 'ptintuc': mptintuc, 'pkhachhang': mpkhachhang, 'phinhanh': mphinhanh, 'pupload': mpupload, 'ptaikhoan': mptaikhoan, 'phethong': mphethong, 'pthungrac': mpthungrac, 'pTitle': mFullName, 'pGroupId': mNhomTin, 'pAccount': mpAccount, 'pPassword1': mpPassword1, 'pPassword2': mpPassword2, 'pEmail': mpEmail },
        dataType: 'json',
        url: "/AdminAccount/Create/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_AdminAccount_PopUpKetQuaTaoMoi(data.message);
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

function Admin_AdminAccount_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mFullName = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mpAccount = $('#pAccountId').prop('checked');
    var mpActive = $('#pActiveId').val();
    var mpEmail = $('#pEmailId').val();

    var mpsanpham = $('#psanpham').prop('checked');
    var mptintuc = $('#ptintuc').prop('checked');
    var mpkhachhang = $('#pkhachhang').prop('checked');
    var mphinhanh = $('#phinhanh').prop('checked');
    var mpupload = $('#pupload').prop('checked');
    var mptaikhoan = $('#ptaikhoan').prop('checked');
    var mphethong = $('#phethong').prop('checked');
    var mpthungrac = $('#pthungrac').prop('checked');

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'psanpham': mpsanpham, 'ptintuc': mptintuc, 'pkhachhang': mpkhachhang, 'phinhanh': mphinhanh, 'pupload': mpupload, 'ptaikhoan': mptaikhoan, 'phethong': mphethong, 'pthungrac': mpthungrac, 'pId': mId, 'pTitle': mFullName, 'pGroupId': mNhomTin, 'pAccount': mpAccount, 'pActive': mpActive, 'pEmail': mpEmail },
        dataType: 'json',
        url: "/AdminAccount/Edit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_AdminAccount_PopUpKetQuaSua(data.message, mId);
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


function Admin_AdminAccount_Reset() {

    $('#pTitleId').val("");
    $('#pLinkId').val("");
    $('#pSummaryId').val("");
    $('#ImageUrl').val("");
    ClosePopup();
}

function Admin_AdminAccount_ComeHome() {

    window.location = "/AdminAccount/Index";
}

function Admin_AdminAccount_ComeSua(pId) {

    window.location = "/AdminAccount/Edit?pId=" + pId + "";
}
function ThayDoiKieuNhomAdminAccount(pPage) {
    var pType = $("#pGroupId").val();
    window.location = "/AdminAccount/Index?pPage=1&pType=" + pType + "";
}