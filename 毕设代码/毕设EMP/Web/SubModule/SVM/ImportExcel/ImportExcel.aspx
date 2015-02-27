<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ImportExcel.aspx.cs" Inherits="SubModule_SVM_ImportExcel_ImportExcel" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="从Excel导入零售商进货及销货数量"></asp:Label></h2>
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
                                                        第一步：下载门店销量模板</h3>
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
                                                <td class="dataField" width="500">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                                                Width="260px" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.下载销量模板" Width="100px"
                                                        OnClick="bt_DownloadTemplate_Click" />
                                                </td>
                                                <td>
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
                                                        第二步：上传门店销量Excel表格</h3>
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
                                                    <asp:Button ID="bt_Import" runat="server" Text="3.确认提交" Width="100px" OnClick="bt_Import_Click"
                                                        OnClientClick="return confirm(&quot;请确认上传的Excel文件数据准确，确认提交导入？&quot;)" />
                                                </td>
                                                <td>
                                                    <span style="color: #FF0000">第三步需确认上传Excel文件数据准确后，再点击"确认提交"按钮。</span>
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
                                <tr>
                                    <td>
                                        <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
                                                <ContentTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                                                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                                                    <Items>
                                                                        <mcs:MCSTabItem Text="零售商进货" Value="1" />
                                                                        <mcs:MCSTabItem Text="零售商销货" Value="2" />
                                                                    </Items>
                                                                </mcs:MCSTabControl>
                                                            </td>
                                                        </tr>
                                                        <tr class="tabForm">
                                                            <td>
                                                                <mcs:UC_GridView ID="gv_SellIn" runat="server" Width="100%" AutoGenerateColumns="true"
                                                                    OnDataBound="gv_Sell_DataBound" DataKeyNames="序号" OnRowDeleting="gv_SellIn_RowDeleting">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteText="删除" />
                                                                    </Columns>
                                                                </mcs:UC_GridView>
                                                                <mcs:UC_GridView ID="gv_SellOut" runat="server" Width="100%" AutoGenerateColumns="true"
                                                                    OnDataBound="gv_Sell_DataBound" DataKeyNames="序号" OnRowDeleting="gv_SellOut_RowDeleting">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteText="删除" />
                                                                    </Columns>
                                                                </mcs:UC_GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
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
                <span style="color: #0000FF">一、操作步骤：<br />
                    必须严格按顺序操作：<br />
                    第1步：先从EMP系统导出一个工作簿模板（含“零售商进货”、“零售商销货”共2个工作表）；<br />
                    第2步：依导出的模板上分别在2个工作表中填录数据并上传至系统；<br />
                    第3步：经复核无误确认提交（2个工作表将同时被提交），系统自动流转至办事处主管审批。<br />
                    二、关于模板的使用<br />
                    1.模板导出时间：每月21日-25日，导出上一会计月工作簿模板，以保证会计月零售商、导购员及产品名称的数据完整及准确性。<br />
                    2.模板格式不得调整：<br />
                    A.导出的模板中“门店ID”“门店名称”“门店分类”“会计月”“导购ID”“导购姓名”“产品名称”（即模板中已存在的数据信息）不得更改，包括不得新增或删除零售商、导购员及产品名称。<br />
                    B.Excel模板中，包括2个工作表，分别为【零售商进货】及【零售商销货】，请勿删除或更改工作表名称。<br />
                    3.在遵循以上两点操作的基础上，仅需将零售商的进货填入【零售商进货】、销货数量填入【零售商销货】（如零售商有多名导购员，需将零售商总销量拆分后，再对应导购员录入于模板中），保存后即可上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                    4.填录进、销数量时，必须按产品最小零售包装的产品数量填录（即按罐、袋、盒等最小单位），某一产品无销量时，可以空着，不用填0。<br />
                    5.同一零售商同一会计月份所有进货的多笔数据合并在一起，同一零售商同一导购员同一会计月所有销货的多笔数据合并在一起，均在模板中填一次即可。<br />
                    三、数据导入规则<br />
                    1.会计月内，同一零售商、同一导购、同一产品进货或销货数据，如出现多次导入，系统只默认最后一次正确导入的数据。<br />
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。 </span>
                <br />
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 50;
        divGridView.style.height = window.screen.availHeight - 500;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
