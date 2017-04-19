/// <reference path="jquery-1.10.2.min.js" />

function Admin_Product_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Product_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_Product_HienThiPopUpAn(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Product_An(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_Product_HienThiPopUpHien(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Product_Hien(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}
function Admin_Product_PopUpKetQuaTao(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Product_Reset();">Tiếp tục thêm tin</a><a class="controlno" href="javascript:Admin_Product_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Product_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_Product_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_Product_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_Product_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/SanPhamThucHienXoa",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/SanPham?pPage=" + pPage + "";
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

function Admin_Product_An(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/SanPhamThucHienAn",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/SanPham?pPage=" + pPage + "";
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
function Admin_Product_Hien(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/Admin/SanPhamThucHienHien",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Admin/SanPham?pPage=" + pPage + "";
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

function Admin_Product_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var editor = CKEDITOR.instances.editor1;
    var mNoiDung = editor.getData();
    $("#editor1").val(mNoiDung);
    var formData = $('#formtaomoi').serialize();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: formData,
        dataType: 'json',
        url: "/Admin/SanPhamThucHienThemMoi/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Product_PopUpKetQuaTao(data.message);
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

function Admin_Product_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $("#pId").val();
    var editor = CKEDITOR.instances.editor1;
    var mNoiDung = editor.getData();
    $("#editor1").val(mNoiDung);
    var formData = $('#formtaomoi').serialize();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: formData,
        dataType: 'json',
        url: "/Admin/SanPhamThucHienSua/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_Product_PopUpKetQuaSua(data.message, mId);
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


function Admin_Product_Reset() {

    $('#pTitleId').val("");
    $('#ImageUrl').val("");
    $('#pSummaryId').val("");
    var editor = CKEDITOR.instances.editor1;
    editor.setData('');
    ClosePopup();
}

function Admin_Product_ComeHome() {

    window.location = "/Admin/SanPham";
}

function Admin_Product_ComeSuaTin(pId) {

    window.location = "/Admin/SanPhamSua?pId=" + pId + "";
}
function ThayDoiKieuSanPham(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/Admin/SanPham?pPage=1&pType="+pType+"";
}