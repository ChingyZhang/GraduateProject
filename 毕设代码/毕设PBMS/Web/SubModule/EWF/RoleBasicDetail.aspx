<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RoleBasicDetail.aspx.cs" Inherits="SubModule_EWF_Role_RoleBasicDetail" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 129px; height: 24px;">
                            <h2>
                                角色详细信息</h2>
                        </td>
                        <td align="left">
                            ID号:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="保 存" OnClick="bt_Save_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
        <tr>
            <td class="tabForm" style="width: 885px">
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left" colspan="4" height="22" valign="middle">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            角色名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Name" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="dataLabel" style="width: 120px;">
                            描述
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Description" Width="220px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            角色类型
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" Width="120px"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            &nbsp;
                        </td>
                        <td class="dataField">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
