<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowProcessStartDetail.aspx.cs" Inherits="SubModule_EWF_FlowProcessStartDetail" %>

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
                                流程环节详细信息</h2>
                        </td>
                        <td align="left">
                            ID号:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="left">
                            环节类型<asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" runat="server"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="保 存" OnClick="bt_Save_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
        <tr>
            <td class="tabForm" width="100%">
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left" colspan="4" height="22" valign="middle">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            所属流程
                        </td>
                        <td class="dataField">
                            <asp:Label ID="lb_AppName" runat="server"></asp:Label>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Name" Width="300px" runat="server">开始</asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px;">
                            描述
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_Description" Width="500px" runat="server" TextMode="MultiLine"
                                Height="61px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            默认下一环节
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_DefaultNextProcess" DataTextField="Name" DataValueField="ID"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            排序号
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Sort" runat="server" Width="50px">1</asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="tbx_Sort" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Sort"
                                Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
