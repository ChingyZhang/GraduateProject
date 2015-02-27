<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_SalaryLevelList.aspx.cs" Inherits="SubModule_PM_PM_SalaryLevelList" %>

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
                                工资列表查询</h2>
                        </td>
                        <td align="right">
                            <table id="Table3" cellspacing="0" cellpadding="0" border="0">
                                <tr runat="server" id="tr_basicsearch" align="left">
                                    <td width="260px">
                                        管理片区
                                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" Width="200px" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_Salary_Search" runat="server" Text="查  询" Width="60px" UseSubmitBehavior="False"
                                            OnClick="btn_Salary_Search_Click" />
                                        <asp:Button ID="btn_Salary_Add" runat="server" Text="新  增" Width="60px" UseSubmitBehavior="False"
                                            OnClick="btn_Salary_Add_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" PanelCode="Panel_PM_List_004"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="15" PageIndex="0" DataKeyNames="PM_SalaryLevel_ID">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="PM_SalaryLevel_ID" DataNavigateUrlFormatString="PM_SalaryLevelDetail.aspx?ID={0}"
                                    DataTextField="PM_SalaryLevel_Name" HeaderText="奖金标准名称" ControlStyle-CssClass="listViewTdLinkS1" />
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
