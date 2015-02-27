<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PropertyContractFeeApply.aspx.cs" Inherits="SubModule_CM_PD_PropertyContractFeeApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 280px">
                            <h2>
                                物业租赁费用申请</h2>
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
                                申请条件</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="50%">
                            <tr>
                                <td class="dataLabel" height="22">
                                    归属月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    归属管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="280px" AutoPostBack="True" 
                                        onselected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    物业名称
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=6"
                                        Width="300px" onselectchange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    租赁合同</td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Contract" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_Contract_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    费用开始日期
                                </td>
                                <td class="dataField" align="left">
                                    <asp:TextBox ID="tbx_BeginDate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="tbx_BeginDate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToValidate="tbx_BeginDate" Display="Dynamic" ErrorMessage="必需为日期型" 
                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    费用截止日期
                                </td>
                                <td class="dataField" align="left">
                                    <asp:TextBox ID="tbx_EndDate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="tbx_EndDate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                        ControlToValidate="tbx_EndDate" Display="Dynamic" ErrorMessage="必需为日期型" 
                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="bt_Generate" runat="server" Text="生成申请单" Width="80px" OnClick="bt_Generate_Click"/>
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
