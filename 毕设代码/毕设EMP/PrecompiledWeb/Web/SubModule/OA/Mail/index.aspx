<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Mail_index, App_Web_siz_p2qv" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function checkAll() {
            var items = document.getElementsByTagName("input");
            for (var i = 0; i < items.length; i++) {

                if (items[i].type == "checkbox") {
                    items[i].checked = true;
                }
            }
        }
        function unCheckAll() {
            var items = document.getElementsByTagName("input");
            for (var i = 0; i < items.length; i++) {

                if (items[i].type == "checkbox") {
                    items[i].checked = false;
                }
            }
        }
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
        <tr>
            <td align="right" width="20">
                <img height="16" src="../../../Images/icon/284.gif" width="16" alt="" />
            </td>
            <td align="right" width="60">
                <h2>
                    邮件管理</h2>
            </td>
            <td align="right">
                <asp:Label ID="lb_sampleSelect" runat="server" Width="88px">快捷查询</asp:Label>
                <asp:DropDownList ID="ddl_sampleSelect" runat="server" Width="120px">
                    <asp:ListItem Value="Sender">发件人</asp:ListItem>
                    <asp:ListItem Value="Receiver">收件人</asp:ListItem>
                    <asp:ListItem Value="Subject">主题</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="tbx_SelectContent" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="bt_sampleSelect" runat="server" Visible="true" Text="查找" Width="58px"
                    OnClick="bt_sampleSelect_Click" />
                <asp:Label ID="lblMsg" runat="server" Width="88px"></asp:Label></font>&nbsp;
                <asp:Button ID="writenewmail" runat="server" Visible="true" Text="写新邮件" OnClick="btnwritenewmail_Click"
                    Width="58px"></asp:Button>&nbsp;
                <asp:Button ID="btnClear" runat="server" Visible="False" Text="清 空" OnClick="btnClear_Click">
                </asp:Button>
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="删 除" Visible="False" OnClick="btnDelete_Click">
                </asp:Button>&nbsp;
                <input id="Button1" type="button" runat="server" value="全 选" onclick="checkAll()"
                    class="button" />&nbsp;
                <input id="Button2" type="button" runat="server" value="取 消" onclick="unCheckAll()"
                    class="button" />&nbsp;
                <asp:DropDownList ID="listFolderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FolderListChange"
                    Visible="false">
                    <asp:ListItem Value="0">请选择邮件夹...</asp:ListItem>
                    <asp:ListItem Value="1">收件夹</asp:ListItem>
                    <asp:ListItem Value="2">已发邮件</asp:ListItem>
                    <asp:ListItem Value="3">垃圾邮件</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr class="tabForm" id="tr_ExtMailReceive" runat="server" visible="false">
            <td align="center">
                <asp:DropDownList ID="listExtMail" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnBeginReceive" runat="server" Text="开始接收" OnClick="btnBeginReceive_Click"
                    Visible="false" Width="64px"></asp:Button>
                <asp:Button ID="btnExtPopSetup" runat="server" Text="外部邮箱设置" OnClick="btnExtPopSetup_Click"
                    Visible="false" Width="86px"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left">
                            <h2>
                                邮件列表</h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                SelectedIndex="0" Width="100%">
                                <Items>
                                    <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="收件箱" Description=""
                                        Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                    <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="发件箱" Description=""
                                        Value="2" Enable="True" Visible="True"></cc1:MCSTabItem>
                                    <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="垃圾邮件" Description=""
                                        Value="3" Enable="True" Visible="False"></cc1:MCSTabItem>
                                </Items>
                            </cc1:MCSTabControl>
                        </td>
                    </tr>
                    <tr class="tabForm">
                        <td>
                            <mcs:UC_GridView ID="ud_Mail" runat="server" PageSize="10" PageIndex="0" AutoGenerateColumns="False"
                                Width="100%" PanelCode="Page_ML_MailList_1" AllowPaging="True" DataKeyNames="ML_Mail_ID"
                                OnPageIndexChanging="ud_Mail_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="70px" HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                            &nbsp;
                                            <%# (string)DataBinder.Eval(Container.DataItem,"ML_Mail_IsRead").ToString()=="N"?"<img src='../../../Images/mailclose.gif'>":"<img src='../../../Images/mailopen.gif'>" %>&nbsp;
                                            <%# DisplayHasAttachFile((int)DataBinder.Eval(Container.DataItem, "ML_Mail_ID"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="ML_Mail_ID" HeaderText="标题" Target="_self"
                                        DataNavigateUrlFormatString="Readmail.aspx?ID={0}" DataTextField="ML_Mail_Subject"
                                        ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="500px"></asp:HyperLinkField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据</EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
