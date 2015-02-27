<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Panel_Table.aspx.cs" Inherits="Web_Panel_Table" %>

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
                                Panel与表定义
                            </h2>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1" Width="100%">
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
                <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                    <tr>
                        <td class="dataLabel">
                            Panel名
                        </td>
                        <td class="dataField">
                            <asp:Label ID="lbl_PanelName" runat="server"></asp:Label>
                        </td>
                        <td class="dataLabel">
                            表名
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_TableName" runat="server" DataTextField="DisplayName" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataField" align="right">
                            <asp:Button ID="bt_OK" runat="server" Text="新 增" OnClick="bt_OK_Click" 
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%">
                            <asp:GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID" OnRowDeleting="gv_List_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="PanelName" HeaderText="Panel名称" SortExpression="PanelName" />
                                    <asp:BoundField DataField="DisplayName" HeaderText="表显示名称" SortExpression="DisplayName" />
                                    <asp:BoundField DataField="TableName" HeaderText="表实际名称" SortExpression="TableName" />
                                    <asp:CommandField DeleteText="删除" ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
