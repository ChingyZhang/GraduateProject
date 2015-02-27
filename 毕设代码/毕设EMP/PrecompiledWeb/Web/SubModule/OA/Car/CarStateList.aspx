<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Car_CarStateList, App_Web_dk3o7pe1" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 220px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="">公司车辆使用状态列表</asp:Label></h2>
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
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <mcs:MCSTabItem Text="空闲中" Value="1" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Text="使用中" Value="2" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="Car_CarList_ID" PanelCode="Panel_OA_Car_CarList_01" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                            AllowPaging="True" OnRowDataBound="gv_List_RowDataBound">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="查看" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="车辆去向" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_DispatchRide" runat="server" CssClass="listViewTdLinkS1"></asp:HyperLink>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
