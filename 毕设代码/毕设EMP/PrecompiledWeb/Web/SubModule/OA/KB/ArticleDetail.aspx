<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_KB_CatalogManage, App_Web_43gieen2" enableEventValidation="false" stylesheettheme="basic" %>

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
                            <asp:Button ID="bt_Edit" runat="server" OnClick="bt_Edit_Click" Text="文章修改" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lb_Title" runat="server" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="28">
                            关键字：<asp:Label ID="lb_Keyword" runat="server"></asp:Label>
                            &nbsp;作者：<asp:Label ID="lb_Author" runat="server"></asp:Label>
                            &nbsp;阅读次数：<asp:Label ID="lb_ReadCounts" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <table cellspacing="5" cellpadding="0" width="900px" height="93px">
                                <tr>
                                    <td width="43px" background="../../../Images/gif/gif-0505.gif">
                                    </td>
                                    <td style="font-size: medium" align="left">
                                        <asp:Literal ID="lt_article_comment" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="Page_KB_Article">
                </mcs:UC_DetailView>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="30" CanUpload="false"
                    CanDelete="false" />
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td valign="middle" align="left">
                            该文章对您是否有用
                            <asp:RadioButtonList ID="rbl_VisitFlag" runat="server" RepeatColumns="2" RepeatLayout="Flow"
                                Width="94px">
                                <asp:ListItem Value="Y" Selected="True">是</asp:ListItem>
                                <asp:ListItem Value="N">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Button ID="btn_OK" runat="server" Text="提 交" Width="67px" OnClick="btn_OK_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tb_Comment" runat="server" cellspacing="0" cellpadding="0" width="100%"
                    align="center">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h3>
                                            评论列表</h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_del_comment" runat="server" Text="删除评论" OnClick="btn_del_comment_Click"
                                            OnClientClick="return confirm('是否确认删除选中的评论信息?')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="ud_grid_comment" runat="server" PanelCode="Panel_KB_CommentList"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="KB_Comment_ID" AllowPaging="True"
                                PageIndex="0" PageSize="8">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_ID" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                    <tr id="tr_AddComment" runat="server">
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                                            <tr>
                                                <td height="28px">
                                                    <h3>
                                                        我要评论</h3>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btn_add_comment" runat="server" Text="添加评论" OnClick="btn_add_comment_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabForm">
                                        <asp:TextBox ID="tbx_CommentContent" runat="server" TextMode="MultiLine" Width="500px"
                                            Height="40px"></asp:TextBox>
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
