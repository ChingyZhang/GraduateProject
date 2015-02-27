<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetailerCLContract_Print.aspx.cs"
    Inherits="SubModule_CM_RT_RetailerCLContract_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
        p.MsoNormal
        {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            font-size: 10.5pt;
            font-family: "Times New Roman" , "serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        .style2
        {
            height: 346px;
        }
    </style>
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="0" cellpadding="0" width="100%" border="0"  style="text-align:left">
            <tr>
                <td align="center">
                    <asp:Label ID="lb_Header" runat="server" CssClass="style1"></asp:Label>
                    <span class="style1">陈列协议</span>
                </td>
            </tr>
            <tr>
                <td style="font-size: large;">
                    甲方：__________________<br />
                    <br />
                    乙方：<asp:Label ID="lb_client" style="text-decoration:underline" runat="server"></asp:Label>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 经甲乙双方友好协商，为加强双方合作、促进我司产品在贵处的销售，<br />
                                        使双方获得最大利润，特签订以下陈列协议<br />
                </td>
            </tr>
            <tr>
                <td style="font-size: large;" class="style2">
                    <div style="margin-left: 30px">
                        1、 陈列产品：<span style="text-decoration: underline"> 雅士利系列产品
                            <br />
                        </span>2、 陈列类型：<asp:CheckBox ID="chk_ZG" runat="server" Text="专柜" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chk_DJ" runat="server" Text="端架" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chk_DT" runat="server" Text="堆头" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chk_BZ" runat="server" Text="包柱" /><br />
                        3、 陈列时间：<asp:Label ID="lbl_time" runat="server"></asp:Label><br />
                        4、 陈列费用：人民币<asp:Label ID="lb_TotalCost" runat="server" Font-Size="16px" Font-Bold="true"  style="text-decoration:underline" 
                            Text=""></asp:Label>元/月，大写人民币：<asp:Label ID="lb_TotalCostCN" runat="server" Font-Size="16px"
                                Font-Bold="true" Text=""  style="text-decoration:underline" ></asp:Label>元；
                        <br />
                        5、 支付方式为：<input type="checkbox" />现金 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="checkbox" />帐扣，乙方提供给甲方相应正规发票！
                        <br />
                        6、 陈列要求：乙方保证甲方产品陈列丰富，陈列协议时间段内陈列位置<br />
                                                不变，
                        零售价格按照甲方要求的统一价盘执行！不得自行进行降价等行<br />
                                                为！陈列协议到期时乙方应优先与甲方继续签订陈列协议！乙方允许甲方<br />
                        相应人员对陈列位置及产品等进行拍照等检核动作。乙方允许甲方对不同<br />
                                                陈列类型做出不同的形象包装！
                        <br />
                        7、 其他补充条款（陈列位置等）：_____________________________<br />
                      _________________________________________________________<br />
                        8、 本协议一式两份，甲乙双方各执一份，双方共同遵守，其他未尽事宜<br />
                                                双方协商解决！
                        <br />
                        <br />
                        甲&nbsp;&nbsp;&nbsp; 方：_________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;乙&nbsp;&nbsp;&nbsp;&nbsp; 
                        方：<asp:Label ID="lbl_partyB" style="text-decoration:underline" runat="server"></asp:Label><br />
                        <br />
                        经办人：_________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;经办人：<asp:Label ID="lbl_SignMan" style="text-decoration:underline" runat="server"></asp:Label><br />
                        <br />
                        联系电话：________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系电话：__________________<br />
                        <br />
                        签订时间：      年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;签订时间： 
                        年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
