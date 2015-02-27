<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Search_SelectPromotor.aspx.cs"
    Inherits="SubModule_PM_Search_SelectPromotor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>导购员选择</title>
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
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr class="tabForm">
                    <td>
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td height="35px">
                                    管理片区：<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_Promotor.dbo.PM_Promotor.Name">姓名</asp:ListItem>
                                        <asp:ListItem Value="MCS_Promotor.dbo.PM_Promotor.Code">代码</asp:ListItem>
                                        <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_Promotor.dbo.PM_Promotor.ExtPropertys,'|',16)">固定电话</asp:ListItem>
                                        <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_Promotor.dbo.PM_Promotor.ExtPropertys,'|',17)">手机号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">所在终端门店代码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName">所在终端门店名称</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
                                    <input onclick="f_ReturnValue()" type="button" value="确定选择" class="button" style="width: 60px" />
                                    <asp:TextBox ID="tbx_value" runat="server" Width="0px" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="tbx_text" runat="server" Width="0px" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right">
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
                                        PanelCode="Panel_PM_List_002" DataKeyNames="PM_Promotor_ID,PM_Promotor_Name" PageSize="10" Width="100%"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" PageIndex="0">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
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
    </form>
</body>
</html>
