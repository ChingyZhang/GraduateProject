<%@ page language="C#" autoeventwireup="true" inherits="SubModule_PopShowProcessDetail, App_Web_drecnu3z" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>填报进度表-未完成详情</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
             <tr>
               <td width="24"><img height="16" src="../DataImages/ClientManage.gif" width="16" /></td>
               <td nowrap="noWrap"><h2>填报进度表详情</h2></td>
               <td align="right" nowrap="noWrap">
               <asp:Button ID="bt_Refresh" runat="server" Text="刷新" Width="80px" /></td>
             </tr>
             <tr>
             <td colspan="3" class="h3Row" height="30">
             <h3><p id="p_headr" runat="server">陈列费用申请门店数-未完成</p></h3>
             </td>
             </tr>
        </table>
        <mcs:UC_GridView ID="gv_Detail" runat="server" AllowPaging="true" PageSize="10" 
            Width="100%" AutoGenerateColumns="true" Binded="false" 
            EmptyDataText="&quot;无数据&quot;" 
            onpageindexchanging="gv_Detail_PageIndexChanging">
        </mcs:UC_GridView>
    </div>
    </form>
</body>
</html>
