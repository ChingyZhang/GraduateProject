<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_FieldsInPanel, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
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
                                字段列表
                            </h2>
                        </td>
                        <td align="left">
                            Panel名称:<asp:Label ID="lbl_PanelName" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Text="新 增" OnClick="bt_OK_Click" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="3" Width="100%">
                    <Items>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="详细信息维护" Description=""
                            Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="包含数据表维护" Description=""
                            Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="数据表关系维护" Description=""
                            Value="2" Enable="True" Visible="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="模块字段维护" Description=""
                            Value="3" Enable="True" Visible="True"></cc1:MCSTabItem>
                    </Items>
                </cc1:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="ID" AutoGenerateColumns="False"
                                        OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                        Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0">
                                        <Columns>
                                            <asp:BoundField DataField="FieldID" HeaderText="字段名称" />
                                            <asp:BoundField DataField="LabelText" HeaderText="显示文本" />
                                            <asp:TemplateField HeaderText="顺序编号">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SortID" runat="server" Text='<%# Bind("SortID") %>' Width="20px"></asp:Label>
                                                    <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="+" />
                                                    <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="-" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Enable" HeaderText="可用标志" />
                                            <asp:BoundField DataField="Visible" HeaderText="可见标志" />
                                            <asp:BoundField DataField="ControlType" HeaderText="控件类型" />
                                            <asp:CommandField SelectText="详细信息" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:CommandField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                        OnClientClick="return confirm(&quot;是否确认删除?&quot;)" Text="删除"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
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
