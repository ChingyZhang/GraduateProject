<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_AdjustOfferBalanceFee.aspx.cs"
    Inherits="SubModule_CSO_Pop_AdjustOfferBalanceFee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整营养教育新客费</title>
    <base target="_blank" />
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
                                        <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            调整营养教育新客费
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="确认调整" Width="80px" />&nbsp;
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
                                        VIP名称
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_DoctorName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        调整前名单费用
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_ActualFee" runat="server" Text=""></asp:Label>元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        扣减金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_AdjustFee" runat="server"></asp:TextBox>
                                        <span class="style1">元*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_AdjustFee" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_AdjustFee"
                                            Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        扣减原因
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_AdjustReason" runat="server" Width="300px"></asp:TextBox>
                                        <span class="style1">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_AdjustReason"
                                            Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
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
