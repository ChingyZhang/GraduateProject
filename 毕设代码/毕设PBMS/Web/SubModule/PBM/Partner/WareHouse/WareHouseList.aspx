<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="WareHouseList.aspx.cs" Inherits="SubModule_PBM_Partner_WareHouse_WareHouseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="客户仓库列表"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="新 增" Width="60px" />
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
                                                DataKeyNames="CM_WareHouse_ID" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                                PanelCode="Panel_TDP_WareHouseList_01" Binded="False" ConditionString="" 
                                                OrderFields="" TotalRecordCount="0">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="true" SelectText="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                                    </asp:CommandField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="CM_WareHouse_ID" 
                                                        DataNavigateUrlFormatString="~/SubModule/PBM/Inventory/InventoryList.aspx?WareHouseID={0}" 
                                                        Text="查看库存" ControlStyle-CssClass="listViewTdLinkS1" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
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
