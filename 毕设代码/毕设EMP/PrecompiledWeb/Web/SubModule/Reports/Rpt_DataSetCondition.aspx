<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_Rpt_DataSetCondition, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集字段列表"></asp:Label></h2>
                        </td>
                        <td class="dataLabel">
                            数据集:<asp:Label ID="lb_DataSetName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="5">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="数据集参数" Value="1" />
                        <mcs:MCSTabItem Text="包含数据表" Value="2" />
                        <mcs:MCSTabItem Text="数据表关系" Value="3" />
                        <mcs:MCSTabItem Text="数据集字段" Value="4" />
                        <mcs:MCSTabItem Text="查询条件" Value="5" Enable="false" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbx_search" runat="server" Width="800px" AutoPostBack="true" OnSelectedIndexChanged="lbx_search_SelectedIndexChanged"
                                        Rows="8"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td height="28">
                                    条件字段:
                                    <asp:DropDownList ID="ddl_TableName" runat="server" DataTextField="DisplayName" DataValueField="ID"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_TableName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_Field" runat="server" DataTextField="DisplayName" DataValueField="ID"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_op" runat="server" Visible="False">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_TreeLevel" runat="server" Visible="false" DataTextField="Value"
                                        DataValueField="Key">
                                    </asp:DropDownList>
                                    <asp:RadioButtonList ID="rbl_ValueFrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_ValueFrom_SelectedIndexChanged"
                                        RepeatColumns="2" RepeatLayout="Flow">
                                        <asp:ListItem Value="M" Selected="True">手工设定</asp:ListItem>
                                        <asp:ListItem Value="P">来自参数</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:DropDownList ID="ddl_Param" runat="server" DataTextField="ParamName" 
                                        DataValueField="ParamName">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbx_searchvalue" runat="server" Visible="False"></asp:TextBox>
                                    <mcs:MCSTreeControl ID="MCSTreeControl1" runat="server" Visible="false" Width="200px"
                                        IDColumnName="ID" NameColumnName="Name" ParentColumnName="SuperID" />
                                    <mcs:MCSSelectControl ID="MCSSelectControl1" runat="server" Visible="false"></mcs:MCSSelectControl>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_addsearch" runat="server" Text="添加条件" OnClick="btn_addsearch_Click"
                                        Width="80px" Enabled="False"></asp:Button>
                                    <asp:Button ID="btn_Del" runat="server" Text="删除条件" OnClick="btn_Del_Click" Width="80px">
                                    </asp:Button>
                                    <asp:Button ID="bt_Save" runat="server" Text="保存条件" Width="80px" OnClick="bt_Save_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="cbl_SearchValue" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                        DataTextField="Name" DataValueField="ID">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                                                    <tr>
                                                        <td height="28px">
                                                            <h3>
                                                                自定义条件</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel" width="80px">
                                                            条件名称
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_CustomConditionName" runat="server" Width="700px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel">
                                                            条件SQL
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_CustomConditionSQL" runat="server" Width="700px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="bt_Replace" runat="server" Text="替换条件" Width="80px" OnClick="bt_Replace_Click" />
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
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
