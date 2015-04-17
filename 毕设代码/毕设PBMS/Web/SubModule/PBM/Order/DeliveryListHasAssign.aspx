<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="DeliveryListHasAssign.aspx.cs" Inherits="SubModule_PBM_Order_DeliveryListHasAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" /></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="已派预售销售单列表"></asp:Label></h2>
                        </td>

                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_SummaryPrint" runat="server" Text="打印汇总单" Width="80px" />
                            <asp:Button ID="bt_DetailPrint" runat="server" Text="打印明细单" Width="80px" />
                            <asp:Button ID="bt_BatConfirm" runat="server" Text="批量确认完成" Width="80px" OnClientClick="return confirm('是否确认将选中的送货单设为送货完成? 默认全部现金收款，该操作不可撤销!')" Enabled="False" OnClick="bt_BatConfirm_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="dataLabel">预计送货日期
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
                        <td class="dataLabel">送货人</td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_DeliveryMan" runat="server" DataTextField="RealName" DataValueField="ID"></asp:DropDownList>
                        </td>
                        <td class="dataLabel">业务员
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID"></asp:DropDownList></td>
                        <td class="dataLabel">出货仓库</td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_SupplierWareHouse" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked" Width="100%">
                    <Items>
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem1" Text="汇总" Value="0" />
                        <mcs:MCSTabItem runat="server" ID="MCSTabItem2" Text="明细" Value="1" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>

                            <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="PBM_Delivery_ID" PageSize="15" Width="100%"
                                PanelCode="Panel_PBM_Delivery_SaleOutList_01">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbx" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="PBM_Delivery_ID" DataNavigateUrlFormatString="../Delivery/SaleOut/SaleOutDetail.aspx?ID={0}"
                                        HeaderText="单号" DataTextField="PBM_Delivery_SheetCode" ControlStyle-CssClass="listViewTdLinkS1">
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>

                            <mcs:UC_GridView ID="gv_Summary" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15"
                                Width="100%" ShowFooter="False" OnPageIndexChanging="gv_Summary_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="送货人" DataField="DeliveryManName" Visible="false" />
                                    <asp:BoundField HeaderText="商品分类" DataField="CategoryName" Visible="false" />
                                    <asp:BoundField HeaderText="商品编码" DataField="ProduceCode" />
                                    <asp:BoundField HeaderText="商品名称" DataField="ProductName" />
                                    <asp:TemplateField HeaderText="数量" SortExpression="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Quantity" runat="server" Text='<%# ((int)Eval("Quantity_T")==0?"":(Eval("Quantity_T").ToString()+Eval("Packagint_T").ToString()))+
                                                    ((int)Eval("Quantity_P")==0?"":(Eval("Quantity_P").ToString()+Eval("Packagint_P").ToString())) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额" SortExpression="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Amount" runat="server" Text='<%# Bind("Amount", "{0:0.##元}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="重量" SortExpression="Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Weight" runat="server" Text='<%# Bind("Weight", "{0:0.#Kg}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

