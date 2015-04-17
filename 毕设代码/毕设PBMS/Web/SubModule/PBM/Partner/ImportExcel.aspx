<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="ImportExcel.aspx.cs" Inherits="SubModule_PBM_Product_ImportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table>
                    <tr>
                        <td width="24" nowrap="noWrap">
                            <img alt="" height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>从Excel导入经销商资料</h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="h3Row" style="height: 30px;">
            <td colspan="2">
                <h3>第一步：下载经销商模板&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;特别提醒：在操作以下步骤时，请先阅读本页最下方说明，以免出现操作错误，导致导入不成功！</h3>
            </td>
        </tr>
        <tr>
            <td class="tabForm" colspan="2">
                <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.下载模板" Width="100px" OnClick="bt_DownloadTemplate_Click" />
                <%--nClick="bt_DownloadTemplate_Click"--%>
            </td>
        </tr>
        <tr style="height: 10px;">
            <td colspan="2"></td>
        </tr>
        <tr class="h3Row" style="height: 30px;">
            <td colspan="2">
                <h3>第二步：上传经销商Excel表格&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;依导出的模板工作表中填录经销商数据并上传至系统；</h3>
            </td>
        </tr>
        <tr class="tabForm">
            <td class="dataField" width="50%">
                <span class="dataLabel">Excel文件</span>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /></td>
            <td align="left" width="120">
                <asp:Button ID="bt_UploadExcel" runat="server" Text="2.上传Excel" Width="100px" OnClick="bt_UploadExcel_Click" /></td>
        </tr>
        <tr style="height: 10px;">
            <td colspan="2"></td>
        </tr>
        <%--<tr>
            <td>
                <asp:Literal ID="lb_ErrorInfo" runat="server" Text="第2步："></asp:Literal>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="udp_product" runat="server">
                    <ContentTemplate>
                        <div id="div_gift" align="center" runat="server" style="text-align: left; overflow: auto; height: 500px;"
                            visible="false">
                        </div>
                        <%--<asp:Literal runat="server" ID="div_gift" Visible="true" Text="说明"></asp:Literal>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span style="color: #FF0000; font-weight: bold">提示：</span><br />
                <span style="color: #0000FF" runat="server" id="rtgiftprompt">一、操作步骤<br />
                    必须严格按顺序操作：<br />
                    第1步：导出一个工作簿模板；<br />
                    第2步：依导出的模板工作表中填录经销商数据并上传至系统；<br />
                    二、关于模板的使用<br />
                    2.模板格式不得调整：<br />
                    A.导出的模板（模板中已存在的数据信息）不得更改，包括不得新增或删除列。<br />
                    B.请勿删除或更改工作表名称。<br />
                    C.填写数据模板时如果将个别行列设置了”隐藏”，请务必在上传模板之前将这些行列”取消隐藏”，否则影响数据导入完整性。<br />
                    D.上传系统数据模板请确认模板无携带“宏”文件，否则可能导致数据导入报错。<br />
                    3.在遵循以上两点操作的基础上，仅需将零售商销量数据填入工作表中，保存后即可一次性上传至系统中。填录时也可通过复制粘贴“数值”至表模中。<br />
                    、
                    三、数据导入规则<br />
                    1.同一会计月份、同一零售商、同一导购销量数据，如有多次数据导入，系统只默认最后一次正确导入的数据。<br />
                    2.如出现无法导入记录，仅需对提示“未成功导入”的数据行更正后再次导入 即可(为规避重复导入，请对已成功导入的数据行先做整行删除)。<br />
                </span>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
