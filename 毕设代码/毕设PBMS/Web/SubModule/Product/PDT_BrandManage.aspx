<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" StylesheetTheme="basic"
    AutoEventWireup="true" CodeFile="PDT_BrandManage.aspx.cs" Inherits="SubModule_Product_PDT_BrandManage" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        品牌维护
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="添加" OnClick="bt_OK_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="Page_PDT_005">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                            <tr height=28px>
                                <td>
                                    <h3>
                                        品牌列表</h3>
                                </td>
                                <td>
                                    品牌名称：<asp:TextBox ID="tbx_Search" Width="120" runat="server"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btn_Search" OnClick="btn_Search_Click" runat="server" Width="60"
                                        Text="查找" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_SelectAll" runat="server" Text="全 选" OnClick="btn_SelectAll_Click" />
                                    <asp:Button ID="btn_SelectBack" runat="server" Text="反 选" OnClick="btn_SelectBack_Click" />
                                    <asp:Button ID="btn_Delete" runat="server" Text="删除产品" OnClick="btn_Delete_Click"
                                        OnClientClick="javascript:return confirm('确定删除选中的产品？');" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_PDT_List_003" PageSize="10"
                            PageIndex="0" AutoGenerateColumns="false" Width="100%" OnSelectedIndexChanging="ud_grid_SelectedIndexChanging"
                            OnRowEditing="ud_grid_RowEditing" DataKeyNames="PDT_Brand_ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:CommandField ShowEditButton="true" EditText="包含的分类" ControlStyle-CssClass="listViewTdLinkS1"  Visible="false"/>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
