<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_StaffSalary_StaffSalaryGenerate, App_Web_llmqckq0" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                生成员工工资</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                查询条件</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm" align="center">
                <table cellpadding="0" cellspacing="0" border="0" width="50%" height="100">
                    <tr>
                        <td class="dataLabel">
                            月份
                        </td>
                        <td class="dataField" align="left">
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel">
                            职务角色
                        </td>
                        <td class="dataField" align="left">
                            <asp:DropDownList ID="ddl_PositionType" runat="server" >
                            <asp:ListItem Text="办事处主管" Value="1"></asp:ListItem>
                            <asp:ListItem Text="业务代表" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="bt_Generate" runat="server" Text="生成工资" Width="80px" OnClick="bt_Generate_Click"
                                OnClientClick="return confirm(&quot;生成工资前，请确定是否已将当前办事处的员工新酬信息填写完?&quot;)" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
