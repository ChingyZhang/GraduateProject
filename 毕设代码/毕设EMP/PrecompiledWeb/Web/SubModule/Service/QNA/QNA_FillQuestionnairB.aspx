<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_FillQuestionnairB, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>填写问卷</title>
    <base target="_self">
    </base>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellspacing="0" cellpadding="0" width="500px" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                    </td>
                                    <td nowrap="noWrap" style="width: 180px">
                                        <h2>
                                            <asp:Label ID="lb_PageTitle" runat="server" Text="填写问卷"></asp:Label></h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Save" runat="server" Text="保存问卷" OnClick="bt_Save_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            问卷名称：
                            <asp:Label ID="lb_ProjectName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">
                                            标题：<asp:Label ID="lb_QuestionTitle" runat="server" Font-Bold="True" ForeColor="#FF0000"
                                                Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                            <asp:Label ID="lb_ID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                            <asp:Label ID="lbl_OptionMode" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"OptionMode") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            描述：<asp:Literal ID="ltl_Description" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rbl_Result" runat="server" DataTextField="OptionName" DataValueField="ID"
                                                RepeatColumns="4" RepeatDirection="Horizontal" Visible="False" AutoPostBack="True"
                                                OnSelectedIndexChanged="rbl_Result_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                            <asp:CheckBoxList ID="cbl_Result" runat="server" DataTextField="OptionName" DataValueField="ID"
                                                RepeatColumns="4" RepeatDirection="Horizontal" Visible="False" AutoPostBack="True"
                                                OnSelectedIndexChanged="cbl_Result_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                            <asp:TextBox ID="tbx_Result" runat="server" Visible="False" Width="300px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="rev_Result" runat="server" ErrorMessage="格式不对" ControlToValidate="tbx_Result"
                                                Enabled="false"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>
                                </SeparatorTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
