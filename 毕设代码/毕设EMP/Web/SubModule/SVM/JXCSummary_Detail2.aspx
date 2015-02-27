<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JXCSummary_Detail2.aspx.cs" Inherits="SubModule_SVM_JXCSummary_Detail2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="260" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="客户进销存明细统计"></asp:Label>
                            </h2>
                        </td>
                        <td align="right" width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTab_DisplayMode" runat="server" Width="100%" SelectedIndex="1"
                    OnOnTabClicked="MCSTab_DisplayMode_OnTabClicked">
                    <Items>
                        <mcs:MCSTabItem Text="当前客户进销存" Value="0" />
                        <mcs:MCSTabItem Text="下游客户进销存" Value="1" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    供货商
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Supplier" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?NoParent=Y"
                                        Width="250px" OnSelectChange="select_Supplier_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_DisplayType" runat="server" RepeatColumns="2" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_DisplayType_SelectedIndexChanged">
                                        <asp:ListItem Text="按客户显示" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="按产品汇总" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsOpponent" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_IsOpponent_SelectedIndexChanged">
                                        <asp:ListItem Text="成品" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="赠品" Value="9"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" class="dataField">
                                    <asp:Button ID="bt_Search" runat="server" OnClick="bt_Search_Click" Text="查 看" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        进销存明细列表</h3>
                                </td>
                                <td align="right">
                                    数量单位：听\袋\盒，金额单位：元RMB
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                        <Items>
                                            <mcs:MCSTabItem ID="MCSTabItem0" Text="按数量统计" runat="server" Value="0" />
                                            <mcs:MCSTabItem ID="MCSTabItem1" Text="按出厂价金额统计" runat="server" Value="1" />
                                            <mcs:MCSTabItem ID="MCSTabItem2" Text="按批发价金额统计" runat="server" Value="2" />
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_ListByClient" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Client" Width="100%" OnPageIndexChanging="gv_ListByClient_PageIndexChanging">
                                        <Columns>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="Client,AccountMonth" DataNavigateUrlFormatString="JXCSummary_Detail.aspx?ClientID={0}&AccountMonth={1}"
                                                ItemStyle-Width="80px" >
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="OrganizeCityName" HeaderText="管理区域" SortExpression="OrganizeCityName" />
                                            <asp:BoundField DataField="AccountMonthName" HeaderText="会计月份" SortExpression="AccountMonthName" />
                                            <asp:BoundField DataField="ClientName" HeaderText="客户全称" SortExpression="ClientName" />
                                            <asp:BoundField DataField="ClientTypeName" HeaderText="客户类型" SortExpression="ClientTypeName"
                                                Visible="false" />
                                            <asp:BoundField DataField="BeginningInventory" HeaderText="期初库存" SortExpression="BeginningInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="PurchaseVolume" HeaderText="本期进货" SortExpression="PurchaseVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransitInventory" HeaderText="在途库存" SortExpression="TransitInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="RecallVolume" HeaderText="下游退货" SortExpression="RecallVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="SalesVolume" HeaderText="本期销售" SortExpression="SalesVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="GiftVolume" HeaderText="买赠" SortExpression="GiftVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ReturnedVolume" HeaderText="退货" SortExpression="ReturnedVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferInVolume" HeaderText="调入" SortExpression="TransferInVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferOutVolume" HeaderText="调出" SortExpression="TransferOutVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="EndingInventory" HeaderText="期末盘存" SortExpression="EndingInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="StaleInventory" HeaderText="盘存盈亏" SortExpression="StaleInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ComputInventory" HeaderText="实物库存" SortExpression="ComputInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="审核标志" SortExpression="ApproveFlag" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_ListByProduct" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="Product" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="PDTBrandName" HeaderText="品牌" SortExpression="PDTBrandName"
                                                Visible="false" />
                                            <asp:BoundField DataField="PDTClassifyName" HeaderText="系列" SortExpression="PDTClassifyName"
                                                Visible="false" />
                                            <asp:BoundField DataField="ProductName" HeaderText="产品简称" SortExpression="ProductName" />
                                            <asp:BoundField DataField="BeginningInventory" HeaderText="期初库存" SortExpression="BeginningInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="PurchaseVolume" HeaderText="本期进货" SortExpression="PurchaseVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransitInventory" HeaderText="在途库存" SortExpression="TransitInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="RecallVolume" HeaderText="下游退货" SortExpression="RecallVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="SalesVolume" HeaderText="本期销售" SortExpression="SalesVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="StaleInventory" HeaderText="盘存盈亏" SortExpression="StaleInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="GiftVolume" HeaderText="买赠" SortExpression="GiftVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ReturnedVolume" HeaderText="退货" SortExpression="ReturnedVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferInVolume" HeaderText="调入" SortExpression="TransferInVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferOutVolume" HeaderText="调出" SortExpression="TransferOutVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="EndingInventory" HeaderText="期末盘存" SortExpression="EndingInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ComputInventory" HeaderText="实物库存" SortExpression="ComputInventory"
                                                DataFormatString="{0:0.##}" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_DisplayType" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_IsOpponent" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
