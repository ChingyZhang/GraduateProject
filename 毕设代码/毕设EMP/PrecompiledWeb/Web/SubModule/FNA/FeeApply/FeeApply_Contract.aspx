<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApply_FeeApply_Contract, App_Web_5zp237uh" enableEventValidation="false" stylesheettheme="basic" %>

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
                                合同费用提前预支付申请</h2>
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
                                    申请月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="280px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    客户名称
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx"
                                        Width="300px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    付款合同
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Contract" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Contract_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    可申请付款<br />
                                    日期范围</td>
                                <td align="left" class="dataField">
                                    <asp:Label ID="lb_PayDateRegion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22">
                                    申请费用期限
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                    至
                                    <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" 
                                        DataValueField="ID" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="bt_Generate" runat="server" Text="生成申请单" Width="80px" OnClick="bt_Generate_Click" />
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
