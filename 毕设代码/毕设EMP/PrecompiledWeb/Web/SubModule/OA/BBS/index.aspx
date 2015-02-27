<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_BBS_index, App_Web_3-csmuv_" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="MCSFramework.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function HiddleBoard(id) {
            var tabs = document.getElementsByTagName("table");
            for (var i = 0; i < tabs.length; i++) {
                if (tabs[i].id == "tab_board") {
                    if (tabs[i].style.display == "block" && tabs[i].title == id) {
                        tabs[i].style.display = "none";
                    }
                    else if (tabs[i].style.display == "none" && tabs[i].title == id) {
                        tabs[i].style.display = "block";
                    }
                }
            }
        }
        function HiddleOnLine() {
            var tab = document.getElementById("tab_online")
            if (tab.style.display == "block")
                tab.style.display = "none";
            else
                tab.style.display = "block";
        }

    </script>

    <script runat="server">
        // 获取分类标题
        public string GetCatalog(int id)
        {
            string name = new MCSFramework.BLL.OA.BBS_CatalogBLL(id).Model.Name;
            string description = new MCSFramework.BLL.OA.BBS_CatalogBLL(id).Model.Description;
            if (description != "")
                return "【" + name + "】" + "——" + description;
            else
                return "【" + name + "】";
        }

        // 判断是否公共板块
        public bool IsPublic(int id, string isPublic)
        {
            if (isPublic == "1") return true;
            else
            {

                MCSFramework.BLL.OA.BBS_BoardUserMemberBLL usermemberbll = new MCSFramework.BLL.OA.BBS_BoardUserMemberBLL();
                if (usermemberbll.GetUserRoleByBoard(id, username) > 0)
                    return true;
                else
                    return false;
            }
        }


        // 判断有无板块管理权限
        public bool HasAdmin(int id)
        {

            if (Admin)
                return true;
            else
            {
                MCSFramework.BLL.OA.BBS_BoardUserMemberBLL usermemberbll = new MCSFramework.BLL.OA.BBS_BoardUserMemberBLL();
                if (usermemberbll.GetUserRoleByBoard(id, username) == 1)
                    return true;
                else
                    return false;
            }
        }

    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td background="../../../Images/bbsback.jpg">
                            <img src="../../../Images/bbs.jpg">
                        </td>
                        <td id="Td1" align="right" background="../../../Images/bbsback.jpg" runat="server"
                            visible="true">
                            <asp:Button ID="bt_Search" runat="server" Visible="true" Text="搜索" 
                                OnClick="bt_Search_Click" Width="60px">
                            </asp:Button>
                            <asp:Button ID="bt_Insert" runat="server" Text="添加分类" OnClick="bt_Insert_Click" 
                                Width="60px">
                            </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbUser" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="10"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;欢迎您
                        </td>
                        <td align="right">
                            今日&nbsp;&nbsp;<asp:Label ID="lbNowForumItemCount" runat="server"></asp:Label>&nbsp;&nbsp;篇贴子&nbsp;&nbsp;/&nbsp;&nbsp;
                            昨日&nbsp;&nbsp;<asp:Label ID="lbYesterdayForumItemCount" runat="server"></asp:Label>&nbsp;&nbsp;篇贴子&nbsp;&nbsp;/&nbsp;&nbsp;
                            最高日&nbsp;&nbsp;<asp:Label ID="lbTopDayForumItemCount" runat="server"></asp:Label>&nbsp;&nbsp;篇贴子
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            头衔：<asp:Label ID="lbRole" ForeColor="Black" runat="server" Font-Bold="true" Font-Size="10"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            帖子：<asp:Label ID="lbForumItem" Font-Bold="true" Font-Size="10" ForeColor="Black"
                                runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            共&nbsp;&nbsp;<asp:Label ID="lbForumItemCount" runat="server"></asp:Label>&nbsp;&nbsp;篇贴子&nbsp;&nbsp;/&nbsp;&nbsp;
                            <asp:Label ID="lbReplyCount" runat="server"></asp:Label>&nbsp;&nbsp;篇回复
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            您上次访问是在&nbsp;&nbsp;<asp:Label ID="lbLastTime" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            共&nbsp;&nbsp;<asp:Label ID="lbUserCount" runat="server"></asp:Label>&nbsp;&nbsp;位会员<%--&nbsp;&nbsp;/&nbsp;&nbsp;欢迎新会员
                     <a href="../Mail/Compose.aspx?Action=3&Receiver=<%Response.Write(lastCreateUserName);%>">
                    <asp:Label ID="lbNewUser" Font-Bold="true" Font-Size="10"  ForeColor="Black" runat="server"></asp:Label></a>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Repeater ID="rpt_catalog" runat="server">
                    <ItemTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" class="moduleTitle">
                            <tr style="cursor: hand" onclick="HiddleBoard(<%# DataBinder.Eval(Container.DataItem,"ID")%>)"
                                height="28px">
                                <td align="left" width="70%">
                                    <asp:Label ID="LCatalog" Font-Bold="true" ForeColor="#990033" Font-Size="10" runat="server"
                                        Text='<%# GetCatalog((int)DataBinder.Eval(Container.DataItem,"ID"))%>'></asp:Label>
                                </td>
                                <td align="right" width="30%">
                                    <table>
                                        <tr>
                                            <td runat="server" id="admin" visible="<%# Admin%>">
                                                <asp:Panel ID="adminop" runat="server">
                                                    <a href="BoardManage.aspx?CatalogID=<%# DataBinder.Eval(Container.DataItem,"ID")%>">
                                                        添加板块</a>|
                                                    <asp:LinkButton ID="btndelcatalog" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID")%>'
                                                        OnClick="DeleteCatalog" runat="server" OnClientClick="return confirm('确定删除吗?')">删除分类</asp:LinkButton>|
                                                    <a href="CatalogManage.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID")%>">编辑分类</a>&nbsp;&nbsp;</asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table cellspacing="0" cellpadding="0" width="100%" style="display: block" id="tab_board"
                                        title='<%# DataBinder.Eval(Container.DataItem,"ID")%>'>
                                        <tr runat="server">
                                            <td width="5%">
                                            </td>
                                            <td width="40%" align="left" valign="middle" height="24">
                                                <font color="Gray"><b>论坛板块</b></font>
                                            </td>
                                            <td align="center" height="24" width="10%" nowrap>
                                                <font color="Gray"><b>帖子数</b></font>
                                            </td>
                                            <td align="center" width="10%" nowrap height="24">
                                                <font color="Gray"><b>回复数</b></font>
                                            </td>
                                            <td align="center" valign="middle" height="24" width="20%" nowrap>
                                                <font color="Gray"><b>最新帖子</b></font>
                                            </td>
                                            <td align="center" valign="middle" height="24" width="15%" nowrap>
                                                <font color="Gray"><b>版主</b></font>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rpt_board" OnItemDataBound="rpt_board_ItemDataBound" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("catalog_board") %>'
                                            runat="server">
                                            <ItemTemplate>
                                                <tr id="Tr1" visible='<%# IsPublic((int)DataBinder.Eval(Container.DataItem, "[\"ID\"]"),DataBinder.Eval(Container.DataItem, "[\"IsPublic\"]").ToString()) %>'
                                                    runat="server">
                                                    <td class="tabForm" colspan="6">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="5%" nowrap valign="middle" align="center">
                                                                    <%# (int)DataBinder.Eval(Container.DataItem, "[\"HitCount\"]") == 0 ? "<img src='../../../Images/forum_nonews.gif' border='0'>" : "<img src='../../../Images/forum_isnews.gif' border='0'>" %>
                                                                </td>
                                                                <td align="left" valign="middle" width="40%">
                                                                    <p>
                                                                        <a href="listview.aspx?Board=<%# DataBinder.Eval(Container.DataItem,"[\"ID\"]")%>"
                                                                            style="font-weight: bold; color: #990033;"><font size="2.5">
                                                                                <%# DataBinder.Eval(Container.DataItem, "[\"Name\"]")%></font> </a>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label ID="LBoardDescription" runat="server">
                                                                <%# DataBinder.Eval(Container.DataItem,"[\"Description\"]")%>
                                                                        </asp:Label></p>
                                                                </td>
                                                                <td id="Td1" align="center" runat="server" width="10%" height="45" nowrap>
                                                                    <asp:Label ID="LForumTimes" runat="server">
									                         <%# DataBinder.Eval(Container.DataItem, "[\"Count_Item\"]").ToString() %>
                                                                    </asp:Label>
                                                                </td>
                                                                <td id="Td2" align="center" runat="server" height="45" nowrap width="10%">
                                                                    <asp:Label ID="LReplays" runat="server">
				                                               <%# DataBinder.Eval(Container.DataItem, "[\"Count_Reply\"]").ToString()%>
                                                                    </asp:Label>
                                                                </td>
                                                                <td valign="top" align="left" height="45" width="20%" nowrap>
                                                                    <a href="display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "[\"ItemID\"]") ==null ? "0":DataBinder.Eval(Container.DataItem, "[\"ItemID\"]").ToString() %>"
                                                                        >
                                                                        <asp:Label ID="LForumItem" runat="server">
                                                               <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "[\"Title\"]").ToString()) == true  ? "" : "主题：" + DataBinder.Eval(Container.DataItem, "[\"Title\"]").ToString()%>
                                                                        </asp:Label></a>
                                                                    <br>
                                                                    <asp:Label ID="LSender" runat="server">
 								                          <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "[\"Sender\"]").ToString()) == true ? "" : "发布者：" + DataBinder.Eval(Container.DataItem, "[\"Sender\"]").ToString()%>
                                                                    </asp:Label>
                                                                    <br>
                                                                    <asp:Label ID="LSendTime" runat="server">
	                                                      <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "[\"SendTime\"]").ToString()) == true ? "" : "发布时间：" + DataBinder.Eval(Container.DataItem, "[\"Sender\"]").ToString()%> 
                                                                    </asp:Label>
                                                                </td>
                                                                <td id="Td3" runat="server" height="45" width="15%" nowrap align="left" valign="middle">
                                                                    <table>
                                                                        <tr>
                                                                            <% count = 0; %>
                                                                            <asp:Repeater ID="rpt_boardmaster" DataSource='<%# ((DataRow)Container.DataItem).GetChildRows("board_boardmaster")%>'
                                                                                runat="server">
                                                                                <ItemTemplate>
                                                                                    <td>
                                                                                        <a href="../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"[\"UserName\"]")%>">
                                                                                            <%# DataBinder.Eval(Container.DataItem,"[\"UserName\"]")%>
                                                                                        </a>,
                                                                                    </td>
                                                                                    <% count++; if (count % 3 == 0) Response.Write("</tr><tr>"); %>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="6">
                                                                    <asp:Panel ID="Adminop1" runat="server" Visible='<%# HasAdmin((int)DataBinder.Eval(Container.DataItem, "[\"ID\"]")) %>'>
                                                                        <a href="BoardMasterManage.aspx?BoardID=<%# DataBinder.Eval(Container.DataItem,"[\"ID\"]")%>">
                                                                            设定斑竹</a>|
                                                                        <asp:LinkButton ID="lbtnDelBoard" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"[\"ID\"]")%>'
                                                                            OnClick="DeleteBoard" runat="server" OnClientClick="return confirm('确定删除吗?')">删除板块</asp:LinkButton>|
                                                                        <a href="BoardManage.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"[\"ID\"]")%>">编辑板块</a>|<a
                                                                            href="BoardUserMemberManage.aspx?BoardID=<%# DataBinder.Eval(Container.DataItem, "[\"ID\"]")%>">
                                                                            编辑成员</a>&nbsp;&nbsp;</asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr height="1px">
                                                    <td colspan="6">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr height="6">
                                            <td colspan="6">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr onclick="HiddleOnLine()">
                        <td colspan="2" class="tabForm" style="color: #000099">
                            【在线用户】----
                            <asp:Label ID="labOnLine" runat="server"></asp:Label>人在线
                            <img src='../../../Images/online_admin.gif'>管理员
                            <img src='../../../Images/online_moderator.gif'>版主
                            <img src='../../../Images/online_member.gif'>会员
                        </td>
                    </tr>
                    <tr class="h3Row" id="tab_online" style="display:block; cursor:hand">
                        <td style="width: 50px" align="center" valign="middle">
                            <img src='../../../Images/online.gif'>
                        </td>
                        <td align="left" width=95%>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                                <tr>
                                    <% count = 0; %>
                                    <asp:Repeater ID="rpt_boardmember" runat="server">
                                        <ItemTemplate>
                                            <td style="width: 150px" align="left">
                                                <%# GetOnLineRight((string)DataBinder.Eval(Container.DataItem, "Username"))%>
                                                <a href="../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"Username")%>"
                                                    >
                                                    <%# DataBinder.Eval(Container.DataItem, "Username")%>
                                                </a>
                                            </td>
                                            <% count++; if (count % 7 == 0) Response.Write("</tr><tr>"); %></ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <img src='../../../Images/forum_isnews.gif' border='0'>有新帖论坛
                <img src='../../../Images/forum_nonews.gif' border='0'>无新帖论坛
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
