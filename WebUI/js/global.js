//toast notify
function showToast(type, message,time) {
    var title = "";
    console.log(type + "|" + message);
    switch (type) {
        case "msg":
            title = "";
            iziToast.success({
                title: title,
                message: message,
                timeout: time
            });
            break;
        case "err":
            title = "Lỗi: ";
            iziToast.error({
                title: title,
                message: message,
                timeout: time
            });
            break;
        case "war":
            title = "Oop!";
            iziToast.warning({
                title: title,
                message: message,
                timeout: time
            });
            break;
        case "dv":
            title = "Lưu ý ";
            iziToast.error({
                title: title,
                message: message,
                timeout: time
            });
            break;
        default:
            title = "";
            iziToast.message({
                title: title,
                message: message,
                timeout: time
            });
    }
};

