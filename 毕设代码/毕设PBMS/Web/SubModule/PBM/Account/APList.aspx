<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="APList.aspx.cs" Inherits="SubModule_PBM_Account_APList" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="往来客户应付记录明细"></asp:Label></h2>
                        </td>
                        <td align="right">往来客户</td>
                        <td class="dataField" align="left">
                             <asp:DropDownList ID="ddl_Supplier" runat="server" DataTextField="FullName" DataValueField="ID" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Supplier_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td align="right">日期:
						    <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>&nbsp;
						    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Balance" runat="server" Text="结应付款" OnClick="bt_Balance_Click" Width="60px" />
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
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
                                        PanelCode="Panel_PBM_AC_ARList_01" DataKeyNames="AC_ARAPList_ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx" runat="server" Visible='<%# Eval("AC_ARAPList_BalanceFlag").ToString()=="未结款" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="发货单号">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_Delivery" Text='<%#Eval("PBM_Delivery_SheetCode") %>' CssClass="listViewTdLinkS1" Visible='<%# Eval("AC_ARAPList_RelateDeliveryId").ToString()!="" %>'
                                                        NavigateUrl='<%# Eval("AC_ARAPList_RelateDeliveryId","~/SubModule/PBM/Delivery/Purchase/PurchaseDetail.aspx?ID={0}") %>'></asp:HyperLink>
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

