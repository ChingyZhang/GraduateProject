<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FlowProcessJointDecisionDetail.aspx.cs" Inherits="SubModule_EWF_FlowProcessJointDecisionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 129px; height: 24px;">
                            <h2>
                                流程环节详细信息</h2>
                        </td>
                        <td align="left">
                            ID号:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="left" style="height: 24px">
                            环节类型<asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" runat="server"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="保 存" OnClick="bt_Save_Click" />
                            <asp:Button ID="bt_Del" runat="server" Text="删 除" Width="60px" OnClick="bt_Del_Click"
                                OnClientClick="return confirm(&quot;是否确认删除?&quot;)" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table2" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                <tr>
                    <td class="tabForm" width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        基本信息</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    所属流程
                                </td>
                                <td class="dataField">
                                    &nbsp;<asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px;">
                                    描述
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Description" Width="500px" runat="server" TextMode="MultiLine"
                                        Height="61px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    默认下一环节
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DefaultNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    排序号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Sort" runat="server" Width="50px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="tbx_Sort" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Sort"
                                        Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_ProcessDecision" visible="true" class="tabForm">
                    <td class="tabForm" width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        会审信息</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    消息主题
                                </td>
                                <td class="dataField" >
                                    <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="400px" Text="该任务需要您参与会审!"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_MessageSubject"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    超期时间
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_TimeoutHours" runat="server" Width="60px" Text="0"></asp:TextBox>
                                    小时<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_TimeoutHours"
                                        Display="Dynamic" ErrorMessage="必需为整型" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_TimeoutHours"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    (超期后流程将自动走默认下一环节)
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    审批通过规则
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_NeedAllPositive" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_NeedAllPositive_SelectedIndexChanged">
                                        <asp:ListItem Value="Y">必须所有人员审批通过</asp:ListItem>
                                        <asp:ListItem Value="N">允许部分人员审批通过</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    至少审批通过人数
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_AtLeastPositiveNum" runat="server" Width="60px" Enabled="False">0</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_AtLeastPositiveNum"
                                        Display="Dynamic" ErrorMessage="必需为整型" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    审核通过下一环节
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_PositiveNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    审核不通过下一环节
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_NegativeNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_Recipients" visible="true" class="tabForm">
                    <td class="tabForm" style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="2">
                                            <h4>
                                                参与会审角色</h4>
                                        </td>
                                        <td style="height: 30px;" align="right" colspan="5">
                                            <asp:Button ID="btn_Save_Recipients" runat="server" Text="添加会审角色" Width="80px" OnClick="btn_Save_Recipients_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" height="28px">
                                            会审批角色
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_RecipientRole_Decision" DataTextField="Name" DataValueField="ID"
                                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RecipientRole_Decision_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            关联数据对象
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_DataObject_Decision" DataTextField="DisplayName" DataValueField="ID"
                                                runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            备注说明
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_Recipients_Remark" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="6">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,RecipientRole"
                                                        Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDeleting="gv_List_RowDeleting">
                                                        <Columns>
                                                            <asp:BoundField DataField="RecipientRole" HeaderText="审批人" />
                                                            <asp:BoundField DataField="DataObject" HeaderText="关联数据对象" />
                                                            <asp:BoundField DataField="Remark" HeaderText="备注说明" />
                                                            <asp:ButtonField CommandName="Select" Text="修改" ControlStyle-CssClass="listViewTdLinkS1" />
                                                            <asp:ButtonField CommandName="Delete" Text="删除" ControlStyle-CssClass="listViewTdLinkS1" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            无数据
                                                        </EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Recipients" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
