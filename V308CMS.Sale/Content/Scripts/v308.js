/// <reference path="jquery-1.10.2.min.js" />
function AddError(pvalue) {
    $("#err_list_login_id").html('Bạn chưa nhập tài khoản');
}
function openShopCart() {
    $('.shp-lst').css({ opacity: 1, visibility: 'visible' });
}
function closeShopCart() {
    $('.shp-lst').css({ opacity: 0, visibility: 'hidden' });
}
function ShowLogin() {
    $('#popup').css("display", "block");
    $('#web365-login').css("display", "block");
    $('#web365-register').css("display", "none");
}
function ShowRegister() {
    $('#popup').css("display", "block");
    $('#web365-login').css("display", "none");
    $('#web365-register').css("display", "block");
}
function ClosePopUpLogin() {
    $('#popup').css("display", "none");
    $('#web365-login').css("display", "none");
    $('#web365-register').css("display", "none");
    $('#web365-warning').css("display", "none");
}
function ShowWarning(content) {
    $('#web365-warning').css("display", "block");
    $('#popup').css("display", "block");
    $('#web365-login').css("display", "none");
    $('#web365-register').css("display", "none");
    $('#contentwarning').html(content);
}
function DangNhapHome() {
    $("#err_list_login_id").html('');
    var mUserName = $("#pUserName").val();
    var mPassWord = $("#pPassWord").val();
    console.log(mUserName);
    if (mUserName == null || mUserName == '') {
        $("#err_list_login_id").html('Bạn chưa nhập tài khoản');
        return;
    }
    if (mPassWord == null || mPassWord == '') {
        $("#err_list_login_id").html('Bạn chưa nhập mật khẩu');
        return;
    }
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pUserName': mUserName, 'pPassWord': mPassWord },
        dataType: 'json',
        url: "/Account/CheckDangNhap",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                //alert(data.message);
                window.location = "/";
            }
            else {
                $("#err_list_login_id").html(data.message);
                $("#wait").css("display", "none");
            }
        },
        error: function (x, t, m) {
            $("#err_list_login_id").html('Lỗi kết nối tới máy chủ !');
            $("#wait").css("display", "none");
        }
    });
}

function DangKyHome() {
    $("#err_list_login_id").html('');
    var mUserName = $("#pUserName").val();
    var mPassWord = $("#pPassWord").val();
    var mPassWord2 = $("#pPassWord2").val();
    var mEmail = $("#pEmail").val();
    var mFullName = $("#pFullName").val();
    var mMobile = $("#pMobile").val();
    if (mUserName == null || mUserName == '') {
        $("#err_list_login_id").html('Bạn chưa nhập tài khoản');
        return;
    }
    if (mPassWord == null || mPassWord == '') {
        $("#err_list_login_id").html('Bạn chưa nhập mật khẩu');
        return;
    }
    if (mPassWord2 == null || mPassWord2 == '') {
        $("#err_list_login_id").html('Bạn chưa nhập mật khẩu lần 2');
        return;
    }
    if (mEmail == null || mEmail == '') {
        $("#err_list_login_id").html('Bạn chưa nhập email');
        return;
    }
    if (mFullName == null || mFullName == '') {
        $("#err_list_login_id").html('Bạn chưa nhập họ tên');
        return;
    }
    if (mMobile == null || mMobile == '') {
        $("#err_list_login_id").html('Bạn chưa nhập số điện thoại');
        return;
    }
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pUserName': mUserName, 'pPassWord': mPassWord, 'pPassWord2': mPassWord2, 'pEmail': mEmail, 'pFullName': mFullName, 'pMobile': mMobile },
        dataType: 'json',
        url: "/Account/CheckDangKy",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                //alert(data.message);
                window.location = "/";
            }
            else {
                $("#err_list_login_id").html(data.message);
                $("#wait").css("display", "none");
            }
        },
        error: function (x, t, m) {
            $("#err_list_login_id").html('Lỗi kết nối tới máy chủ !');
            $("#wait").css("display", "none");
        }
    });
}
function GetShopCart() {
    //$("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/Home/getShopCart",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                $("#f-list").html(data.html);
                $("#div_gio_hang_tong_tien").html(data.totalprice);
            }
            openShopCart();
            setTimeout(function () {
                closeShopCart();
            }, 2000);
        },
        error: function (x, t, m) {
            //$("#cError").html('Lỗi kết nối tới máy chủ !');
            //$("#wait").css("display", "none");
        }
    });
}
function GetShopCartSilent() {
    //$("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/Home/getShopCart",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                $("#f-list").html(data.html);
                $("#div_gio_hang_tong_tien").html(data.totalprice);
            }
        },
        error: function (x, t, m) {
            //$("#cError").html('Lỗi kết nối tới máy chủ !');
            //$("#wait").css("display", "none");
        }
    });
}
function MuaHangHome(pId) {
    //$("#amount2").html('');
    var mnumberproduct = $("#amount2").val();
    //lay dinh luong so che
    var mUnit = $('#slunit-id').val();
    var mproductId = pId;
    if (mnumberproduct == null || mnumberproduct == '') {
        alert('Bạn chưa nhập số lượng muốn mua.');
        return;
    }
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pNumber': mnumberproduct, 'pId': mproductId, 'pUnit': mUnit},
        dataType: 'json',
        url: "/Home/addToShopCart",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                GetShopCart();
            }
            else {
                //$("#cError").html(data.message);
                $("#wait").css("display", "none");
            }
        },
        error: function (x, t, m) {
            //$("#cError").html('Lỗi kết nối tới máy chủ !');
            $("#wait").css("display", "none");
        }
    });
}



function HoanThanhMuaHangHome() {
    var mEmail = $("#pEmail").val();
    var mFullName = $("#pFullName").val();
    var mMobile = $("#pMobile").val();
    var mTimeDelivery = $("#js-select-time-delivery").val();
    var mDayDelivery = $("#js-select-day-delivery").val();
    var mAddress = $("#pAddress").val();
    var mAddressDelivery = $("#pAddressDelivery").val();
    var mCity = $("#select-city").val();
    var mDistrict = $("#select-district").val();
    //
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pEmail': mEmail, 'pCity': mCity, 'pDistrict': mDistrict, 'pTimeDelivery': mTimeDelivery, 'pDayDelivery': mDayDelivery, 'pFullName': mFullName, 'pMobile': mMobile, 'pAddress': mAddress, 'pAddressDelivery': mAddressDelivery },
        dataType: 'json',
        url: "/Home/ThucHienThanhToan",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                alert(data.message);
                window.location = "/";
                $("#wait").css("display", "none");
            }
            else if (data.code == 2) {
                alert(data.message);
                $("#wait").css("display", "none");
                window.location = "/dang-nhap.html";
            }
            else {
                alert(data.message);
                $("#wait").css("display", "none");
            }
        },
        error: function (x, t, m) {
            alert(data.message);
            $("#wait").css("display", "none");
        }
    });
}
function ShowShopCart1() {
    $('#shopcart1').css("display", "block");
    $('#shopcart2').css("display", "none");
    $('#shopcart3').css("display", "none");
}

function ShowShopCart2() {
    $('#shopcart2').css("display", "block");
    $('#shopcart1').css("display", "none");
    $('#shopcart3').css("display", "none");
}
function ShowShopCart3() {
    $('#shopcart3').css("display", "block");
    $('#shopcart1').css("display", "none");
    $('#shopcart2').css("display", "none");
}