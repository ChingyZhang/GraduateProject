<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JournalCalendar.aspx.cs" Inherits="SubModule_OA_Journal_JournalCalendar" %>

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
                                <td nowrap="noWrap">
                                    <h2>
                                        工作日志--日历视图</h2>
                                </td>
                                <td class="dataLabel">
                                    <asp:Label ID="lb_Staff" runat="server" Text="日志填报人:"></asp:Label>
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="120" OnSelectChange="select_Staff_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    <asp:CheckBox ID="cbx_Plan" runat="server" Checked="True" Text="显示工作计划" 
                                        AutoPostBack="True" oncheckedchanged="cbx_Plan_CheckedChanged" />
                                    <asp:CheckBox ID="cbx_Journal" runat="server" Checked="True" Text="显示工作日志" 
                                        AutoPostBack="True" oncheckedchanged="cbx_Journal_CheckedChanged" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="写新日志" Width="80px" OnClientClick="NewJournal(0)"
                                        OnClick="bt_Add_Click" />
                                    <asp:Button ID="bt_ListView" runat="server" OnClick="bt_ListView_Click" Text="列表视图" />
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
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我的日志" Description=""
                                    Value="0" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="他人日志" Description=""
                                    Value="1" Enable="True"></mcs:MCSTabItem>
                                      <mcs:MCSTabItem Target="_self" NavigateURL="AdvanceFind.aspx" ImgURL="" Text="日志高级查询" Description=""
                            Value="2" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth"
                            Width="100%" OnDayRender="Calendar1_DayRender" ShowGridLines="True" OnSelectionChanged="Calendar1_SelectionChanged">
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TodayDayStyle BackColor="#EEEEEE" ForeColor="Red" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <DayStyle HorizontalAlign="Left" VerticalAlign="Top" Height="50px" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#990033" VerticalAlign="Bottom" />
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="Brown" Height="20px" />
                            <TitleStyle BackColor="#EEEEEE" BorderColor="#EEEEEE" BorderWidth="1px" Font-Bold="True"
                                Font-Size="12pt" ForeColor="#333399" Height="30px" />
                        </asp:Calendar>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
