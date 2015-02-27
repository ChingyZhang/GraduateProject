<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="SubModule_OA_KB_index" %>

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
                            &nbsp;<asp:Button ID="btn_Add" runat="server" Text="发布新知识" Width="80px" OnClick="btn_Add_Click"
                                UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45px">
                                                            <img src="../../../Images/gif/gif-0282.gif" />
                                                        </td>
                                                        <td width="230px">
                                                            查找内容:<asp:TextBox ID="txt_keyword" runat="server" Width="152px"></asp:TextBox>
                                                        </td>
                                                        <td width="220">
                                                            <asp:CheckBox ID="chb_title" Text="标题" runat="server" Checked="True" />
                                                            <asp:CheckBox ID="chb_keyword" Text="关键字" runat="server" />
                                                            <asp:CheckBox ID="chb_content" Text="内容" runat="server" />
                                                            <asp:CheckBox ID="chb_author" Text="作者" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="bt_Search" runat="server" ImageUrl="~/Images/gif/gif-0343.gif"
                                                                OnClick="bt_Search_Click" />
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_KB_ArticleList" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="KB_Article_ID" Binded="False" ConditionString="" TotalRecordCount="0"
                                                    AllowSorting="true" AllowPaging="True" PageSize="15">
                                                    <Columns>
                                                        <asp:HyperLinkField DataNavigateUrlFields="KB_Article_ID" DataNavigateUrlFormatString="ArticleDetail.aspx?ID={0}"
                                                            HeaderText="标题" DataTextField="KB_Article_Title" ControlStyle-CssClass="listViewTdLinkS1"
                                                            >
                                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                                        </asp:HyperLinkField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
