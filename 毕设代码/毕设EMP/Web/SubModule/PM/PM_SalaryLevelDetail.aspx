<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_SalaryLevelDetail.aspx.cs" Inherits="SubModule_PM_PM_SalaryLevelDetail" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                导购员奖金标准详细信息</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Salary_AddLevel" runat="server" Text="添  加" Width="60px" UseSubmitBehavior="False"
                                OnClick="btn_Salary_AddLevel_Click" />
                            <asp:Button ID="btn_Salary_ApprovLevel" runat="server" Text="审  核" Width="60px" UseSubmitBehavior="False"
                                OnClick="btn_Salary_ApprovLevel_Click" Visible="False" />
                        </td>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="panel2" runat="server" DetailViewCode="Page_PM_005" Visible="true">
                </mcs:UC_DetailView>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" height="30px" border="0" class="h3Row">
                    <tr>
                        <td>
                            <h3>
                                导购员工资等级详细信息</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class=tabForm>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="dataLabel">
                            开始完成率
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="txt_kswcl" runat="server" Enabled="false" Width="60px"></asp:TextBox>%
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_kswcl"
                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_kswcl"
                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" 
                                Type="Double" ValidationGroup="1"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            截止完成率
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="txt_jzwcl" runat="server" Enabled="false" Width="60px"></asp:TextBox>%
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_jzwcl"
                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_jzwcl"
                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"
                                ValidationGroup="1"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator4"
                                    runat="server" ErrorMessage="必须大于开始完成值" ControlToCompare="txt_kswcl" ControlToValidate="txt_jzwcl"
                                    Operator="GreaterThan" ValidationGroup="1" Display="Dynamic" 
                                Type="Double"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            提成系数
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="txt_tcxs" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_tcxs"
                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_tcxs"
                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" 
                                Type="Double" ValidationGroup="1"></asp:CompareValidator>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Salary_AddRate" runat="server" Text="添  加" Width="60px" UseSubmitBehavior="False"
                                OnClick="btn_Salary_AddRate_Click" Enabled="false" ValidationGroup="1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row" height="30px">
                    <tr>
                        <td>
                            <h3>
                                系数列表</h3>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_del_levelRate" runat="server" Text="删 除" Width="60px" OnClick="btn_del_levelRate_Click"
                                UseSubmitBehavior="False" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" id="tb_salary">
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="ud_grid_salary" runat="server" PanelCode="Panel_PM_SalaryLevelRate"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="PM_SalaryLevelDetail_ID"
                                AllowPaging="True" PageIndex="0" PageSize="8">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_ID" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
