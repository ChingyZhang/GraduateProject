<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgDetail.aspx.cs" Inherits="SubModule_OA_SM_SMDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>消息中心</title>

    <script language="JavaScript">
        function LoadEvent() {

            //		    w = window.open('SMDetail.aspx', 'send', 'width:500,height:800,toolbar=no,status=no,scrollbars=yes,resizable=yes');
            if (document.form1.btnNext != null) {//查看状态	
                CloseTimer();
                //document.form1.btnReply.focus();
            }
            else {//回复状态
                TimeShow.innerHTML = "";
                words.innerHTML = "";
                document.form1.txtContent.focus();
            }
        }

        TimeStart = 20;
        function CloseTimer() {
            if (TimeStart == 0)
                window.close();

            TimeShow.innerHTML = TimeStart;
            TimeStart--;

            var timer = setTimeout("CloseTimer()", 1000);
        }


        function Reply() {
            if (event.keyCode == 10) {
                document.form1.btnReply.click();
            }
        }
    </script>

</head>
<bgsound src='../../../images/chimes.wav' loop='1' />
<body onload="LoadEvent()" topmargin="0" leftmargin="0">
    <form method="post" runat="server" id="form1">
    <table cellspacing="1" cellpadding="1" width="100%" height="100%" border="0">
        <tr>
            <td>
                <table class="moduleTitle" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center" width="30">
                            <img height="16" src="../../../Images/page.gif" width="16">
                        </td>
                        <td>
                            <h1>
                                <asp:Label ID="lblInstruction" runat="server">---最新消息---</asp:Label></h1>
                        </td>
                        <td align="right">
                            <span id="TimeShow" style="font-weight: bold; color: #ff0000"></span><span id="words">
                                &nbsp;秒后关闭</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table id="tnb" cellspacing="0" cellpadding="3" rules="all" border="0">
                    <tr>
                        <td align="left" width="50px">
                            <asp:Label ID="lblSender" runat="server" Font-Size="X-Small">发送者</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRealName" runat="server" Width="77px"></asp:TextBox><asp:TextBox
                                ID="txtMsgID" runat="server" Visible="False" Width="7px"></asp:TextBox><asp:TextBox
                                    ID="txtSender" runat="server" Visible="False" Width="9px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="50px">
                            <asp:Label ID="Label1" runat="server" Font-Size="X-Small">发送时间</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_msgtime" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="middle" align="center">
                        <td align="left">
                            <asp:Label ID="lblContent" runat="server" Font-Size="X-Small">内  容</asp:Label>
                        </td>
                        <td align="left" height="100" valign="top" width="200">
                            <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr valign="middle" align="center">
                        <td align="left" colspan="2">
                            <asp:Button ID="btnReply" runat="server" Enabled="False" Text="回复" OnClick="btnReply_Click">
                            </asp:Button>&nbsp;
                            <asp:Button ID="btnNext" runat="server" Enabled="False" Text="下一条" OnClick="btnNext_Click">
                            </asp:Button>&nbsp;
                            <asp:Button ID="btnRead" runat="server" Text="我知道啦!" OnClick="btnRead_Click"></asp:Button>
                            <%--<td align="left">
                                <asp:Button ID="btnHistory" runat="server" Enabled="False" Text="历史记录">
                                </asp:Button></td>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
