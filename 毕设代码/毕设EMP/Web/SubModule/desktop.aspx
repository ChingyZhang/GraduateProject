<%@ Page Language="c#" Inherits="SubModule_Desktop" CodeFile="Desktop.aspx.cs" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    StylesheetTheme="basic" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="MCSFramework.BLL.OA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%Session["ClientID"] = null;%>

    <script language="javascript">
        function dialwinprocess(CurrDate, CurrTime, whichPg, TaskID) {
            if (whichPg == 1)
                var newdialoguewin = window.showModalDialog("../Schedule/Manage.aspx?CurrDate=" + CurrDate + "&CurrTime=" + CurrTime, window, "dialogWidth:1000px;DialogHeight=700px;status:no");
            else if (whichPg == 2)
                var newdialoguewin = window.showModalDialog("../Schedule/TaskDetail.aspx?TaskID=" + TaskID + "&Date=" + CurrDate, window, "dialogWidth:700px;DialogHeight=600px;status:no");
            else if (whichPg == 3)
                var newdialoguewin = window.showModalDialog("../Schedule/TaskStatus.aspx?TaskID=" + TaskID + "&Date=" + CurrDate, window, "dialogWidth:700px;DialogHeight=600px;status:no");
        }
    
    </script>

    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0">
                    <Items>
                        <mcs:MCSTabItem Text="默认桌面" Enable="false" />
                        <mcs:MCSTabItem Text="运营指标" NavigateURL="desktop_opi.aspx" Visible="false" />
                        <mcs:MCSTabItem Text="日跟踪表进度" NavigateURL="OA/TrackCard/Desktop_TrackCard.aspx" Visible="false" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="3" cellpadding="3" width="100%" align="center" border="0">
                    <tr>
                        <td valign="top" width="340px" style="border-right-style: solid; border-right-width: 1px;
                            border-right-color: #C0C0C0;">
                            <table cellspacing="2" cellpadding="2" align="left" border="0" height="300px" width="100%"
                                style="margin-top: -8px">
                                <tr valign="top">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_MyTask" runat="server">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td width="32px">
                                                                        <img alt="" src="../Images/Icon/26.gif" border="0">
                                                                    </td>
                                                                    <td valign="middle" height="32px">
                                                                        <h3>
                                                                            我的任务</h3>
                                                                    </td>
                                                                    <td valign="middle" height="32px" align="right">
                                                                        <asp:ImageButton ID="bt_RefreshTask" runat="server" ImageUrl="~/Images/gif/gif-0230.GIF"
                                                                            OnClick="bt_RefreshTask_Click" Style="height: 22px" ToolTip="刷新任务" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_TodayTask" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                AlternatingRowStyle-BackColor="White" AllowPaging="false" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="TaskName" />
                                                                    <asp:BoundField DataField="TaskCount" />
                                                                    <asp:HyperLinkField DataNavigateUrlFields="TaskUri"  Text="查看" ControlStyle-CssClass="listViewTdLinkS1">
                                                                    </asp:HyperLinkField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:Label ID="lab_SpecialPN" runat="server" Text="" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <table id="tbl_Notice" runat="server" cellspacing="0" cellpadding="0" align="left"
                                            border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td width="32px">
                                                                <img width="32px" alt="" src="../Images/gif/gif-0176.gif" border="0" />
                                                            </td>
                                                            <td valign="middle">
                                                                <h3>
                                                                    公司最新公告</h3>
                                                            </td>
                                                            <td align="right">
                                                                <a href="OA/PN/index.aspx" target="_self">
                                                                    <img alt="" src="../Images/gif/gif-0332.gif" title="查看更多" border="0"></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <mcs:UC_GridView ID="gv_Notice" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                AlternatingRowStyle-BackColor="White" AllowPaging="True" ShowHeader="False" OnPageIndexChanging="gv_Notice_PageIndexChanging">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="17px">
                                                                        <ItemTemplate>
                                                                            <%# (DateTime)DataBinder.Eval(Container.DataItem, "InsertTime") > DateTime.Today.AddDays(-7) ? "<img src='../Images/gif/gif-0304.gif' title='1周内的公告'/>" : "<img src='../Images/gif/gif-0297.gif'/> "%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="17px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "~/SubModule/OA/PN/read.aspx?ID={0}") %>'
                                                                                 Text='<%# Eval("Topic") %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                                            <br />
                                                                            <asp:Label ID="lb_InsertTime" runat="server" Text='<%# Eval("InsertTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <table id="tbl_NewKB" runat="server" cellspacing="0" cellpadding="0" width="100%"
                                                    border="0">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0" style="border-top-style: solid;
                                                                border-top-width: 1px; border-top-color: #C0C0C0;">
                                                                <tr>
                                                                    <td width="32px">
                                                                        <img alt="" src="../Images/icon/08.GIF" border="0">
                                                                    </td>
                                                                    <td valign="middle" height="32px">
                                                                        <h3>
                                                                            知识库最新发布</h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:HyperLink ID="hy_KBMore" runat="server" ImageUrl="~/Images/gif/gif-0332.gif"
                                                                                NavigateUrl="OA/KB/index.aspx" ToolTip="查看更多"></asp:HyperLink>                                                                        
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:DataList ID="dl_KB" runat="server" Width="100%" OnItemDataBound="dl_KB_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" cellpadding="0" align="left" border="0" width="100%">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td height="28px">
                                                                            <h3>
                                                                                <img src="../Images/gif/gif-0948.gif" /><a href="OA/KB/Index.aspx?RootCatalog=<%# DataBinder.Eval(Container.DataItem,"ID")%>"
                                                                                    style="font-weight: bold; color: #990033;"><font size="2.5">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Name")%></font> </a>
                                                                            </h3>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <mcs:UC_GridView ID="gv_KB" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                AlternatingRowStyle-BackColor="White" ShowHeader="False" AllowPaging="true" PageSize="5"
                                                                                OnPageIndexChanging="gv_KB_PageIndexChanging">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:HyperLink ID="hy_KB" runat="server" NavigateUrl='<%# Eval("ID", "OA/KB/ArticleDetail.aspx?ID={0}") %>'
                                                                                                 Text='<%# Eval("Title") %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                                                            <br />
                                                                                            <asp:Label ID="lb_ApproveTime" runat="server" Text='<%# Eval("ApproveTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </mcs:UC_GridView>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table></FooterTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_BBS" runat="server"
                                                    visible="false">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td width="32px">
                                                                        <img alt="" src="../Images/gif/gif-0318.gif" border="0">
                                                                    </td>
                                                                    <td valign="middle" height="32px">
                                                                        <h3>
                                                                            论坛最新发帖及回帖</h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <a href="OA/BBS/index.aspx" target="_self">
                                                                            <img alt="" src="../Images/gif/gif-0332.gif" title="查看更多" border="0"></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_ReplyLatest" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                AlternatingRowStyle-BackColor="White" AllowPaging="false" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:HyperLinkField DataNavigateUrlFields="ID,Board"  DataNavigateUrlFormatString="~/SubModule/OA/BBS/display.aspx?ID={0}&BoardID={1}"
                                                                        DataTextField="Title" ControlStyle-CssClass="listViewTdLinkS1">
                                                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                                                    </asp:HyperLinkField>
                                                                    <asp:BoundField DataField="Replyer" ItemStyle-Width="40px">
                                                                        <ItemStyle Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="LastReplyTime" ItemStyle-Width="100px">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="top" width="460px" style="border-right-style: solid; border-right-width: 1px;
                            border-right-color: #C0C0C0;">
                            <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0" style="margin-top: -8px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" align="left" border="0" width="100%" id="tbl_Decission"
                                                    runat="server">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td width="20px">
                                                                        <img width="32px" alt="" src="../Images/gif/gif-0072.gif" border="0" />
                                                                    </td>
                                                                    <td valign="middle" height="25px">
                                                                        <h3>
                                                                            临时申请</h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:HyperLink ID="hy_ApproveFeeApplySummary" runat="server" ImageUrl="~/Images/gif/gif-0584.gif"
                                                                            NavigateUrl="~/SubModule/FNA/FeeApply/ApplySummary/FeeApplySummary.aspx?State=1"
                                                                            ToolTip="查看待我审批的费用申请汇总单"></asp:HyperLink>
                                                                        <asp:HyperLink ID="hy_EWFMore" NavigateUrl="EWF/TaskList_NeedDecision.aspx" Target="_self"
                                                                            runat="server" ImageUrl="../Images/gif/gif-0332.gif" ToolTip="查看更多"></asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <mcs:MCSTabControl ID="MCSTab_EWF" runat="server" Width="100%" SelectedIndex="0"
                                                                            OnOnTabClicked="MCSTab_EWF_OnTabClicked">
                                                                            <Items>
                                                                                <mcs:MCSTabItem Text="待我审批" />
                                                                                <mcs:MCSTabItem Text="待我协审" />
                                                                                <mcs:MCSTabItem Text="抄送任务" />
                                                                                <mcs:MCSTabItem Text="我发起未审批的任务" />
                                                                            </Items>
                                                                        </mcs:MCSTabControl>
                                                                    </td>
                                                                </tr>
                                                                <tr class="tabForm">
                                                                    <td>
                                                                        <mcs:UC_GridView ID="gv_Decission" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                            AlternatingRowStyle-BackColor="White" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Decission_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ReadFlag").ToString() != "Y" ? "<img src='../Images/mailclose.gif' title='未读'/>" : "<img src='../Images/mailopen.gif' title='已读'/>"%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="AppName" HeaderText="流程名称" SortExpression="MessageSubject" />
                                                                                <asp:BoundField DataField="Title" HeaderText="任务标题" SortExpression="Title" />
                                                                                <asp:BoundField DataField="ApplyStaffName" HeaderText="发起人" ItemStyle-Width="40px" />
                                                                                <asp:BoundField DataField="JobStartTime" HeaderText="时间" DataFormatString="{0:MM-dd HH:mm}" />
                                                                                <asp:HyperLinkField DataNavigateUrlFields="CurrentJobID,TaskID" Target="_self" DataNavigateUrlFormatString="EWF/Recipient.aspx?CurrentJobID={0}&TaskID={1}"
                                                                                    Text="审批" ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="25px"></asp:HyperLinkField>
                                                                            </Columns>
                                                                        </mcs:UC_GridView>
                                                                        <mcs:UC_GridView ID="gv_EWFInviteConsultList" runat="server" AutoGenerateColumns="False"
                                                                            Visible="false" Width="100%" AllowPaging="True" PageSize="5" OnPageIndexChanging="gv_EWFInviteConsultList_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ReadFlag").ToString() != "Y" ? "<img src='../Images/mailclose.gif' title='未读'/>" : "<img src='../Images/mailopen.gif' title='已读'/>"%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="AppName" HeaderText="流程名称" />
                                                                                <asp:BoundField DataField="Title" HeaderText="任务标题" />
                                                                                <asp:BoundField DataField="ApplyStaffName" HeaderText="发起人" />
                                                                                <asp:BoundField DataField="TaskStatusName" HeaderText="任务状态" />
                                                                                <asp:BoundField DataField="InvitedTime" HeaderText="时间" DataFormatString="{0:MM-dd HH:mm}" />
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <a href="EWF/InviteConsultDetail.aspx?InviteConsult=<%# DataBinder.Eval(Container.DataItem, "ID")%>&TaskID=<%# DataBinder.Eval(Container.DataItem,"TaskID")%>"
                                                                                            class="listViewTdLinkS1" >回复邀审</a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </mcs:UC_GridView>
                                                                        <mcs:UC_GridView ID="gv_EWFCCList" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            Visible="false" AllowPaging="True" PageSize="5" OnPageIndexChanging="gv_EWFCCList_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ReadFlag").ToString() != "Y" ? "<img src='../Images/mailclose.gif' title='未读'/>" : "<img src='../Images/mailopen.gif' title='已读'/>"%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="AppName" HeaderText="流程名称" />
                                                                                <asp:BoundField DataField="Title" HeaderText="任务标题" />
                                                                                <asp:BoundField DataField="ApplyStaffName" HeaderText="发起人" />
                                                                                <asp:BoundField DataField="StartTime" HeaderText="时间" DataFormatString="{0:MM-dd HH:mm}" />
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <a href="EWF/TaskDetail_CC.aspx?JobCCID=<%# DataBinder.Eval(Container.DataItem, "ID")%>&TaskID=<%# DataBinder.Eval(Container.DataItem,"TaskID")%>"
                                                                                            class="listViewTdLinkS1" >查看及批注</a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </mcs:UC_GridView>
                                                                        <mcs:UC_GridView ID="gv_MyTaskList" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                                            Width="100%" AllowPaging="True" OnPageIndexChanging="gv_MyTaskList_PageIndexChanging"
                                                                            Visible="false">
                                                                            <Columns>
                                                                                <asp:HyperLinkField HeaderText="" Text="查看详细" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/SubModule/EWF/TaskDetail.aspx?TaskID={0}"
                                                                                    ControlStyle-CssClass="listViewTdLinkS1" />
                                                                                <asp:BoundField DataField="ID" HeaderText="流程标识" />
                                                                                <asp:BoundField DataField="App" HeaderText="流程名称" />
                                                                                <asp:TemplateField HeaderText="主题">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Status" HeaderText="当前状态" />
                                                                                <asp:BoundField DataField="StartTime" HeaderText="发起时间" />
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                无数据
                                                                            </EmptyDataTemplate>
                                                                        </mcs:UC_GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="MCSTab_EWF" EventName="OnTabClicked" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" align="left" border="0" width="100%" id="tbl_Mail"
                                            runat="server">
                                            <tr>
                                                <td>
                                                    <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td width="20px">
                                                                <img alt="" width="32px" src="../Images/email.png" border="0" />
                                                            </td>
                                                            <td valign="middle" height="25px">
                                                                <h3>
                                                                    我的邮箱</h3>
                                                            </td>
                                                            <td align="right">
                                                                <a href="OA/Mail/Compose.aspx" target="_self">
                                                                    <img alt="" src="../Images/gif/gif-0648.gif" title="写新邮件" border="0"></a>&nbsp;
                                                                <a href="OA/Mail/Index.aspx" target="_self">
                                                                    <img alt="" src="../Images/gif/gif-0332.gif" title="查看更多" border="0"></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <mcs:UC_GridView ID="gv_Mail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                AlternatingRowStyle-BackColor="White" AllowPaging="True" PageSize="5" ShowHeader="False"
                                                                OnPageIndexChanging="gv_Mail_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <%# (string)DataBinder.Eval(Container.DataItem, "IsRead").ToString() == "N" ? "<img src='../Images/mailClose.gif'>" : "<img src='../Images/mailOpen.gif'>"%>
                                                                            <%# DisplayHasAttachFile((int)DataBinder.Eval(Container.DataItem, "ID"))%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:HyperLinkField DataNavigateUrlFields="ID" Target="_self" DataNavigateUrlFormatString="~/SubModule/OA/Mail/Readmail.aspx?ID={0}"
                                                                        DataTextField="Subject" ItemStyle-Width="350px"></asp:HyperLinkField>
                                                                    <asp:BoundField DataField="Sender" ItemStyle-Width="40px" />
                                                                    <asp:BoundField DataField="SendTime" ItemStyle-Width="100px" />
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_BBS1" runat="server">
                                            <tr>
                                                <td>
                                                    <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td width="20px">
                                                                <img alt="" src="../Images/gif/gif-0318.gif" border="0">
                                                            </td>
                                                            <td valign="middle" height="25px">
                                                                <h3>
                                                                    论坛</h3>
                                                            </td>
                                                            <td align="right">
                                                                <a href="OA/BBS/index.aspx" target="_self">
                                                                    <img alt="" src="../Images/gif/gif-0332.gif" title="查看更多" border="0"></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="tabForm">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DataList ID="dl_Board" runat="server" Width="100%" OnItemDataBound="dl_Board_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" cellpadding="0" align="left" border="0" width="100%">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td height="28px">
                                                                            <h3>
                                                                                <img src="../Images/gif/gif-0057.gif" /><a href="OA/BBS/listview.aspx?Board=<%# DataBinder.Eval(Container.DataItem,"ID")%>"
                                                                                    style="font-weight: bold; color: #990033;"><font size="2.5">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Name")%></font> </a>
                                                                            </h3>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <mcs:UC_GridView ID="grd_BBS" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                AlternatingRowStyle-BackColor="White" ShowHeader="False" AllowPaging="true" PageSize="5"
                                                                                OnPageIndexChanging="grd_BBS_PageIndexChanging">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <a href="OA/BBS/display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "[\"ID\"]")%>&BoardID=<%# DataBinder.Eval(Container.DataItem, "[\"Board\"]")%>"
                                                                                                class="listViewTdLinkS1">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "[\"Title\"]")%></a>
                                                                                        </ItemTemplate>
                                                                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lab_Sender" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[\"Sender\"]")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="40px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lab_SendTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[\"SendTime\"]")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="100px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </mcs:UC_GridView>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table></FooterTemplate>
                                                            </asp:DataList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="2" cellpadding="2" align="left" border="0" width="100%" style="margin-top: -8px">
                                <tr valign="top">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td width="32px">
                                                                        <img alt="" src="../Images/Icon/279.gif" border="0" style="height: 31px; width: 37px">
                                                                    </td>
                                                                    <td>
                                                                        <h3>
                                                                            报表查看</h3>
                                                                    </td>
                                                                    <td valign="middle" height="32px" align="right">
                                                                        <a href="Reports/Rpt_ReportList.aspx" class="listViewTdLinkS1" >
                                                                            <img src="../Images/gif/gif-0230.GIF" border="0" /></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <mcs:MCSTabControl ID="MCSTabControl2" runat="server" Width="100%" SelectedIndex="0"
                                                                            OnOnTabClicked="MCSTab_EWF_OnTabClicked">
                                                                            <Items>
                                                                                <mcs:MCSTabItem Text="常用报表" />
                                                                                <mcs:MCSTabItem Text="报表平台" NavigateURL="Reports/Rpt_ReportList.aspx" />
                                                                            </Items>
                                                                        </mcs:MCSTabControl>
                                                                    </td>
                                                                </tr>
                                                                <tr class="tabForm">
                                                                    <td>
                                                                        <mcs:UC_GridView ID="gv_ReportList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                            DataKeyNames="ReportID" PageSize="10" Width="100%" OnRowDeleting="gv_ReportList_RowDeleting"
                                                                            OnPageIndexChanging="gv_ReportList_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:HyperLinkField DataNavigateUrlFields="ReportID" DataNavigateUrlFormatString="Reports/ReportViewer.aspx?Report={0}"
                                                                                    DataTextField="Name" HeaderText="报表名称" ControlStyle-CssClass="listViewTdLinkS1"
                                                                                    >
                                                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                                                </asp:HyperLinkField>
                                                                                <asp:BoundField DataField="ViewCount" HeaderText="浏览次数" Visible="false" />
                                                                                <asp:CommandField ShowDeleteButton="True" DeleteText="移出常用" ControlStyle-CssClass="listViewTdLinkS1" />
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                无数据
                                                                            </EmptyDataTemplate>
                                                                        </mcs:UC_GridView>
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
                                <tr valign="top">
                                    <td>
                                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td width="32px">
                                                    <img alt="" src="../Images/Icon/12.gif" border="0">
                                                </td>
                                                <td valign="middle" height="32px">
                                                    <h3>
                                                        <a href="Reports/ReportViewer.aspx?Report=97186d42-7c80-4215-8d9c-9280af2ef739&LoadCache=false"
                                                            class="listViewTdLinkS1" >审批进度查询</a></h3>
                                                </td>
                                                <td valign="middle" height="32px" align="right">
                                                    <a href="Reports/ReportViewer.aspx?Report=97186d42-7c80-4215-8d9c-9280af2ef739&LoadCache=false"
                                                        class="listViewTdLinkS1" >
                                                        <img src="../Images/gif/gif-0230.GIF" border="0" /></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td width="32px">
                                                                        <img alt="" src="../Images/Icon/31.gif" border="0">
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <h3>
                                                                            填报进度表</h3>
                                                                    </td>
                                                                    <td valign="middle">
                                                                        <asp:Label ID="lb_Staff" runat="server" Text="填报人:"></asp:Label>
                                                                        <mcs:MCSSelectControl ID="select_FillDataStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                                            Width="120" OnSelectChange="select_FillDataStaff_SelectChange" />
                                                                    </td>
                                                                    <td>
                                                                        <a href="Reports/ReportViewer.aspx?Report=3c99e65d-cfb0-4e5b-9156-4f2594a9a290&LoadCache=false"
                                                                            class="listViewTdLinkS1" >查看填报进度明细</a>
                                                                    </td>
                                                                    <td valign="middle" height="32px" align="right">
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/gif/gif-0230.GIF"
                                                                            OnClick="bt_RefreshFillDataProgress_Click" Style="height: 22px" ToolTip="刷新" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_FillDataProgress" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                AlternatingRowStyle-BackColor="White" AllowPaging="false" 
                                                                ondatabound="gv_FillDataProgress_DataBound" DataKeyNames="ItemName,ItemTargetCount,ItemCompleteCount">
                                                                <HeaderStyle CssClass="" BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="ItemName" HeaderText="项目" />
                                                                    <asp:BoundField DataField="ItemTargetCount" HeaderText="目标数量" />
                                                                   <%-- <asp:BoundField DataField="ItemCompleteCount" HeaderText="实际数量" Visible="false" />--%>
                                                                    <asp:TemplateField HeaderText="实际数量">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lb_CompleteCount" runat="server" Text='<%#Eval("ItemCompleteCount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="完成率">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lb_Percent" runat="server" Text='<%# (int)Eval("ItemTargetCount")==0? "0%" : ((decimal)(int)Eval("ItemCompleteCount")/(int)Eval("ItemTargetCount")).ToString("0.#%") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:HyperLinkField DataNavigateUrlFields="ItemUri"  Text="查看" ControlStyle-CssClass="listViewTdLinkS1">
                                                                    </asp:HyperLinkField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="color: Blue">
                                                            提示:该进度表数据最多有30分钟缓存延迟，如需刷新进度数据，请点击该栏目右上角的放大镜图标。
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <span style="color: Red">数据加载中，请稍候</span></ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
