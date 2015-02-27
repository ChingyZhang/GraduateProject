<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeApply_FLContract.aspx.cs" Inherits="SubModule_FNA_FeeApply_FeeApply_FLContract" %>

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
                                批量生成返利费用申请单</h2>
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
                                <td class="dataLabel" height="22px">
                                    返利销量月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name" 
                                        ParentColumnName="SuperID" Width="280px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    经销商
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" Width="280px" 
                                        PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1)\"' 
                                        onselectchange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" height="22px">
                                    <asp:Button ID="bt_Generate" runat="server" Text="生成申请单" Width="80px" OnClick="bt_Generate_Click" Enabled="false"
                                        OnClientClick="return confirm(&quot;请确认所有需要申请费用的返利协议均已批复通过！&quot;)" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="true"
                                         AllowPaging="True" PageSize="15" AllowSorting="true" onpageindexchanging="gv_List_PageIndexChanging" 
                                         >
                                    </mcs:UC_GridView>
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
