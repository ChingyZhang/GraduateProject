<%@ Page Language="C#" AutoEventWireup="true" CodeFile="read.aspx.cs" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    Inherits="SubModule_OA_PN_readter" ValidateRequest="false" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table1" class="moduleTitle">
        <tr>
            <td align="right" width="20">
                <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
            </td>
            <td align="left" width="150">
                <h2>
                    阅读公告</h2>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btn_return" runat="server" Text="返回公告列表" OnClick="btn_return_Click"
                    Width="100px" />
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="5" cellpadding="5" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbl_Topic" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"
                                ForeColor="#CC0000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            发布人：<asp:Label ID="lbl_InsertStaffName" runat="server" Text="Label"></asp:Label>
                            发布时间：<asp:Label ID="lbl_InsertTime" runat="server" Text="Label"></asp:Label>
                            评论条数：<asp:Label ID="lab_comment" runat="server" Text="Label"></asp:Label>
                            阅读次数：<asp:Label ID="lab_hasRead" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr size="1" width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="lbl_content" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr height="30px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="80" CanUpload="false"
                                CanDelete="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tbl_comment">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" height="28px">
                    <tr>
                        <td align="right" width="20">
                        </td>
                        <td align="left" width="150">
                            <h2>
                                &nbsp;</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_LookComment" runat="server" OnClick="btn_LookComment_Click" Text="查看及发表评论"
                                Width="100px" />
                        </td>
                    </tr>
                </table>
                <table cellspacing="5" cellpadding="5" width="100%" border="0" runat="server" id="table_comment"
                    visible="false">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="dgshow" runat="server" AutoGenerateColumns="False" Width="100%"
                                        AllowPaging="True" OnPageIndexChanging="dgshow_PageIndexChanging" PageSize="8">
                                        <Columns>
                                            <asp:TemplateField HeaderText="公告评论">
                                                <ItemTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 196px">
                                                                回复人：<asp:Literal ID="replayer" runat="server" Text='<%# DisplayFullInfo(DataBinder.Eval(Container.DataItem,"RealName").ToString())%>'> </asp:Literal>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 196px">
                                                                回复时间：<asp:Literal ID="replaytime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CommentTime")%>'> </asp:Literal>
                                                            </td>
                                                            <td>
                                                                <%# FormatTxt(MCSFramework.SQLDAL.OA.BBS_ForumItemDAL.txtMessage(DataBinder.Eval(Container.DataItem, "Content").ToString())) %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="Table5" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="20">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16"> </img>
                                    </td>
                                    <td align="left">
                                        <h2>
                                            发表评论</h2>
                                    </td>
                                    <td align="right">
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tbl_comments"
                                runat="server">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="content" runat="server" TextMode="MultiLine" Width="676px" Height="203px"></asp:TextBox>
                                        <br />
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text=" 提 交 " />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_cancel" runat="server" Text=" 取 消 " OnClick="btn_cancel_Click" />
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
