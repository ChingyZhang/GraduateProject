<%@ page language="C#" autoeventwireup="true" inherits="SubModule_StaffManage_StaffDetail_Print, App_Web_it73ecr1" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center">
                    <asp:Label ID="lb_Header" runat="server" CssClass="style1"></asp:Label>
                    <span class="style1">员工明细</span>
                </td>
            </tr>
            <tr>
                <td>
                    <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="Page_SM_002_Print"
                        Visible="true">
                    </mcs:UC_DetailView>
                </td>
            </tr>
            <tr id="tr_StaffInOrganizeCity" runat="server">
                <td>
                   
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                            <tr>
                                                <td>
                                                    <h3>
                                                        员工兼管的其他管理片区</h3>
                                                </td>
                                                <td align="right" class="dataField">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <mcs:UC_GridView ID="gv_StaffInOrganizeCity" runat="server" Width="100%" AutoGenerateColumns="False"
                                                        DataKeyNames="ID" >
                                                        <Columns>
                                                            <asp:BoundField DataField="Name" HeaderText="名称" />
                                                            <asp:BoundField DataField="Code" HeaderText="代码" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            无数据</EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                       
                </td>
            </tr>
            <tr id="tr_LoginUser" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                    <tr>
                                        <td>
                                            <h3>
                                                员工登录帐户</h3>
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
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                                DataKeyNames="UserName" Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0">
                                                <Columns>
                                                    <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                    <asp:BoundField DataField="CreateDate" HeaderText="创建日期" SortExpression="CreationDate" />
                                                    <asp:BoundField DataField="LastLoginDate" HeaderText="最后登录日期" SortExpression="LastLoginDate" />
                                                    <asp:CheckBoxField DataField="IsApproved" HeaderText="是否有效" SortExpression="IsOnline" />
                                                    <asp:CheckBoxField DataField="IsLockedOut" HeaderText="是否锁定" SortExpression="IsLockedOut" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据</EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
