<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_Budget_Pop_BudgetAssign, App_Web_gigc93-l" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>给作业区分配预算</title>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            作业区可用费用分配
                                        </h2>
                                    </td>
                                    <td class="dataLabel">
                                        计划销量
                                        <asp:Label ID="lb_PlanVolume" runat="server"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        可用费用合计
                                        <asp:Label ID="lb_SumBudget" runat="server"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        推广自行支配费用
                                        <asp:Label ID="lb_RetentionBudget" runat="server"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        可分配费用余额
                                        <asp:Label ID="lb_AssignBalance" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        调拨市场部预算额度
                                        <asp:Label ID="lb_DepartmentBudget" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged"
                                            Text="全选" />
                                    </td>
                                    <td class="dataField">
                                        <asp:Button ID="btnSave" runat="server" Width="60px" Text="保存" OnClick="btnSave_Click" />
                                    </td>
                                    <td class="dataField" align="right">
                                        <asp:Button ID="btnApprove" runat="server" Width="60px" Text="审核" OnClick="btnApprove_Click"
                                            OnClientClick="return confirm('是否确认将选中的预算分配记录审核通过？')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                            DataKeyNames="ID,FeeType" AllowPaging="True" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbx" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FeeType" HeaderText="费用类型" />
                                                <asp:TemplateField HeaderText="分配额度">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbx_BudgetAmount" runat="server" Width="60px" Text='<%# Bind("BudgetAmount","{0:0.##}") %>'
                                                            Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="占计划销量比">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_BudgetRate" runat="server" Text='<%#GetBudgetRate(decimal.Parse(Eval("BudgetAmount").ToString())).ToString("0.##%") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="备注">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbx_Remark" runat="server" Width="320px" Text='<%# Bind("Remark") %>'
                                                             ReadOnly='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="1"%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ApproveFlag" HeaderText="审核标志" />
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <font color="red">注：作业区的可用费用总计，不包括<asp:Label ID="lb_OutSumBudgetFeeType" Font-Bold="true"
                                runat="server" Text=""></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
