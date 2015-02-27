<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageBrowser.aspx.cs" Inherits="ckeditor_ImageBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>图片浏览器</title>
	<style type="text/css">
		body { margin: 0px; }
		form { width:750px; background-color: #E3E3C7; }
		h1 { padding: 15px; margin: 0px; padding-bottom: 0px; font-family:Arial; font-size: 14pt; color: #737357; }
		input[type='button'],input[type='submit'],input[type='reset'] { width:80px;}
	</style>
</head>
<body>
    <form id="form1" runat="server">
 <div>
		<h1>图片浏览器</h1>

		<table width="720px" cellpadding="10" cellspacing="0" border="1" style="background-color:#F1F1E3; margin:15px;">
			<tr>
				<td style="width: 396px;" valign="middle" align="center">
					<asp:Image ID="Image1" runat="server" Style="max-height: 450px; max-width: 380px;" />
				</td>
				<td style="width: 324px;" valign="top">
					上传日期：<br />
					<asp:DropDownList ID="ddl_DirectoryList" runat="server" Style="width: 160px;" 
                        OnSelectedIndexChanged="ChangeDirectory" AutoPostBack="true" />
					<asp:Panel ID="SearchBox" runat="server" DefaultButton="bt_Search">
						查找：<br />
						<asp:TextBox ID="SearchTerms" runat="server"/>
						<asp:Button ID="bt_Search" runat="server" Text="查找" OnClick="Search" 
                            UseSubmitBehavior="false" Width="60px" />
						<asp:Button ID="bt_DeleteImage" runat="server" OnClick="DeleteImage" 
                            OnClientClick="return confirm('是否确认删除选择中的图片文件?');" 
                            Text="删除" Width="60px" />
						<br />
					</asp:Panel>
					<asp:ListBox ID="ImageList" runat="server" Style="width: 280px; height: 180px;" OnSelectedIndexChanged="SelectImage" AutoPostBack="true" />

					<br />
					<br />
					图片尺寸：<br />
					<asp:TextBox ID="ResizeWidth" runat="server" Width="50" OnTextChanged="ResizeWidthChanged" />
					x
					<asp:TextBox ID="ResizeHeight" runat="server" Width="50" OnTextChanged="ResizeHeightChanged" />
					<asp:HiddenField ID="ImageAspectRatio" runat="server" />
					<asp:Button ID="bt_ResizeImage" runat="server" Text="修改图片尺寸" 
                        OnClick="ResizeImage" /><br />
					<asp:Label ID="ResizeMessage" runat="server" ForeColor="Red" />
					<br /><br />
					上传图片（小于10M）<br />
					<asp:FileUpload ID="bt_UploadedImageFile" runat="server" />
					<asp:Button ID="bt_Upload" runat="server" Text="上传" OnClick="Upload" 
                        Width="60px" /><br />
					<br />
				</td>
			</tr>
		</table>
				
		<center>
			<asp:Button ID="bt_OkButton" runat="server" Text="确定" OnClick="Clear" 
                Width="60px" />
			<asp:Button ID="bt_CancelButton" runat="server" Text="取消" 
                OnClientClick="window.top.close(); window.top.opener.focus();" 
                OnClick="Clear" Width="60px" />
			<br /><br />
		</center>
	</div>
    </form>
</body>
</html>
