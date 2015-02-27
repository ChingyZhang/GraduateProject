<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="QNA_ProjectStatistics.aspx.cs" Inherits="SubModule_Service_QNA_QNA_ProjectStatistics" %>

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
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="显示问卷调研统计结果"></asp:Label></h2>
                                </td>
                                <td>
                                    问卷名称：<asp:Label ID="lb_ProjectName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    &nbsp;有效调研份数:<asp:Label ID="lb_TotalCount" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                            <Items>
                                <mcs:MCSTabItem Text="图形分析" Value="0" />
                                <mcs:MCSTabItem Text="表格分析" Value="1" />
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <asp:Repeater ID="rpt_Question" runat="server" OnItemDataBound="rpt_Question_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="left" height="28px">
                                        标题：<asp:Label ID="lb_QuestionTitle" runat="server" Font-Bold="True" ForeColor="#FF0000"
                                            Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                        <asp:Label ID="lb_ID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Repeater ID="rpt_Option" runat="server">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="30px" align="middle" height="20px">
                                                        <img alt="" src="../../../Images/gif/gif-0465.gif" style="width: 12px; height: 13px" />
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label ID="lb_OptionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lb_OptionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OptionName") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lb_Bar" runat="server" Text="." BackColor="#ff9900" ForeColor="#ff9900"></asp:Label>
                                                        <asp:Label ID="lb_Counts" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td width="100px" align="left">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <tr>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                            </SeparatorTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Binded="False" ConditionString="" PageSize="15" PanelCode="" TotalRecordCount="0"
                            Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="SortCode" HeaderText="题号" SortExpression="SortCode" />
                                <asp:BoundField DataField="Title" HeaderText="标题" SortExpression="Title" />
                                <asp:BoundField DataField="OptionName" HeaderText="选项" SortExpression="OptionName" />
                                <asp:BoundField DataField="Counts" HeaderText="调研结果" SortExpression="Counts" />
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
            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
