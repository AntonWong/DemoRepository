
var FlashHelper_version = 1;

function getObj() {
    var elements = new Array();

    for (var i = 0; i < arguments.length; i++) {
        var element = arguments[i];
        if (typeof element == 'string') {
            element = document.getElementById(element);
        }
        if (arguments.length == 1) {

            return element;
        }
       
       elements.push(element);
    }

    return elements;
}


/**************************************** FlashHelper ***************************************************/

var FlashHelper = new Object();
FlashHelper.height = 0;
FlashHelper.width = 0;

FlashHelper.shouldWaitForFlash = function() {
    // todo: should return 3 values: installed, notInstalled, silentInstall


};

FlashHelper.isFlashInstalled = function() {
    var ret;

    if (typeof(this.isFlashInstalledMemo) != "undefined") {
        return this.isFlashInstalledMemo;
    }

    if (typeof(ActiveXObject) != "undefined") {
        try {
            var ieObj = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
        } catch(e) {
        }
        ret = (ieObj != null);
    } else {
        var plugin = navigator.mimeTypes["application/x-shockwave-flash"];
        ret = (plugin != null) && (plugin.enabledPlugin != null);
    }

    this.isFlashInstalledMemo = ret;

    return ret;
};

FlashHelper.getFlash = function() {
    //var flash = (navigator.appName.indexOf ("Microsoft") !=-1)?window["storage"]:document["storage"];
    return getObj("storage");
};

FlashHelper.checkFlash = function () {
    // confirm that the Flash Storage is running

    try {
        return (this.getFlash().ping() == "pong");
    }
    catch (e) { return false; }
}

FlashHelper.writeFlash = function () {
    var swfName = "/Javascript/ajaxcdr.swf";

    if (window.ActiveXObject && !FlashHelper.isFlashInstalled()) {
        // browser supports ActiveX
        // Create object element with 
        // download URL for IE OCX
        document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"');
        document.write(' codebase="http://download.macromedia.com');
        document.write('/pub/shockwave/cabs/flash/swflash.cab#version=8,5,0,0"');
        document.write(' height="' + this.height + '" width="' + this.width + '" id="storage">');
        document.write(' <param name="movie" value="' + swfName + '">');
        document.write(' <param name="quality" value="high">');
        document.write(' <param name="swliveconnect" value="true">');
        document.write('<\/object>');
    }
    else {
        // browser supports Netscape Plugin API

        document.write('<object id="storage" data="' + swfName + '"');
        document.write(' type="application/x-shockwave-flash"');
        document.write(' height="' + this.height + '" width="' + this.width + '">');
        document.write('<param name="movie" value="' + swfName + '">');
        document.write('<param name="quality" value="high">');
        document.write('<param name="swliveconnect" value="true">');
        document.write('<param name="pluginurl" value="http://www.macromedia.com/go/getflashplayer">');
        document.write('<param name="pluginspage" value="http://www.macromedia.com/go/getflashplayer">');
        document.write('<p>You need Flash for this.');
        document.write(' Get the latest version from');
        document.write(' <a href="http://www.macromedia.com/software/flashplayer/">here<\/a>.');
        document.write('<\/p>');
        document.write('<\/object>');
    }
}


FlashHelper.addLoadEvent = function (func) {
    var oldonload = window.onload;

    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload();
            func();
        }
    }
}


FlashHelper.load = function () {
    if (typeof (FlashHelper.onload) != "function") { return; }

    if (FlashHelper.isFlashInstalled()) {
        // if we expect Flash to work, wait for both flash and the document to be loaded
        var finishedLoading = this.flashLoaded && this.documentLoaded;
        if (!finishedLoading) { return; }
    }
    // todo: cancel timer

    var fs = FlashHelper.getFlash();

    if ((!FlashHelper.isFlashInstalled() || this.flashLoaded) && fs) {
        if (FlashHelper.checkFlash()) {
            callAppOnLoad(fs);
        } else {
            callAppOnLoad(null);
        }
    } else {
        callAppOnLoad(null);
    }

    function callAppOnLoad(fs) {
        if (FlashHelper.onloadCalled) { return; } // todo: figure out why this case gets hit
        FlashHelper.onloadCalled = true;
        FlashHelper.onload(fs);
    }
}

function storageOnLoad() {
    FlashHelper.flashLoaded = true;
    FlashHelper.load();
}

function storageOnError() {
    FlashHelper.flashLoaded = true;
    FlashHelper.load();
}

FlashHelper.init = function () {
    this.flashLoaded = false;
    this.documentLoaded = false;

    // attach to the window.onload event
    this.addLoadEvent(onload);

    function onload() {
        if (FlashHelper.isFlashInstalled()) {
            // todo: set a timer
            //setTimeout(storageOnError, 60000);
        }

        FlashHelper.documentLoaded = true;
        FlashHelper.load();
    }
}

FlashHelper.init();


/**************************************** CallbackManager ***************************************************/

var CallbackManager = new Object();
CallbackManager.callbacks = new Array();

