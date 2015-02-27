<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" StylesheetTheme="basic"
    AutoEventWireup="true" CodeFile="PDT_ProductPrice.aspx.cs" Inherits="SubModule_Product_PDT_ProductPrice" %>

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
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="客户产品价格表"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="cbx_Valid" runat="server" Text="仅查看有效期内价表" />
                                    <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="查找" Width="60" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel" width="100px">
                                    客户
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <mcs:MCSSelectControl runat="server" ID="select_Client" Width="300px" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                                OnSelectChange="select_Client_SelectChange" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="dataLabel" width="100px">
                                    标准价盘
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_StandardPrice" runat="server" DataTextField="FullName"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" Text="新增价表" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                            <tr>
                                <td height="28px">
                                    <h3>
                                        客户产品价表列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PanelCode="Panel_PDT_ProductPrice_1" DataKeyNames="PDT_ProductPrice_ID" PageSize="150"
                                    Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging">
                                    <Columns>
                                        <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="PDT_ProductPrice_ID" DataNavigateUrlFormatString="PDT_ProductPriceDetail2.aspx?PriceID={0}"
                                            ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px"></asp:HyperLinkField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        无数据
                                    </EmptyDataTemplate>
                                </mcs:UC_GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
