<%@ Page Language="c#" Inherits="MCSCCS.SubModule.Login.index" CodeFile="Index.aspx.cs"
    StylesheetTheme="basic" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>营销管理系统_V4</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta content="雅士利营销管理系统" name="title">
    <meta content="南京美驰管理系统" name="description">
    <meta content="南京美驰 雅士利 营销管理系统 EMP" name="keywords">

    <script>
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
    </script>

    <%--<script language="JScript" event="OnCompleted(hResult,pErrorObject, pAsyncContext)"
        for="foo">
     if (document.forms[0].tbx_MacAddr!=null){
         document.forms[0].tbx_MacAddr.value = unescape(MACAddr);
         //document.forms[0].txtIPAddr.value = unescape(IPAddr);
         document.forms[0].txt_DNSName.value = unescape(sDNSName);     
     }
    </script>

    <script language="JScript" event="OnObjectReady(objObject,objAsyncContext)" for="foo">
        if (objObject.IPEnabled != null && objObject.IPEnabled != "undefined" && objObject.IPEnabled == true) {
            if (objObject.MACAddress != null && objObject.MACAddress != "undefined")
                MACAddr = MACAddr + objObject.MACAddress + ",";
            if (objObject.IPEnabled && objObject.IPAddress(0) != null && objObject.IPAddress(0) != "undefined")
                IPAddr = IPAddr + objObject.IPAddress(0) + ",";
            if (objObject.DNSHostName != null && objObject.DNSHostName != "undefined")
                sDNSName = objObject.DNSHostName;
        }
    </script>--%>
    <style type="text/css">
        body
        {
        }
        .bg1
        {
            background-image: url(../../Images/bg_login4.jpg);
            background-repeat: no-repeat;
            background-position: center top;
        }
        .bg2
        {
            background-image: url(../../Images/LoginPic.png);
            background-repeat: no-repeat;
        }
        .style1
        {
            font-family: 华文彩云;
            font-size: xx-large;
            font-weight: bold;
            color: #FFFFFF;
        }
    </style>
</head>
<body class="bg1">
    <%--  <object id="locator" classid="CLSID:76A64158-CB41-11D1-8B02-00600806D9B6" height="1px">
    </object>
    <object id="foo" classid="CLSID:75718C9A-F029-11d1-A1AC-00C04FB6C223" height="1px">
    </object>

   <script language="JScript">
        var service = locator.ConnectServer();
        var MACAddr = "";
        var IPAddr = "";
        var sDNSName = "";
        service.Security_.ImpersonationLevel = 3;
        service.InstancesOfAsync(foo, 'Win32_NetworkAdapterConfiguration');
    </script>--%>
    <form id="Form1" method="post" runat="server">
    <table width="985" border="0" height="562" align="center">
        <tr>
            <td width="55">
                &nbsp;
            </td>
            <td width="555">
                <table width="555" border="0" height="562">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="555" height="300">
                        </td>
                    </tr>
                    <tr>
                        <td height="118">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td width="40">
                &nbsp;
            </td>
            <td width="238" valign="top">
                <table width="238" border="0">
                    <tr>
                        <td height="150">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="327" class="bg2">
                            <asp:Login ID="Login1" runat="server" LoginButtonText="登 录" DestinationPageUrl="~/SubModule/desktop.aspx"
                                OnLoggedIn="Login1_LoggedIn" OnLoggingIn="Login1_LoggingIn">
                                <LoginButtonStyle CssClass="signinButton" Width="60px" />
                                <LayoutTemplate>
                                    <table width="233" border="0">
                                        <tr>
                                            <td height="60">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><font size="2" color="#666666">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">用户名:</asp:Label></font></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="33" align="center">
                                                <asp:TextBox ID="UserName" runat="server" Style="height: 22px; font-size: 16px;"
                                                    Width="180px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="必须填写“用户名”。" ToolTip="必须填写“用户名”。" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><font size="2" color="#666666">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label></font></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="33" align="center">
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Style="height: 22px;
                                                    font-size: 16px;" Width="180px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="必须填写“密码”。" ToolTip="必须填写“密码”。" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><font size="2" color="#666666">
                                                    <asp:Label ID="lb_VerifyCode" runat="server" AssociatedControlID="Password">验证码:</asp:Label></font></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" height="33">
                                                <asp:TextBox ID="tbx_VerifyCode" runat="server" Style="height: 22px; font-size: 16px;"
                                                    Width="80px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_VerifyCode"
                                                    Display="Dynamic" ErrorMessage="必填" ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                                <img id="imgVerify" alt="看不清？换张图" height="22" onclick="javascript:RefreshVerifyCode();"
                                                    src="../../VerifyCode.aspx?" style="cursor: hand" width="80" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="color: Red;">
                                                <asp:CheckBox ID="RememberMe" runat="server" Text="下次记住我。" Visible="false" />
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="40" align="center">
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Login" ValidationGroup="Login1"
                                                    ImageUrl="~/Images/loginbutton.jpg" />
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:Login>
                        </td>
                    </tr>
                    <tr>
                        <td height="118">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            
            <td   align="center" colspan="5">
                <asp:Label ID="lb_BrowserVersion" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;
                <asp:HyperLink ID="hy_DownloadIE" NavigateUrl="http://download.microsoft.com/download/1/6/1/16174D37-73C1-4F76-A305-902E9D32BAC9/IE8-WindowsXP-x86-CHS.exe"
                    runat="server">IE8(For XP)官方下载地址</asp:HyperLink>
            </td>
           
        </tr>
        <tr>
            <td align="center" colspan="5" height="50">
                @ 2004-2013 <a class="copyRightLink" href="http://www.meichis.com" style="color: #990033">
                    南京美驰资讯科技开发有限公司 </a>版权所有.
            </td>
        </tr>
    </table>
    <%--<table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td colspan="2" style="color: White">
                            为安全起见，登录本系统时需要获取您计算的网卡MAC地址，请将使用IE或IE内核的浏览器，并将本系统地址加入信任站点，谢谢合作！如有疑问请咨询系统管理员！
                        </td>
                    </tr>
                    <tr height="10px">
                        <td colspan="2">
                            <a href="../../Help/0.0.将EMP系统加入安全站点并启用ActiveX控件.htm"  style="color: Blue">
                                点击打开【设置IE安全性】帮助视频</a>
                        </td>
                    </tr>
                    <tr height="10px">
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="color: White">
                            MAC地址
                        </td>
                        <td>
                            <asp:TextBox ID="tbx_MacAddr" runat="server" Width="160px" ReadOnly="true" BorderStyle="None"
                                BackColor="#4A8BCA" ForeColor="White"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: White" width="80px">
                            计算机名称
                        </td>
                        <td>
                            <asp:TextBox ID="txt_DNSName" runat="server" Width="160px" ReadOnly="true" BorderStyle="None"
                                BackColor="#4A8BCA" ForeColor="White"></asp:TextBox>
                        </td>
                    </tr>
                </table>--%>
    
    </form>
</body>
</html>
