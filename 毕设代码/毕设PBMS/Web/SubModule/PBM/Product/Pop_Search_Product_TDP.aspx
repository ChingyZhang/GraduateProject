<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_Search_Product_TDP.aspx.cs" Inherits="SubModule_PBM_Product_Pop_Search_Product_TDP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择商品</title>
    <base target="_self" />

    <script language="javascript" type="text/javascript">
        function f_ReturnValue(value) {
            window.returnValue = value;
            window.close();
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td valign="top" width="120px">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                                <tr>
                                                    <td align="right" width="20">
                                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                                    </td>
                                                    <td align="left">
                                                        <h2>目录</h2>
                                                    </td>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120px" valign="top">
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
                                                    <td nowrap="noWrap" style="width: 70px">
                                                        <h2>
                                                            <asp:Label ID="lb_PageTitle" runat="server" Text="商品列表"></asp:Label></h2>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td align="left">关键字:<asp:TextBox ID="tbx_SearchKey" runat="server" Width="50px"></asp:TextBox>
                                                        <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查 找" Width="60px" />
                                                        <input onclick="f_ReturnValue()" type="button" class="button" value="确定选择" style="width: 60px" />
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
                                                                    DataKeyNames="PDT_Product_ID,PDT_Product_FullName" PageSize="15" Width="100%"
                                                                    PanelCode="Panel_PDT_PopList_002" OnRowDataBound="gv_List_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button Text="选择" ID="bt_select" CssClass="listViewTdLinkS1" runat="server" />
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
        </div>
    </form>
</body>
</html>
