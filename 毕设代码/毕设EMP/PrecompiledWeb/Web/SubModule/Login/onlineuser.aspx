<%@ page language="c#" inherits="OnlineUser, App_Web_atkk77yx" stylesheettheme="basic" enableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>Online</title>
    <base target="_self">
    </base>

    <script language="javascript">
        //        function opendialwin() {
        //            var newdialoguewin = window.showModalDialog("../SM/Setup.aspx", window, "dialogWidth:600px;DialogHeight=150px;status:no");
        //            if (newdialoguewin != null) {

        //            }
        //        }

        //        function show_sm() {
        //            if (typeof (msgwin) != "undefined" && msgwin.open && !meizz.closed) {
        //                msgwin.focus();
        //            }
        //            else {
        //                mytop = screen.availHeight - 310;
        //                myleft = 0;
        //                var msgwin = window.open("../SM/MsgManage.aspx", "auto_call_show", "height=230,width=400,status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,top=" + mytop + ",left=" + myleft + ",resizable=yes");
        //            }
        //        }

        //        function showonline() {
        //            onlinewin = window.open("onlineperson.aspx", "online", "width:500,height:800,toolbar=no,status=no,scrollbars=yes,resizable=yes")
        //            //onlinewin.moveTo(20,20)
        //            onlinewin.resizeTo(600, 500)
        //        }
        //        function re() {
        //            location.reload();
        //        }
        //        function setup() {
        //            opendialwin();
        //        }

    </script>

</head>
<body leftmargin="0" topmargin="0">
    <form method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
    </asp:Timer>
    <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="300000">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div align="center">
                <table width="100%" cellspacing="0" cellpadding="0" border="0" class="welcome">
                    <tr>
                        <td>
                            <a href="javascript:showonlineuser()">
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Icon/07.gif" Width="18px" /></a>
                        </td>
                        <td valign="middle" height="33">
                            <font color="white">在线人数:<asp:Label ID="lb_OnlineUsers" runat="server" Font-Bold="true"
                                ForeColor="red"></asp:Label>
                            </font>
                            <asp:Literal ID="lit" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
