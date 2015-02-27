<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .PageTitle1
        {
            font-size: 24px;
            text-align: center;
        }
        .PageTitle2
        {
            font-size: 22px;
            text-align: center;
        }
        .style1
        {
            width: 141px;
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
                    <asp:Label ID="lb_Header" runat="server" CssClass="PageTitle1"></asp:Label>
                    <span class="PageTitle1">费用报销审批单--总表</span>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_DetailView ID="pn_FeeWriteOff" runat="server" DetailViewCode="Page_FNA_FeeWriteOffDetail_Print">
                    </mcs:UC_DetailView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td height="28">
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolordark="#ffffff"
                        bordercolorlight="#666666">
                        <tr height="30px">
                            <td align="right" width="100px">
                                小计金额(大写)
                            </td>
                            <td width="50%">
                                &nbsp;<asp:Label ID="lb_TotalCostCN" runat="server" Font-Size="16px" Font-Bold="true"
                                    Text=""></asp:Label>
                            </td>
                            <td align="right" width="100px">
                                小计金额(小写)
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lb_TotalCost" runat="server" Text=""></asp:Label>元
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_DetailView ID="pn_Remark" runat="server" DetailViewCode="Page_FNA_FeeWriteOffDetail_Remark">
                    </mcs:UC_DetailView>
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
                        <tr>
                            <td height="22" align="center">
                                会计主管
                            </td>
                            <td align="center" class="style1">
                                复核
                            </td>
                            <td align="center">
                                出纳
                            </td>
                            <td align="center">
                                报销人
                            </td>
                            <td align="center">
                                领款人
                            </td>
                        </tr>
                        <tr>
                            <td height="34" align="center">
                                &nbsp;
                            </td>
                            <td align="center" class="style1">
                                &nbsp;
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    打印时间：<%=System.DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") %>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <asp:Literal ID="lb_RepeaterNextPage" runat="server">
                </asp:Literal>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center">
                            <span class="PageTitle2">费用报销审批单--明细</span><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="spanRow" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr height="28px">
                                    <td nowrap>
                                        <span>
                                            报销单科目明细</span>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lb_SheetCode" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        单位：<span >元</span>
                                    </td>
                                    <td align="right">
                                        单据及附件&nbsp;共&nbsp;&nbsp;&nbsp;&nbsp;张&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID,ApplyDetailID,Client" OnRowDataBound="gv_List_RowDataBound"
                                GridLines="Both" BorderWidth="1" BorderColor="Black" CssClass="">
                                <HeaderStyle BackColor="White" CssClass="" Font-Size="12px" Height="28px" />
                                <RowStyle Height="55px" />
                                <Columns>
                                    <asp:BoundField DataField="Client" HeaderText="发生客户" SortExpression="Client" ItemStyle-Font-Size="14px" />
                                    <asp:TemplateField HeaderText="客户渠道" ItemStyle-Font-Size="14px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_RTChannel" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle"
                                        ItemStyle-Font-Size="14px" />
                                    <asp:TemplateField HeaderText="申请报销额" ItemStyle-Font-Size="14px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_WriteOffCost" Text='<%# Math.Round((decimal)Eval("WriteOffCost"),1).ToString("0.##")%>'
                                                runat="server" Visible='<%#(decimal)Eval("WriteOffCost")!=0 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="总费用" ItemStyle-Font-Size="14px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_AllCost" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BeginMonth" HeaderText="发生月份" SortExpression="BeginMonth"
                                        ItemStyle-Font-Size="14px" />
                                    <asp:TemplateField HeaderText="备注" ItemStyle-Font-Size="9px" SortExpression="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Remark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请批复号" SortExpression="ApplyDetailID" ItemStyle-Font-Size="9px"
                                        ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_ApplySheetCode" runat="server" Text=""></asp:Label><br />
                                            <asp:Label ID="lb_ContractCode" runat="server" Text=""></asp:Label><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="费用所属品类" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_RelateBrand" runat="server" Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="批复报销额" Visible="false" ItemStyle-Font-Size="14px">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_CanWriteOffCost"  runat="server" Text='<%# Math.Round((decimal)DataBinder.Eval(Container,"DataItem.WriteOffCost")+
                                        (decimal)DataBinder.Eval(Container,"DataItem.AdjustCost"),1).ToString("0.##")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AdjustCost" HeaderText="扣减额" DataFormatString="{0:0.#}"
                                        HtmlEncode="False" SortExpression="AdjustCost" Visible="false" />
                                    <asp:BoundField DataField="AdjustMode" HeaderText="扣减方式" SortExpression="AdjustMode"
                                        Visible="false" />
                                    <asp:BoundField DataField="AdjustReason" HeaderText="扣减原因" SortExpression="AdjustReason"
                                        Visible="false" />
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
                                    <td align="middle" width="100px">
                                        小计金额(大写)
                                    </td>
                                    <td width="50%">
                                        &nbsp;<asp:Label ID="lb_SubTotalCostCN" runat="server" Font-Size="14px" Font-Bold="true"
                                            Text=""></asp:Label>
                                    </td>
                                    <td align="middle" width="100px">
                                        小计金额(小写)
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label ID="lb_SubTotalCost" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr height="25px">
                                    <td align="right" width="100px">
                                        备注
                                    </td>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td align="right" width="100px">
                                        领导审批
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="15" align="left" font-size="12px">
                                        会计主管
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" align="left" font-size="12px">
                                        复核
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" align="left" font-size="12px">
                                        出纳
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" align="left" font-size="12px">
                                        报销人
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" align="left" font-size="12px">
                                        领款人
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            打印时间：<%=System.DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") %>&nbsp;
                        </td>
                    </tr>
                    <tr height="5px">
                        <td>
                            &nbsp;<hr />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <table id="tb_EvectionRouteList" runat="server" visible="false" border="0" cellpadding="0"
            cellspacing="0" width="100%">
            <tr>
                <td>
                    <div class="PageNext">
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <span class="PageTitle2">费用报销审批单--差旅行程及用车明细</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="spanRow" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr height="28px">
                                        <td nowrap>
                                            <span>
                                                差旅行程及用车明细</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <mcs:UC_GridView ID="gv_EvectionRouteList" runat="server" Width="100%" AutoGenerateColumns="False"
                                    PanelCode="Panel_FNA_EvectionRouteList_01" AllowPaging="false" GridLines="Both"
                                    BorderWidth="1" BorderColor="Black" CssClass="">
                                    <HeaderStyle BackColor="White" CssClass="" />
                                    <Columns>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
                                </mcs:UC_GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="PageNext">
    </div>
    <div id="detailPrint" runat="server" visible="false">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <span style="text-align: center; font-size:  x-large;">
                        <asp:Label ID="lbl_message" runat="server" Text="lbl_message"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    打印时间：<%=System.DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") %>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="33%" align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;抵货款类型：<label id="div_type0" runat="server"></label>
                            </td>
                            <td width="33%" align="center">
                                核销单号：<label id="div_SheetCode" runat="server"></label>
                            </td>
                            <td width="34%" align="center">
                                &nbsp;&nbsp;&nbsp;单位：元
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="spanRow" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr height="28px">
                            <td nowrap>
                                <span>
                                    报销单科目明细</span>
                            </td>
                            <td align="right">
                                第 1 页，共 1 页&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" 
                        AutoGenerateColumns="False" DataKeyNames="ID,ApplyDetailID,Client" OnRowDataBound="gv_ListDetail_RowDataBound"
                        GridLines="Both" BorderWidth="1" BorderColor="Black" CssClass="" 
                        Binded="True">
                        <HeaderStyle BackColor="White" CssClass="" />
                        <Columns>
                            <asp:BoundField DataField="Client" HeaderText="发生客户" SortExpression="Client" />
                            <asp:TemplateField HeaderText="客户渠道">
                                <ItemTemplate>
                                    <asp:Label ID="lb_RTChannel" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle" />
                            <asp:TemplateField HeaderText="申请报销额">
                                <ItemTemplate>
                                    <asp:Label ID="lb_WriteOffCost" Text='<%# ((decimal)Eval("WriteOffCost")).ToString("0.#")%>'
                                        runat="server" Visible='<%#(decimal)Eval("WriteOffCost")!=0 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总费用">
                                <ItemTemplate>
                                    <asp:Label ID="lb_AllCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BeginMonth" HeaderText="月份" SortExpression="BeginMonth"
                                ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lb_Remark" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请批复号" ItemStyle-Width="180px" SortExpression="ApplyDetailID">
                                <ItemTemplate>
                                    <asp:Label ID="lb_ApplySheetCode" runat="server" Text="" Width="180px"></asp:Label>
                                </ItemTemplate>

<ItemStyle Width="180px"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="费用所属品类">
                                <ItemTemplate>
                                    <asp:Label ID="lb_RelateBrand" runat="server" Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="批复报销额" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lb_CanWriteOffCost"  runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.WriteOffCost")+
                                        (decimal)DataBinder.Eval(Container,"DataItem.AdjustCost")).ToString("0.#")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjustCost" HeaderText="扣减额" DataFormatString="{0:0.#}"
                                HtmlEncode="False" SortExpression="AdjustCost" Visible="false" />
                            <asp:BoundField DataField="AdjustMode" HeaderText="扣减方式" SortExpression="AdjustMode"
                                Visible="false" />
                            <asp:BoundField DataField="AdjustReason" HeaderText="扣减原因" SortExpression="AdjustReason"
                                Visible="false" />
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
                                &nbsp;<asp:Label ID="lab_TotalCostCN" runat="server" Font-Size="16px" Font-Bold="true"
                                    Text=""></asp:Label>
                            </td>
                            <td align="right" width="100px">
                                小计金额(小写)
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lab_TotalCost" runat="server" Text=""></asp:Label>
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
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;签 收 日 期：
                    </td>
                </tr>
                <tr height="30px">
                    <td>
                        ④ 营业部主管：
                    </td>
                    <td>
                        ⑤ 营业部出纳：
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
        <br />
        <div class="PageNext">
        </div>
        <br />
        <table>
            <tr>
                <td style="text-align: center; font-size: x-large">
                    代 垫 费 用<b><label id="div_type" runat="server"></label></b>明 细 表 回 执
                </td>
            </tr>
            <tr>
                <td style="font-size: large;">
                    <label id="div_client" style="text-decoration: underline; font-weight: bold" runat="server">
                    </label>
                    <label id="div_insType" runat="server">
                    经销商：
                    </label>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp; 本次收到<label id="label_insName" runat="server">贵公司</label>代垫费用单据，费用产生期间：________________，金额：__________；<br />
                    <br />
                    经我营业部出纳初次复核，可以寄往总部<b><label id="div_type2" runat="server"></label></b>的金额为：____________，<br />
                    <br />
                    退回不合规单据金额为：___________，退回不合规 单据：_____________份。<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp; 以上符合规定的单据寄往总部后，由财务部确认<b><label id="div_type3" runat="server"></label></b>相关款项。<br />
                    <br />
                    <br />
                    <span style="margin-left: 200px">营业部出纳签名： </span>
                    <br />
                    <br />
                    <span style="margin-left: 200px">营业部出纳联系电话： </span>
                    <br />
                    <br />
                    <span style="margin-left: 200px">日 期：</span>
                    <br />
                    <br />
                    <br />
                    <span style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        注：本表一式二联，由<label id="label_insName3" runat="server">经销商</label>填写后交业务员核对签收，一联交<label id="label_insName2" runat="server">经销商</label>留存，一联作票据传递记录及寄回总公司报账之用</span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
