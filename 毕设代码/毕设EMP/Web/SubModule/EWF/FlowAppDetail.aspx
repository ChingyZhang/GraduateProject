<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowAppDetail.aspx.cs" Inherits="SubModule_EWF_FlowAppDetail" %>

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
                                流程详细信息</h2>
                        </td>
                        <td align="left">
                            ID号:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="80px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Copy" runat="server" Text="复制流程" OnClientClick='return confirm("是否确认以当前流程为模板复制新的流程?")'
                                OnClick="bt_Copy_Click" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="0" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    Width="100%">
                    <Items>
                        <mcs:MCSTabItem Text="流程基本信息" Value="0" />
                        <mcs:MCSTabItem Text="流程环节列表" Value="1" />
                        <mcs:MCSTabItem Text="流程数据字段" Value="2" />
                        <mcs:MCSTabItem Text="允许发起职位" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left" colspan="6" height="22" valign="middle">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" width="100" height="26" class="dataLabel">
                            名称
                        </td>
                        <td height="26" class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_Name" Width="320px" runat="server"></asp:TextBox><span style="font-size: 11pt;
                                color: #ff0000">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Name"
                                ErrorMessage="不能为空" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td height="26" class="dataLabel">
                            流程代码
                        </td>
                        <td height="26" class="dataField">
                            <asp:TextBox ID="tbx_Code" runat="server" Width="180px"></asp:TextBox>
                            <span style="font-size: 11pt; color: #ff0000">* </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_Code"
                                ErrorMessage="不能为空" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" width="100" height="26" class="dataLabel">
                            是否允许批量审批
                        </td>
                        <td height="26" class="dataField">
                            <asp:RadioButtonList ID="rbl_CanBatchApprove" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="允许" Value="Y" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不允许" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td height="26" class="dataLabel">
                            关联业务对象
                        </td>
                        <td height="26" class="dataField">
                            <asp:DropDownList ID="ddl_RelateObject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_RelateObject_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="NULL">不关联</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=">终端门店</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=">经销商</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/CU/CustomerDetail.aspx?ID=">消费者</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/StaffManage/StaffDetail.aspx?ID=">员工</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/PM/PM_PromotorDetail.aspx?PromotorID=">导购员</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=">费用申请单</asp:ListItem>
                                <asp:ListItem Value="~/SubModule/FNA/FeeWriteOff/FeeWriteOffDetail.aspx?ID=">费用核销单</asp:ListItem>
                                <asp:ListItem Value="UserDefined">自定义</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td height="26" class="dataLabel">
                            关联数据字段
                        </td>
                        <td height="26" class="dataField">
                            <asp:DropDownList ID="ddl_RelateDataObject" runat="server" DataTextField="DisplayName"
                                DataValueField="ID" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" width="100" height="26" class="dataLabel">
                            关联页面地址
                        </td>
                        <td height="26" class="dataField" colspan="5">
                            <asp:TextBox ID="tbx_RelateURL" Width="500px" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" width="100" height="26" class="dataLabel">
                            描述
                        </td>
                        <td height="26" class="dataField" colspan="5">
                            <asp:TextBox ID="tbx_Description" Width="500px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" width="100" height="26" class="dataLabel">
                            备注
                        </td>
                        <td height="26" class="dataField" colspan="5">
                            <asp:TextBox ID="tbx_Remark" Width="500px" runat="server" Height="69px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="dataLabel" height="26" valign="middle" width="100">
                            创建人及时间
                        </td>
                        <td class="dataField" height="26">
                            <asp:Label ID="lbl_InsertTime" runat="server"></asp:Label>
                            <asp:Label ID="lbl_InsertSatff" runat="server"></asp:Label>
                        </td>
                        <td align="left" class="dataLabel" height="26" valign="middle" width="100">
                            &nbsp; 启用标志
                        </td>
                        <td class="dataField" height="26">
                            &nbsp;
                            <asp:DropDownList ID="ddl_EnableFlag" DataTextField="Name" DataValueField="ID" runat="server">
                                <asp:ListItem Value="Y">启用</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" height="26">
                            更新人及时间
                        </td>
                        <td class="dataField" height="26">
                            <asp:Label ID="lbl_UpdateStaff" runat="server"></asp:Label>
                            <asp:Label ID="lbl_UpdateTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
