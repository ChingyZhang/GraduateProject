<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Login_CreateUser, App_Web_atkk77yx" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                用户登录帐号设置</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tabForm">
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" DisplaySideBar="True"
                    LoginCreatedUser="False" FinishCompleteButtonText="完成创建" OnContinueButtonClick="CreateUserWizard1_ContinueButtonClick"
                    OnFinishButtonClick="CreateUserWizard1_FinishButtonClick" 
                    PasswordHintText="至少6位" CancelButtonText="返回用户列表" DisplayCancelButton="True" 
                    Email="test@test.com" oncancelbuttonclick="CreateUserWizard1_CancelButtonClick">
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                        </asp:CreateUserWizardStep>
                        <asp:WizardStep ID="RelateStaff" runat="server" Title="关联员工">
                            <mcs:MCSTreeControl ID="ddl_Position" runat="server" AutoPostBack="True" Height="17px"
                                IDColumnName="ID" NameColumnName="Name" ParentColumnName="SuperID" RootValue="1"
                                Width="200px" OnSelected="ddl_Position_Selected" />
                            <asp:DropDownList ID="ddl_Staff" runat="server" DataTextField="RealName" DataValueField="ID">
                            </asp:DropDownList>
                        </asp:WizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                </asp:CreateUserWizard>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
