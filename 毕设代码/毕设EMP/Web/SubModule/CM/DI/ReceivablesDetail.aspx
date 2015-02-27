<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ReceivablesDetail.aspx.cs" Inherits="SubModule_CM_DI_ReceivablesDetail" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="经销商应收账明细"></asp:Label>
                            </h2>
                        </td>
                        <td align="right" width="100%">
                                    <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="保 存" 
                                        Width="60px" />
                            &nbsp;
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
                                <td class="dataLabel" height="28" width="80">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel" width="80">
                                    客户
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                        Width="250px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel" width="80">
                                    记账类别</td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ChangeType" DataValueField="Key" 
                                        DataTextField="Value" runat="server">
                                        <asp:ListItem Selected="True" Value="4">经销商费用承担</asp:ListItem>
                                        <asp:ListItem Value="5">经销商奖励</asp:ListItem>
                                        <asp:ListItem Value="6">经销商扣款</asp:ListItem>
                                        <asp:ListItem Value="7">办事处提现</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28">
                                    金额</td>
                                <td>
                                    <asp:TextBox ID="tbx_Amount" runat="server"></asp:TextBox>
                                    元<span style="color: #FF0000">*</span><asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Amount" 
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToValidate="tbx_Amount" Display="Dynamic" ErrorMessage="格式必需为数字" 
                                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    备注说明</td>
                                <td colspan="3">
                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
