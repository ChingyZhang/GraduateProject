<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Journal_SynergeticPlanAndJournal, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 240px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="与我相关的协助拜访计划与日志"></asp:Label></h2>
                        </td>
                        <td align="left">
                            开始日期
                            <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                            至
                            <asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ControlToValidate="tbx_enddate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_enddate"
                                Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查询" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="5" width="100%" border="0">
                    <tr>
                        <td width="350px">
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <h3>
                                            抄送我的协助拜访计划</h3>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <h3>
                                            抄送我的协助拜访日志</h3>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_PlanList" runat="server" AutoGenerateColumns="False" Width="100%"
                                PanelCode="Panel_OA_WorkingPlan_Synergetic_01">
                                <Columns>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </td>
                        <td>
                            <mcs:UC_GridView ID="gv_JournalList" runat="server" AutoGenerateColumns="False" Width="100%"
                                PanelCode="Panel_OA_JournalList_Synergetic_01">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="详细" OnClientClick='<%#Bind("JN_Journal_ID","Javascript:OpenJournal({0})") %>'
                                                CssClass="listViewTdLinkS1"></asp:LinkButton>
                                        </ItemTemplate>
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
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
