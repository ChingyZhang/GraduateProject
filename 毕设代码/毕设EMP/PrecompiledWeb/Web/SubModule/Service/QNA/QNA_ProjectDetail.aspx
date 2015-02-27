<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_ProjectDetail, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="问卷详细信息页"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_ViewStatistics" runat="server" CausesValidation="False" 
                                onclick="bt_ViewStatistics_Click" Text="查看结果" />
                            <asp:Button ID="bt_Enabled" runat="server" onclick="bt_Enabled_Click" 
                                Text="启 用" Width="60px" />
                            <asp:Button ID="bt_Disabled" runat="server" onclick="bt_Disabled_Click" 
                                Text="停 用" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_QNA_ProjectDetail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
                <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tab_QNAToPosition"
                            runat="server">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    面向职位范围</h3>
                                            </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td width="420px">
                                                            面向的职位
                                                            <mcs:MCSTreeControl ID="tr_ToPosition" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="300px" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px">
                                                        <td>
                                                            包括全部子职位:<asp:CheckBox ID="chb_ToPositionChild" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px" align="center">
                                                        <td>
                                                            <asp:Button ID="bt_Insert1" runat="server" Width="60px" Text="添加职位" OnClick="bt_Insert1_Click" />
                                                            <asp:Button ID="bt_Detele1" runat="server" Width="60px" Text="删除职位" OnClick="bt_Detele1_Click" />
                                                            <asp:Button ID="bt_Clear1" runat="server" Width="60px" Text="清除所有" OnClick="bt_Clear1_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lb_PositionChild" runat="server" Height="150" Width="320" SelectionMode="Multiple">
                                                </asp:ListBox>
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
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tab_QNAToOrganizeCity"
                            runat="server">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    面向管理片区范围</h3>
                                            </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td width="420px">
                                                            面向的区域
                                                            <mcs:MCSTreeControl ID="tr_ToOrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="250px" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px">
                                                        <td>
                                                            包括全部子区域:<asp:CheckBox ID="chb_ToOganizeCityChild" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr height="28px" align="center">
                                                        <td>
                                                            <asp:Button ID="bt_Insert2" runat="server" Width="60px" Text="添加区域" OnClick="bt_Insert2_Click" />
                                                            <asp:Button ID="bt_Detele2" runat="server" Width="60px" Text="删除区域" OnClick="bt_Detele2_Click" />
                                                            <asp:Button ID="bt_Clear2" runat="server" Width="60px" Text="清除区域" OnClick="bt_Clear2_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lb_CityChild" runat="server" Height="150" Width="320" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr
        <tr id="tr_QuestionList" runat="server">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap>
                                        <h3>
                                            问卷标题列表
                                        </h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_AddQuestion" runat="server" Text="新增标题" UseSubmitBehavior="False"
                                            OnClick="bt_AddQuestion_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="QNA_Question_ID" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                        PanelCode="Panel_QNA_QuestionList_01">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
