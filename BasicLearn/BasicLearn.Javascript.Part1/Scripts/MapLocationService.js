$(function () {
    var _pos = {
        latitude: 116.404,
        longitude: 39.915
    }
    var getPos = function (position) {
        _pos.latitude = position.coords.latitude;
        _pos.longitude = position.coords.longitude;

        var bMap = new BMap.Map("container");
        var point = new BMap.Point(_pos.longitude, _pos.latitude);
        bMap.centerAndZoom(point, 15);
        bMap.enableScrollWheelZoom();

        var marker2 = new BMap.Marker(point);  // 创建标注
        bMap.addOverlay(marker2);              // 将标注添加到地图中

        var infoWindow2 = new BMap.InfoWindow("<p style='font-size:14px;'>I'am here</p>");
        marker2.addEventListener("click", function () { this.openInfoWindow(infoWindow2); });
    }
    var errorPos = function (error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                alert("User denied the request for Geolocation.")
                break;
            case error.POSITION_UNAVAILABLE:
                alert("Location information is unavailable.")
                break;
            case error.TIMEOUT:
                alert("The request to get user location timed out.")
                break;
            case error.UNKNOWN_ERROR:
                alert("An unknown error occurred.")
                break;
        }
    }
    //getCurrentPosition中getPos是一个异步事件
    navigator.geolocation.getCurrentPosition(getPos, errorPos);

    /*
    setTimeout(function () {
    debugger;
    bMap.setCenter(_pos.latitude, _pos.longitude);
    bMap.setZoom(15);
    }, 1500);*/
});

