<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_ClientPayment_ClientPaymentForcast, App_Web_juj__mqc" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="经销商回款预估"></asp:Label></h2>
                        </td>
                        <td align="right" width="100%">
                            &nbsp;<asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                            <asp:Button ID="btn_Init" runat="server" Text="初骀化" Width="60px" OnClick="btn_Init_Click" />
                            <asp:Button ID="btn_Add" runat="server" Text="新增" Width="60px" OnClick="btn_Add_Click"
                                Visible="false" />
                            <asp:Button ID="btn_Save" runat="server" Text="保存" Width="60px" OnClick="btn_Save_Click" />
                            <asp:Button ID="btn_Approve" runat="server" OnClientClick="return confirm(&quot;是否确认将选中的记录批量审批为通过？&quot;)"
                                Text="审 核" OnClick="btn_Approve_Click" />
                            <asp:Button ID="btn_CancelApprove" runat="server" OnClientClick="return confirm(&quot;是否确认将选中的记录批量审批为通过？&quot;)"
                                Text="取消审核" OnClick="btn_CancelApprove_Click" />
                            <asp:Button ID="btn_delete" runat="server" Text="删除" Width="60px" OnClick="btn_delete_Click"
                                OnClientClick="return confirm(&quot;是否确认将选中的记录批量删除？&quot;)" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    经销商
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="200px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    回款日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                            ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataField" align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        预估回款记录</h3>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PageSize="30" Width="100%" Binded="False" ConditionString="" DataKeyNames="FNA_ClientPaymentForcast_ID"
                                        OrderFields="" PanelCode="Panel_ClientPaymentForcast_list" TotalRecordCount="0">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="预计回款日期">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayDate" runat="server" onfocus="setday(this)" Width="70px"
                                                        Text='<%# Bind("FNA_ClientPaymentForcast_PayDate","{0:yyyy-MM-dd}") %>' Enabled='<%#  DataBinder.Eval(Container,"DataItem.FNA_ClientPaymentForcast_ApproveFlag").ToString()=="未审核"%>'></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_PayDate"></asp:CompareValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="预计回款额(元)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAmount" runat="server" Text='<%# Bind("FNA_ClientPaymentForcast_PayAmount","{0:0.###}") %>'
                                                        Enabled='<%#  DataBinder.Eval(Container,"DataItem.FNA_ClientPaymentForcast_ApproveFlag").ToString()=="未审核"%>'
                                                        Width="100px" ToolTip="预计回款额(元)"></asp:TextBox><asp:CompareValidator ID="CompareValidator2"
                                                            runat="server" ControlToValidate="tbx_PayAmount" Display="Dynamic" ErrorMessage="必须是数字"
                                                            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="FNA_ClientPaymentForcast_ID"
                                                DataNavigateUrlFormatString="ClientPaymentForcasDetail.aspx?ID={0}" ControlStyle-CssClass="listViewTdLinkS1"
                                                ItemStyle-Width="80px" ></asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_Init" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_delete" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Save" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_CancelApprove" EventName="Click" />
                                </Triggers>
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
