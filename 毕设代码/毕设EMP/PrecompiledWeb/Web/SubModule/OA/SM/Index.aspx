<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_SM_index, App_Web_mlnd3-hy" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function selectAll() {
            var len = document.forms[0].elements.length;
            var i;
            for (i = 0; i < len; i++) {
                if (document.forms[0].elements[i].type == "checkbox") {
                    document.forms[0].elements[i].checked = true;
                }
            }
        }

        function unSelectAll() {
            var len = document.forms[0].elements.length;
            var i;
            for (i = 0; i < len; i++) {
                if (document.forms[0].elements[i].type == "checkbox") {
                    document.forms[0].elements[i].checked = false;
                }
            }
        }

        function SendMsg(username, realname) {
            var w;
            if (username != '')
                w = window.showModalDialog('MsgSender.aspx?SendTo=' + username + '&SendToRealName=' + realname, window, 'dialogWidth:600px;dialogheight:350px;toolbar=no;status=no,;scrollbars=yes;resizable=yes');
            else
                w = window.showModalDialog('MsgSender.aspx', window, 'dialogWidth:600px;dialogheight:350px;toolbar=no;status=no,;scrollbars=yes;resizable=yes');
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h1>
                            ---短讯管理---</h1>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lb_sampleSelect" runat="server" Width="88px">快捷查询</asp:Label>
                        <asp:DropDownList ID="ddl_sampleSelect" runat="server" Width="120px">
                            <asp:ListItem Value="SenderRealName">发送者</asp:ListItem>
                            <asp:ListItem Value="ReceiverRealName" Enabled="false">接收者</asp:ListItem>
                            <asp:ListItem Value="Content">短信内容</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="tbx_SelectContent" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="bt_sampleSelect" runat="server" Visible="true" Text="查找" Width="58px"
                            OnClick="bt_sampleSelect_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我的消息" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="已发送消息" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Description="" Enable="True" ImgURL="" NavigateURL="javascript:SendMsg('','');"
                                    Target="_self" Text="写新消息" Value="2" Visible="True" />
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr runat="server" id="tr_send" visible="false" class="tabForm">
                    <td>
                        <mcs:UC_GridView ID="ud_Grid_Send" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="ID,MsgID" Width="100%"
                            OnPageIndexChanging="ud_Grid_Send_PageIndexChanging" OnSorting="ud_Grid_Send_Sorting"
                            OnRowDeleting="ud_Grid_Send_RowDeleting" Binded="False" ConditionString="" 
                            OrderFields="" PanelCode="" TotalRecordCount="0">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="接收者">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_Sender" runat="server" NavigateUrl=<%# "javascript:SendMsg('"+ Server.UrlEncode(DataBinder.Eval(Container.DataItem, "Receiver").ToString()).Replace("%","*")+"','"+ DataBinder.Eval(Container.DataItem,"ReceiverRealName") +"');"%>
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ReceiverRealName") %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否已读">
                                    <ItemTemplate>
                                        <%# (string)DataBinder.Eval(Container.DataItem, "IsRead").ToString() == "N" ? "<img src='../../../Images/mailclose.gif'>" : "<img src='../../../Images/mailopen.gif'>"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="短讯类型" Visible="false">
                                    <ItemTemplate>
                                        <%# (string)DataBinder.Eval(Container.DataItem,"Type").ToString()=="1"?"站内短讯":"站外短讯" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="短讯内容">
                                    <ItemTemplate>
                                        <asp:Literal ID="Label1" runat="server" Text='<%# Bind("Content") %>'></asp:Literal>
                                    </ItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="SendTime" HeaderText="发送时间" SortExpression="SendTime" />
                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                        </mcs:UC_GridView>
                        <br />
                        <input class="button" onclick="selectAll()" type="button" value="全 选" id="Button2">&nbsp;
                        <input class="button" onclick="unSelectAll()" type="button" value="取 消">&nbsp;
                        <asp:Button ID="bt_SendDelete" runat="server" Text="删除选中" OnClick="btn_SendDelete_Click"
                            OnClientClick="javascript:return confirm('您确认要删除吗?');"></asp:Button>&nbsp;
                        <asp:Button ID="bt_SendDeleteAll" runat="server" Text="删除所有" OnClientClick="javascript:return confirm('您确认要删除所有吗?');"
                            OnClick="bt_SendDeleteAll_Click" />
                    </td>
                </tr>
                <tr runat="server" id="tr_msglist" visible="true" class="tabForm">
                    <td>
                        <mcs:UC_GridView ID="ud_Grid_Recv" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="ID,MsgID" Width="100%" OnPageIndexChanging="ud_Grid_Recv_PageIndexChanging"
                            OnSorting="ud_Grid_Recv_Sorting" OnRowDeleting="ud_Grid_Recv_RowDeleting" OnSelectedIndexChanging="ud_Grid_Recv_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_Msg_ID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="MsgID" HeaderText="MsgID" Visible="false" />
                                <asp:TemplateField HeaderText="发送者">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_Sender" runat="server" NavigateUrl=<%# "javascript:SendMsg('"+ Server.UrlEncode(DataBinder.Eval(Container.DataItem, "Sender").ToString()).Replace("%","*")+"','"+ DataBinder.Eval(Container.DataItem,"SenderRealName") +"');"%>
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SenderRealName") %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="短讯类型" Visible="false">
                                    <ItemTemplate>
                                        <%# (string)DataBinder.Eval(Container.DataItem,"Type").ToString()=="1"?"站内短讯":"站外短讯" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否已读">
                                    <ItemTemplate>
                                        <%# (string)DataBinder.Eval(Container.DataItem, "IsRead").ToString() == "N" ? "<img src='../../../Images/mailclose.gif'>" : "<img src='../../../Images/mailopen.gif'>"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="短讯内容">
                                    <ItemTemplate>
                                        <asp:Literal ID="Label1" runat="server" Text='<%# Bind("Content") %>'></asp:Literal>
                                    </ItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="SendTime" HeaderText="发送时间" SortExpression="SendTime" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="已阅" Visible='<%#(string)DataBinder.Eval(Container.DataItem, "IsRead").ToString() == "N" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                        </mcs:UC_GridView>
                        <br />
                        <input class="button" onclick="selectAll()" type="button" value="全 选" id="Button1">&nbsp;
                        <input class="button" onclick="unSelectAll()" type="button" value="取 消">&nbsp;
                        <asp:Button ID="btnMsgDelete" runat="server" Text="删除选中" OnClick="btnMsgDelete_Click"
                            OnClientClick="javascript:return confirm('您确认要删除吗?');"></asp:Button>&nbsp;
                        <asp:Button ID="bt_MsgDeleteAll" runat="server" Text="删除所有" OnClientClick="javascript:return confirm('您确认要删除所有吗?');"
                            OnClick="bt_MsgDeleteAll_Click" />
                        &nbsp;<asp:Button ID="bt_AllRead" runat="server" onclick="bt_AllRead_Click" 
                            Text="全部已阅" Width="60px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="MCSTabControl2" EventName="OnTabClicked" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
