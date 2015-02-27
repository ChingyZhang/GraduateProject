<%@ page language="C#" autoeventwireup="true" inherits="SubModule_CM_Search_SelectClient, App_Web_ry5zmltj" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>��ҵ�ͻ���ѯѡ��</title>
    <base target="_self" />

    <script language="javascript" type="text/javascript">
    <!--
        function f_ReturnValue(value) {
            window.returnValue = value;
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
                                <td>
                                    ��ѯ������<asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">�ͻ����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName" Selected="True">�ͻ�ȫ��</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.ShortName">�ͻ����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum">�绰����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">�ͻ���ַ</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_LinkMan.Name">��Ҫ��ϵ��</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td height="35px">
                                    ����Ƭ����<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" />
                                    &nbsp;&nbsp;�ͻ����ͣ�<asp:DropDownList ID="ddl_ClientType" runat="server" DataTextField="Value"
                                        DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="bt_Find" runat="server" Text="��ѯ" Width="60px" OnClick="bt_Find_Click" />
                                   <%-- <input onclick="f_ReturnValue()" type="button" class="button" value="ȷ��ѡ��" style="width: 60px" visible="false" />--%>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_value" runat="server" Width="0px" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="tbx_text" runat="server" Width="0px" Enabled="false"></asp:TextBox>
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
                                        DataKeyNames="CM_Client_ID,CM_Client_FullName" PageSize="10" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                        PageIndex="0" PanelCode="Panel_CM_PopSearchList" onrowdatabound="gv_List_RowDataBound" 
                                        >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button Text="ѡ��" ID="bt_select" CssClass="listViewTdLinkS1" runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
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
