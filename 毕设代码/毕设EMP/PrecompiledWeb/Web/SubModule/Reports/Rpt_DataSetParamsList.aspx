<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_Rpt_DataSetParamsList, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集参数列表"></asp:Label></h2>
                        </td>
                        <td class="dataLabel">
                            数据集:<asp:Label ID="lb_DataSetName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="数据集参数" Value="1" Enable="false" />
                        <mcs:MCSTabItem Text="包含数据表" Value="2" />
                        <mcs:MCSTabItem Text="数据表关系" Value="3" />
                        <mcs:MCSTabItem Text="数据集字段" Value="4" />
                        <mcs:MCSTabItem Text="查询条件" Value="5" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Rpt_DataSetParams_ID" PageSize="15" Width="100%" PanelCode="Panel_Rpt_DataSetParams_List"
                                        OnRowDataBound="gv_List_RowDataBound">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="Rpt_DataSetParams_ID" DataNavigateUrlFormatString="Rpt_DataSetParamsDetail.aspx?ID={0}"
                                                Text="查看" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="排序" SortExpression="ParamSortID">
                                                <ItemTemplate>
                                                    <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="↓" />
                                                    <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="↑" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
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
