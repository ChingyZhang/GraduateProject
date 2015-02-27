<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Journal_JournalList, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

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
                                工作日志--列表视图
                            </h2>
                        </td>
                        <td align="left">
                            开始日期
                            <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                            至
                            <asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="tbx_enddate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_enddate"
                                Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            日志类型
                            <asp:DropDownList ID="ddl_JournalType" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 询" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="写新日志" Width="60px" OnClientClick="NewJournal(0)"
                                OnClick="bt_Add_Click" CausesValidation="false" />
                            <asp:Button ID="bt_CalendarView" runat="server" OnClick="bt_CalendarView_Click" Text="日历视图"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我的日志" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="他人日志" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="AdvanceFind.aspx" ImgURL="" Text="日志高级查询" Description=""
                            Value="2" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_Condition"
                            runat="server" visible="false" height="30px">
                            <tr>
                                <td width="60px" class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField" width="220px">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                </td>
                                <td width="60px" class="dataLabel">
                                    员工职务
                                </td>
                                <td class="dataField" width="220px">
                                    <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" RootValue="0" />
                                </td>
                                <td class="dataField" width="160">
                                    <asp:CheckBox ID="cb_IncludeChild" runat="server" Checked="True" Style="color: #FF0000"
                                        Text="包含下级职位" />
                                </td>
                                <td width="60px" class="dataLabel">
                                    员工姓名
                                </td>
                                <td class="dataField" width="120px">
                                    <asp:TextBox ID="tbx_StaffName" runat="server"></asp:TextBox>
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="JN_Journal_ID" PageSize="15" Width="100%"
                            PanelCode="Panel_OA_JournalList_001" OnSelectedIndexChanged="gv_List_SelectedIndexChanged">
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
