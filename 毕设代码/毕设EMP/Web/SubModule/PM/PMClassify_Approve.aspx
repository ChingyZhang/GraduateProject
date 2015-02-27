<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMClassify_Approve.aspx.cs"
    Inherits="SubModule_PM_PMClassify_Approve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导购员类别转换流程维护</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: 33px">
                    <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                        <tr>
                            <td width="24">
                                <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                            </td>
                            <td nowrap="noWrap" style="width: 180px">
                                <h2>
                                    导购员类别转换流程维护</h2>
                            </td>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="确认发起流程" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr><td><input name="BeginWorkDate" type="text" id="BeginWorkDate" onfocus="setday(this);" style="width:100px;" /><span style="color:Red;">&nbsp;&nbsp;*</span><span id="ctl21" style="color:Red;display:none;">必填</span><span id="ctl22" style="color:Red;display:none;">日期格式不正确</span></td></tr>--%>
        </table>
        <mcs:UC_DetailView ID="DV_pm" runat="server" DetailViewCode="PMClassify_Approve">
        </mcs:UC_DetailView> 
        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="Page_PM_003">
        </mcs:UC_DetailView>
    </div>
    </form>
</body>
</html>
