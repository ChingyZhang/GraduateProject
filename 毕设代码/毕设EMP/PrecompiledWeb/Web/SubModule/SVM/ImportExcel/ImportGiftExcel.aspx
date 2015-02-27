<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_ImportExcel_ImportGift, App_Web_v4fiyk14" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="从Excel导入零售商赠品进货"></asp:Label></h2>
                        </td>
                        <td>
                            <font color="red">归属会计月份:<asp:Label ID="lb_MonthTitle" runat="server" Text=""></asp:Label></font>
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
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        <asp:Label ID="lb_setp1" runat="server" Text=" 第一步：下载零售商赠品进货模板"></asp:Label></h3>
                                                </td>
                                                <td>
                                                    <h3>
                                                        特别提醒：在操作以下步骤时，请先阅读本页最下方说明，以免出现操作错误，导致导入不成功！</h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabForm">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td class="dataLabel" width="80">
                                                    责任业代
                                                </td>
                                                <td class="dataField" width="400">
                                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                                        Width="260px" OnSelectChange="select_Staff_SelectChange" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.下载模板" Width="100px" OnClick="bt_DownloadTemplate_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:UpdatePanel ID="udp_product" runat="server">
                                                        <ContentTemplate>
                                                            <div id="div_gift" align="center" runat="server" style="overflow: scroll; height: 500px"
                                                                visible="false">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <mcs:MCSTabControl ID="MCSTabControl2" runat="server" Width="100%" SelectedIndex="0"
                                                                                OnOnTabClicked="MCSTabControl2_OnTabClicked">
                                                                                <Items>
                                                                                    <mcs:MCSTabItem Text="成品赠品" Value="1" />
                                                                                    <mcs:MCSTabItem Text="试用装" Value="2" />
                                                                                    <mcs:MCSTabItem Text="高值促销品" Value="3" />
                                                                                </Items>
                                                                            </mcs:MCSTabControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tabForm">
                                                                        <td>
                                                                            <mcs:UC_GridView ID="gv_product" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                Binded="False" ConditionString="" DataKeyNames="PDT_Product_ID" PageSize="20"
                                                                                PanelCode="Panel_PDT_List_001" Width="100%">
                                                                                <Columns>
                                                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged"
                                                                                                Text="全选" Width="60px" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_ID" runat="server" AutoPostBack="true" Checked='<%#ProdutctGifts.ToString().IndexOf(Eval("PDT_Product_ID").ToString()+",")!=-1||Testers.ToString().IndexOf(Eval("PDT_Product_ID").ToString()+",")!=-1||Gifts.ToString().IndexOf(Eval("PDT_Product_ID").ToString()+",")!=-1 %>'
                                                                                                OnCheckedChanged="chk_ID_CheckedChanged" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    无数据。。。
                                                                                </EmptyDataTemplate>
                                                                            </mcs:UC_GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl2" EventName="OnTabClicked" />
                                                            <asp:AsyncPostBackTrigger ControlID="select_Staff" EventName="SelectChange" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        <asp:Label ID="lb_setp2" runat="server" Text="  第二步：上传零售商赠品进货Excel表格"></asp:Label></h3>
                                                </td>
                                                <td nowrap>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabForm">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td class="dataLabel" width="80">
                                                    Excel文件
                                                </td>
                                                <td class="dataField" width="380px">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                                                </td>
                                                <td align="left" width="120">
                                                    <asp:Button ID="bt_UploadExcel" runat="server" Text="2.上传Excel" Width="100px" OnClick="bt_UploadExcel_Click" />
                                                </td>
                                                <td align="left">
                                                    &nbsp;
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
                                        <asp:Literal ID="lb_ErrorInfo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000; font-weight: bold">提示：</span><br />
                <span style="color: #0000FF" runat="server" id="rtgiftprompt">一、操作步骤<br />
                    必须严格按顺序操作：<br />
                    第1步：先进行赠品筛选，即仅需筛选出零售商本期有管理的本品赠品、试用装、高值异质赠品的品名（以下简称3类赠品）；<br />
                    第2步：导出一个工作簿模板（按3类赠品分3个工作表）；<br />
                    第3步：依导出的模板上分别在3个工作表中填录零售商赠品进货数据并上传至系统；<br />
                    第4步：经复核无误确认提交（3个工作表将同时被提交），系统自动流转至办事处主管审批。<br />
                    二、关于模板的使用<br />
                    1.模板导出时间：每月21日-25日，导出上一会计月工作簿模板，以保证会计月内零售商及赠品品名数据完整及准确性。<br />
                    2.模板格式不得调整：<br />
                    A.导出的模板中“门店ID”“门店名称”“ 门店分类”“ 会计月”“ 赠品名称”（即模板中已存在的数据信息）不得更改，包括不得新增或删除零售商或赠品 名称。<br />
                    B.Excel模板中，包括3个工作表，请勿删除或更改工作表名称。<br />
                    C.填写数据模板时如果将个别行列设置了”隐藏”，请务必在上传模板之前将这些行列”取消隐藏”，否则影响数据导入完整性。<br />
                    D.上传系统数据模板请确认模板无携带“宏”文件，否则可能导致数据导入报错。<br />
                    3.在遵循以上两点操作的基础上，仅需将零售商3类赠品进货分别填入3个工作表中，保存后即可一次性上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                    4.填录赠品进货数量时，必须按赠品最小包装单位填录（即按罐、袋、盒等最小单位），某一赠品无数量时，可以空着，不用填0。<br />
                    5.同一会计月份、同一零售商、同一赠品多笔进货数据需做合并，一次性填入模板。<br />
                    三、数据导入规则<br />
                    1.同一会计月份、同一零售商、同一赠品进货数据，如有多次数据导入，系统只默认最后一次正确导入的数据。<br />
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。<br /> </span><span style="color: #0000FF"
                        runat="server" id="rtproductprompt" visible="false">一、操作步骤：<br />
                        必须严格按顺序操作：<br />
                        第1步：先从EMP系统导出一个工作簿模板（含“零售商进货”、“零售商销货”共2个工作表）；<br />
                        第2步：依导出的模板上分别在2个工作表中填录数据并上传至系统；<br />
                        第3步：经复核无误确认提交（2个工作表将同时被提交），系统自动流转至办事处主管审批。<br />
                        二、关于模板的使用<br />
                        1.模板导出时间：每月21日-25日，导出上一会计月工作簿模板，以保证会计月零售商、导购员及产品名称的数据完整及准确性。<br />
                        2.模板格式不得调整：<br />
                        A.导出的模板中“门店ID”“门店名称”“门店分类”“会计月”“导购ID”“导购姓名”“产品名称”（即模板中已存在的数据信息）不得更改，包括不得新增或删除零售商、导购员及产品名称。<br />
                        B.Excel模板中，包括2个工作表，分别为【零售商进货】及【零售商销货】，请勿删除或更改工作表名称。<br />
                        C.填写数据模板时如果将个别行列设置了”隐藏”，请务必在上传模板之前将这些行列”取消隐藏”，否则影响数据导入完整性。<br />
                        D.上传系统数据模板请确认模板无携带“宏”文件，否则可能导致数据导入报错。<br />
                        3.在遵循以上两点操作的基础上，仅需将零售商的进货填入【零售商进货】、销货数量填入【零售商销货】（如零售商有多名导购员，需将零售商总销量拆分后，再对应导购员录入于模板中），保存后即可上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                        4.填录进、销数量时，必须按产品最小零售包装的产品数量填录（即按罐、袋、盒等最小单位），某一产品无销量时，可以空着，不用填0。<br />
                        5.同一零售商同一会计月份所有进货的多笔数据合并在一起，同一零售商同一导购员同一会计月所有销货的多笔数据合并在一起，均在模板中填一次即可。<br />
                        三、数据导入规则<br />
                        1.会计月内，同一零售商、同一导购、同一产品进货或销货数据，如出现多次导入，系统只默认最后一次正确导入的数据。<br />
                        2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。 </span>
                <br />
                <span style="color: #0000FF" runat="server" id="diproductprompt" visible="false">一、操作步骤：<br />
                    必须严格按顺序操作：<br />
                    第1步：先从EMP系统导出一个工作簿模板（含“分销商进货”1个工作表）；<br />
                    第2步：依导出的模板上分别在工作表中填录数据并上传至系统； 第3步：经复核无误确认提交，系统自动流转至办事处主管审批。
                    <br />
                    二、关于模板的使用
                    <br />
                    1.模板导出时间：每月21日-25日，导出上一会计月工作簿模板，以保证会计月分销商及产品名称的数据完整及准确性。<br />
                    2.模板格式不得调整：
                    <br />
                    A.导出的模板中“分销商ID”“分销商名称”“会计月”“产品名称”（即模板中已存在的数据信息）不得更改，包括不得新增或删除分销商及产品名称。<br />
                    B.Excel模板中，包括1个工作表【分销商进货】，请勿删除或更改工作表名称。
                    <br />
                    C.填写数据模板时如果将个别行列设置了”隐藏”，请务必在上传模板之前将这些行列”取消隐藏”，否则影响数据导入完整性。<br />
                    D.上传系统数据模板请确认模板无携带“宏”文件，否则可能导致数据导入报错。<br />
                    3.在遵循以上两点操作的基础上，仅需将分销商的进货填入【分销商进货】保存后即可上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                    4.填录进、销数量时，必须按产品最小零售包装的产品数量填录（即按罐、袋、盒等最小单位），某一产品无销量时，可以空着，不用填0。<br />
                    5.同一分销商同一会计月份所有进货的多笔数据合并在一起，均在模板中填一次即可。 三、数据导入规则
                    <br />
                    1.同一会计月份、同一零售商、同一赠品进货数据，如有多次数据导入，系统只默认最后一次正确导入的数据。<br />
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。</span><br />
                <span style="color: #0000FF" runat="server" id="digiftprompt" visible="false">一、操作步骤：<br />
                    必须严格按顺序操作：<br />
                    第1步：先进行赠品筛选，即仅需筛选出分销商本期有管理的本品赠品、试用装、高值异质赠品的品名（以下简称3类赠品）；<br />
                    第2步：导出一个工作簿模板（按3类赠品分3个工作表）；<br />
                    第3步：依导出的模板上分别在3个工作表中填录分销商赠品进货数据并上传至系统；<br />
                    第4步：经复核无误确认提交（3个工作表将同时被提交），系统自动流转至办事处主管审批。<br />
                    二、关于模板的使用<br />
                    1.模板导出时间：每月21日-25日，导出上一会计月工作簿模板，以保证会计月内分销商及赠品品名数据完整及准确性。<br />
                    2.模板格式不得调整：<br />
                    A.导出的模板中“分销商ID”“分销商名称”“ 会计月”“ 赠品名称”（即模板中已存在的数据信息）不得更改，包括不得新增或删除分销商或赠品名称。<br />
                    B.Excel模板中，包括3个工作表，请勿删除或更改工作表名称。<br />
                    C.填写数据模板时如果将个别行列设置了”隐藏”，请务必在上传模板之前将这些行列”取消隐藏”，否则影响数据导入完整性。<br />
                    D.上传系统数据模板请确认模板无携带“宏”文件，否则可能导致数据导入报错。<br />
                    3.在遵循以上两点操作的基础上，仅需将分销商3类赠品进货分别填入3个工作表中，保存后即可一次性上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                    4.填录赠品进货数量时，必须按赠品最小包装单位填录（即按罐、袋、盒等最小单位），某一赠品无数量时，可以空着，不用填0。<br />
                    5.同一会计月份、同一分销商、同一赠品多笔进货数据需做合并，一次性填入模板。<br />
                    三、数据导入规则<br />
                    1.同一会计月份、同一分销商、同一赠品进货数据，如有多次数据导入，系统只默认最后一次正确导入的数据。<br />
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入即可。<br />
                </span>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
