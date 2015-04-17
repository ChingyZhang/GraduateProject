<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientInMap.aspx.cs" Inherits="SubModule_CM_Map_ClientInMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="http://webapi.amap.com/maps?v=1.2&key=ddbbb5266a915795aeb9c954b4d3a4bf" type="text/javascript"></script>
    <script type="text/javascript" src="mapjs.js"></script>
    <base target="_self" />
    <style type="text/css">
        #control input {
            width: 80px;
            height: 30px;
        }
    </style>
</head>
<body style="background: white; padding-top: 10px;">
    <form id="form1" runat="server">
        <div id="control" style="width: 800px; height: 40px; text-align: right; float: right;">
            客户名称：<asp:Label ID="lb_FullName" runat="server" Text=""></asp:Label>
            地址：<asp:Label ID="lb_Address" runat="server" Text=""></asp:Label>
            <input id="btn_snyc" class="button" type="button" value="门店地址定位" runat="server" />
            <input id="btn_addpoint" class="button" type="button" onclick="addPointer()" value="点选新位置" runat="server" />
            <asp:Button runat="server" ID="btn_OK" Text="保存更改" OnClientClick="fillText()" OnClick="btn_OK_Click" />
            <input id="lngX" name="lngX" type="text" runat="server" style="display: none" /><input id="latY" name="latY" type="text" runat="server" style="display: none" />
        </div>
        <div id="div" style="width: 800px; height: 500px;">
        </div>


    </form>
</body>
</html>
