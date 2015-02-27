<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_PromotorRefreshCallFee, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

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
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        导购通讯补贴批量设置
                                    </h2>
                                </td>
                                <td>
                                    <div style="float: right">
                                        <asp:Button ID="bt_OK" runat="server" Text="确  认" OnClientClick='confirm("请确认导购ID正确，无我司通讯补贴的导购将不能申请我司通讯补贴！")'
                                            Width="62px" OnClick="bt_OK_Click" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div width="100%">
                <h3>
                    无通讯补贴导购ID:</h3>
                <span>操作说明:复制EXCEL中无通讯补贴导购ID,将导购薪酬定义中【我司通讯补贴】置0</span>
                <div style="display: inline; margin-right: 40">
                    <asp:TextBox ID="txt_NOsubsidyID" runat="server" Height="274px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_NOsubsidyErrorInfo" runat="server"></asp:Literal>
                <h3>
                    有通讯补贴导购ID:</h3>
                <span>操作说明:复制EXCEL中有通讯补贴导购ID,将导购薪酬定义中【我司通讯补贴】设为
                    <asp:Label ID="TextBox1" runat="server" Text="" ForeColor="Red"></asp:Label></span>
                <div>
                    <asp:TextBox ID="txt_SubsidyID" runat="server" Height="274px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_SubsidyErrorInfo" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="bt_OK" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
