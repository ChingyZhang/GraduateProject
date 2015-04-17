<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="InventoryAdjustDetail.aspx.cs" Inherits="SubModule_PBM_Inventory_InventoryAdjustDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="盘点单详细信息"></asp:Label></h2>
                                </td>
                                <td align="right">&nbsp;
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Height="23px" OnClick="bt_Delete_Click" OnClientClick="return confirm('是否确认删除该单据?');" Text="删除" Width="60px" />
                                    <asp:Button ID="bt_Confirm" runat="server" OnClick="bt_Confirm_Click" OnClientClick="return confirm('是否确认审核该单据?');" Text="审核" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PBM_Delivery_AdjustDetail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                            <tr>
                                                <td>
                                                    <h3>盘点商品清单</h3>
                                                </td>
                                                <td width="60" align="right">商品分类:</td>
                                                <td>
                                                    <mcs:MCSTreeControl ID="tr_Category" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" RootValue="0" AutoPostBack="True" OnSelected="tr_Category_Selected" />
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" ShowFooter="true" AllowPaging="false"
                                            DataKeyNames="ID,Product" Width="100%" OnRowDataBound="gv_List_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="分类" SortExpression="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_ProductCategory" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Product" HeaderText="商品" SortExpression="Product" />
                                                <asp:BoundField DataField="LotNumber" HeaderText="批号" SortExpression="LotNumber" />
                                                <asp:BoundField DataField="ProductDate" HeaderText="生产日期" SortExpression="ProductDate" DataFormatString="{0:yyyy-MM-dd}" />
                                                <asp:TemplateField HeaderText="单价" SortExpression="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Price" runat="server" Text='<%# Bind("Price", "{0:0.###}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="盈亏数量" SortExpression="DeliveryQuantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Quantity" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddl_Mode" runat="server">
                                                            <asp:ListItem Text="盘盈" Value="I" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="盘亏" Value="D"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="tbx_Quantity_T" runat="server" Text="" Width="30px"></asp:TextBox>
                                                        <asp:Label ID="lb_P_T" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_Quantity_T" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="必须为数字" ControlToValidate="tbx_Quantity_T" Operator="DataTypeCheck" Type="Integer" Display="Dynamic"></asp:CompareValidator>
                                                        <asp:TextBox ID="tbx_Quantity_P" runat="server" Text="" Width="30px"></asp:TextBox>
                                                        <asp:Label ID="lb_P_P" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填" ControlToValidate="tbx_Quantity_P" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="必须为数字" ControlToValidate="tbx_Quantity_P" Operator="DataTypeCheck" Type="Integer" Display="Dynamic"></asp:CompareValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="盈亏金额" SortExpression="DeliveryQuantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Fee" runat="server" Text='<%# (((int)Eval("DeliveryQuantity") * Math.Round((decimal)Eval("Price") * (int)Eval("ConvertFactor"), 2)/(int)Eval("ConvertFactor")).ToString("0.##")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="盘点前库存">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_InventoryQuantity" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                无数据
                                            </EmptyDataTemplate>
                                        </mcs:UC_GridView>

                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_OK" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

