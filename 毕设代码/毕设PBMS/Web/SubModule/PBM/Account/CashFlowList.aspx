<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="CashFlowList.aspx.cs" Inherits="SubModule_PBM_Account_CashFlowList" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="现金收款列表"></asp:Label></h2>
                        </td>

                        <td align="right">往来客户</td>
                        <td class="dataField" align="left">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <mcs:MCSSelectControl runat="server" ID="select_TradeClient" PageUrl="~/SubModule/PBM/Retailer/Pop_Search_SelectClient.aspx"
                                        Width="200px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="dataLabel">业务员
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_AgentStaff" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                        <td align="right">收款日期:
						            <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>
                            至
                            <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>&nbsp;
						    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_ReceiptPreReceived" runat="server" Text="收预收款" Width="60px" OnClick="bt_ReceiptPreReceived_Click" />
                            <asp:Button ID="bt_PayPrePayment" runat="server" Text="付预付款" Width="60px" OnClick="bt_PayPrePayment_Click" />
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
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="AC_CashFlowList_ID" PageSize="15" Width="100%"
                                        PanelCode="Panel_PBM_AC_CashFlowList_01">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx" runat="server" Visible='<%# Eval("AC_CashFlowList_ConfirmState").ToString()=="未结款" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="发货单号">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_Delivery" Text='<%#Eval("PBM_Delivery_SheetCode") %>' CssClass="listViewTdLinkS1" Visible='<%# Eval("AC_CashFlowList_RelateDeliveryId").ToString()!="" %>'
                                                        NavigateUrl='<%# Eval("AC_CashFlowList_RelateDeliveryId","~/SubModule/PBM/Delivery/SaleOut/SaleOutDetail.aspx?ID={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="订货单号">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_Order" Text='<%#Eval("PBM_Order_SheetCode") %>' CssClass="listViewTdLinkS1" Visible='<%# Eval("AC_CashFlowList_RelateOrderId").ToString()!="" %>'
                                                        NavigateUrl='<%# Eval("AC_CashFlowList_RelateOrderId","~/SubModule/PBM/Order/OrderDetail.aspx?ID={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
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