// assigns and returns a unique callback name for the input callback
CallbackManager.registerCallback = function(callback) {
    // todo: could be improved (look for the first available spot in the callbacks table, if necessary, expand it)
    var length = this.callbacks.push(selfDeleteCallback);
    var callbackID = length - 1;

    return "CallbackManager.callbacks[" + callbackID + "]";

    function selfDeleteCallback(obj) {
        delete CallbackManager.callbacks[callbackID];
        setTimeout(function() { callback(obj); }, 0);
        return;
    }
};

/**************************************** FlashXmlHttpRequest ***************************************************/

var FlashXMLHttpRequest = function() {
    var self = this;
    var method, url, contentType = null;
    var headers = new Array();

    this.open = function(method, url, async, user, password) {
        method = method;
        url = url;
    }
    this.send = function(body) {
        var fs = FlashHelper.getFlash();

        function callback(varName) {
            var response = FlashHelper.getFlash().GetVariable(varName);
            self.responseText = response;

            if (self.onload) {
                self.onload();
            }
        }

        fs.XmlHttp(url, CallbackManager.registerCallback(callback), method, body, contentType, headers);
    };

    this.setRequestHeader = function(header, value) {
        if (header.toLowerCase() == "Content-Type".toLowerCase()) {
            contentType = value;
            return;
        }

        headers.push(header);
        headers.push(value);
    };

    this.getRequestHeader = function() {
    };
    this.getResponseHeader = function() { alert("not supported"); }
    this.getAllResponseHeaders = function() { alert("not supported"); }
    this.abort = function() { alert("not supported"); }
    this.addEventListener = function(b, c) { alert("not supported"); }
    this.dispatchEvent = function() { alert("not supported"); }
    this.openRequest = function(a, b, c, d, e) { this.open(a, b, c, d, e); }
    this.overrideMimeType = function() { alert("not supported"); }
    this.removeEventListener = function(c) { alert("not supported"); }
};

/**************************** PHP to Javascript Project *****************************/
function js_substr(fString, fStart, fLength) {
    fString += '';

    if (fStart < 0) {
        fStart += fString.length;
    }

    if (fLength == undefined) {
        fLength = fString.length;
    } else if (fLength < 0) {
        fLength += fString.length;
    } else {
        fLength += fStart;
    }

    if (fLength < fStart) {
        fLength = fStart;
    }

    return fString.substring(fStart, fLength);
}

function js_strpos(haystack, needle, offset) {
    var i = (haystack + '').indexOf(needle, offset);
    return i === -1 ? false : i;
}

function js_strtoupper(str) {
    return (str + '').toUpperCase();
}

/**************************** PostRequest(url, method) *****************************/
FlashHelper.onload = startApp;

function startApp(fs) {
    if (!fs) { alert("Flash not loaded"); return; }
    //fs.Debug();
}

var PostResponse = "";
var AjaxCrossDomainResname = "";
function displayResponse() {
    PostResponse = FlashHelper.getFlash().GetVariable("retText");
    if (PostResponse) {
        eval(AjaxCrossDomainResname);
    }
}
/*
url             请求地址
method          Get or Post
Isformurlencoded  发送表单、url参数
responseParam   参数
callback        回调函数
*/
function PostRequest(url, method, isFormurlencoded, responseParam, callback) {
    method = js_strtoupper(method);
    AjaxCrossDomainResname = callback;
    var fs = FlashHelper.getFlash();
    if (!isFormurlencoded) {
        var isjson = typeof (responseParam) == "object" && Object.prototype.toString.call(responseParam).toLowerCase() == "[object object]" && !responseParam.length;
        if (isjson) {
            responseParam = JSON.stringify(responseParam);
        }
        fs.XmlHttp(url, "displayResponse", method, responseParam, "application/json; charset=utf-8");
        return;
    }
   
    var body = '';
    var form = document.forms[responseParam];
    for (var i = 0; i < form.length; i++) {
        //如果是单选按钮、复选框、单选下拉框
        if (form.elements[i].type == "radio" || form.elements[i].type == "checkbox" || form.elements[i].type == "select") {
            if (form.elements[i].checked && form.elements[i].name != "") {
                body += encodeURI(form.elements[i].name) + '=' + encodeURI(form.elements[i].value) + '&';
            }
        }
            //如果是多选下拉框
        else if (form.elements[i].type == "select-multiple" && form.elements[i].name != "") {
            for (var sm = 0; sm < form.elements[i].length; sm++) {
                if (form.elements[i][sm].selected) {
                    body += encodeURI(form.elements[i].name) + '=' + encodeURI(form.elements[i][sm].value) + '&';
                }
            }
        }
            //Button、Hidden、Password、Submit、Text、Textarea等文本类型
        else {
            if (form.elements[i].name != "") {
                body += encodeURIComponent(form.elements[i].name) + '=' + encodeURIComponent(form.elements[i].value) + '&';
            }
        }
    }
    if (js_substr(body, -1, 1) == "&") {
        body = js_substr(body, 0, -1);
    }
    fs.XmlHttp(url, "displayResponse", method, body, "application/x-www-form-urlencoded");
}

FlashHelper.writeFlash();
