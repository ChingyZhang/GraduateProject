<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_ImportExcel_ImportOrganizeTarget, App_Web_v4fiyk14" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="300">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="从Excel导入重点品项目标及费率"></asp:Label></h2>
                        </td>                        
                        <td class="dataLabel">
                            <font color="red">会计月</font>
                      
                            <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                    <Items>
                        <mcs:MCSTabItem Text="下载模版" Value="1" />
                        <mcs:MCSTabItem Text="上传办事处重点品项" Value="2" />
                        <mcs:MCSTabItem Text="上传办事处费率" Value="3"  Visible="false" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tb_step1">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        <asp:Label ID="lb_setp1" runat="server" Text=" 第一步：下载办事处重点品项及费率模板"></asp:Label></h3>
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
                                                <td class="dataLabel">
                                                    品牌
                                                </td>
                                                <td class="dataField">
                                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.下载模板" Width="100px" OnClick="bt_DownloadTemplate_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tabForm" colspan="5">
                                                    <asp:CheckBoxList runat="server" RepeatColumns="12" CellPadding="2" CellSpacing="10"
                                                        RepeatDirection="Horizontal" DataTextField="Name" DataValueField="ID" ID="chk_Citylist">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:UpdatePanel ID="udp_product" runat="server">
                                                        <ContentTemplate>
                                                            <div id="div_gift" align="center" runat="server" style="overflow: scroll; height: 500px">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
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
                                                                                            <asp:CheckBox ID="chk_ID" runat="server" AutoPostBack="true" Checked='<%#Produtcts.ToString().IndexOf(","+Eval("PDT_Product_ID").ToString()+",")!=-1 %>'
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddl_Brand" EventName="selectedindexchanged" />
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
                                                        <asp:Label ID="lb_setp2" runat="server" Text="  第二步：上传办事处重点品项Excel表格"></asp:Label></h3>
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
                <span style="color: #0000FF">一、操作步骤<br />
                    必须严格按顺序操作：<br />
                    第1步：先进行重点品项筛选<br />
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
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。<br />
                </span>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
