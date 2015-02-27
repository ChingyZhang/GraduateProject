<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_FeeWriteOffListByFeeApply.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_Pop_FeeWriteOffListByFeeApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>根据费用申请单查看相关核销单列表</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                        <tr>
                            <td width="24">
                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                            </td>
                            <td nowrap="noWrap">
                                <h2>
                                    根据费用申请单查看相关核销单列表
                                </h2>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowSorting="true" DataKeyNames="FNA_FeeWriteOff_ID" PanelCode="Panel_FNA_FeeWriteOffList_ByFeeApply"
                        OnDataBound="gv_List_DataBound">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeWriteOff_ID" DataNavigateUrlFormatString="FeeWriteoffDetail.aspx?ID={0}"
                                 Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="批复核销额" SortExpression=" FNA_FeeWriteOffDetail_WriteOffCost + FNA_FeeWriteOffDetail_AdjustCost">
                                <ItemTemplate>
                                    <asp:Label ID="lb_ApproveCost" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"FNA_FeeWriteOffDetail_WriteOffCost") + (decimal)DataBinder.Eval(Container.DataItem,"FNA_FeeWriteOffDetail_AdjustCost")).ToString("0.###")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            无数据
                        </EmptyDataTemplate>
                    </mcs:UC_GridView>
                </td>
            </tr>
            <tr>
                <td align="Center" height="28">
                    合计批复总额：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>元
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
