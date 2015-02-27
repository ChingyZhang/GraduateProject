<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_FillQuestionnairA, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>填写问卷</title>
    <base target="_self">
    </base>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2 {
            text-align: left;
            font-size: 12px;
            font-weight: normal;
            height: 25px;
        }
        .style3 {
            font-size: 12px;
            font-weight: normal;
            height: 25px;
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
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td width="80px" class="style2">
                                        问卷名称
                                    </td>
                                    <td class="style3">
                                        <asp:Label ID="lb_ProjectName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80px" class="dataLabel">
                                        <span class="style1">标题</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QuestionTitle" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80px" class="dataLabel">
                                        描述
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QuestionDescription" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80px" class="dataLabel">
                                        结果选项
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="cbl_Result" runat="server" AutoPostBack="True" DataTextField="OptionName"
                                            DataValueField="ID" RepeatColumns="3" RepeatDirection="Horizontal" Visible="False"
                                            OnSelectedIndexChanged="cbl_Result_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                        <asp:RadioButtonList ID="rbl_Result" runat="server" AutoPostBack="True" DataTextField="OptionName"
                                            DataValueField="ID" RepeatColumns="3" RepeatDirection="Horizontal" Visible="False"
                                            OnSelectedIndexChanged="rbl_Result_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="tbx_Result" runat="server" Rows="3" TextMode="MultiLine" Width="400px"
                                            Visible="False"></asp:TextBox><asp:RegularExpressionValidator ID="rev_Result" runat="server"
                                                ErrorMessage="格式不对" ControlToValidate="tbx_Result" Enabled="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80px" class="dataLabel">
                                        下一标题
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_NextQuestion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" height="32px">
                                        <asp:Button ID="bt_Previous" runat="server" CausesValidation="False" OnClientClick="return confirm(&quot;是否确认回到上一题?&quot;)"
                                            Text="上一题" OnClick="bt_Previous_Click" Width="70px" />
                                        <asp:Button ID="bt_Next" runat="server" Text="下一题" OnClick="bt_Next_Click" Width="70px"
                                            ForeColor="Red" />
                                        <asp:Button ID="bt_ToEnd" runat="server" Text="结束问卷" Width="70px" CausesValidation="False"
                                            OnClientClick="return confirm(&quot;是否确认中途结束该问卷?&quot;)" OnClick="bt_ToEnd_Click" />
                                        <asp:Button ID="bt_Save" runat="server" Text="保存问卷" Width="70px" Visible="False"
                                            OnClick="bt_Save_Click" ForeColor="Red" />
                                        <asp:Button ID="bt_Cancel" runat="server" Text="取消问卷" Width="70px" OnClientClick="return confirm('是否确认取消保存当前的问卷?')"
                                            Visible="False" OnClick="bt_Cancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
