
var kquery = {
    ajax: function (configs) {
        var settings = {
            "url": "",
            "method": "post",
            "user": "",
            "password": "",
            "data": null,
            "responseType": "text",
            "headers": {},
            "enableCache": true,
            "onSucceed": null,
            "onClientError": null,
            "onServerError": null
        };
        for (s in settings) {
            settings[s] = configs[s] ? configs[s] : settings[s];
        }
        var xhr = getXHR();
        xhr.onreadystatechane = function () {
            if (xhr.readyState == 4) {/* complete */
                var result = settings["responseType"] == "text" ? xhr.responseText : xhr.responseXML;
                if (((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) && typeof (settings["onSucceed"] == "function")) {
                    settings["onSucceed"](result, xhr.status); //callback after succeed
                }
                else if (xhr.status >= 400 && xhr.status < 500) {//onClientError
                    settings["onClientError"](result, xhr.status);
                }
                else if (xhr.status >= 500) {//onServerError
                    settings["onServerError"](result, xhr.status);
                }
            }
        }
        xhr.open(settings["method"], settings["url"], settings["user"], settings["password"]);
        //设置自定义headers
        if (typeof (settings["headers"]) == "object") {
            var headers = settings["headers"];
            for (h in headers) {
                xhr.setRequestHeader(h, headers[h]);
            }
        }
        //禁用缓存
        if (!settings["enableCache"]) {
            xhr.setRequestHeader("If-Modified-Since", "0");
        }
        //post请求
        if (settings["method"].toLowerCase() == "post") {
            xhr.setRequestHeader("Content-Type",
            "application/x-www-form-urlencoded;charset=UTF-8");
            var data = "";
            if (typeof (settings["data"]) == "object") {
                for (d in settings["data"]) {
                    data += (d + '=' + settings["data"][d]);
                }
            } else {
                data = settings["data"];
            }
            xhr.send(data);
        } else {
            xhr.send(data); //get请求
        }
    }
}

//创建对象XmlHttpRequest
function getXHR() {
    if (window.XMLHttpRequest) {
        //firefox,ie7+,chrome,safari
        return new window.XMLHttpRequest;
    }
    else {
        try {
            return new ActiveXObject("Microsoft.XMLHTTP");
        }catch (e) {
            return null;
        }
    }
}