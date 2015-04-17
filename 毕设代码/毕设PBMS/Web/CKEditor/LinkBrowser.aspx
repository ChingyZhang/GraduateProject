<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkBrowser.aspx.cs" Inherits="ckeditor_LinkBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件链接浏览器</title>
    <style type="text/css">
        body{  margin: 0px; }        
        form { width:500px;background-color: #E3E3C7;  }        
        h1 { padding: 15px;  margin: 0px;  padding-bottom: 0px;  font-family:Arial; font-size: 14pt; color: #737357;  }        
        .tab-panel .ajax__tab_body {background-color: #E3E3C7; }
        input[type='button'],input[type='submit'],input[type='reset'] { width:80px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>文件链接浏览器</h1>
        <table width="100%" cellpadding="10" cellspacing="0" border="1" style="background-color:#F1F1E3;">
            <tr>
                <td valign="top">
                    上传日期：<br />
                    <asp:DropDownList ID="ddl_DirectoryList" runat="server" Style="width: 160px;" 
                        OnSelectedIndexChanged="ChangeDirectory" AutoPostBack="true" />
                    <br /><br />
                    
                    <asp:Panel ID="SearchBox" runat="server" DefaultButton="bt_Search">
                        查找：<br />
                        <asp:TextBox ID="SearchTerms" runat="server"/>
                        <asp:Button ID="bt_Search" runat="server" Text="查找" OnClick="Search" 
                            UseSubmitBehavior="false" Width="60px" />
                        <asp:Button ID="bt_DeleteFile" runat="server" OnClick="DeleteFile" 
                            OnClientClick="return confirm('是否确认删除选择中的文件?');" Text="删除" Width="60px" />
                        <br />
                    </asp:Panel>
                    <asp:ListBox ID="ImageList" runat="server" Style="width: 300px; height: 220px;" OnSelectedIndexChanged="SelectImage" AutoPostBack="true" />
                    <br /><br />
                    上传文件（小于10M)<br />
                    <asp:FileUpload ID="bt_UploadedImageFile" runat="server" />
                    &nbsp;<asp:Button ID="bt_Upload" runat="server" Text="上传" OnClick="Upload" 
                        Width="60px" /><br />
                    <br />
                </td>
            </tr>
        </table>

        <center>
            <asp:Button ID="bt_OkButton" runat="server" Text="确定" OnClick="Clear" 
                Width="60px" />
            <asp:Button ID="bt_CancelButton" runat="server" Text="取消" 
                OnClientClick="window.top.close(); window.top.opener.focus();" OnClick="Clear" 
                Width="60px" />
            <br /><br />
        </center>

    </div>
    </form>
</body>
</html>
