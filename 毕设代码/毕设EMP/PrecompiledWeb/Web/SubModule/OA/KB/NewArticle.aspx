<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_KB_NewArticle, App_Web_z_fgc1lb" validaterequest="false" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td style="width: 25px">
                            <img height="16" src="../../../DataImages/help_book.gif" width="15">
                        </td>
                        <td align="left">
                            <h1>
                                知识库文章</h1>
                        </td>
                        <td align="right">
                            &nbsp;
                            <asp:Button ID="btn_ok" runat="server" Text="保 存" OnClick="btn_ok_Click" Height="21px"
                                Width="80px" />
                            <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;是否确认删除该文章?&quot;)"
                                Text="删 除" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="top">
                            <table cellspacing="1" cellpadding="1" width="100%">
                                <tr>
                                    <td>
                                        <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="Page_KB_NewArticle"
                                            Visible="true">
                                        </mcs:UC_DetailView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                <tr id="tr_Approve" runat="server">
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                                                        <tr>
                                                            <td height="28px">
                                                                <h3>
                                                                    审核人意见</h3>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btn_check" runat="server" Text="审核通过" Width="80px" OnClick="btn_check_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tabForm">
                                                    <asp:TextBox ID="txt_approve_idea" runat="server" TextMode="MultiLine" Width="600px"
                                                        Height="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="30" Visible="false" />
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
