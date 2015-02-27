<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeWriteOffDetail_Print2.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .PageTitle1
        {
            font-size: 22px;
            text-align: center;
        }
        .PageTitle2
        {
            font-size: 18px;
            text-align: center;
        }
        .style1
        {
            height: 30px;
        }
    </style>
    <style media="print">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <h2 style="text-align: center; font-size: large">
                        <asp:Label ID="lbl_message" runat="server" Text="lbl_message"></asp:Label>
                    </h2>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr height="28px">
                            <td nowrap>
                                <h3>
                                    费用核销单详细信息</h3>
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="tabForm" height="60" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="style1">
                                核销单号:<span id="span_sheetcode" runat="server"></span>
                            </td>
                            <td class="style1" colspan="2">
                                管理片区:<span id="span_orgnizecity" runat="server"></span>
                            </td>
                            <td align="right" class="style1">
                                第<span id="span1" runat="server"></span>页，共<span id="span2" runat="server"></span>页
                            </td>
                        </tr>
                        <tr>
                            <td>
                                费用代垫<span id="p_ddtype" runat="server">客户</span>:<span id="span_client" runat="server"></span>
                            </td>
                            <td class="style1">
                                会计月:<span id="span_accountmonth" runat="server"></span>
                            </td>
                            <td>
                                抵货款类型:&nbsp;<span id="span_type" runat="server"></span>
                            </td>
                            <td align="right" class="style1">
                                单位:元
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="ID,ApplyDetailID,Client" OnRowDataBound="gv_ListDetail_RowDataBound"
                        GridLines="Both" BorderWidth="1" BorderColor="Black" CssClass="">
                        <HeaderStyle BackColor="White" CssClass="" Font-Size="12px" Height="28px" />
                        <Columns>
                            <asp:BoundField DataField="Client" HeaderText="发生客户" SortExpression="Client" ItemStyle-Font-Size="12px" />
                            <asp:TemplateField HeaderText="客户渠道" ItemStyle-Font-Size="12px">
                                <ItemTemplate>
                                    <asp:Label ID="lb_RTChannel" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle"
                                ItemStyle-Font-Size="12px" />
                            <asp:TemplateField HeaderText="申请<br/>报销额" ItemStyle-Font-Size="12px">
                                <ItemTemplate>
                                    <asp:Label ID="lb_WriteOffCost" Text='<%# Math.Round((decimal)Eval("WriteOffCost"),1).ToString("0.##")%>'
                                        runat="server" Visible='<%#(decimal)Eval("WriteOffCost")!=0 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总提报额" ItemStyle-Font-Size="12px">
                                <ItemTemplate>
                                    <asp:Label ID="lb_AllCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjustCost" HeaderText="扣款额" DataFormatString="{0:0.##}"
                                HtmlEncode="False" SortExpression="AdjustCost" />
                            <asp:BoundField DataField="AdjustMode" HeaderText="扣减方式" SortExpression="AdjustMode"
                                Visible="false" />
                            <asp:BoundField DataField="AdjustReason" HeaderText="扣减信息" SortExpression="AdjustReason" />
                            <asp:BoundField DataField="BeginMonth" HeaderText="发生月份" SortExpression="BeginMonth"
                                ItemStyle-Font-Size="12px" />
                            <asp:TemplateField HeaderText="备注" ItemStyle-Font-Size="10px" SortExpression="Remark"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lb_Remark" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请批复号" SortExpression="ApplyDetailID" ItemStyle-Font-Size="10px"
                                ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lb_ApplySheetCode" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                    </mcs:UC_GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolordark="#ffffff"
                        bordercolorlight="#666666">
                        <tr height="30px">
                            <td align="right" width="100px">
                                小计金额(大写)
                            </td>
                            <td width="50%">
                                &nbsp;<asp:Label ID="lab_SubTotalCostCN" runat="server" Font-Size="16px" Font-Bold="true"
                                    Text=""></asp:Label>
                            </td>
                            <td align="right" width="100px">
                                小计金额(小写)
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lab_SubTotalCost" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolordark="#ffffff"
                bordercolorlight="#666666">
                <tr height="30px" align="center">
                    <td>
                        财务主管
                    </td>
                    <td>
                        复核会计
                    </td>
                    <td>
                        初审会计
                    </td>
                    <td>
                        出纳
                    </td>
                </tr>
                <tr height="30px">
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    打印时间：<%=System.DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") %>&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
