var mapObj, zoom, tool, marker, infoWindow, showpiont;
var markerArr = new Array();
var windowsArr = new Array();
function mapInit() {
    if (mapObj == null) {
        mapObj = new AMap.Map("div");
        mapObj.plugin(["AMap.ToolBar", "AMap.Scale"], function () {
            tool = new AMap.ToolBar({
                direction: false,
                ruler: false,
                autoPosition: false
            });
            mapObj.addControl(tool);
            scale = new AMap.Scale();
            mapObj.addControl(scale);
            //initmethod("m124", '123', '12314', '12314113123');
        });
    }
    if (marker != null) {
        //mapObj.removeOverlays();
        mapObj.addOverlays(marker);
        mapObj.setZoom(13);
        mapObj.setCenter(marker.getPosition());
        mapObj.bind(marker, "mouseover", function (e) {
            inforWindow.open(mapObj, marker.getPosition());
        });
        mapObj.bind(marker, "mouseout", function (e) {
            inforWindow.close();
        });
    }
    else {
        mapObj.setZoom(13);
        if (document.all.lngX.value != "" && document.all.latY.value != "") {
            addMarker("最终地址", document.all.lngX.value, document.all.latY.value);
        }
    }
}
function addComplexMarker(x, y, id, name, tel, addr) {
    //构建点对象
    if (marker == null) {
        marker = new AMap.Marker({
            id: id,//唯一ID
            position: new AMap.LngLat(x, y),//基点位置
            offset: new AMap.Pixel(-14, -34),//相对于基点的位置
            draggable: true,
            icon: new AMap.Icon({  //复杂图标
                size: new AMap.Size(27, 36),//图标大小
                image: "http://api.amap.com/webapi/static/Images/custom_a_j.png", //大图地址
                imageOffset: new AMap.Pixel(-28, 0)//相对于大图的取图位置
            })
        });

        marker.setContent("<img src='http://api.amap.com/webapi/static/Images/0.png'/>");


        var info = [];
        info.push("<b>" + name + "</b>");
        info.push("电话 :" + tel);
        info.push("地址 : " + addr);

        inforWindow = new AMap.InfoWindow({
            offset: new AMap.Pixel(0, -28),
            content: info.join("<br/>")
        });
    }
    else { marker.setPosition(new AMap.LngLat(x, y)); }
}
function addMarker(addr, x, y) {
    //构建点对象
    if (marker == null) {
        marker = new AMap.Marker({
            position: new AMap.LngLat(x, y),//基点位置
            offset: new AMap.Pixel(-14, -34),//相对于基点的位置
            draggable: true,
            icon: new AMap.Icon({  //复杂图标
                size: new AMap.Size(27, 36),//图标大小
                image: "http://api.amap.com/webapi/static/Images/custom_a_j.png", //大图地址
                imageOffset: new AMap.Pixel(-28, 0)//相对于大图的取图位置
            })
        });
        marker.setContent("<img src='http://api.amap.com/webapi/static/Images/0.png'/>");


        var info = [];
        info.push("" + addr);

        inforWindow = new AMap.InfoWindow({
            offset: new AMap.Pixel(0, -28),
            content: info.join("<br/>")
        });
    } else { marker.setPosition(new AMap.LngLat(x, y)); }
}
function fillText() {
    document.all.lngX.value = marker.getPosition().lng;
    document.all.latY.value = marker.getPosition().lat;
}
function loadScript() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://webapi.amap.com/maps?v=1.2&key=ddbbb5266a915795aeb9c954b4d3a4bf&callback=mapInit";
    document.body.appendChild(script);
}
window.onload = loadScript;

function initmethod(id, name, tel, addr) {

    showpiont = function (e) {
        var z = mapObj.getZoom();

        if (z < 13) {
            alert("当前视野范围较大，请放大至更小的视野，提高门店选择的精确性！");
            return;
        }
        addComplexMarker(e.lnglat.lng, e.lnglat.lat, id, name, tel, addr);
        mapInit();
    };
    var v = document.getElementById("btn_snyc");
    if(v!=null) v.onclick = function () { geocoder(addr) };
}
function addPointer() {
    mapObj.bind(mapObj, "click", showpiont);
}




function geocoder(addr) {
    var MGeocoder;
    //加载地理编码插件  
    mapObj.plugin(["AMap.Geocoder"], function () {
        MGeocoder = new AMap.Geocoder({

        });
        //返回地理编码结果   
        AMap.event.addListener(MGeocoder, "complete", geocoder_CallBack);
        //地理编码  
        MGeocoder.getLocation(addr);
    });
}
//地理编码返回结果展示     
function geocoder_CallBack(data) {
    var geocode = new Array();
    geocode = data.geocodes;
    if (geocode.length > 0) {
        addMarker(geocode[0].formattedAddress, geocode[0].location.getLng(), geocode[0].location.getLat());
        mapInit();
    }
    else
        alert("没有定位结果！");

}