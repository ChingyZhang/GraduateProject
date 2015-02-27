<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="Car_UsageList.aspx.cs" Inherits="SubModule_OA_Car_Car_Car_UsageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 160px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="车辆使用信息列表"></asp:Label></h2>
                                </td>
                                <td class="dataLabel" width="60px">
                                    管理片区
                                </td>
                                <td class="dataField" width="200px" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" DisplayRoot="True" AutoPostBack="True"
                                        OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel" width="40px">
                                    车号
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Car" runat="server" DataTextField="CarNo" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" width="60px">
                                    发车日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                            ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>&nbsp;
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <mcs:MCSTabItem Text="车辆每日行程" Value="1" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Text="车辆费用申请" Value="2" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List_Evection" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Car_DispatchRide_ID" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_Evection_SelectedIndexChanging"
                                        PanelCode="Panel_OA_Car_DispatchRide_EvectionRouteList">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_List_FeeApply" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="FNA_FeeApply_ID" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_FeeApply_SelectedIndexChanging"
                                        PanelCode="Panel_OA_Car_FeeApplyList" Visible="false">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
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
