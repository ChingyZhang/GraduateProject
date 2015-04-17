<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="compose.aspx.cs" Inherits="SubModule_OA_Mail_compose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function dialwinprocess(type) {
            var newdialoguewin = window.showModalDialog("Selectreceiver1.aspx", window, "dialogWidth:600px;DialogHeight=600px;status:no");
            if (newdialoguewin != null) {
                if (newdialoguewin.length > 0) {
                    ReceiverTypeArray = newdialoguewin.split("|");
                    try {

                        document.getElementById("txtSendTo").value = ReceiverTypeArray[0];
                        document.getElementById("txtCcTo").value = ReceiverTypeArray[2];
                        document.getElementById("txtBccTo").value = ReceiverTypeArray[4];
                        document.getElementById("hdnTxtSendTo").value = ReceiverTypeArray[1];
                        document.getElementById("hdnTxtCcTo").value = ReceiverTypeArray[3];
                        document.getElementById("hdnTxtBccTo").value = ReceiverTypeArray[5];
                    }
                    catch (e) { }
                }
            }
        }
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td valign="top" height="38">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../../Images/icon/284.GIF" width="16" />
                        </td>
                        <td align="left" width="150">
                            <h2>
                                我的邮件</h2>
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
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="right" width="100" class="dataLabel">
                            <asp:Label ID="lblSendTo" runat="server">收件人:</asp:Label>
                        </td>
                        <td class="dataField">
                            <input style="width: 404px; height: 19px" id="txtSendTo" readonly type="text" size="62"
                                value="<%=SendToRealName%>" name="txtSendTo">&nbsp;<a style="cursor: hand" onclick="dialwinprocess(1)"
                                    href="#"><font face="宋体">选择收件人</font></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="dataLabel">
                            <asp:Label ID="lblCcTo" runat="server">抄送人:</asp:Label>
                        </td>
                        <td class="dataField">
                            <input style="width: 405px; height: 19px" id="txtCcTo" readonly type="text" size="62"
                                value="<%=CcToRealName%>" name="txtCcTo">&nbsp;<a style="cursor: hand" onclick="dialwinprocess(2)"
                                    href="#"><font face="宋体">选择抄送人</font></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="dataLabel">
                            <asp:Label ID="lblBccTo" runat="server">秘抄人:</asp:Label>
                        </td>
                        <td class="dataField">
                            <input style="width: 406px; height: 19px" id="txtBccTo" readonly type="text" size="62"
                                value="<%=BccToRealName%>" name="txtBccTo">&nbsp;<a style="cursor: hand" onclick="dialwinprocess(2)"
                                    href="#"><font face="宋体">选择密送人</font></a>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px" align="right" class="dataLabel">
                            <asp:Label ID="lblSubject" runat="server" Width="40px">主&nbsp;&nbsp;题:</asp:Label>
                        </td>
                        <td style="height: 21px" class="dataField">
                            <asp:TextBox ID="txtSubject" runat="server" Width="484px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="30">
                        <td align="right" class="dataLabel">
                            <asp:Label ID="lblImportance" runat="server">重要性:</asp:Label>
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="listImportance" runat="server" Width="150px">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="cbRemind" runat="server" ForeColor="red" Text="站内短消息提醒"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr height="30">
                        <td align="right" class="dataLabel">
                            <asp:Label ID="labSendTags" runat="server">发送标签:</asp:Label>
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="listSendTags" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="dataLabel">
                            <asp:Label ID="lblBody" runat="server">内&nbsp;&nbsp;容:</asp:Label>
                        </td>
                        <td class="dataField">
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
                        <td class="dataLabel">
                            附件：
                        </td>
                        <td class="dataField">
                            <table id="tblAttachFiles" width="486" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center">
                                        <table cellspacing="0" cellpadding="0" width="485" border="0">
                                            <tr>
                                                <td style="width: 45px" rowspan="2">
                                                    <table style="width: 44px; height: 76px" width="44" align="center" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    附件1
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    附件2
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    附件3
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    附件4
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td style="width: 150px" rowspan="2">
                                                    <input id="hif1" style="width: 146px; height: 19px" type="file" name="filecontrol1"
                                                        runat="server"><br>
                                                    <input id="hif2" style="width: 146px; height: 19px" type="file" name="filecontrol2"
                                                        runat="server"><br>
                                                    <input id="hif3" style="width: 146px; height: 19px" type="file" name="filecontrol3"
                                                        runat="server"><br>
                                                    <input id="hif4" style="width: 146px; height: 19px" type="file" name="filecontrol4"
                                                        runat="server">
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUpload" runat="server" Text="添加附件" OnClick="btnUpload_Click" Width="64px">
                                                    </asp:Button>
                                                </td>
                                                <td rowspan="2">
                                                    <asp:ListBox ID="listUp" runat="server" Width="200px" Height="81px" 
                                                        SelectionMode="Multiple">
                                                    </asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnRemove" runat="server" Text="删除附件" OnClick="btnRemove_Click" Width="64px">
                                                    </asp:Button></font>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Button ID="btnSendMail" runat="server" Text=" 发  送 " OnClick="btnSendMail_Click"
                                            Width="48px"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" value="<%=SendTo%>" id="hdnTxtSendTo" name="hdnTxtSendTo">
    <input type="hidden" value="<%=CcTo%>" id="hdnTxtCcTo" name="hdnTxtCcTo">
    <input type="hidden" value="<%=BccTo%>" id="hdnTxtBccTo" name="hdnTxtBccTo">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
