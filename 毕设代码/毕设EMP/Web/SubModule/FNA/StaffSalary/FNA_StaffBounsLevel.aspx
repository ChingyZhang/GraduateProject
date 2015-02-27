<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FNA_StaffBounsLevel.aspx.cs" Inherits="SubModule_FNA_StaffSalary_FNA_StaffBounsLevel" %>

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
                                员工季度资金基数</h2>
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
                        管理片区
                    </td>
                    <td>
                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                            ParentColumnName="SuperID" Width="200px" AlwaysSelectChildNode="False" SelectDepth="0"
                            RootValue="0" DisplayRoot="True" />
                    </td>
                    <td class="dataLabel" style="width: 56px">
                        季度;
                    </td>
                    <td class="dataField">
                    <asp:DropDownList ID="ddl_Quarter" runat="server" DataTextField="Name"  DataValueField="ID"></asp:DropDownList>
                    
                    </td>
                    <td class="dataLabel">
                        &nbsp;
                    </td>
                    <td class="dataField">
                        &nbsp;
                    </td>
                    <td class="dataField" align="right">
                        <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="60px" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="tabForm">
                <tr>
                    <td class="dataLabel">
                        层级
                    </td>
                    <td class="dataLabel">
                        销量坎级(万元)
                    </td>
                    <td class="dataLabel">
                        奖金基数(元)
                    </td>
                </tr>
                <tr>
                    <td class="dataField">
                        <asp:DropDownList ID="ddl_level" runat="server" DataValueField="Key" DataTextField="value"
                            Width="160px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td class="dataLabel">
                        <asp:TextBox ID="txt_sales1" runat="server" Text="0" Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            Text="*" ValidationGroup="1" ControlToValidate="txt_sales1"></asp:RequiredFieldValidator>
                        <span class="dataLabel" style="text-align: center">--</span>
                        <asp:TextBox ID="txt_sales2" runat="server" Text="0" Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            Text="*" ValidationGroup="1" ControlToValidate="txt_sales2"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_sales1"
                            Display="Dynamic" ErrorMessage="销量必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_sales2"
                            Display="Dynamic" ErrorMessage="销量必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </td>
                    <td class="dataField">
                        <asp:TextBox ID="txt_BounsBase" runat="server" Text="0" Width="60px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txt_BounsBase"
                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            Text="*" ValidationGroup="1" ControlToValidate="txt_BounsBase"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                <tr>
                    <td nowrap style="width: 100px" colspan="1">
                        <h3>
                            季度资金列表
                        </h3>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <mcs:UC_GridView ID="gvList" runat="server" AllowPaging="True" ConditionString=""
                PanelCode="Panel_FNA_StaffBonusLevel" Width="100%" AutoGenerateColumns="False"
                Binded="False" OnSelectedIndexChanging="gvList_SelectedIndexChanging" OrderFields=""
                TotalRecordCount="0" DataKeyNames="FNA_StaffBounsLevel_ID" OnRowDeleting="gvList_RowDeleting">
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
