<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowProcessDataBaseDetail.aspx.cs" Inherits="SubModule_EWF_FlowProcessDataBaseDetail" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                <td class="dataLabel" width="100">
                                    所属流程
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" width="100">
                                    名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server">执行数据库</asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    描述
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Description" Width="500px" runat="server" TextMode="MultiLine"
                                        Height="61px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
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
                <tr runat="server" id="tr_ProcessDataBase" visible="true" class="tabForm">
                    <td class="tabForm" style="width: 100%">
                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td align="left" colspan="2" height="22" valign="middle">
                                    <h4>
                                        执行数据库信息</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="100">
                                    数据库连接
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DSN" Width="400px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    存储过程名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_StoreProcName" Width="400px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr1" visible="true" class="tabForm">
                    <td class="tabForm" style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="2">
                                            <h4>
                                                存储过程参数维护</h4>
                                        </td>
                                        <td style="height: 30px;" align="right" colspan="2">
                                            <asp:Button ID="btn_Save_Param" runat="server" Text="添 加" Width="60" OnClick="btn_Save_Param_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" width="100">
                                            参数名称
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_ParamName" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="dataLabel">
                                            是否输出参数
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_IsOutput" runat="server">
                                                <asp:ListItem Value="Y">是</asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True">否</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel">
                                            关联数据对象
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_DataObject_DataBase" DataTextField="DisplayName" DataValueField="ID"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_DataObject_DataBase_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            参数常量值
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_ConstStrValue" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                        Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDeleting="gv_List_RowDeleting">
                                                        <Columns>
                                                            <asp:BoundField DataField="ParamName" HeaderText="参数名称" />
                                                            <asp:BoundField DataField="IsOutput" HeaderText="是否输出参数" />
                                                            <asp:BoundField DataField="IsDataObject" HeaderText="是否数据对象" />
                                                            <asp:BoundField DataField="DataObject" HeaderText="关联数据对象" />
                                                            <asp:BoundField DataField="ConstStrValue" HeaderText="常量字符串" />
                                                            <asp:ButtonField CommandName="Select" Text="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                                                            <asp:ButtonField CommandName="Delete" Text="删除" ControlStyle-CssClass="listViewTdLinkS1" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            无数据
                                                        </EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Param" EventName="Click" />
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
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="0" border="0" class="h3Row" width="100%">
                                        <tr>
                                            <td>
                                                <h3>
                                                    常量定义</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $Initiator$
                                </td>
                                <td class="dataField">
                                    当前流程发程发起人员工ID号
                                </td>
                                <td class="dataLabel">
                                    $InitiatorName$
                                </td>
                                <td class="dataField">
                                    当前流程发程发起人员工姓名
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $Today$
                                </td>
                                <td class="dataField">
                                    当前系统的日期，格式为yyyy-MM-dd
                                </td>
                                <td class="dataLabel">
                                    $Now$
                                </td>
                                <td class="dataField">
                                    当前系统的时间，格式为HH:mm:ss
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $AppName$
                                </td>
                                <td class="dataField">
                                    当前工作流程的名称
                                </td>
                                <td class="dataLabel">
                                    $LastApproveStaffName$
                                </td>
                                <td class="dataField">
                                    当前流程最后一次审批人的姓名
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $TaskID$
                                </td>
                                <td class="dataField">
                                    当前发起的工作流程的ID
                                </td>
                                <td class="dataLabel">
                                    $InitiateDateTime$"
                                </td>
                                <td class="dataField">
                                    流程发起时间 yyyy-MM-dd HH:mm:ss
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
