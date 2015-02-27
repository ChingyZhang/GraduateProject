<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JournalOnWorkingPlan.aspx.cs" Inherits="SubModule_OA_Journal_JournalOnWorkingPlan" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                工作日志与计划排期表</h2>
                        </td>
                        <td>
                            <asp:CheckBox ID="cb_DisplayCheckedOnly" runat="server" AutoPostBack="True" OnCheckedChanged="cb_DisplayCheckedOnly_CheckedChanged"
                                Text="仅显示选中项" />
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存日志" />
                            <asp:Button ID="bt_AddJournal" runat="server" Text="填报非计划日志" OnClientClick='javascript:NewJournal()'
                                ToolTip="点击此按钮填报事先没有列入计划的实际工作日志！" OnClick="bt_AddJournal_Click" Width="100px" />
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_OA_WorkingPlan_02">
                                                </mcs:UC_DetailView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 400px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SortID,WorkingClassify,Description,RelateStaff,RelateClient,OfficialCity"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="SortID" HeaderText="序号" SortExpression="SortID" ItemStyle-Width="30px">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WorkingClassifyName" HeaderText="工作类型" ItemStyle-Width="60px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="工作内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("RelateStaffName") %>'></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("RelateClientName") %>'></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OfficialCityName" HeaderText="城市" ItemStyle-Width="70px">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="预计交通费" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="tbx_Cost1" runat="server" Text='<%# Bind("Cost1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="预计住宿费" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="tbx_Cost2" runat="server" Text='<%# Bind("Cost2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="天数">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_PCount" runat="server" Text='<%# Bind("P_Counts") %>'></asp:Label>
                                            /
                                            <asp:Label ID="lb_JCount" runat="server" Text='<%# Bind("J_Counts") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cb_DisplayCheckedOnly" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Save" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="91" CanDelete="false"
                    CanSetDefaultImage="false" CanUpload="false" />
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 60;
        divGridView.style.height = window.screen.availHeight - 400;         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
