<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" StylesheetTheme="basic"
    AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="SubModule_Product_ProductList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="150">
                                    <h2>产品列表</h2>
                                </td>
                                <td align="right">品牌：<asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>

                                    &nbsp;关键字：<asp:TextBox ID="tbx_Search" Width="120" runat="server"></asp:TextBox>
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                    &nbsp;<asp:RadioButtonList ID="rbl_State" runat="server" DataTextField="Value" DataValueField="Key"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    </asp:RadioButtonList>
                                    &nbsp;<asp:Button ID="btn_Search" OnClick="btn_Search_Click" runat="server" Width="60"
                                        Text="查找" />
                                    <asp:Button ID="btn_Add" runat="server" Text="添加产品" OnClick="btn_Add_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_PDT_List_001" AllowPaging="True"
                            AutoGenerateColumns="False" Width="100%" DataKeyNames="PDT_Product_ID" Binded="False"
                            ConditionString="" PageSize="15">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="PDT_Product_ID" DataNavigateUrlFormatString="ProductDetail.aspx?ID={0}&IsOpponent=1"
                                    Text="查看" ControlStyle-CssClass="listViewTdLinkS1"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# Bind("PDT_Product_ID","ProductPictureList.aspx?ID={0}")%>'
                                            Text="图片管理" CssClass="listViewTdLinkS1" Target="_blank" ImageUrl='<%# MCSFramework.BLL.Pub.ATMT_AttachmentBLL.GetFirstPreviewPicture(11,(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ID"))%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
