<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="TaskList_InitByMe.aspx.cs" Inherits="SubModule_EWF_TaskList_InitByMe" %>

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
                        <td nowrap="noWrap">
                            <h2>
                                流程任务信息</h2>
                        </td>
                        <td class="dataLabel">
                            发起时间段:<asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <span style="color: #FF0000" __designer:mapid="4c">*</span><asp:CompareValidator
                                ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="tbx_begin" Display="Dynamic"></asp:RequiredFieldValidator>
                            至
                            <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <span style="color: #FF0000" __designer:mapid="50">*</span><asp:CompareValidator
                                ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Search" runat="server" Text="查 询" Width="60px" OnClick="btn_Search_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="2" Width="100%">
                    <Items>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="待我审批" Description=""
                            Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我已审批" Description=""
                            Value="1" Enable="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="由我发起" Description=""
                            Value="2" Enable="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="已完成任务" Description=""
                            Value="3" Enable="True"></cc1:MCSTabItem>
                    </Items>
                </cc1:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="dataLabel" width="100">
                            查询流程
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_App" runat="server" DataTextField="Name" DataValueField="ID" />
                        </td>
                        <td class="dataLabel" width="100">
                            流程当前状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Status" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            流程结束状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_FinishStatus" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" AllowPaging="True"
                                        OnPageIndexChanging="gv_List_PageIndexChanging" Binded="False" ConditionString=""
                                        PanelCode="" TotalRecordCount="0">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="详细信息" Visible="true">
                                                <ItemStyle Width="50px" />
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="ID" HeaderText="流程标识" />
                                            <asp:BoundField DataField="App" HeaderText="流程名称" />
                                            <asp:TemplateField HeaderText="主题">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Status" HeaderText="当前状态" />
                                            <asp:HyperLinkField DataTextField="RelateURL" HeaderText="关联URL" DataNavigateUrlFields="RelateURL"
                                                DataNavigateUrlFormatString="{0}" ControlStyle-CssClass="listViewTdLinkS1" Target="_blank"
                                                Visible="false">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="StartTime" HeaderText="发起时间" />
                                            <asp:BoundField DataField="EndTime" HeaderText="结束时间" />
                                            <asp:BoundField DataField="FinishStatus" HeaderText="流程完成状态" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
                                </Triggers>
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
