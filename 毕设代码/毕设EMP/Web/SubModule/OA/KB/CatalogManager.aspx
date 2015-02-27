<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="CatalogManager.aspx.cs" Inherits="SubModule_OA_KB_CatalogManage" %>

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
                                目录维护</h1>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
                            <table cellspacing="0" cellpadding="0" width="95%">
                                <tr>
                                    <td>
                                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                            SelectedIndex="1" Width="100%">
                                            <Items>
                                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="修改目录" Description=""
                                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="添加目录" Description=""
                                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                            </Items>
                                        </cc1:MCSTabControl>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="Page_KB_CatalogManager"
                                            Visible="true">
                                        </mcs:UC_DetailView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btn_ok" runat="server" Text="确定" OnClick="btn_ok_Click" Height="21px"
                                            Width="62px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
