<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="InventoryList.aspx.cs" Inherits="SubModule_PBM_Inventory_InventoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top" width="180px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td align="right" width="20">
                                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td align="left" width="150">
                                                <h2>商品目录</h2>
                                            </td>
                                            <td align="right">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px" valign="top">
                                    <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn" ExpandDepth="1"
                                        OnSelectedNodeChanged="tr_List_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td width="24">
                                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap" style="width: 90px">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="商品库存列表"></asp:Label></h2>
                                            </td>
                                            <td class="dataField" align="left">仓库：
                                                <asp:DropDownList ID="ddl_WareHouse" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddl_WareHouse_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td>商品关键字:<asp:TextBox ID="tbx_SearchKey" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查 找" Width="60px" />
                                                <asp:Button ID="bt_Adjust" runat="server" OnClick="bt_Adjust_Click" Text="调 整" Width="60px" />
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
                                                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowFooter="true"
                                                            DataKeyNames="ID,Product" PageSize="15" Width="100%" OnRowDataBound="gv_List_RowDataBound" OnPageIndexChanging="gv_List_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="所在仓库" DataField="WareHouse" SortExpression="WareHouse" />
                                                                <asp:BoundField HeaderText="商品名称" DataField="Product" SortExpression="Product" />
                                                                <asp:BoundField HeaderText="批号" DataField="LotNumber" SortExpression="LotNumber" />
                                                                <asp:TemplateField HeaderText="整件价" SortExpression="Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_Price_T" runat="server" Text='<%# Bind("Price", "{0:0.###}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="单价" SortExpression="Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_Price_P" runat="server" Text='<%# Bind("Price", "{0:0.###}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="数量" SortExpression="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_Quantity" runat="server" Text='<%# Bind("Quantity", "{0:0.#}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbx" runat="server" />
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

