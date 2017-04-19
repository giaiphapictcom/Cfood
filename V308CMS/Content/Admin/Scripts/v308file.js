/// <reference path="jquery-1.10.2.min.js" />

function Admin_File_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_File_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_File_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_File_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_File_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_File_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_File_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_File_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_File_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/ThucHienXoaFile",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/File?pPage=" + pPage + "";
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

function Admin_File_ThucHienLuuMoi() {
    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mLink = $('#pLinkId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageLink = $('#ImageUrl').val();
    var mpValue = $('#pValueId').val();
    var mKichHoat = $('#pActiveId').attr("checked");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTitle': mTieuDe, 'pGroupId': mNhomTin, 'pActive': mKichHoat, 'pLink': mLink, 'pSummary': mSummary, 'pImageUrl': mImageLink, 'pValue': mpValue },
        dataType: 'json',
        url: "/Admin/FileThucHienThemMoi/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_File_PopUpKetQuaTaoMoi(data.message);
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

function Admin_File_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mLink = $('#pLinkId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageLink = $('#ImageUrl').val();
    var mpValue = $('#pValueId').val();
    var mKichHoat = $('#pActiveId').attr("checked");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTitle': mTieuDe, 'pActive': mKichHoat, 'pGroupId': mNhomTin, 'pLink': mLink, 'pSummary': mSummary, 'pImageUrl': mImageLink, 'pValue': mpValue },
        dataType: 'json',
        url: "/Admin/ThucHienSuaFile/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_File_PopUpKetQuaSua(data.message, mId);
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


function Admin_File_Reset() {

    $('#pTitleId').val("");
    $('#pLinkId').val("");
    $('#pSummaryId').val("");
    $('#ImageUrl').val("");
    ClosePopup();
}

function Admin_File_ComeHome() {

    window.location = "/Admin/File";
}

function Admin_File_ComeSua(pId) {

    window.location = "/Admin/FileSua?pId=" + pId + "";
}
function ThayDoiKieuNhomFile(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/Admin/File?pPage=1&pType=" + pType + "";
}