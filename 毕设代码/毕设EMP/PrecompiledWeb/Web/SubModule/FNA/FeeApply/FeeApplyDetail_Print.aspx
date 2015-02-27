<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeApply_FeeApplyDetail_Print, App_Web_5zp237uh" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lb_Header" runat="server" CssClass="style1"></asp:Label>
                    <span class="style1">费用申请单</span>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_DetailView ID="pn_FeeApply" runat="server" DetailViewCode="Page_FNA_FeeApplyDetail_Print">
                    </mcs:UC_DetailView>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr height="28px">
                            <td nowrap>
                                <h3>
                                    费用申请科目明细列表</h3>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="ID,AccountTitle">
                        <Columns>
                            <asp:BoundField DataField="Client" HeaderText="客户" SortExpression="Client" />
                            <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle" />
                            <asp:BoundField DataField="BeginDate" HeaderText="开始日期" SortExpression="BeginMonth"
                                HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="EndDate" HeaderText="截止日期" SortExpression="EndMonth" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Remark" HeaderText="说明" SortExpression="Remark" />
                            <asp:BoundField DataField="ApplyCost" HeaderText="申请金额" SortExpression="ApplyCost"
                                HtmlEncode="false" DataFormatString="{0:0.###元}" />
                            <asp:TemplateField HeaderText="调整金额" SortExpression="AdjustCost">
                                <ItemTemplate>
                                    <asp:Label ID="lb_AdjustCost" runat="server" Text='<%# Bind("AdjustCost","{0:0.###}") %>'></asp:Label>元
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="调整原因" SortExpression="AdjustReason">
                                <ItemTemplate>
                                    <asp:Label ID="lb_AdjustReason" runat="server" Text='<%# Bind("AdjustReason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="批复金额">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"ApplyCost") + (decimal)DataBinder.Eval(Container.DataItem,"AdjustCost")).ToString("0.###元")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Flag" HeaderText="报销标志" SortExpression="Flag" />
                        </Columns>
                    </mcs:UC_GridView>
                </td>
            </tr>
            <tr>
                <td align="left" height="28">
                    合计费用：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                    元
                </td>
            </tr>
            <tr>
                <td>
                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr height="28px">
                            <td nowrap>
                                <h3>
                                    审批批复信息</h3>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:Label ID="lb_TaskApproveInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
