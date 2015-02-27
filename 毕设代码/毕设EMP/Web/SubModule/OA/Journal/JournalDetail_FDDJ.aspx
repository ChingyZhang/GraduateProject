<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JournalDetail_FDDJ.aspx.cs" Inherits="SubModule_OA_Journal_JournalDetail_FDDJ" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" width="160" height="28px">
                                    <h2>
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                        辅导与代表详细信息</h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="80px" Text="保存" OnClick="bt_OK_Click"
                                        ToolTip="保存当前的信息" />
                                    <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;是否确认删除该工作日志?&quot;)"
                                        Text="删除" Width="80px" ToolTip="删除当前的信息" />
                                    <asp:Button ID="bt_ListView" runat="server" Text="返回列表" Width="80px" CausesValidation="false"
                                        OnClick="bt_ListView_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_OA_JournalDetail_FDDJ">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr id="tr_uploadfile" runat="server">
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="tr_comment">
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="right" height="28px" class="dataField">
                                    已有<asp:Label ID="lb_CommentCounts" runat="server" ForeColor="Red" Text="Label"></asp:Label>条评论<asp:Button
                                        ID="btn_LookComment" runat="server" OnClick="btn_LookComment_Click" Text="查看评论"
                                        Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="table_comment"
                                        visible="false">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="dgshow" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    AllowPaging="True" OnPageIndexChanging="dgshow_PageIndexChanging" PageSize="8">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="日志评论">
                                                            <ItemTemplate>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 196px">
                                                                            回复人：<asp:Literal ID="replayer" runat="server" Text='<%# new MCSFramework.BLL.Org_StaffBLL(int.Parse(DataBinder.Eval(Container.DataItem,"Staff").ToString())).Model.RealName %>'> </asp:Literal>
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
                                            </td>
                                        </tr>
                                        <tr id="tr_AddComment" runat="server">
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <h2>
                                                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16"> </img>
                                                                发表评论</h2>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="tbx_content" runat="server" TextMode="MultiLine" Width="600px" Height="100px"></asp:TextBox>
                                                            <br />
                                                            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="保存评论" />
                                                            &nbsp;
                                                            <asp:Button ID="btn_cancel" runat="server" Text="取消评论" OnClick="btn_cancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
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
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
