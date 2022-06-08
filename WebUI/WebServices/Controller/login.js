$(document).ready(function () {
    $(document).bind('keypress', function (e) {
        var keycode = (e.keyCode ? e.keyCode : e.which);
        if (keycode == '13') {
            document.getElementById('login').click();
        }
    });
});



function CheckLogin() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/WebServices/Login.asmx/checklogin",
        data: {
            username: "'" + $("#username").val() + "'", password: "'" + $('#userpassword').val() + "'"
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //console.log(result.d);
            if (result.d == "0") {
                window.showToast("err", "Tài khoản không tồn tại hoặc chưa được kích hoạt!", 3000);
            } else if (result.d == "-1") {
                window.showToast("err", "Mật khẩu không hợp lệ vui lòng thử lại!", 3000);
            } else if (result.d == "-2") {
                window.showToast("err", "Tài khoản chưa kích hoạt hoặc tạm thời bị khóa!", 3000);
            }
            else if (result.d == "1") {
                $("#spinner").show();
                if (getUrlParameter('ReturnURL') != undefined) {
                    setTimeout('ResponseRedirect("/?ReturnURL=' + getUrlParameter('ReturnURL') + '")', 200);
                } else {
                    setTimeout('ResponseRedirect("/")', 200);
                }

            } else {
                window.showToast("err", "Tài khoản của bạn đang bị tạm khóa đến " + result.d + " do nhập sai mật khẩu quá 10 lần, vui lòng thử lại sau!", 5000);
            }

        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
            window.showToast("err", "Có lỗi xảy ra, vui lòng thử lại sau!", 3000);
        }
    });
}

function validate() {
    var isValid = true;
    if ($("#userpassword").val().trim() == "" && $("#username").val().trim() == "") {
        window.showToast("err", "Tên đăng nhập và mật khẩu không được để trống!", 3000);
        $("#username").focus();
        isValid = false;
    } else {
        if ($("#username").val().trim() == "") {
            window.showToast("err", "Tên đăng nhập không được để trống!", 3000);
            $("#username").focus();
            isValid = false;
        }
        if ($("#userpassword").val().trim() == "") {
            window.showToast("err", "Mật khẩu không được để trống!", 3000);
            $("#userpassword").focus();
            isValid = false;
        }
    }
    return isValid;
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};