<%@ page language="C#" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerFLContract_Print, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门店无导返利协议打印</title>
    <style type="text/css">
        #div_Content p label
        {
            border-bottom: 1px solid #000;
        }
        #div_Content p b
        {
            font: 黑体,幼圆,宋体;
        }
        #div_Content p font
        {
            color: Blue;
        }
        table td
        {
            font-size: 12pt;
            height: 40px;
        }
    </style>
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div style="width: auto; text-align: center; font-size: 12pt;">
        <hr />
        <span style="font-size: xx-large">返 利 协 议</span>
        <br />
        <span style="font-size: 9pt">（适用于无导购终端门店）</span>
        <br />
        <div style="float: right; margin-right: 10px;">
            编号:<asp:Label ID="lb_Code" runat="server" Width="190px" Font-Size="Smaller">123</asp:Label>
        </div>
        <br />
        <div id="divHeader" align="left" style="width: auto; padding-left: 20px;">
            <span>甲方：<label id="lb_Supplier" runat="server" width="300px"></label></span>&nbsp;<span>
                (经销商名称)</span><br />
            <span>乙方：<label id="lb_Client" runat="server">
            </label>
            </span>&nbsp;&nbsp;&nbsp;&nbsp;<span> (无导购终端门店名称)</span></div>
        <div id="div_Content" style="width: auto; padding-left: 20px;" align="left">
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;经甲乙双方友好协商，为加强双方合作、促进双方共赢，特签订以下协议：</p>
            <p>
                <b>一、协议有效期</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本协议有效期限从
                <label id="lb_StartTime" runat="server">
                    XXXX年XX月XX日</label>
                至
                <label id="lb_EndTime" runat="server">
                    XXXX年XX月XX日</label>
                止（本协议另有约定的情况除外）。</p>
            <p>
                <b>二、进货及政策标准</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、本协议签订后，甲方即可根据乙方的需求开始送货，但为确保乙方店内产品日期的新</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 鲜度，避免积压。乙方会计月度进货限定额度(进货价格合计)为：___________元以内，</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 如需调整月度限定额度则由双方另行商议评定。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、返利标准如下：</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A.非积分产品
                <label id="lbl_fjfjp" runat="server">
                </label>
                在限</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 定的进货额度内进货，可享受甲方按进货价盘给予的
                <label id="lbl_fjffl" runat="server">
                </label>
                %的非积分返利；</p>
            <p>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                其中
                <label id="lbl_fjfxdcp" runat="server">
                </label>
                产品在限定的进货额度内，可享受按进货价盘给予的</p>
            <p>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 <label id="lbl_fjfxdcpfl" runat="server">
                </label>
                %的非积分返利。&nbsp;&nbsp; </p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; B.积分产品
                <label id="lbl_jfcp" runat="server">
                </label>
                可享受甲方按进货<p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 价盘给予的
                <label id="lbl_jffl" runat="server">
                </label>
                %的非积分返利，其中
                <label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>产品在限定的进货额度内，<p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 可享受按进货价盘给予的<label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>%的非积分返利。&nbsp;&nbsp;
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、 其它说明。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A.<span
                    style="mso-spacerun: 'yes'; font-size: 11.0000pt; font-family: '宋体';">乙方会计月度进货合计超过甲方限定进货额度的，则按限定进货额给予返利；<o:p></o:p></span></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; B.
                <label id="lbl_wfljf" runat="server">
                </label>
                产品不享有非积分返利。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4、无退货政策：自乙方签订协议之日起。</p>
            <%-- <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font>A:若有换货:则换货部分不再重复享受返利及赠品;</font></p>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font>B:若有退货:则丙方需在退货款中扣除丙方退货额的返利(退货返利率依照进货时返利率计算)，及退还赠品；</font></p>
            <p><font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注：1）以上返利不重复累计；2）以上产品金额由乙方提供，具体系以乙方销售给丙方且经丙方确认的供价计算。</font></p>--%>
            <p>
                <b>三、甲方权利和义务</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、甲方可以在乙方店铺优先选择陈列位置且不小于_______ 个陈列面，甲方可以做形象陈列和相关促销活动；</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、甲方应按乙方要求及时供货给乙方店铺，若不能及时送达，应提前通知乙方并承担迟延责任；</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、若乙方违反本协议，甲方有权扣减返利、取消返利政策和解除本协议。</p>
            <p>
                <b>四、乙方的权利和义务</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、乙方保证其提供给甲方数据的真实性和准确性，否则，乙方应承担相应责任。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、乙方保证在协议有效期内按甲方要求销售产品。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、乙方保证不得串货、乱价，若有变动应取得甲方书面认可后执行，否则甲方有权拒绝支付返利。</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4、对甲方逾期送货，乙方有权追究甲方迟延送货责任。</p>
            <p>
                <b>五、乙方相关开户资料信息</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 乙方收款人姓名：
                <label id="lb_Client2" runat="server">
                    110</label>
                ；乙方收款人银行帐号/卡号：<label id="lb_ClientAccount" runat="server">110</label>
                ；开户行全称：<label id="lb_AccountName" runat="server">110</label>
                ； （备注：若乙方提供信息有误导致返利</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 无法到账，乙方自行承担责任。乙方须确保协议签订人即是收款人。）</p>
            <p>
                <b>六、支付方式</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;在乙方提供收款人姓名、开户行全称和账（卡）号前提下，甲方每月4号前通过银行汇款足额支付给乙方上月返利。</p>
            <p>
                <b>七、协议效力及补充</b></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本协议经甲乙双方代表签字并加盖公章后生效。因本协议发生的任何争议和纠纷，双方协商解决。</p>
            <p>
                <b>八、自本协议签章之日起，前期已签订的其它返利协议自动失效，以本协议为准。</b></p>
            <p>
                <b>九、本合同一式两份，甲、乙各执一份。</b></p>
        </div>
        <table style="text-align: left; margin-left: 15pt; width: 98%; font-size: 12pt;"
            cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 甲方：<label runat="server" id="lb_Supplier2"></label>
                </td>
                <td width="33%">
                    乙方：<label runat="server" id="lb_Client3"></label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 联系电话：
                </td>
                <td>
                    联系电话：
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 签字：
                </td>
                <td>
                    签字：
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
