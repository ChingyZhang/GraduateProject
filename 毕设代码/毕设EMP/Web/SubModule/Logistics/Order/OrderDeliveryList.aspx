<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderDeliveryList.aspx.cs" Inherits="SubModule_Logistics_ORD_OrderApplyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                定单发放记录
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新增发放单" OnClick="bt_Add_Click" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 28px">
                            <h3>
                                查询条件</h3>
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
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="180px" />
                        </td>
                        <td class="dataLabel">
                            结算月份
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            发放状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            发货单号
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_SheetCode" runat="server" Width="140px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 28px">
                            <h3>
                                发放记录列表</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    PanelCode="Panel_LGS_OrderDeliveryList_001" DataKeyNames="ORD_OrderDelivery_ID">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ORD_OrderDelivery_ID" DataNavigateUrlFormatString="OrderDeliveryDetail.aspx?ID={0}"
                            DataTextField="ORD_OrderDelivery_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                            HeaderText="发放单号" >
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:HyperLinkField>
                        <%--<asp:TemplateField HeaderText="总金额">
                            <ItemTemplate>
                                <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
