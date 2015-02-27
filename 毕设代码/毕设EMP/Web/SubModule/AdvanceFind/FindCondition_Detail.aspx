<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FindCondition_Detail.aspx.cs" Inherits="SubMoudle_FindCondition_FindCondition_Detail"
    Title="客户高级查询条件详细信息" %>

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
                                高级查询条件维护</h2>
                        </td>
                        <td align="center">
                            查询条件ID:<asp:Label ID="lb_ID" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="确 定" OnClick="bt_OK_Click" />&nbsp;<asp:Button
                                ID="bt_Delete" runat="server" CausesValidation="False" OnClick="bt_Delete_Click"
                                Text="删除条件" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px">
                            所属Panel:
                        </td>
                        <td class="dataField" >
                            <asp:DropDownList ID="ddl_PanelList" runat="server" DataTextField="name" DataValueField="id">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px">
                            是否公用
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_IsPublic" runat="server">
                                <asp:ListItem Value="Y">公用</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">私有</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            条件名称
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_Name" Width="500px" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name"
                                Display="Dynamic" ErrorMessage="必须填写"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            条件文本字符串
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_ConditionText" Width="500px" runat="server" Rows="8" TextMode="MultiLine"></asp:TextBox><br />
                            多个条件间以|号分隔
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            条件值字符串
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_ConditionValue" Width="500px" runat="server" Rows="8" TextMode="MultiLine"></asp:TextBox><br />
                            多个条件间以|号分隔
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px">
                            SQL
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_ConditionSQL" Width="500px" runat="server" Rows="8" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px">
                            创建日期
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_CreateDate" Width="120px" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
