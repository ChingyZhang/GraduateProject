<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_SalaryList.aspx.cs" Inherits="SubModule_PM_PM_SalaryList" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        导购员工资列表</h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Generate" runat="server" Text="新增工资" Width="80px" OnClick="bt_Generate_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        查询条件</h3>
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
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    工资类别
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_PMClassify" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    经销商
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="180px" />
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="CBAll" runat="server" Text="全选" AutoPostBack="True" Visible="false" />
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                    <asp:Button ID="bt_Merge" runat="server" Text="合并工资单" Width="60px" OnClientClick="return confirm(&quot;合并工资单后不可恢复，是否确认合并工资单?&quot;)"
                                        OnClick="bt_Merge_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        导购员工资列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                    DataKeyNames="PM_Salary_ID,PM_Salary_AccountMonth" PanelCode="Panel_PM_Salary_List001"
                                    OnSelectedIndexChanging="gv_List_SelectedIndexChanging" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbx" runat="server" Visible='<%# new MCSFramework.BLL.Promotor.PM_SalaryBLL(int.Parse(DataBinder.Eval(Container, "DataItem.PM_Salary_ID").ToString())).Model.State.ToString()=="1"  %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hy_link" runat="server" NavigateUrl='<%# Eval("PM_Salary_ID", "PM_SalaryDetailList.aspx?ID={0}") %>'
                                                    Text="查看详细" ></asp:HyperLink>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工资总额">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Promotor.PM_SalaryBLL.GetSumSalary((int)Eval("PM_Salary_ID")).ToString("0.###") %>'></asp:Label>元
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="人数">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_Counts" runat="server" Text='<%# new MCSFramework.BLL.Promotor.PM_SalaryBLL((int)Eval("PM_Salary_ID")).Items.Count(p => p["FlagCancel"] != "1").ToString("") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("PM_Salary_TaskID", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                    Text="审批记录" Visible='<%# Eval("PM_Salary_TaskID").ToString()!="" %>' ></asp:HyperLink>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcs:UC_GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
