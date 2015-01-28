var apiUri = "http://localhost:12947/";

/******* 去除空格 begin *******/
String.prototype.trim = function () {
    return this.replace(/ /g, '');
    //return this.replace(/(^[\s]*)|([\s]*$)/g, '');
}

///URL传参取值
function Request(strName) {
    var strHref =unescape(window.document.location.href).trim();
    var intPos = strHref.indexOf("?");
    var strRight = strHref.substr(intPos + 1);
    var arrTmp = strRight.split("&");
    for (var i = 0; i < arrTmp.length; i++) {
        var arrTemp = arrTmp[i].split("=");
        if (arrTemp[0].toUpperCase() == strName.toUpperCase()) {
            return decodeURIComponent(arrTemp[1].split('/')[0]);
        }
    }
    return "";
}