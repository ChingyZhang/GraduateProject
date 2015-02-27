<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="StaffSalary_Param.aspx.cs" Inherits="SubModule_FNA_StaffSalary_StaffSalary_Param" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 33px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                员工绩效参数</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
                height="30" class="tabForm">
                <tr>
                    <td class="dataLabel" width="100px">
                        关联职位
                    </td>
                    <td>
                        <mcs:MCSTreeControl ID="tr_Position" runat="server" DisplayRoot="True" IDColumnName="ID"
                            TableName="MCS_SYS.dbo.Org_Position" NameColumnName="Name" ParentColumnName="SuperID"
                            Width="240px" />
                    </td>
                    <td class="dataField" align="right">
                        <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_StaffSalary_Param">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                <tr>
                    <td nowrap style="width: 100px" colspan="1">
                        <h3>
                            绩效计算参数
                        </h3>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <mcs:UC_GridView ID="gvList" runat="server" AllowPaging="True" ConditionString=""
                PanelCode="Panel_StaffSalary_Param" Width="100%" AutoGenerateColumns="False"
                Binded="False" OnSelectedIndexChanging="gvList_SelectedIndexChanging" OrderFields=""
                TotalRecordCount="0" DataKeyNames="FNA_StaffSalary_Param_Position" OnRowDeleting="gvList_RowDeleting">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="修改" ControlStyle-CssClass="listViewTdLinkS1">
                        <ControlStyle CssClass="listViewTdLinkS1" />
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                        <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                    </asp:CommandField>
                </Columns>
            </mcs:UC_GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
