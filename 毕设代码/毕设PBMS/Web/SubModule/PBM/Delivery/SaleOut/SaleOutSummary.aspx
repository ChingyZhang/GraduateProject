<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="SaleOutSummary.aspx.cs" Inherits="SubModule_PBM_Delivery_SaleOut_SaleOutSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" /></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="销售单汇总"></asp:Label></h2>
                        </td>

                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="dataLabel">销售日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                        runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_begin"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    至
                                    <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator2"
                                        runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                                <td class="dataLabel">业务员</td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_SalesMan" runat="server" DataTextField="RealName" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">送货人</td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DeliveryMan" runat="server" DataTextField="RealName" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked" Width="100%">
                    <Items>
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem1" Text="按产品汇总" Value="0" />
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem2" Text="按客户汇总" Value="1" />
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem3" Text="销售收款汇总" Value="2" />
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem4" Text="销售收款明细" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List_Client" runat="server" DataKeyNames="Client" AutoGenerateColumns="False" Width="100%"
                                        AllowPaging="true" PageSize="15" OnSelectedIndexChanging="gv_List_Client_SelectedIndexChanging" OnPageIndexChanging="gv_List_Client_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="DeliveryManName" HeaderText="送货人" SortExpression="DeliveryManName" />
                                            <asp:BoundField DataField="VehicleNo" HeaderText="送货车辆" SortExpression="VehicleNo" />
                                            <asp:BoundField DataField="ClientName" HeaderText="终端店" SortExpression="ClientName" />
                                            <asp:BoundField DataField="TotalWipeAmount" HeaderText="优惠额" SortExpression="TotalAmount" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="实收销售额" SortExpression="TotalAmount" DataFormatString="{0:0.##}" />
                                            <asp:CommandField ButtonType="Link" SelectText="查看销售单" ControlStyle-CssClass="listViewTdLinkS1" ShowSelectButton="true" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_List_Product" runat="server" DataKeyNames="Product" AutoGenerateColumns="False" Width="100%"
                                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_List_Product_PageIndexChanging">
                                        <Columns>
                                            <%--<asp:BoundField DataField="DeliveryManName" HeaderText="送货人" SortExpression="DeliveryManName" />
                                            <asp:BoundField DataField="VehicleNo" HeaderText="送货车辆" SortExpression="VehicleNo" />--%>
                                            <asp:BoundField DataField="ProductName" HeaderText="商品" SortExpression="ProductName" />
                                            <asp:BoundField DataField="Quantity_T" HeaderText="整件" SortExpression="Quantity_T" />
                                            <asp:BoundField DataField="Quantity_P" HeaderText="零散" SortExpression="Quantity_P" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="销售额" SortExpression="TotalAmount" DataFormatString="{0:0.##}" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_PayInfoSummary" runat="server" DataKeyNames="" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gv_PayInfoSummary_PageIndexChanging" AllowPaging="True" PageSize="15">
                                        <Columns>
                                            <asp:BoundField DataField="DeliveryManName" HeaderText="送货人" SortExpression="DeliveryManName" />
                                            <asp:BoundField DataField="PayModeName" HeaderText="支付方式" SortExpression="PayModeName" />
                                            <asp:BoundField DataField="Amount" HeaderText="销售额" SortExpression="Amount" DataFormatString="{0:0.##}" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_PayInfoDetail" runat="server" DataKeyNames="" AutoGenerateColumns="true" Width="100%" OnDataBound="gv_PayInfoDetail_DataBound" OnPageIndexChanging="gv_PayInfoDetail_PageIndexChanging" AllowPaging="True" PageSize="15">
                                        <Columns>
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

