/// <reference path="../assets/js/jquery-1.10.2.min.js" />
function DangNhap() {
    $("#ThongBao").html('');
    var mUserName = $("#txtUserName").val();
    var mPassWord = $("#txtPassword").val();
    if (mUserName == null || mUserName == '') {
        $("#ThongBao").html('Bạn chưa nhập tài khoản');
        return;
    }
    if (mPassWord == null || mPassWord == '') {
        $("#ThongBao").html('Bạn chưa nhập mật khẩu');
        return;
    }
    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pUserName': mUserName, 'pPassWord': mPassWord},
        dataType: 'json',
        url: "/Home/Login",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/Home/Index";
            }
            else {
                $("#ThongBao").html(data.message);
                $("#wait").css("display", "none");
            }
        },
        error: function (x, t, m) {
            $("#ThongBao").html('Lỗi kết nối tới máy chủ !');
            $("#wait").css("display", "none");
        }
    });
}