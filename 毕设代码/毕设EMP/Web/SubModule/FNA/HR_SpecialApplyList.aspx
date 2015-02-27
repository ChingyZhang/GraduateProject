<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="HR_SpecialApplyList.aspx.cs" Inherits="SubModule_FNA_HR_SpecialApplyList"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td style="width: 16">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" style="width: 150px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="">特殊申请</asp:Label></h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="300">
                            管理片区:<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" Height="17px" />
                        </td>
                        <td>
                            会计月:
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td> 
                        <td align="right">
                            科目: <asp:DropDownList ID="ddl_AccountTitleType" runat="server" Width ="200px" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>&nbsp;
                        </td>
                        
                        <td align="right">
                            申请单号:<asp:TextBox ID="tbx_SheetCode" runat="server"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                        
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    录入人：
                                    <mcs:MCSSelectControl ID="Select_InsertStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="150px"></mcs:MCSSelectControl>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                          <td align="right">
                            审核状态: <asp:DropDownList ID="ddl_ApproveFlag" runat="server" Width ="80px" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
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
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID" PageSize="15" Width="100%" PanelCode="" Binded="False" ConditionString=""
                                        OrderFields="" TotalRecordCount="0" OnPageIndexChanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="HR_SpecialApplyDetail.aspx?ID={0}"
                                                Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="SheetCode" HeaderText="申请单号" />
                                            <asp:TemplateField HeaderText="管理片区">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lb_OrganizeCity" Text='<%# MCSFramework.BLL.TreeTableBLL.GetFullPathName("Addr_OrganizeCity",(int)DataBinder.Eval(Container,"DataItem.OrganizeCity")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AccountMonth" HeaderText="会计月" />
                                            <asp:BoundField DataField="AccountTitleType" HeaderText="科目" />
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="审批状态" />
                                            <asp:BoundField DataField="InsertStaff" HeaderText="录入人" />
                                            <asp:BoundField DataField="InsertTime" HeaderText="录入时间" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Task", "~/SubModule/EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="审批记录" Visible='<%# Eval("Task").ToString()!="0" %>' ></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
