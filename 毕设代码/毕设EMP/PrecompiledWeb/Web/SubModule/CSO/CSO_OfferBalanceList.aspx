<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CSO_CSO_OfferBalanceList, App_Web_quved-rv" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0px">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0px" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="费用单列表页"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                                    <asp:Button ID="bt_Add" runat="server" Text="提取新费用" Width="80px" OnClick="bt_Add_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="删除费用" Width="80px" OnClick="bt_Delete_Click"
                                        OnClientClick="return confirm('确定删除？')" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap>
                                    <h3>
                                        查询条件
                                    </h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    结算月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" DisplayRoot="True" />
                                </td>
                                <td class="dataLabel">
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" Width="120px" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                        <tr>
                                            <td>
                                                <h3>
                                                    费用单列表</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%" PanelCode="Panel_CSO_OfferBalanceList_01" DataKeyNames="CSO_OfferBalance_ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Select" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.CSO_OfferBalance_State").ToString()=="未提交"%>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField Text="查看" Target="_blank" DataNavigateUrlFields="CSO_OfferBalance_ID"
                                                DataNavigateUrlFormatString="CSO_OfferBalanceDetail.aspx?OfferBalanceID={0}"
                                                ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("CSO_OfferBalance_ApproveTask", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="审批记录" Visible='<%# Eval("CSO_OfferBalance_ApproveTask").ToString()!="" %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
