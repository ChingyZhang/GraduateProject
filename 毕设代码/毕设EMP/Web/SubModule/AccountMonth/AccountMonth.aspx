<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="AccountMonth.aspx.cs" Inherits="SubModule_AccountMonth_AccountMonth"
    StylesheetTheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
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
                                        会计月维护</h2>
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
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="120px" runat="server"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Name" Display="Dynamic"
                                        ErrorMessage="不能为空"></asp:RequiredFieldValidator><span style="color: #ff0000"></span>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                </td>
                                <td class="dataField">
                                    <span style="color: #ff0000"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    会计年度
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_AccountYear" runat="server" Width="120px"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_AccountYear" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_AccountYear"
                                        ErrorMessage="年度格式不正确" MaximumValue="3000" MinimumValue="2000" Type="Integer"></asp:RangeValidator><span
                                            style="color: #ff0000"></span><span style="color: #ff0000"></span>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px">
                                    会计月份
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_AccountMonth" runat="server" Width="120px"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                            runat="server" ControlToValidate="tbx_AccountMonth" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                    <span style="color: #ff0000"></span>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbx_AccountMonth"
                                        ErrorMessage="月份格式不正确" MaximumValue="12" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    开始日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_BeginDate" Width="120px" runat="server" onfocus="setday(this)"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="tbx_BeginDate" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><span
                                                style="color: #ff0000"></span><span style="color: #ff0000"></span><asp:CompareValidator
                                                    ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_BeginDate"></asp:CompareValidator><span
                                                        style="color: #ff0000"></span>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    截止日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_EndDate" Width="120px" runat="server" onfocus="setday(this)"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                            runat="server" ControlToValidate="tbx_EndDate" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><span
                                                style="color: #ff0000"></span><span style="color: #ff0000"></span><asp:CompareValidator
                                                    ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_EndDate"></asp:CompareValidator><span
                                                        style="color: #ff0000"></span>
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
                                    <asp:GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="ID" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnSorting="gv_List_Sorting">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True">
                                                <ItemStyle Width="100px" />
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                                            <asp:BoundField DataField="BeginDate" HeaderText="开始日期"/>
                                             <asp:BoundField DataField="EndDate" HeaderText="截至日期"/>
                                            <asp:BoundField DataField="Month" HeaderText="会计月份" SortExpression="Month" />
                                            <asp:BoundField DataField="Year" HeaderText="会计年度" SortExpression="AccountYear" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    合计记录条数：<font color="red"><asp:Label ID="lb_rowcount" runat="server"></asp:Label></font>
                                    转到第<asp:TextBox ID="tbx_PageGo" runat="server" Width="30px"></asp:TextBox>页
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="tbx_PageGo"
                                        Display="Dynamic" ErrorMessage="必须为数字格式" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    <asp:Button ID="bt_PageOk" runat="server" Text="确定" OnClick="bt_PageOk_Click" />
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
