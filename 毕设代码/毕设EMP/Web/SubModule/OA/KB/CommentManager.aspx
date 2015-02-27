<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="CommentManager.aspx.cs" Inherits="SubModule_OA_KB_index" %>

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
                                知识库评论维护</h1>
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
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td><h1>文章列表</h1></td>
                                    </tr>
                                    <tr class="tabForm">
                                        <td>
                                            <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_KB_ArticleManager" AutoGenerateColumns="False"
                                                Width="100%" DataKeyNames="KB_Article_ID">
                                                <Columns>
                                                
                                                <asp:HyperLinkField Text="查看评论" DataNavigateUrlFields="KB_Article_ID" DataNavigateUrlFormatString="CommentManager.aspx?KB_Article_ID={0}"
                                                ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                                </Columns>
                                            </mcs:UC_GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><h1>评论列表</h1></td>
                                    </tr>
                                    <tr class="tabForm">
                                        <td>
                                            <mcs:UC_GridView ID="ud_grid1" runat="server" PanelCode="Panel_KB_CommentList" AutoGenerateColumns="False"
                                                Width="100%" DataKeyNames="KB_Comment_ID">
                                                <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField Text="查看" DataNavigateUrlFields="KB_Comment_ID" DataNavigateUrlFormatString="NewArticle.aspx?KB_Comment_ID={0}"
                                                ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                                </Columns>
                                            </mcs:UC_GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                           
                            <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60px" OnClick="btn_Delete_Click" />
                                        </td>
                                    </tr>
                                </table>
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
