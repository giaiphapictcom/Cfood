/// <reference path="jquery-1.10.2.min.js" />

function Admin_News_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_News_XoaTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_News_HienThiPopUpAnTinTuc(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_News_AnTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_News_HienThiPopUpHienTinTuc(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_News_HienTinTuc(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_News_PopUpKetQuaTaoTin(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_News_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_News_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_News_PopUpKetQuaSuaTin(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_News_ComeSuaTin('+pId+');">Tiếp tục</a><a class="controlno" href="javascript:Admin_News_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_News_XoaTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId},
        dataType: 'json',
        url: "/News/Delete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/News/Index?pPage=" + pPage + "";
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

function Admin_News_AnTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/News/ChangeStatus",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/News/Index?pPage=" + pPage + "";
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
function Admin_News_HienTinTuc(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/News/ChangeStatus",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/News/Index?pPage=" + pPage + "";
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

function Admin_News_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mAnh = $('#ImageUrl').val();
    var mMoTa = $('#pSummaryId').val();
    var editor = CKEDITOR.instances.editor1;
    var mNoiDung = editor.getData();
    var mThuTu = $('#pOrderId').val();
    var mKichHoat = $('#pActiveId').attr("checked");
    var mSlide = $('#pSlideId').attr("checked");
    var mHot = $('#pHotId').attr("checked");
    var mFast = $('#pFastId').attr("checked");
    var mFeatured = $('#pFeaturedId').attr("checked");
    var mDescription = $('#pDescriptionId').val();
    var mKeyWord = $('#pKeyWordId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pDescription': mDescription, 'pKeyWord': mKeyWord, 'pTieuDe': mTieuDe, 'pImageUrl': mAnh, 'pGroupId': mNhomTin, 'pMoTa': mMoTa, 'pNoiDung': mNoiDung, 'pKichHoat': mKichHoat, 'pUuTien': mThuTu, 'pHot': mHot, 'pFast': mFast, 'pFeatured': mFeatured, 'pSlide': mSlide },
        dataType: 'json',
        url: "/News/Create/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_News_PopUpKetQuaTaoTin(data.message);
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

function Admin_News_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mNhomTin = $('#pGroupId').val();
    var mAnh = $('#ImageUrl').val();
    var mMoTa = $('#pSummaryId').val();
    var editor = CKEDITOR.instances.editor1;
    var mNoiDung = editor.getData();
    var mThuTu = $('#pOrderId').val();
    var mKichHoat = $('#pActiveId').attr("checked");
    var mSlide = $('#pSlideId').attr("checked");
    var mHot = $('#pHotId').attr("checked");
    var mFast = $('#pFastId').attr("checked");
    var mFeatured = $('#pFeaturedId').attr("checked");
    var mDescription = $('#pDescriptionId').val();
    var mKeyWord = $('#pKeyWordId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pDescription': mDescription, 'pKeyWord': mKeyWord, 'pId': mId, 'pTieuDe': mTieuDe, 'pImageUrl': mAnh, 'pGroupId': mNhomTin, 'pMoTa': mMoTa, 'pNoiDung': mNoiDung, 'pKichHoat': mKichHoat, 'pUuTien': mThuTu, 'pHot': mHot, 'pFast': mFast, 'pFeatured': mFeatured, 'pSlide': mSlide },
        dataType: 'json',
        url: "/News/Edit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_News_PopUpKetQuaSuaTin(data.message, mId);
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


function Admin_News_Reset() {

   $('#pTitleId').val("");
   $('#ImageUrl').val("");
   $('#pSummaryId').val("");
   var editor = CKEDITOR.instances.editor1;
   editor.setData('');
   ClosePopup();
}

function Admin_News_ComeHome() {

    window.location = "/News/Index";
}

function Admin_News_ComeSuaTin(pId) {

    window.location = "/News/Edit?pId=" + pId + "";
}
function ThayDoiKieuTinTuc(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/News/Index?pPage=1&pType=" + pType + "";
}