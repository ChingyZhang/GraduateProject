<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Journal_WorkingPlan_List, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                        工作计划排期表<asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
                                </td>
                                <td width="120px" class="dataLabel">
                                    计划开始日期范围
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                        Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    至<asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_begindate"
                                        Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查询" Width="80px" OnClick="bt_Find_Click" />
                                    <asp:Button ID="bt_New" runat="server" Text="填报新计划" Width="80px" OnClick="bt_New_Click" />
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
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我的工作计划" Description=""
                                    Value="0" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="他人工作计划" Description=""
                                    Value="1" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="人员工作情况统计"
                                    Description="" Value="2" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" PanelCode="Panel_OA_WorkingPlan_List_01"
                            AllowPaging="true" PageSize="15" DataKeyNames="JN_WorkingPlan_ID" AutoGenerateColumns="False">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="JN_WorkingPlan_ID" DataNavigateUrlFormatString="WorkingPlan_CalendarList.aspx?ID={0}"
                                    Text="查看计划" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1" >
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="JN_WorkingPlan_ID" DataNavigateUrlFormatString="JournalOnWorkingPlan.aspx?PlanID={0}"
                                    Text="实际日志" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1" >
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                            </Columns>
                        </mcs:UC_GridView>
                           <mcs:UC_GridView ID="gv_planList" runat="server" Width="100%" 
                            GridLines="Both" CellPadding="1"
                            BackColor="#CCCCCC" CellSpacing="1" CssClass="" BorderWidth="0px" Visible="false"
                            PageSize="50" AllowPaging="true"  DataKeyNames="Staff" ondatabound="gv_planList_DataBound" 
                           >
                            <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Staff" DataNavigateUrlFormatString="JournalCalendar.aspx?Staff={0}"
                                    Text="工作日历" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1"  >
                                    <ControlStyle CssClass="listViewTdLinkS1"/>
                                </asp:HyperLinkField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
