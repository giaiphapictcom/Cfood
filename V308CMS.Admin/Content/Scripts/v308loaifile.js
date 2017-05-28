/// <reference path="jquery-1.10.2.min.js" />

function Admin_FileGroup_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_FileGroup_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_FileGroup_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_FileGroup_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_FileGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_FileGroup_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_FileGroup_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_FileGroup_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_FileGroup_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/FileType/Delete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/FileType/Index?pPage=" + pPage + "";
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

function Admin_FileGroup_ThucHienLuuMoi() {

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
        url: "/FileType/Create/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_FileGroup_PopUpKetQuaTaoMoi(data.message);
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

function Admin_FileGroup_ThucHienLuuSua() {

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
        url: "/FileType/Edit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_FileGroup_PopUpKetQuaSua(data.message, mId);
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


function Admin_FileGroup_Reset() {

    $('#pTitleId').val("");
    ClosePopup();
}

function Admin_FileGroup_ComeHome() {

    window.location = "/FileType/Index";
}

function Admin_FileGroup_ComeSua(pId) {

    window.location = "/FileType/Edit?pId=" + pId + "";
}
function ThayDoiKieuNhomFile2(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/FileType/Index?pPage=1&pType=" + pType + "";
}