/// <reference path="jquery-1.10.2.min.js" />

function Admin_ProductDistributor_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductDistributor_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_ProductDistributor_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductDistributor_Reset();">Tiếp tục thêm</a><a class="controlno" href="javascript:Admin_ProductDistributor_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ProductDistributor_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductDistributor_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_ProductDistributor_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ProductDistributor_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/ProductDistributor/Delete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/ProductDistributor/Index?pPage=" + pPage + "";
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

function Admin_ProductDistributor_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mThuTu = $('#pOrderId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageUrl = $('#ImageUrl').val();

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pTieuDe': mTieuDe, 'pUuTien': mThuTu, 'pSummary': mSummary, 'pUrlImage': mImageUrl },
        dataType: 'json',
        url: "/ProductDistributor/Create/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductDistributor_PopUpKetQuaTaoMoi(data.message);
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

function Admin_ProductDistributor_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mThuTu = $('#pOrderId').val();
    var mSummary = $('#pSummaryId').val();
    var mImageUrl = $('#ImageUrl').val();

    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pTieuDe': mTieuDe, 'pUuTien': mThuTu, 'pSummary': mSummary, 'pUrlImage': mImageUrl },
        dataType: 'json',
        url: "/ProductDistributor/Edit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductDistributor_PopUpKetQuaSua(data.message, mId);
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


function Admin_ProductDistributor_Reset() {

    $('#pTitleId').val("");
    ClosePopup();
}

function Admin_ProductDistributor_ComeHome() {

    window.location = "/ProductDistributor/Index";
}

function Admin_ProductDistributor_ComeSua(pId) {

    window.location = "/ProductDistributor/Edit?pId=" + pId + "";
}