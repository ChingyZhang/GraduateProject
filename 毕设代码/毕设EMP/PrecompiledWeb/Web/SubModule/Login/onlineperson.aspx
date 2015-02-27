<%@ page language="c#" inherits="OnlinePerson, App_Web_atkk77yx" stylesheettheme="basic" enableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>在线人员</title>
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
                                在线用户详细信息</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btSendAllSM" runat="server" Text="给全体发送短讯" OnClick="btSendAllSM_Click"
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
                        <asp:BoundField DataField="Username" HeaderText="用户名" SortExpression="Username" />
                        <asp:BoundField DataField="RealName" HeaderText="姓名" SortExpression="RealName" />
                        <asp:BoundField DataField="LoginTime" HeaderText="登录时间" SortExpression="LoginTime"
                            HtmlEncode="false" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="OnlineSpan" HeaderText="在线时间" SortExpression="OnlineSpan"
                            HtmlEncode="false" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="OrganizeCityName" HeaderText="管理片区" SortExpression="OrganizeCityName" />
                        <asp:BoundField DataField="IpAddr" HeaderText="IP地址" SortExpression="IpAddr" />
                        <asp:BoundField DataField="IPLocation" HeaderText="登陆物理位置" SortExpression="IPLocation" />
                        <asp:BoundField DataField="PositionName" HeaderText="职位" SortExpression="PositionName" />
                        <asp:BoundField DataField="ActiveModuleName" HeaderText="位置" SortExpression="ActiveModuleName" />
                        <asp:HyperLinkField DataNavigateUrlFields="Username,RealName" ControlStyle-CssClass="listViewTdLinkS1"
                            DataNavigateUrlFormatString="../OA/SM/MsgSender.aspx?SendTo={0}&SendToRealName={1}"
                            Text="发送短讯" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
