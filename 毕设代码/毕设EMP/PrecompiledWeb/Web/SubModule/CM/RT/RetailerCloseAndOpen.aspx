<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerCloseAndOpen, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        门店状态批量设置
                                    </h2>
                                </td>
                                <td>
                                    <div style="float: right">
                                        <asp:Button ID="bt_OK" runat="server" Text="确  认" OnClientClick='confirm("请确认门店ID正确，关闭的门店将不能申请返利费用！")'
                                            Width="62px" OnClick="bt_OK_Click" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div width="100%">
                <h3>
                    失效门店ID:</h3>
                <span>操作说明:复制EXCEL中失效门店ID，门店失效之后，生成返利费用为0</span>
                <div style="display: inline; margin-right: 40">
                    <asp:TextBox ID="txt_LoseID" runat="server" Height="274px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_LoseErrorInfo" runat="server"></asp:Literal>
                <h3>
                    关闭门店ID:</h3>
                <span>操作说明:复制EXCEL中关闭门店ID，门店关闭之后，生成返利费用为0</span>
                <div>
                    <asp:TextBox ID="txt_CloseID" runat="server" Height="274px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_CloseErrorInfo" runat="server"></asp:Literal>
                <h3>
                    开通门店ID:</h3>
                <span>操作说明:复制EXCEL中开启门店ID</span>
                <div style="display: inline; margin-right: 40">
                    <asp:TextBox ID="txt_OpenID" runat="server" Height="274px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_OpenErrorInfo" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="bt_OK" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
