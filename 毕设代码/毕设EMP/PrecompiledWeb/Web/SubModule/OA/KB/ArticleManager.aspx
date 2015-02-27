<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_KB_index, App_Web_43gieen2" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left" style="width: 157px">
                            <h2>
                                知识库目录</h2>
                        </td>
                        <td style="width: 25px">
                            <img height="16" src="../../../DataImages/help_book.gif" width="15">
                        </td>
                        <td align="left">
                            <h1>
                                知识库文章</h1>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="cb_OnlyNoApprove" runat="server" AutoPostBack="True" 
                                oncheckedchanged="cb_OnlyNoApprove_CheckedChanged" Text="仅显示未审核的文章" />
                            <asp:CheckBox ID="cb_DisplayDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="cb_DisplayDeleted_CheckedChanged"
                                Text="仅显示已删除的文章" />
                            &nbsp;<asp:Button ID="btn_Add" runat="server" Text="发布新文章" Width="80px" OnClick="btn_Add_Click" />
                            <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="80px" OnClick="btn_Delete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="180px" height="400px" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:TreeView ID="tr_Catalog" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                                ExpandDepth="1" OnSelectedNodeChanged="tr_Catalog_SelectedNodeChanged">
                                                <NodeStyle CssClass="listViewTdLinkS1" />
                                                <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_KB_ArticleManager"
                                    PageSize="15" AutoGenerateColumns="False" Width="100%" DataKeyNames="KB_Article_ID"
                                    AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_ID" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField Text="预览" DataNavigateUrlFields="KB_Article_ID" DataNavigateUrlFormatString="ArticleDetail.aspx?ID={0}"
                                            ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                            <ItemStyle Width="30px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField Text="编辑" DataNavigateUrlFields="KB_Article_ID" DataNavigateUrlFormatString="NewArticle.aspx?KB_Article_ID={0}"
                                            ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                            <ItemStyle Width="30px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </mcs:UC_GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
