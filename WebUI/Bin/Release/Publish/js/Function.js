
function isBlank(val) {
    if (val == null) {
        return true;
    }
    for (var i = 0; i < val.length; i++) {
        if ((val.charAt(i) != ' ') && (val.charAt(i) != "\t") && (val.charAt(i) != "\n") && (val.charAt(i) != "\r")) {
            return false;
        }
    }
    return true;
}

function CheckNumber(str) {
    for (var i = 0; i < str.length; i++) {
        var temp = str.substring(i, i + 1);
        if (!(temp == "." || (temp >= 0 && temp <= 9))) {
            return false;
        }
    }
    return true;
}

function CheckNumber2(str) {
    for (var i = 0; i < str.length; i++) {
        var temp = str.substring(i, i + 1);
        if (!(temp >= 0 && temp <= 9)) {
            return false;
        }
    }
    return true;
}

function CheckNumber3(str) {
    for (var i = 0; i < str.length; i++) {
        var temp = str.substring(i, i + 1);
        if (!(temp == "," || temp == "." || (temp >= 0 && temp <= 9))) {
            return false;
        }
    }

    if (str.substring(0, 1) == ",") {
        return false;
    }
    else{
        var count = 0;
        for (var i = 0; i < str.length; i++) {
            var temp = str.substring(i, i + 1);
            if (temp == ",") { 
                count = count+1
            }
        }
        if (count > 1) {
            return false;
        }
    } 
    
    return true;
}


function isEmail(s) {

    if (s == "") return false;
    if (s.indexOf(" ") > 0) return false;
    if (s.indexOf("@") == -1) return false;

    var i = 1;
    var sLength = s.length;
    if (s.indexOf(".") == -1) return false;
    if (s.indexOf("..") != -1) return false;
    if (s.indexOf(".@") != -1) return false;
    if (s.indexOf("@") != s.lastIndexOf("@")) return false;
    if (s.lastIndexOf(".") == s.length - 1) return false;

    //Neu email la chuoi ky tu khong thuoc chuoi ky tu sau
    var str = "0123456789abcdefghikjlmnopqrstuvwxyzABCDEFGHIKJLMNOPQRSTUVWXYZ-@._";
    for (var j = 0; j < s.length; j++)
        if (str.indexOf(s.charAt(j)) == -1)
        return false;

    return true;
}


function noNumbers(e) {
    var keynum;
    var keychar;
    var numcheck;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }
    if (keynum == 8 || keynum == undefined)
        return true;
    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    return numcheck.test(keychar);
}

function noNumbers2(e) {
    var keynum;
    var keychar;
    var numcheck;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }
    if (keynum == 46 || keynum == 8 || keynum == undefined)
        return true;
    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    return numcheck.test(keychar);
}

function noNumbers3(e) {
    var keynum;
    var keychar;
    var numcheck;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }
    //alert(keynum);
    //if (keynum == 8 || keynum == undefined)
    //Bo sugn them truong hop nhap dau phay "," 44
    if (keynum == 44 || keynum == 8 || keynum == undefined)
        return true;
    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    return numcheck.test(keychar);
}

function formatText(controlID) {
    var ctrl = document.getElementById(controlID);
    var str = ctrl.value;

    while (str.indexOf('.') != -1)
        str = str.replace(".", "");

    var i = str.length;
    var count = 0;
    var templ = '';
    while (i >= 0) {
        count++
        if (count % 4 == 0 && i > 0) {
            templ = str.charAt(i - 1) + '.' + templ;
            count = 1;
        }
        else
            templ = str.charAt(i - 1) + templ;
        i--;
    }
    ctrl.value = templ;
}
function setCookie(name, value) {

    var date = new Date(),
        expires = 'expires=';
    date.setTime(date.getTime() + 60 * 60 * 50000);
    expires += date.toGMTString();
    //document.cookie = name + '=' + value + '; ' + expires + '; path=/';
    document.cookie = name + "=" + escape(value) +
        ";domain=" + window.location.hostname +
        ";path=/" +
        ";expires=" + expires;
}

function getCookie(name) {
    var allCookies = document.cookie.split(';'),
        cookieCounter = 0,
        currentCookie = '';
    for (cookieCounter = 0; cookieCounter < allCookies.length; cookieCounter++) {
        currentCookie = allCookies[cookieCounter];
        while (currentCookie.charAt(0) === ' ') {
            currentCookie = currentCookie.substring(1, currentCookie.length);
        }
        if (currentCookie.indexOf(name + '=') === 0) {
            return currentCookie.substring(name.length + 1, currentCookie.length);
        }
    }
    return false;
}
function ResponseRedirect(url) {
    window.location.replace(url);
}
var format = function (num) {
    var str = num.toString().replace("$", ""), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(".") > 0) {
        parts = str.split(".");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] != ",") {
            output.push(str[j]);
            if (i % 3 == 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return (formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
};
$(document).ready(function () {
    //called when key is pressed in textbox
    $(".formatMoney").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
        
            return false;
        }
    });
});