<%@ Page Language="c#" Inherits="MCSCCS.SubModule.Login.index" CodeFile="Index.aspx.cs"
    StylesheetTheme="basic" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>PBMS分销管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta content="PBMS分销管理系统" name="title">
    <meta content="南京美驰 PBMS分销管理系统" name="description">
    <meta content="南京美驰 分销管理" name="keywords">

    <script type="text/javascript" src="../../js/slider/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="../../js/slider/kick.js"></script>
    <script type="text/javascript" src="../../js/slider/mousewheel.js"></script>
    <script type="text/javascript" src="../../js/slider/easing.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/slider/slider.css">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#slider').rhinoslider({
                effect: 'kick',
                showTime: 6000,
                effectTime: 1200,
                controlsPrevNext: false,
                controlsPlayPause: false,
                controlsMousewheel: false,
                autoPlay: true,
                pauseOnHover: false,
                changeBullets: 'before',
                showControls: 'never',
                showBullets: 'always',
                animateActive: false,
                shiftValue: '50',
                styles: 'width:100%'
            });

        });
    </script>
    <style type="text/css">
        .rhino-container {
            font-size: 0;
            padding: 0;
            margin: 0;
            overflow: hidden;
        }

        #slider {
            position: relative;
            width: 100%;
            height: 397px;
            padding: 0;
            margin: 0;
        }

            #slider .s1 {
                background: url(../../Images/LoginBanner01.jpg) center center no-repeat;
            }

            #slider .s2 {
                background: url(../../Images/LoginBanner02.jpg) center center no-repeat;
            }

        .page-width { /* set regular width */
            width: 1024px;
            margin: 0 auto;
            position: relative;
            z-index: 999999;
        }

        .loginbox {
            height: 348px;
            width: 360px;
            position: absolute;
            top: -360px;
            left: 600px;
            z-index: 999999;
            background-color: white;
            filter: alpha(opacity=92);
            -moz-opacity: 0.92;
            -khtml-opacity: 0.92;
            opacity: 0.92;
            border-radius:10px;
            box-shadow: 10px 10px 20px #000; 
        }
    </style>

    <script type="text/javascript">
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
    </script>

</head>
<body class="">

    <form id="Form1" method="post" runat="server">
        <div class="page-width">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 27px"></td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <%--<td style="height: 89px; width: 229px">
                                    <img src="../../images/LOGO.png" alt=""></td>--%>
                                <td style="height: 89px; width: 168px">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="height: 46px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="color: #ED1C16; font-size: 16px; font-family: 微软雅黑; font-weight: bold;">企业进销存管理系统</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="height: 89px; width: 667px" colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>


        <div id="slider">
            <div class="s1">&nbsp;</div>
            <div class="s2">&nbsp;</div>
        </div>

        <div class="page-width">
            <div class="loginbox">
                <asp:Login ID="Login1" runat="server" LoginButtonText="登 录" DestinationPageUrl="~/SubModule/desktop.aspx"
                    OnLoggedIn="Login1_LoggedIn" OnLoggingIn="Login1_LoggingIn">
                    <LoginButtonStyle CssClass="signinButton" Width="60px" />
                    <LayoutTemplate>
                        <div style="padding-top: 20px; padding-left: 20px">
                            <div>
                                <img src="../../Images/Yabais/Login_Tip.png" />
                            </div>
                            <div style="padding-top: 60px; width: 360px; clear: both">
                                <div style="width: 90px; float: left">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Style="height: 22px; font-size: 18px;font-weight:bolder;color:red">用户名:</asp:Label>
                                </div>
                                <div style="float: left">
                                    <asp:TextBox ID="UserName" runat="server" Style="height: 22px; font-size: 16px; font-weight:bolder"
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="必须填写“用户名”。" ToolTip="必须填写“用户名”。" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="padding-top: 20px; width: 360px; clear: both">
                                <div style="width: 90px; float: left">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Style="height: 22px; font-size: 18px;font-weight:bolder;color:red">密&nbsp;&nbsp;&nbsp;&nbsp;码:</asp:Label>
                                </div>
                                <div style="float: left">
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Style="height: 22px; font-size: 16px;; font-weight:bolder"
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="必须填写“密码”。" ToolTip="必须填写“密码”。" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="padding-top: 20px; width: 360px; clear: both">
                                <div style="width: 90px; float: left">
                                    <asp:Label ID="lb_VerifyCode" runat="server" AssociatedControlID="Password" Style="height: 22px; font-size: 18px;font-weight:bolder;color:red">验证码:</asp:Label>
                                </div>
                                <div style="float: left">
                                    <asp:TextBox ID="tbx_VerifyCode" runat="server" Style="height: 22px; font-size: 16px;"
                                        Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_VerifyCode"
                                        Display="Dynamic" ErrorMessage="必填" ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                    <img id="imgVerify" alt="看不清？换张图" height="22" onclick="javascript:RefreshVerifyCode();"
                                        src="../../VerifyCode.aspx?" style="cursor: hand" width="80" />
                                </div>
                            </div>
                            <div style="padding-top: 20px; clear: both">
                            </div>
                            <div style="padding-top: 20px; text-align: center; clear: both">
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Login" ValidationGroup="Login1"
                                    ImageUrl="~/Images/Yabais/Login_Button.png" />
                            </div>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </div>
        </div>

        <div class="PageBottom">
            <div style="padding-left: 20%; padding-top: 12px; color: white">
                中粮可口可乐饮料（山东）有限公司          
            </div>
        </div>
        <div style="color: gray; text-align: center">
            
        </div>



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
