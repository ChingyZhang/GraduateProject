<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="SubModule_PBM_Product_ProductList" %>

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
                                                <h2>
                                                    <asp:Label ID="Label1" runat="server" Text="商品目录"></asp:Label></h2>
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
                                            <td nowrap="noWrap" style="width: 80px">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="商品列表"></asp:Label></h2>
                                            </td>

                                            <td align="right">
                                                <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查 找" Width="60px" />
                                                <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="新增自营商品" Width="80px" />
                                                <asp:Button ID="bt_AddMoreProduct" runat="server" Text="移入更多商品" ToolTip="从厂商产品库中选择更多的商品加入到经营商品库中" Width="100px" OnClientClick="javascript:PopMore();" OnClick="bt_AddMoreProduct_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>查询条件：</td>
                                            <td align="right">商品关键字</td>
                                            <td>
                                                <asp:TextBox ID="tbx_SearchKey" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="right">经营状态</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_SalesState" runat="server" DataTextField="Value" DataValueField="Key" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="ddl_SalesState_SelectedIndexChanged"></asp:DropDownList>
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
                                                            DataKeyNames="PDT_Product_ID" PageSize="15" Width="100%" PanelCode="Panel_TDP_PDT_Product_List">
                                                            <Columns>
                                                                <asp:HyperLinkField DataNavigateUrlFields="PDT_Product_ID" DataNavigateUrlFormatString="ProductDetail.aspx?ID={0}"
                                                                    Text="查看商品" ControlStyle-CssClass="listViewTdLinkS1">
                                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                                </asp:HyperLinkField>
                                                                <asp:TemplateField HeaderText="默认销售价(件)" SortExpression="Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_Price" runat="server" Text='<%# ((decimal)Eval("PDT_ProductExtInfo_SalesPrice")*(int)Eval("PDT_Product_ConvertFactor")).ToString("0.##") %>'></asp:Label>
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

