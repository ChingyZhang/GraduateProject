<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="NoticeDetail.aspx.cs" Inherits="SubModule_OA_PN_NoticeDetail"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../Images/icon/284.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                公告管理
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存公告" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_PreView" runat="server" Width="60px" Text="预览" OnClick="bt_PreView_Click" />
                            <asp:Button ID="bt_Approve" runat="server" Width="60px" Text="审核" OnClick="bt_Approve_Click"
                                OnClientClick="return confirm('是否确认将该公告设为审核通过?')" />
                            <asp:Button ID="bt_Back" runat="server" Width="60px" Text="公告列表" OnClick="bt_Back_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="UC_Notice" runat="server" DetailViewCode="Page_PN_NoticeDetail"
                            Visible="true">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tab_PNToPosition"
                            runat="server">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    面向职位范围</h3>
                                            </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td width="420px">
                                                            面向的职位
                                                            <mcs:MCSTreeControl ID="tr_ToPosition" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="300px" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px">
                                                        <td>
                                                            包括全部子职位:<asp:CheckBox ID="chb_ToPositionChild" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px" align="center">
                                                        <td>
                                                            <asp:Button ID="bt_Insert1" runat="server" Width="60px" Text="添加职位" OnClick="bt_Insert1_Click" />
                                                            <asp:Button ID="bt_Detele1" runat="server" Width="60px" Text="删除职位" OnClick="bt_Detele1_Click" />
                                                            <asp:Button ID="bt_Clear1" runat="server" Width="60px" Text="清除所有" OnClick="bt_Clear1_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lb_PositionChild" runat="server" Height="150" Width="320" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tab_PNToOrganizeCity"
                            runat="server">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    面向管理片区范围</h3>
                                            </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td width="420px">
                                                            面向的区域
                                                            <mcs:MCSTreeControl ID="tr_ToOrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="250px" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px">
                                                        <td>
                                                            包括全部子区域:<asp:CheckBox ID="chb_ToOganizeCityChild" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px" align="center">
                                                        <td>
                                                            <asp:Button ID="bt_Insert2" runat="server" Width="60px" Text="添加区域" OnClick="bt_Insert2_Click" />
                                                            <asp:Button ID="bt_Detele2" runat="server" Width="60px" Text="删除区域" OnClick="bt_Detele2_Click" />
                                                            <asp:Button ID="bt_Clear2" runat="server" Width="60px" Text="清除区域" OnClick="bt_Clear2_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lb_CityChild" runat="server" Height="150" Width="320" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="5" cellpadding="5" width="100%" border="0" id="Table6" class="tabForm"
                    runat="server">
                    <tr>
                        <td style="text-align: left;">
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="80" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
