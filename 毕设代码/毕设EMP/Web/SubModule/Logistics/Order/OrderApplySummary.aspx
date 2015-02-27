<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderApplySummary.aspx.cs" Inherits="SubModule_Logistics_Order_OrderApplySummary" %>

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
                                成品订单汇总信息
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                查询条件</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    查看层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    申请月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    产品类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ProductType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    品牌
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    品系
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    品项
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Product" PageUrl="~/SubModule/Product/Pop_Search_Product.aspx?IsOpponent=1"
                                        Width="250px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="查看汇总" Value="1" />
                                                <mcs:MCSTabItem Text="查看明细" Value="2" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" DataKeyNames="OrganizeCity"
                                            CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" BorderWidth="0px" AllowPaging="True"
                                            PageSize="50" AutoGenerateColumns="False" Binded="False" 
                                            ConditionString="" OrderFields=""
                                            PanelCode="" TotalRecordCount="0" 
                                            onpageindexchanging="gv_List_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="OrganizeCityName" HeaderText="管理片区" SortExpression="OrganizeCityName" />
                                                <asp:BoundField DataField="Level" HeaderText="层级" SortExpression="Level" />
                                                <asp:BoundField DataField="BrandName" HeaderText="品牌" SortExpression="BrandName" />
                                                <asp:BoundField DataField="ClassifyName" HeaderText="品系" SortExpression="ClassifyName" />
                                                <asp:BoundField DataField="ProductName" HeaderText="品项" SortExpression="ProductName" />
                                                <asp:BoundField DataField="Weight" HeaderText="重量(吨)" SortExpression="Weight" />
                                                <asp:BoundField DataField="Price" HeaderText="金额(元)" SortExpression="Price" />
                                                <asp:TemplateField HeaderText="数量">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tbx_Quantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                                        <asp:Label ID="lb_Packaging_T" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"ProductID")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_LGS_OrderApplyList_001" DataKeyNames="ORD_OrderApply_ID" AllowPaging="True"
                                            PageSize="25">
                                            <Columns>
                                                <asp:TemplateField HeaderText="请购单号">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#  Eval("ORD_OrderApply_ID","OrderProductApplyDetail.aspx?ID={0}") %>'
                                                             Text='<%# Eval("ORD_OrderApply_SheetCode") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="总金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ORD_OrderApply_TaskID", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("ORD_OrderApply_TaskID").ToString()!="" %>' ></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
