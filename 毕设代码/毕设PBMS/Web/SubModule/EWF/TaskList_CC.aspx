<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="TaskList_CC.aspx.cs" Inherits="SubModule_EWF_TaskList_CC" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="抄送我的任务信息"></asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            抄送时间段:<asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                    <tr>
                        <td class="dataLabel">
                            查询流程
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_App" runat="server" AutoPostBack="True" DataTextField="Name"
                                Width="200px" DataValueField="ID" OnSelectedIndexChanged="ddl_App_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            发起人片区
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="150px" DisplayRoot="True" />
                        </td>
                        <td class="dataLabel">
                            发起人姓名
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_InitiatorStaffName" runat="server" Width="70px"></asp:TextBox>
                        </td>
                        <td class="dataLabel">
                            标题
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Search" runat="server" Text="查 询" OnClick="btn_Search_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID,ID"
                                Width="100%" AllowPaging="True" OnPageIndexChanging="gv_List_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%# (string)DataBinder.Eval(Container.DataItem, "ReadFlag").ToString() != "Y" ? "<img src='../../Images/mailclose.gif' title='未读'/>" : "<img src='../../Images/mailopen.gif' title='已读'/>"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="TaskDetail_CC.aspx?JobCCID=<%# DataBinder.Eval(Container.DataItem, "ID")%>&TaskID=<%# DataBinder.Eval(Container.DataItem,"TaskID")%>"
                                                class="listViewTdLinkS1" target="_blank">查看及批注</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TaskID" HeaderText="标识" />
                                    <asp:BoundField DataField="Title" HeaderText="标题" />
                                    <asp:BoundField DataField="MessageSubject" HeaderText="消息主题" />
                                    <asp:BoundField DataField="AppName" HeaderText="流程名称" />
                                    <asp:BoundField DataField="ApplyStaffName" HeaderText="发起人" />
                                    <asp:BoundField DataField="TaskStatusName" HeaderText="任务状态" />
                                    <asp:BoundField DataField="StartTime" HeaderText="抄送时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                    <asp:BoundField DataField="Comment" HeaderText="批注内容" />
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
