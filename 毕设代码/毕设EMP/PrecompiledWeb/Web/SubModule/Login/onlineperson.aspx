<%@ page language="c#" inherits="OnlinePerson, App_Web_atkk77yx" stylesheettheme="basic" enableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>������Ա</title>
       <script language="JavaScript"  type="text/javascript">
        function SendMsg(username, realname) {
            var w;
            if (username != '')
                w = window.showModalDialog('../OA/SM/MsgSender.aspx?SendTo='+username+'&SendToRealName='+ realname, window,'dialogWidth:600px;dialogheight:350px;toolbar=no;status=no,;scrollbars=yes;resizable=yes');
            else
                w = window.showModalDialog('../OA/SM/MsgSender.aspx', window, 'dialogWidth:600px;dialogheight:350px;toolbar=no;status=no,;scrollbars=yes;resizable=yes');
           
        }

    </script>
</head>
<body>
    <form method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left" width="150">
                            <h2>
                                �����û���ϸ��Ϣ</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btSendAllSM" runat="server" Text="��ȫ�巢�Ͷ�Ѷ" OnClick="btSendAllSM_Click"
                                />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gv_List" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnSorting="gv_List_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Username" HeaderText="�û���" SortExpression="Username" />
                        <asp:BoundField DataField="RealName" HeaderText="����" SortExpression="RealName" />
                        <asp:BoundField DataField="LoginTime" HeaderText="��¼ʱ��" SortExpression="LoginTime"
                            HtmlEncode="false" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="OnlineSpan" HeaderText="����ʱ��" SortExpression="OnlineSpan"
                            HtmlEncode="false" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="OrganizeCityName" HeaderText="����Ƭ��" SortExpression="OrganizeCityName" />
                        <asp:BoundField DataField="IpAddr" HeaderText="IP��ַ" SortExpression="IpAddr" />
                        <asp:BoundField DataField="IPLocation" HeaderText="��½����λ��" SortExpression="IPLocation" />
                        <asp:BoundField DataField="PositionName" HeaderText="ְλ" SortExpression="PositionName" />
                        <asp:BoundField DataField="ActiveModuleName" HeaderText="λ��" SortExpression="ActiveModuleName" />
                        <asp:HyperLinkField DataNavigateUrlFields="Username,RealName" ControlStyle-CssClass="listViewTdLinkS1"
                            DataNavigateUrlFormatString="../OA/SM/MsgSender.aspx?SendTo={0}&SendToRealName={1}"
                            Text="���Ͷ�Ѷ" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
