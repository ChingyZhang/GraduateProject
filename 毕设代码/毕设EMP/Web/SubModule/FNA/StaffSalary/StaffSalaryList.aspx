<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="StaffSalaryList.aspx.cs" Inherits="SubModule_PM_PM_StaffSalaryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                员工工资列表</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Generate" runat="server" Text="新增工资" Width="80px" 
                                OnClick="bt_Generate_Click" />
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
                            月份
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" 
                                DataValueField="ID">
                            </asp:DropDownList>
                            至<asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" 
                                DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            审批状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
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
                                员工工资列表</h3>
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
                            DataKeyNames="FNA_StaffSalary_ID" PanelCode="Panel_FNA_StaffSalary_List001" 
                            onselectedindexchanging="gv_List_SelectedIndexChanging" AllowPaging="True">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="查看" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="奖金总额">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_StaffSalaryBLL.GetSumSalary(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_StaffSalary_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                            </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_StaffSalary_TaskID", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                            Text="审批记录" Visible='<%# Eval("FNA_StaffSalary_TaskID").ToString()!="" %>' ></asp:HyperLink>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
