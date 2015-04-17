<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceUsageList.aspx.cs" Inherits="SubModule_PBM_Account_BalanceUsageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="预付余额账户变动明细列表"></asp:Label></h2>
                        </td>

                        <td align="right">日期:
						<asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>&nbsp;
						    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                        PanelCode="Panel_PBM_AC_BalanceUsageList_01" Binded="False" ConditionString="" EnableModelValidation="True" OrderFields="" TotalRecordCount="0">
                                        <Columns>
                                            <asp:TemplateField HeaderText="发货单号">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_Delivery" Text='<%#Eval("PBM_Delivery_SheetCode") %>' CssClass="listViewTdLinkS1" Visible='<%# Eval("AC_BalanceUsageList_DeliveryId").ToString()!="" %>'
                                                        NavigateUrl='<%# Eval("AC_BalanceUsageList_DeliveryId","~/SubModule/PBM/Delivery/SaleOut/SaleOutDetail.aspx?ID={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("AC_BalanceUsageList_CashFlowId", "CashFlowDetail.aspx?ID={0}") %>' Visible='<%# Eval("AC_BalanceUsageList_CashFlowId").ToString()!="" %>'
                                                        Text="查看收款单"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
