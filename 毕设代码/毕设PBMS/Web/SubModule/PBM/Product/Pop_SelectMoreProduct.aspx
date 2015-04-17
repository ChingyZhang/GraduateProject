<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_SelectMoreProduct.aspx.cs" Inherits="SubModule_PBM_Product_Pop_SelectMoreProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                                        <h2>商品目录管理</h2>
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
                                <table cellspacing="0" cellpadding="0" width="95%" border="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                                <tr>
                                                    <td width="24">
                                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                                    </td>
                                                    <td nowrap="noWrap" style="width: 120px">
                                                        <h2>尚未经营<asp:Label ID="lb_PageTitle" runat="server" Text="商品列表"></asp:Label></h2>
                                                    </td>
                                                    <td></td>
                                                    <td align="right">商品关键字:<asp:TextBox ID="tbx_SearchKey" runat="server"></asp:TextBox>
                                                        <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查 找" Width="60px" />
                                                    </td>
                                                    <td align="right">

                                                        <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged" Text="全选" />
                                                        <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="加入经营" Width="60px" ToolTip="将选中的商品加入经营目录" />
                                                        <asp:Button ID="bt_AddAll" runat="server" OnClick="bt_AddAll_Click" Text="全部经营" OnClientClick="return confirm('是否确认将该类别下所有商品加入经营目录?');" Width="60px" ToolTip="将该分类下所有的商品加入经营目录" />
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
                                                                    DataKeyNames="PDT_Product_ID" PageSize="15" Width="100%" PanelCode="Panel_PDT_List_001">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="cbx"></asp:CheckBox>
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
                                                                <asp:AsyncPostBackTrigger ControlID="cbx_CheckAll" EventName="CheckedChanged" />
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
        </div>
        <div style="width: 100%; text-align: center">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="False">
                <ProgressTemplate>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/WebWait.gif" />
                    请稍候...
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </form>
</body>
</html>
