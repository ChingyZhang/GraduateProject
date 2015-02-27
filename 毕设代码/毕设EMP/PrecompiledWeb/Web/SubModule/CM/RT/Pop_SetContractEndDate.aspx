<%@ page language="C#" autoeventwireup="true" inherits="SubModule_CM_RT_Pop_SetContractEndDate, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="500px">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            设置截止时间
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_SetEndDate" runat="server" Text="确认" Width="80px" OnClick="bt_SetEndDate_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabDetailView">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="tabDetailViewDL">
                                        协议提前截止日期设置
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="txt_EndDate" runat="server" onfocus="setday(this)"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                            Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="txt_EndDate"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lb_Remark" runat="server" Text="说明：若需手动提前终止协议，需按实际费用发生月份设置以上截止日期，“截止日期”设定不能大于原截止日期" ForeColor="Red"></asp:Label>
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
