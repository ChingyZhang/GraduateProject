<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeWriteOffDetail_Print1.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print1" %>

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
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    打印时间：<%=System.DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") %>&nbsp;
                </td>
            </tr>
        </table>
        <br />
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
                                    报销单科目明细</h3>
                            </td>
                            <td align="right">
                                <asp:Label ID="lb_SheetCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="FNA_FeeWriteOffDetail_ApplyDetailID,FNA_FeeWriteOffDetail_WriteOffCost,FNA_FeeWriteOffDetail_AdjustCost"
                        GridLines="Both" BorderWidth="1" BorderColor="Black" PanelCode="Panel_FeeWriteOffDetail_Print1"
                        CssClass="" OnRowDataBound="gv_ListDetail_RowDataBound">
                        <HeaderStyle BackColor="White" CssClass="" />
                        <Columns>
                            <asp:TemplateField HeaderText="申请批复号" SortExpression="ApplyDetailID">
                                <ItemTemplate>
                                    <asp:Label ID="lb_ApplySheetCode" runat="server" Text="" Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="费用所属品类">
                                <ItemTemplate>
                                    <asp:Label ID="lb_RelateBrand" runat="server" Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="批复报销额">
                                <ItemTemplate>
                                    <asp:Label ID="lb_CanWriteOffCost" ForeColor="Red" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.FNA_FeeWriteOffDetail_WriteOffCost")+
                                        (decimal)DataBinder.Eval(Container,"DataItem.FNA_FeeWriteOffDetail_AdjustCost")).ToString("0.###元")%>'></asp:Label>
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
                    <tr height="30px">
                        <td>
                            ① 经 销 商：
                        经 销 商：
                        </td>
                        <td>
                            ② 业 务 员：
                        </td>
                        <td>
                            ③ 办事处主管：
                        </td>
                    </tr>
                    <tr height="30px">
                        <td>
                            &nbsp;&nbsp;&nbsp; 报账日期：
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp; 签收日期：
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 签 收 日 期：
                        </td>
                    </tr>
                    <tr height="30px">
                        <td>
                            ④ 区域主管：
                        </td>
                        <td>
                            ⑤ 营业部主管：
                        </td>
                    </tr>
                    <tr height="30px">
                        <td>
                            &nbsp;&nbsp;&nbsp; 签收日期：
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;签&nbsp; 收&nbsp; 日&nbsp; 期：
                        </td>
                    </tr>
                </table>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td>
                    <h1 style="text-align: center; font-size: large">
                        代 垫 费 用 ___________________明 细 表 回 执</h1>
                </td>
            </tr>
            <tr>
                <td style="font-size: medium">
                    _____________________经销商：
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本次收到贵公司代垫费用单据，费用产生期间：________________，金额：_______________；经我营业部
                    出纳初次复核，可以寄往总部____________的金额为：____________，退回不合规单据金额为：___________， 退回不合规 单据：_____________份。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 以上符合规定的单据寄往总部后，由财务部确认_____________相关款项。<br />
                    <span style="margin-left: 200px">营业部出纳签名： </span>
                    <br />
                    <span style="margin-left: 200px">营业部出纳联系电话： </span>
                    <br />
                    <span style="margin-left: 200px">日 期：</span>
                    <br />
                    <br />
                    <span style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        注：本表一式二联，由经销商填写后交业务员核对签收，一联交经销商留存，一联作票据传递记录及寄回总公司报账之用</span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
