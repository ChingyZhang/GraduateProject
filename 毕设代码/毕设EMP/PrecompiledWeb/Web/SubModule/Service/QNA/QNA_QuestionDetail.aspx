<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_QuestionDetail, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="问卷的标题详细信息页"></asp:Label></h2>
                        </td>
                        <td align="left">
                            所属问卷:<asp:Label ID="lb_ProjectName" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_ReturnProject" runat="server" CausesValidation="False" OnClick="bt_ReturnProject_Click"
                                Text="返回问卷" UseSubmitBehavior="False" />
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
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_QNA_QuestionDetail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_OptionList" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    选项列表
                                                </h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="dataLabel">
                                                选项名称
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_OptionName" runat="server" Width="160px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_OptionName"
                                                    Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel">
                                                跳转至标题
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_NextQuestion" runat="server" DataTextField="Title" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                是否允许输入文本
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_YesOrNo" runat="server">
                                                    <asp:ListItem Value="Y">是</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">否</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddOption" runat="server" Text="新增选项" UseSubmitBehavior="False"
                                                    OnClick="bt_AddOption_Click" ValidationGroup="1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID" PageSize="5" Width="100%" 
                                        onrowdeleting="gv_List_RowDeleting" >
                                        <Columns>
                                            <asp:BoundField DataField="OptionName" HeaderText="选项名称" SortExpression="OptionName" />
                                            <asp:BoundField DataField="NextQuestion" HeaderText="跳转至标题" SortExpression="NextQuestion" />
                                            <asp:BoundField DataField="CanInputText" HeaderText="是否允许输入文本" SortExpression="CanInputText" />
                                            <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                          
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
