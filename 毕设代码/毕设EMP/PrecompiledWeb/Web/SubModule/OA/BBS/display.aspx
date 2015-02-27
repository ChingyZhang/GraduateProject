<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" validaterequest="false" autoeventwireup="true" inherits="SubModule_OA_BBS_display, App_Web_3-csmuv_" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function SendMsg(username, realname) {
            var w;
            if (username != '')
                w = window.open('../SM/MsgSender.aspx?SendTo=' + username + '&SendToRealName=' + realname, 'send', 'width:500,height:800,toolbar=no,status=no,scrollbars=yes,resizable=yes');
            else
                w = window.open('../SM/MsgSender.aspx', 'send', 'width:500,height:800,toolbar=no,status=no,scrollbars=yes,resizable=yes');
            w.focus();

        }
    </script>

    <script language="C#" runat="server">
        private bool GetReplayOpt(string author)
        {
            if (ViewState["username"].ToString() == author)
            {
                return (true);
            }
            else
                return (false);
        }	
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td background="../../../Images/bbsback.jpg">
                <img src="../../../Images/bbs.jpg">
            </td>
            <td id="Td1" background="../../../Images/bbsback.jpg" runat="server" visible="true">
                <asp:Button ID="bt_Search" runat="server" Visible="true" Text="搜索" OnClick="bt_Search_Click">
                </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <a href="index.aspx">
                    <asp:Label ID="lblCatalog" Font-Size="11" runat="server" Font-Bold="true"></asp:Label></a>>>
                <a href="listview.aspx?Board=<%Response.Write(board);%>">
                    <asp:Label ID="lblBoardName" Font-Size="11" runat="server" Font-Bold="true"></asp:Label></a>>>
                <asp:Label ID="lblForumItemName" ForeColor="#990000" Font-Size="11" runat="server"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tabForm">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellspacing="0" width="100%" cellpadding="0" border="0">
                                <tr class="h3Row">
                                    <td align="left" colspan="3" height="26">
                                        <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="cbx_Tpp" Text="置顶" AutoPostBack="True" runat="server" ForeColor="blue"
                                            OnCheckedChanged="cbx_Tpp_Click"></asp:CheckBox>
                                        <asp:CheckBox ID="cbx_IsPith" Text="精华" AutoPostBack="True" runat="server" ForeColor="blue"
                                            OnCheckedChanged="cbx_IsPith_Click"></asp:CheckBox>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_SubmitComment" runat="server" Text="我要回复" OnClick="btn_SubmitComment_Click"
                                            Width="58px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" id="tbl_ForumItem" runat="server">
                                            <tr>
                                                <td align="left" valign="top" width="200px">
                                                    <b>作者</b><br />
                                                    <asp:Literal ID="sendman" runat="server"></asp:Literal><br />
                                                    <b>发表日期</b><br />
                                                    <asp:Literal ID="sendtime" runat="server"></asp:Literal><br />
                                                    <b>浏览次数</b>：
                                                    <asp:Literal ID="browsetime" runat="server"></asp:Literal><br />
                                                    <b>回复次数</b>：
                                                    <asp:Literal ID="replaytimes" runat="server"></asp:Literal><br />
                                                </td>
                                                <td id="itemcontent" valign="top" align="left" runat="server">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="dgshow" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AllowPaging="True" OnPageIndexChanging="dgshow_PageIndexChanging" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="回复帖">
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td width="200px" valign=top>
                                                                    <b>回复人</b><br />
                                                                    <asp:Literal ID="replayer" runat="server" Text='<%# DisplayFullInfo(DataBinder.Eval(Container.DataItem,"Replyer").ToString())%>'> </asp:Literal><br />
                                                                    <b>回复时间</b><br />
                                                                    <asp:Literal ID="replaytime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReplyTime")%>'> </asp:Literal><br />
                                                                    <asp:LinkButton ID="lbtndelreplay" Visible='<%# right %>' runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                                        OnClick="DelReplay" OnClientClick="return confirm('是否确认删除回复?');">操 作：| 删除回复 |</asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td valign="top">
                                                                    <%# DataBinder.Eval(Container.DataItem,"Title")%>
                                                                    <br />
                                                                    <asp:Literal ID="replaycontent" runat="server" Text=' <%# FormatTxt(MCSFramework.SQLDAL.OA.BBS_ForumItemDAL.txtMessage(DataBinder.Eval(Container.DataItem, "content").ToString()))%>'> </asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                无回复</EmptyDataTemplate>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="80%" border="0" cellpadding="0" cellspacing="0" id="tb_edit" runat="server">
                                <tr>
                                    <td align="left" width="200px">
                                        回复主题：
                                    </td>
                                    <td align="left">
                                        <input id="Title" style="width: 597px; height: 19px" type="text" size="94" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        回复内容：
                                    </td>
                                    <td align="left">
                                         <CKEditor:CKEditorControl ID="ckedit_content" runat="server" ToolbarFull="Source|-|Preview|-|Templates
Cut|Copy|Paste|PasteText|PasteFromWord|-|Print|SpellChecker|Scayt
Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat
Form|Checkbox|Radio|TextField|Textarea|Select|Button|ImageButton|HiddenField
/
Bold|Italic|Underline|Strike|-|Subscript|Superscript
NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
BidiLtr|BidiRtl
Link|Unlink|Anchor
Image|Flash|Table|HorizontalRule|Smiley|SpecialChar|PageBreak|Iframe
/
Styles|Format|Font|FontSize
TextColor|BGColor
Maximize|ShowBlocks|" Width="800px"></CKEditor:CKEditorControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        附件：
                                    </td>
                                    <td align="left">
                                        <input id="hif" style="width: 300px; height: 19px" type="file" runat="server" />
                                        <asp:Button ID="btn_UpAtt" runat="server" Text="上传" OnClick="btn_UpAtt_Click" Width="60px">
                                        </asp:Button>
                                        <asp:Button ID="btn_DelAtt" runat="server" Text="删除" OnClick="btn_DelAtt_Click" Width="60px">
                                        </asp:Button>
                                        <br />
                                        <asp:ListBox ID="lbx_AttList" runat="server" Width="300px" Height="54px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr height="28">
                                    <td align="center" colspan="2" valign="bottom">
                                        <asp:Button ID="btn_OK" runat="server" Width="80px" OnClick="btn_OK_Click" Text="确认回复" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
