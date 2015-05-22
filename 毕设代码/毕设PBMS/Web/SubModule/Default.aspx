<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SubModule_Default" %>

<%@ Register Src="~/Controls/OnlineService.ascx" TagPrefix="uc1" TagName="OnlineService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" webpagesubcode="">
    <title>营销管理系统</title>
    <base target="_self" />
    <script type="text/javascript" language="javascript">
        //获取时间		
        function Timer(span) {
            var tmp = new Date();
            var seconds, minutes, hours, date;
            if (tmp.getSeconds() < 10)
                seconds = "0" + tmp.getSeconds();
            else
                seconds = tmp.getSeconds();
            if (tmp.getMinutes() < 10)
                minutes = "0" + tmp.getMinutes();
            else
                minutes = tmp.getMinutes();
            if (tmp.getHours() < 10)
                hours = "0" + tmp.getHours();
            else
                hours = tmp.getHours();

            document.getElementById('lbl_now').innerText = tmp.getFullYear() + "年" + (tmp.getMonth() + 1) + "月" + tmp.getDate() + "日 " + hours + ":" + minutes + ":" + seconds;
        }

        //得到服务器时间每隔updatespan分钟校验一次，每秒更新一次本地时钟
        function GetServerTime() {
            var clientspan = 1 * 1000;
            //更新本地时钟
            setInterval("Timer(" + clientspan + ")", clientspan);
        }

        function doPrint() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.lastIndexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            history.go(0);
        }
    </script>
</head>
<body id="body1" runat="server">
    <form id="form1" runat="server">
        <div class="page">
            <div class="TopBgPosition">
                <div class="top_logo">
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/SubModule/desktop.aspx">
                        <%--<asp:Image ID="Image8" runat="server" ImageUrl="~/images/LOGO.png" />--%>
                    </asp:HyperLink>
                    <span style="color: #ED1C16; font-size: 16px; font-family: 微软雅黑; font-weight: bold;">企业进销存管理系统</span>
                </div>
                <div class="top_right">
                    <div class="top_rightDiv2">
                        <div class="top_rightDiv3">
                            <asp:Panel ID="pn_IsLogin" runat="server">
                                <div class="top_bar_div0" style="width: 10px"></div>
                                <div class="top_bar_div0">
                                    <font class="fnt4">
                                        <asp:LoginName ID="LoginName1" runat="server" FormatString="欢迎您:({0})" />
                                        <asp:Label ID="lb_UserName" runat="server" Text=""></asp:Label></font>
                                </div>
                                <div class="top_bar_div0">
                                    |
                                </div>
                                <%--<div class="top_bar_div0">
                                    <font class="fnt4">
                                        <asp:HyperLink ID="HyperLink3" NavigateUrl="~/SubModule/OA/Mail/index.aspx" Target="_blank"
                                            runat="server" ForeColor="White">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/mailClose.gif" Width="18" />
                                        </asp:HyperLink>您有<b><font
                                            color="red"><asp:Label ID="lb_NewMailCount" runat="server"></asp:Label></font></b>封新邮件</font>
                                </div>
                                <div class="top_bar_div0">
                                    |
                                </div>--%>
                                <div class="top_bar_div0" style="padding-top: 0px">
                                    <iframe id="fr_onlineuser" runat="server" src="" scrolling="no" width="100" height="100%"
                                        frameborder="no" noresize="noresize"></iframe>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="top_rightDiv4">
                            <div class="top_bar_div">
                                <div class="top_bar_div2">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SubModule/desktop.aspx" Target="fr_Main">回到首页</asp:HyperLink>
                                </div>
                                <div class="top_bar_div3"></div>
                            </div>
                            <div class="top_bar_div">
                                <div class="top_bar_div2">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SubModule/Login/ChangePassword.aspx" Target="fr_Main">修改密码</asp:HyperLink>
                                </div>
                                <div class="top_bar_div3"></div>
                            </div>
                            <div class="top_bar_div">
                                <div class="top_bar_div2">
                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="" Target="fr_Main">建议与反馈</asp:HyperLink>
                                </div>
                                <div class="top_bar_div3"></div>
                            </div>
                            <div class="top_bar_div">
                                <div class="top_bar_div2">
                                    <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutText="注  销" LoginText="登  录"
                                        OnLoggedOut="LoginStatus1_LoggedOut" />
                                </div>
                                <div class="top_bar_div3"></div>
                            </div>

                        </div>
                    </div>
                </div>


            </div>

            <%--<div class="top_bar">
                <div style="float: right; text-align: center; width: 300px">
                    <asp:Label ID="lbl_now" CssClass="top_bar_text" runat="server"></asp:Label>
                </div>
            </div>--%>



            <div id="MainContent" style="width: 100%; clear: both; padding-top: 10px; margin-top: 5px">

                <div id="divLeft" style="width: 165px; float: left; height: 100%" class="mastercontent">
                    <iframe id="fr_leftmenu" runat="server" src="" scrolling="auto" width="160" height="100%"
                        frameborder="0" noresize="resize"></iframe>
                </div>

                <div id="divRight" style="margin-left: 5px; height: 100%; float: left; width: 600px" class="mastercontent">
                    <!--startprint-->
                    <iframe id="fr_Main" name="fr_Main" runat="server" src="" scrolling="auto" width="100%" height="100%"
                        frameborder="0" noresize="resize"></iframe>
                    <!--endprint-->
                </div>

            </div>

        </div>
        <table id="tr_Bottom" runat="server" border="0" cellpadding="0" cellspacing="0" class="underFooter" style="margin-top: 10px"
            width="100%">
            <tr>
                <td align="center" class="copyRight" style="height: 38px">11温医信管1班张诚<br />
                    <asp:Label ID="lbPageResponseTime" runat="server" Text="80" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>

        <uc1:OnlineService runat="server" ID="OnlineService" Visible="false" />
    </form>

    <script type="text/javascript" language="javascript">
        //模式窗口时，隐藏页面框架部分
        if (window.dialogArguments != null) {
            if (window.document.URL.indexOf("ViewFramework") < 0) {
                if (document.getElementById("ctl00_tr_Head") != null) {
                    document.getElementById("ctl00_tr_Head").style.display = "none";
                    document.getElementById("ctl00_tr_Menu").style.display = "none";
                    document.getElementById("ctl00_tr_Bottom").style.display = "none";
                }
            }
        }
        window.onresize = function () {
            document.getElementById("divRight").style.width = (document.body.clientWidth - document.getElementById("divLeft").clientWidth - 70).toString() + "px";
            document.getElementById("MainContent").style.height = (window.screen.height - 320).toString() + "px";
            document.getElementById("fr_leftmenu").style.height = document.getElementById("MainContent").style.height;
            document.getElementById("fr_Main").style.height = document.getElementById("MainContent").style.height;
        }

        window.onresize();
    </script>

</body>

</html>
