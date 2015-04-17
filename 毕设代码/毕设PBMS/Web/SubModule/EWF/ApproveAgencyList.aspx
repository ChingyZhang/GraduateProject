<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ApproveAgencyList.aspx.cs" Inherits="SubModule_EWF_ApproveAgencyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="工作流代理审批授权列表"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged"
                                        Text="全选" />
                                    <asp:Button ID="bt_Disable" runat="server" OnClientClick="return confirm('是否确认将选择的授权失效?')"
                                        Text="失 效" Width="60px" onclick="bt_Disable_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_Search" runat="server" Text="查 询" OnClick="btn_Search_Click"
                                        Width="60px" />
                                    <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="新增授权" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td height="28px">
                                                <h2>
                                                    查询条件</h2>
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
                                                授权人
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_PrincipalStaff" runat="server" Width="180px" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx" />
                                            </td>
                                            <td class="dataLabel">
                                                录入时间段
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                                    runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                                    Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                    ControlToValidate="tbx_begin" Display="Dynamic"></asp:RequiredFieldValidator>
                                                至
                                                <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator2"
                                                    runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                                    Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                                    Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                代理人
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_AgentStaff" runat="server" Width="180px" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx" />
                                            </td>
                                            <td class="dataLabel">
                                                工作流
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_App" runat="server" AutoPostBack="True" DataTextField="Name"
                                                    DataValueField="ID" OnSelectedIndexChanged="ddl_App_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                        SelectedIndex="0" Width="100%">
                                        <Items>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="有效授权" Description=""
                                                Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="失效授权" Description=""
                                                Value="1" Enable="True"></mcs:MCSTabItem>
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" PanelCode="Panel_EWF_ApproveAgency_List_01"
                                        DataKeyNames="EWF_ApproveAgency_ID" Width="100%" AllowPaging="True" OnRowDataBound="gv_List_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx_Check" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField DataNavigateUrlFields="EWF_ApproveAgency_ID" DataNavigateUrlFormatString="ApproveAgencyDetail.aspx?ID={0}"
                                                Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
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
