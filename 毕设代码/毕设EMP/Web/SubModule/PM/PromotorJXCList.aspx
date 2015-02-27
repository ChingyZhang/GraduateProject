<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PromotorJXCList.aspx.cs" Inherits="SubModule_SVM_PromotorJXCList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="180" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="导购员相关门店进销存统计"></asp:Label>
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
            <td class="tabForm">
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
                                    导购员
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Promotor" PageUrl="Search_SelectPromotor.aspx"
                                        Width="250px" OnSelectChange="select_Promotor_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" DataValueField="ID" DataTextField="Name" 
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
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
                                        进销存列表</h3>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Client" PageSize="15"
                                        Width="100%" Binded="False" ConditionString="" 
                                        onpageindexchanging="gv_List_PageIndexChanging" PanelCode="" 
                                        TotalRecordCount="0">
                                        <Columns>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="Client,Month" DataNavigateUrlFormatString="../SVM/JXCDetail.aspx?ClientID={0}&Month={1}"
                                                ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                            <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                           <asp:BoundField DataField="MonthName" HeaderText="会计月份" 
                                                SortExpression="MonthName" />
                                            <asp:BoundField DataField="OrganizeCityName" HeaderText="办事处" 
                                                SortExpression="OrganizeCityName" />
                                            <asp:BoundField DataField="ClientCode" HeaderText="客户编号" 
                                                SortExpression="ClientCode" />
                                            <asp:BoundField DataField="FullName" HeaderText="客户全称" 
                                                SortExpression="FullName" />
                                            <asp:BoundField DataField="IniInventory" HeaderText="期初库存" 
                                                SortExpression="IniInventory" />
                                            <asp:BoundField DataField="SellIn" HeaderText="本期进货" SortExpression="SellIn" />
                                            <asp:BoundField DataField="EndInventory" HeaderText="期末库存" 
                                                SortExpression="EndInventory" />
                                            <asp:BoundField DataField="SellOut_Compute" HeaderText="本期销售(计算值)" 
                                                SortExpression="SellOut_Compute" />
                                            <asp:BoundField DataField="SellOut_Act" HeaderText="本期销售(实际值)" 
                                                SortExpression="SellOut_Act" />
                                           
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
