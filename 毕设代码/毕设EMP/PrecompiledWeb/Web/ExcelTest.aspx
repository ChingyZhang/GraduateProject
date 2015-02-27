<%@ page language="C#" autoeventwireup="true" inherits="ExcelTest, App_Web_vx5flqxa" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:TextBox ID="tbx_FileName" runat="server" Width="243px"></asp:TextBox>
    <asp:Button ID="bt_CreateXLSFile" runat="server" 
        onclick="bt_CreateXLSFile_Click" Text="CreateXLSFile" />
    <asp:Button ID="bt_OpenXLS" runat="server" onclick="bt_OpenXLS_Click" 
        Text="OpenXLS" />
    </form>
</body>
</html>
