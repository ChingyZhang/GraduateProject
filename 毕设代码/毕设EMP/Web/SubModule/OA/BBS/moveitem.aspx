<%@ Page Language="C#" AutoEventWireup="true" CodeFile="moveitem.aspx.cs" Inherits="SubModule_OA_BBS_moveitem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>MoveItem</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div align="center" style="vertical-align:middle">
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
            bordercolor="#0066cc" width="50%" height="56" id="AutoNumber1" class="GbText">
            <tr>
                <td width="880" height="9" class="GbText" bgcolor="#0066cc" align="middle">
                    <b><font color="#ffffff">移动帖子</font></b>
                </td>
            </tr>
            <tr>
                <td width="880" height="26" bgcolor="#e8f4ff" align="middle">
                    <asp:DropDownList ID="ddlBoardList" runat="server">
                    </asp:DropDownList>
                    <asp:Literal ID="ltMessage" runat="server" Visible="False"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="880" height="10" bgcolor="#e8f4ff" align="middle">
                    <input id="cmdOK" type="submit" class="ButtonCss" value="确 定" runat="server" onserverclick="cmdOK_ServerClick"></td>
            </tr>
            <tr>
                <td width="880" height="10" bgcolor="#e8f4ff" align="middle">
                <a href="javascript:window.close();">关闭窗口</a>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
