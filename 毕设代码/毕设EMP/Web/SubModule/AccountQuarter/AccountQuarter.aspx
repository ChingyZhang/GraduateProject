<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="AccountQuarter.aspx.cs" Inherits="SubModule_AccountQuarter_AccountQuarter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        季度维护</h2>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbl_AlertInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                                    <asp:Button ID="btn_Delete" runat="server" CausesValidation="False" OnClick="btn_Delete_Click"
                                        Text="删除" Width="60px" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    会计年度
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_year" runat="server" DataTextField="Year" DataValueField="Year"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="color: #ff0000">*</span>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    会计季度
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Quarter" runat="server" DataValueField="Key" DataTextField="value"
                                        AutoPostBack="true" Width="160px" AppendDataBoundItems="True" OnSelectedIndexChanged="ddl_Quarter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    季度名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="120px" runat="server"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Name" Display="Dynamic"
                                        ErrorMessage="不能为空"></asp:RequiredFieldValidator><span style="color: #ff0000"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    开始月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_BeginMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="color: #ff0000">*</span>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    截止月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                    <span style="color: #ff0000">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                     <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="AC_AccountQuarter_ID" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" PanelCode="Panel_AccountQuarter_list" >
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True">
                                                <ItemStyle Width="100px" />
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
