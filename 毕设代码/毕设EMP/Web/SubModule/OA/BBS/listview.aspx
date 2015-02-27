<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="listview.aspx.cs" Inherits="SubModule_OA_BBS_listview" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System" %>
<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
 <table width="100%" border="0" cellpadding="0" cellspacing="0">
       <tr>
            <td background="../../../Images/bbsback.jpg">
                <img src="../../../Images/bbs.jpg">
            </td>
            <td align="right" id="Td1" background="../../../Images/bbsback.jpg" runat="server" visible="true">
              
                     <asp:Button ID="bt_Search" runat="server" Visible="true" Text="搜索" OnClick="bt_Search_Click">
                     </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
         <tr>
           <td align="left">
                <a href="index.aspx"><asp:Label ID="lblCatalog" Font-Size="11" runat="server" Font-Bold="true"></asp:Label></a>>>
                <asp:Label ID="lblBoardName" ForeColor="#990000" Font-Size="11" runat="server" Font-Bold="true"></asp:Label>
            </td> 
            <td  align="right">
               <table>
                   <tr>
                      <td><h2>版主：</h2></td>
                       <asp:Repeater ID="rpt_boardmaster"  runat="server">
                        <ItemTemplate>
                           <td>
                           <a href="../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"UserName")%>">
                            <asp:Label ID="lblMaster" runat="server" Font-Bold="true">  <%# DataBinder.Eval(Container.DataItem, "UserName") == null ? "*空缺中*" : DataBinder.Eval(Container.DataItem, "UserName")%> 
                              </asp:Label> </a>
                                </td>
                           </ItemTemplate>
                     </asp:Repeater>
                   </tr>
               </table>   
            
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="tabForm">
      <tr>
       <td width="200px"  valign="top" >
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:TreeView ID="tr_Catalog" runat="server" Width="100%" ImageSet="Arrows" Target="if_ListViewFrame"
                            ExpandDepth="0" OnSelectedNodeChanged="tr_Catalog_SelectedNodeChanged">
                            <NodeStyle CssClass="listViewTdLinkS1"/>
                             <SelectedNodeStyle BackColor="#E0E0E0"/>
                        </asp:TreeView>
                    </td>
                </tr>
                <tr>
                   <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left"><h3>点击榜:</h3></td>
                </tr>
                <tr>
                 <td align="right">
                        <cc1:mcstabcontrol ID="MCSTabControl1" runat="server" 
                           CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%"  >
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="日" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="周" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="月" Description=""
                                    Value="2" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="总" Description=""
                                    Value="3" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:mcstabcontrol>
                    </td>
                </tr>
                <tr> 
                  <td>
                        <mcs:UC_GridView ID="gv_HotForumItem" runat="server" PanelCode="Panel_BBS_HotForumItemList" PageSize="10" AutoGenerateColumns="False"
                         DataKeyNames="BBS_ForumItem_ID"   Width="100%" Visible="true">
                         <Columns>
                            <asp:TemplateField  ItemStyle-Width="140px" HeaderText="主题">
                                <ItemTemplate>
                                <a href="../../OA/BBS/display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")%>" > <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Title")%></a>
                              </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>     
            </table>
         </td>
         <td width="1%">
         </td>
         <td valign="top">
            <table cellspacing="0" cellpadding="0" width="100%" border="0" >
            <tr>
             <td align="left">
                   <cc1:mcstabcontrol ID="MCSTabControl2" runat="server" 
                           CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                            SelectedIndex="0" Width="100%"  >
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="全部帖子" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="精华帖子" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:mcstabcontrol>
                </td>
                <td align="right" >
                 <asp:Button ID="bt_NewItem" runat="server" OnClick="bt_NewItem_Click" 
                        Text="发送新贴" Visible="true" Width="65px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="bt_Delete" runat="server" OnClick="btn_Delete_Click" 
                        OnClientClick="javascript:return confirm('您确认要删除吗?');" Text="删除选中贴" 
                        Width="70px" />
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
             <td colspan="2">
                    <mcs:UC_GridView ID="dgrd_Result" runat="server" PageSize="10" PageIndex="0" AutoGenerateColumns="False"
                        Width="100%" PanelCode="Panel_BBS_ForumItemList" AllowPaging="True" 
                        DataKeyNames="BBS_ForumItem_ID" 
                        onpageindexchanging="dgrd_Result_PageIndexChanging">
                        <Columns>
                           <asp:TemplateField HeaderText="状态"  ItemStyle-Width="60px">
                                <ItemTemplate>
                             <%# GetTypeIsPith((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID"))%>
                             <%# GetTypeIsTop((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID"))%>
                             <%# GetType((int)DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")) %> 
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="贴子主题" ItemStyle-Width="500px">
                                <ItemTemplate>
                                <a href="display.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ID")%>&BoardID=<%=BoardID%>"> <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Title")%></a>
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发布人" >
                                    <ItemTemplate>
                                     <a href="../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"BBS_ForumItem_Sender")%>">
                                       <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_Sender")%> </a>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="最后回复" >
                                    <ItemTemplate>
                                     <a href="../SM/MsgSender.aspx?SendTo=<%# DataBinder.Eval(Container.DataItem,"BBS_ForumItem_LastReplyer")%>">
                                       <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_LastReplyer")%> </a>
                                  </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="人气">
                                <ItemTemplate>
                               <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_ReplyTimes").ToString() %> / <%# DataBinder.Eval(Container.DataItem, "BBS_ForumItem_HitTimes").ToString() %>&nbsp;
                              </ItemTemplate>
                            </asp:TemplateField> 
                          <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_ID" runat="server" />
                                </ItemTemplate>
                          </asp:TemplateField> 
                        </Columns>
                        <EmptyDataTemplate>
                            无数据</EmptyDataTemplate>
                    </mcs:UC_GridView>
                </td>
            </tr>
            </table>
            <table width="100%">
                <tr>
                    <td id="Td2" align="center" runat="server" visible="false">
                        <a runat="server" id="backlink">返回论坛返回论坛</a>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        图例说明:
                        <img src="../../../images/hotfolder.gif">热门贴（点击多于5）
                        <img src="../../../images/folder.gif">普通贴
                        <img src="../../../images/istop.jpg">置顶贴
                        <img src="../../../images/pith.jpg">精华贴
                    </td>
                </tr>
            </table>
         </td>
      </tr>
    </table>
    </ContentTemplate>
     <Triggers>
        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
        <asp:AsyncPostBackTrigger ControlID="MCSTabControl2" EventName="OnTabClicked" />
        <asp:AsyncPostBackTrigger ControlID="tr_Catalog" EventName="SelectedNodeChanged" />
     </Triggers>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

