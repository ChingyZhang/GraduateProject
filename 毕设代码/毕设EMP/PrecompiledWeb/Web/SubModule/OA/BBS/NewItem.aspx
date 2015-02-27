<%@ page language="C#" autoeventwireup="true" masterpagefile="~/MasterPage/BasicMasterPage.master" inherits="SubModule_OA_BBS_NewItem, App_Web_3-csmuv_" validaterequest="false" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title></title>
    <base target="_self" />

    <script type="text/javascript">
        function addcontent(str1, str2) {
            document.forms[0].Content.focus();
            if ((document.selection) && (document.selection.type == "Text")) {
                var range = document.selection.createRange();
                var ch_text = range.text;
                range.text = str1 + ch_text + str2;
            }
            else {
                document.forms[0].Content.value = document.forms[0].Content.value + str1 + str2;
                document.forms[0].Content.focus();
            }
        }

        function sysbulletin_click() {
            document.getElementById('cbx_DeskTop').disabled = !document.getElementById('rdob_sysbulletin').checked;

        }

    </script>

    <body>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle">
            <tr>
                <td align="right" width="20">
                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                </td>
                <td align="left" width="150">
                    <h2>
                        <asp:Label ID="lab_title" runat="server" Text="发表新贴" ForeColor="#E81741"></asp:Label>
                    </h2>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table id="AutoNumber1" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
            width="100%" class="tabForm">
            <tr>
                <td align="left" height="22" style="width: 100px">
                    发帖主题
                </td>
                <td align="left" height="22">
                    <input id="Title" style="width: 597px; height: 19px" type="text" size="94" runat="server"><span
                        style="color: #FF0000">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Title"
                        ErrorMessage="主题不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" style="width: 100px">
                    发帖内容
                </td>
                <td valign="top" align="left">
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
                    附件
                </td>
                <td align="left" valign="top">
                    <input id="hif" style="width: 300px; height: 22px" type="file" runat="server" />
                    <asp:Button ID="btn_UpAtt" runat="server" Text="上传" OnClick="btn_UpAtt_Click" Width="60px">
                    </asp:Button>
                    <asp:Button ID="btn_DelAtt" runat="server" Text="删除" OnClick="btn_DelAtt_Click" Width="60px">
                    </asp:Button>
                    <br />
                    <asp:ListBox ID="lbx_AttList" runat="server" Width="300px" Height="60px" SelectionMode="Multiple">
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="2">
                    <input id="cmdOK" runat="server" onserverclick="cmdOK_ServerClick" type="submit"
                        value="确定发帖" style="width: 80px" class="button" />
                </td>
            </tr>
        </table>
    </body>
</asp:Content>
