<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowAppList.aspx.cs" Inherits="SubModule_EWF_FlowAppList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 355px">
                                    <h2>
                                        流程列表</h2>
                                </td>
                                <td align="right">
                                    流程名称:
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                        UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="启用" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="停用" Description=""
                                    Value="1" Enable="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID" PageSize="15" Width="100%" Binded="False" OnPageIndexChanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Code" HeaderText="流程代码" />
                                            <asp:HyperLinkField DataNavigateUrlFields="ID" HeaderText="流程名称" DataNavigateUrlFormatString="FlowAppDetail.aspx?AppID={0}"
                                                DataTextField="Name" SortExpression="Name">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" />
                                            <asp:BoundField DataField="InsertTime" HeaderText="创建时间" SortExpression="InsertTime" />
                                            <asp:BoundField DataField="InsertStaff" HeaderText="创建人" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "Apply.aspx?AppID={0}") %>'
                                                        Text="发起流程" Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
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
