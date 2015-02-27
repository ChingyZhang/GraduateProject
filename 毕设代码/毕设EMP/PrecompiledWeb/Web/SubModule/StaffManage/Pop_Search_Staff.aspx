<%@ page language="C#" autoeventwireup="true" inherits="SubModule_StaffManage_Pop_Search_Staff, App_Web_it73ecr1" enableEventValidation="false" stylesheettheme="basic" %>

<%@ OutputCache VaryByParam="*" Duration="1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司员工查询选择</title>
    <base target="_self" />

    <script language="javascript" type="text/javascript">
    <!--
        function f_ReturnValue() {
            window.returnValue = document.all["tbx_value"].value + "|" + document.all["tbx_text"].value;
            window.close();
        }
    -->
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr class="tabForm">
                        <td>
                            <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        查询条件：<asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                            <asp:ListItem Value="MCS_SYS.dbo.Org_Staff.RealName">员工姓名</asp:ListItem>
                                            <asp:ListItem Value="MCS_SYS.dbo.Org_Staff.StaffCode">员工工号</asp:ListItem>
                                            <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_SYS.dbo.Org_Staff.ExtPropertys,'|',5)">联系电话</asp:ListItem>
                                            <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_SYS.dbo.Org_Staff.ExtPropertys,'|',6)">手机</asp:ListItem>
                                            <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_SYS.dbo.Org_Staff.ExtPropertys,'|',7)">电子邮件</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                        <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查询" Width="60px" />
                                        &nbsp;<input class="button" onclick="f_ReturnValue()" style="width: 60px" type="button"
                                            value="确定选择" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbx_text" runat="server" Enabled="false" Width="0px"></asp:TextBox>
                                        <asp:TextBox ID="tbx_value" runat="server" Enabled="false" Width="0px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="35px">
                                        管理片区：<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" Width="200px" RootValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="35px">
                                        员工职位：<mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" RootValue="0" Width="200px" />
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="cb_IncludeChild" runat="server" Text="包含下级职位" Checked="True" Style="color: #FF0000" />
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
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataKeyNames="Org_Staff_ID,Org_Staff_RealName" PageSize="10" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                            PageIndex="0" PanelCode="Panel_Staff_PopSearchList" OnRowCreated="gv_List_RowCreated">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="cb_select" AutoPostBack="true" OnCheckedChanged="cb_select_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                无数据
                                            </EmptyDataTemplate>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
