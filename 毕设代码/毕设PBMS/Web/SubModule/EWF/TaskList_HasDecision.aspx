<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="TaskList_HasDecision.aspx.cs" Inherits="SubModule_EWF_TaskList_HasDecision" %>

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
                            审批时间段:<asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
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
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="待我审批" Description=""
                            Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我已审批" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="由我发起" Description=""
                            Value="2" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="已完成任务" Description=""
                            Value="3" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="dataLabel">
                                    查询流程
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_App" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID" OnSelectedIndexChanged="ddl_App_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    消息主题
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    我审批的结果
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DecisionResult" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left">
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
                                Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" AllowPaging="true"
                                OnPageIndexChanging="gv_List_PageIndexChanging">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="详细信息">
                                        <ItemStyle Width="100px" />
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="TaskID" HeaderText="流程标识" />
                                    <asp:BoundField DataField="Title" HeaderText="流程标题" />
                                    <asp:BoundField DataField="MessageSubject" HeaderText="消息主题" />
                                    <asp:BoundField DataField="AppName" HeaderText="流程名称" />
                                    <asp:BoundField DataField="ProcessName" HeaderText="环节名称" />
                                    <asp:BoundField DataField="ApplyStaffName" HeaderText="发起人" />
                                    <asp:BoundField DataField="DecisionTime" HeaderText="审批时间" SortExpression="DecisionTime" />
                                    <asp:BoundField DataField="DecisionResultName" HeaderText="审批结果" />
                                    <asp:BoundField DataField="DecisionComment" HeaderText="审批意见" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
