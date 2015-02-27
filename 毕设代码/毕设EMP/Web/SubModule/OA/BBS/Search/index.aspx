<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="SubModule_OA_BBS_Search_index" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td background="../../../../Images/bbsback.jpg">
                                    <img src="../../../../Images/bbs.jpg">
                                </td>
                                <td id="Td1" background="../../../../Images/bbsback.jpg" runat="server" visible="true">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="left" style="width: 25px">
                                    <img src="../../../../Images/search.gif" alt="" style="width: 20px; height: 20px" />
                                </td>
                                <td align="left">
                                    <a href="../../BBS/index.aspx">
                                        <asp:Label ID="lblCatalog" Text="公司论坛" Font-Size="11" runat="server" Font-Bold="true"></asp:Label></a>>>
                                    <asp:Label ID="lblSearch" ForeColor="#990000" Text="搜索" Font-Size="11" runat="server"
                                        Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                            <tr>
                                <td width="200px" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="tr_Catalog" runat="server" Width="100%" ImageSet="Arrows" Target="if_ListViewFrame"
                                                    ExpandDepth="0" OnSelectedNodeChanged="tr_Catalog_SelectedNodeChanged">
                                                    <NodeStyle CssClass="listViewTdLinkS1" />
                                                    <SelectedNodeStyle BackColor="#E0E0E0" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <h3>
                                                    点击榜:</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                    SelectedIndex="0" Width="100%">
                                                    <Items>
                                                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="日" Description="" Value="0"
                                                            Enable="True" Visible="True"></cc1:MCSTabItem>
                                                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="周" Description="" Value="1"
                                                            Enable="True" Visible="True"></cc1:MCSTabItem>
                                                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="月" Description="" Value="2"
                                                            Enable="True" Visible="True"></cc1:MCSTabItem>
                                                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="总" Description="" Value="3"
                                                            Enable="True" Visible="True"></cc1:MCSTabItem>
                                                    </Items>
                                                </cc1:MCSTabControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_HotForumItem" runat="server" PanelCode="Panel_BBS_HotForumItemList"
                                                    PageSize="10" AutoGenerateColumns="False" DataKeyNames="BBS_ForumItem_ID" Width="100%"
                                                    Visible="true">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="140px" HeaderText="主题">
                                                            <ItemTemplate>
                                                                <a href="../../BBS/display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")%>">
                                                                    <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Title")%></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td rowspan="2" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table id="Table1" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td class="tabForm" align="center">
                                                            <table cellspacing="0" cellpadding="0" width="100%" align="center" id="tbl_Search"
                                                                runat="server">
                                                                <tr>
                                                                    <td align="left" colspan="6" style="height: 22px" valign="middle">
                                                                        <h4 class="dataLabel">
                                                                            搜索</h4>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="left" width="88" class="dataLabel" style="height: 26px">
                                                                        &nbsp;输入关键字
                                                                    </td>
                                                                    <td colspan="5" class="dataField" style="height: 26px" align="left">
                                                                        &nbsp;<asp:TextBox ID="tbx_Key" runat="server" Width="440"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="left" width="88" class="dataLabel" style="height: 26px">
                                                                        &nbsp;高级查询
                                                                    </td>
                                                                    <td colspan="5" class="dataField" style="height: 26px" align="left">
                                                                        &nbsp;
                                                                        <asp:RadioButton ID="rbtn_author" runat="server" Text="搜索作者" GroupName="searchoption"
                                                                            Checked="True"></asp:RadioButton>
                                                                        <asp:RadioButton ID="Radiobutton1" runat="server" Text="搜索主题" GroupName="searchoption">
                                                                        </asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 10pt; font-family: 宋体">
                                                                            日期范围<asp:TextBox ID="tbx_Time" runat="server" Width="80px"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddl_Time" runat="server" Width="80">
                                                                                <asp:ListItem Value="0">全部</asp:ListItem>
                                                                                <asp:ListItem Value="w">星期</asp:ListItem>
                                                                                <asp:ListItem Value="d">日</asp:ListItem>
                                                                                <asp:ListItem Value="m">月</asp:ListItem>
                                                                                <asp:ListItem Value="y">年</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            论坛选择<asp:DropDownList ID="dll_Board" runat="server" Width="150px" DataTextField="Name"
                                                                                DataValueField="ID">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Button ID="btn_OK" runat="server" Text="开始搜索" Width="80px" OnClick="btn_OK_Click">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm">
                                                <mcs:UC_GridView ID="dgrd_Result" runat="server" PageSize="10" PageIndex="0" AutoGenerateColumns="False"
                                                    Width="100%" PanelCode="Panel_BBS_ForumItemList" AllowPaging="True" DataKeyNames="BBS_ForumItem_ID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="状态" ItemStyle-Width="60px">
                                                            <ItemTemplate>
                                                                <%# GetTypeIsPith((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID"))%>
                                                                <%# GetTypeIsTop((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID"))%>
                                                                <%# GetType((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="贴子主题" ItemStyle-Width="500px">
                                                            <ItemTemplate>
                                                                <a href="../../BBS/display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")%>">
                                                                    <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Title")%></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="发布人">
                                                            <ItemTemplate>
                                                                <a href="../../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"BBS_ForumItem_Sender")%>">
                                                                    <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Sender")%>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="最后回复">
                                                            <ItemTemplate>
                                                                <a href="../../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"BBS_ForumItem_LastReplyer")%>">
                                                                    <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_LastReplyer")%>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="人气">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ReplyTimes").ToString() %>
                                                                /
                                                                <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_HitTimes").ToString() %>&nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据</EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" id="td_tl" runat="server">
                                                图例说明
                                                <img src="../../../../images/hotfolder.gif">热门贴（点击多于5）
                                                <img src="../../../../images/folder.gif">普通贴
                                                <img src="../../../../images/istop.jpg">置顶贴
                                                <img src="../../../../images/pith.jpg">精华贴
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
