<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgSender.aspx.cs" Inherits="SubModule_OA_SM_MsgSender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    </base>

    <script language="javascript" type="text/javascript">
        function dialwinprocess(type) {
            var newdialoguewin = window.showModalDialog("Selectreceiver.aspx", window, "dialogWidth:600px;DialogHeight=600px;status:no");
            if (newdialoguewin != null) {
                if (newdialoguewin.length > 0) {
                    ReceiverTypeArray = newdialoguewin.split("|");

                    try {
                        document.getElementById("hdnTxtSendTo").value = ReceiverTypeArray[1];
                        document.getElementById("txtSendTo").value = ReceiverTypeArray[0];

                    }
                    catch (e) { alert(e); }
                }
            }
        }

        window.moveTo(120, 120);
        window.resizeTo(600, 480);
       
    </script>

</head>
<body>
    <form id="form" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle">
            <tr>
                <td align="right" width="20">
                    &nbsp;
                </td>
                <td align="left" width="150">
                    <h2>
                        短讯管理--发送短信</h2>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table id="Table1" width="100%" runat="server">
            <tr>
                <td class="tabForm">
                    <table width="100%">
                        <tr>
                            <td align="right" width="80" class="dataLabel">
                                短讯接收人
                            </td>
                            <td class="dataField">
                                <input readonly type="text" size="50" value="<%=SendToRealName%>" id="txtSendTo"
                                    name="txtSendTo"><a onclick="dialwinprocess(1)" href="#"><img id="Img1" src="../../../DataImages/staff.gif"
                                        border="0" align="absMiddle" runat="server"><font color="red">选择人员</font></a>
                            </td>
                        </tr>
                        <tr height="30">
                            <td align="right" valign="top" class="dataLabel">
                                <asp:Label ID="lblContent" runat="server">发送内容</asp:Label><font face="宋体">:</font>
                            </td>
                            <td id="Td1" class="dataField" runat="server">
                                <CKEditor:CKEditorControl ID="ckedit_content" runat="server" onkeypress="Reply()"
                                    Width="400px" Height="160px" Toolbar="Basic" ToolbarBasic="Bold|Italic|-|NumberedList|BulletedList|-|Link|Unlink|-|FontSize
TextColor|BGColor"></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr height="30">
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSend" runat="server" Width="100px" Text="发 送" OnClick="btnSend_Click">
                                </asp:Button><font face="宋体">&nbsp; </font>
                                <asp:Button ID="btnReturn" runat="server" Width="100px" Text="返 回" OnClick="btnReturn_Click">
                                </asp:Button>
                                <input type="hidden" value="<%=SendTo%>" id="hdnTxtSendTo" name="hdnTxtSendTo">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
