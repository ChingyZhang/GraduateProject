<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_AdjustWriteOffDetail.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_Pop_AdjustWriteOffDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整费用核销批复金额</title>
    <base target="_self">
    </base>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
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
                                            调整费用核销批复金额
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
                                        费用科目
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_AccountTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr visible="false" runat="server">
                                    <td class="tabDetailViewDL">
                                        可核销金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_ApplyCost" runat="server" Text=""></asp:Label>元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        申请核销金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_WriteOffCost" runat="server" Text=""></asp:Label>元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        批复核销金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_ApproveCost" runat="server" AutoPostBack="True" 
                                            ontextchanged="tbx_ApproveCost_TextChanged"></asp:TextBox>
                                        <span class="style1">元*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_ApproveCost" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApproveCost"
                                            Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tabDetailViewDL">
                                        扣减金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lbl_DeductCost" runat="server" Text=""></asp:Label>元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        扣减方式
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:RadioButtonList ID="rbl_AdjustMode" runat="server" DataTextField="Value" DataValueField="Key"
                                            RepeatColumns="4" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tabDetailViewDL">
                                        扣减原因
                                    </td>
                                    <td class="tabDetailViewDF">
                                          <asp:DropDownList ID="ddl_DeductReason" runat="server" DataTextField="Value" 
                                            DataValueField="Key"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        扣减备注
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_AdjustReason" runat="server" Width="300px"></asp:TextBox>
                                        <span class="style1">*</span><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填" 
                                            ControlToValidate="tbx_AdjustReason" Display="Dynamic"></asp:RequiredFieldValidator>
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
