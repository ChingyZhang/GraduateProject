﻿<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_FlowProcessCCDetail, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

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
                        <td align="left" style="height: 24px">
                            环节类型<asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" runat="server"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="保 存" OnClick="bt_Save_Click" />
                            <asp:Button ID="bt_Del" runat="server" Text="删 除" Width="60px" OnClick="bt_Del_Click"
                                OnClientClick="return confirm(&quot;是否确认删除?&quot;)" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                    &nbsp;<asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
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
                                    <asp:TextBox ID="tbx_Sort" runat="server" Width="50px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="tbx_Sort" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Sort"
                                        Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_ProcessDecision" visible="true" class="tabForm">
                    <td class="tabForm" width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        抄送人员信息</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    抄送人角色
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RecipientRole_Decision" DataTextField="Name" DataValueField="ID"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RecipientRole_Decision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    &nbsp;关联数据对象
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DataObject_Decision" runat="server" 
                                        DataTextField="DisplayName" DataValueField="ID" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    消息主题</td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="tbx_MessageSubject" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
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
