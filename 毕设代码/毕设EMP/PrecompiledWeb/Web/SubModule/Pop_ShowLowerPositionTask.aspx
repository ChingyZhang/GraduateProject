<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Pop_ShowLowerPositionTask, App_Web_drecnu3z" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
             <tr>
               <td width="24"><img height="16" src="../DataImages/ClientManage.gif" width="16" /></td>
               <td nowrap="noWrap"><h2>下游审批详情详情</h2></td>
               <td align="right" nowrap="noWrap"></td>
             </tr>
             <tr><td>&nbsp;</td></tr>
             <tr>
             <td colspan="3" class="h3Row">
             <h3 id="p_headr" runat="server">陈列费用申请门店数-未完成</h3>
             </td>
             </tr>
        </table>
        <mcs:UC_GridView ID="gv_Detail" runat="server" AllowPaging="True" 
            Width="100%" Binded="False" AutoGenerateColumns="false"
            EmptyDataText="&quot;无数据&quot;" 
            onpageindexchanging="gv_Detail_PageIndexChanging" ConditionString="" 
             OrderFields="" PanelCode="" TotalRecordCount="0">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="StaffCode" HeaderText="员工编码" ReadOnly="True" 
                    SortExpression="StaffCode" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="RealName" HeaderText="员工姓名" ReadOnly="True" 
                    SortExpression="RealName" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Position" HeaderText="职位" ReadOnly="True" 
                    SortExpression="Position" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Mobile" HeaderText="手机号码" ReadOnly="True" 
                    SortExpression="Mobile" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" ReadOnly="True" 
                    SortExpression="OrganizeCity" />
                <asp:BoundField DataField="TaskCount" HeaderText="待审核流程数" ReadOnly="True" 
                    SortExpression="TaskCount" HeaderStyle-Wrap="false" />
            </Columns>
        </mcs:UC_GridView>
    </div>
    </form>
</body>
</html>
