<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="Org_StaffNumberLimit.aspx.cs" Inherits="SubModule_StaffManage_Org_StaffNumberLimit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                员工预算人数限定</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="BtnSelect" runat="server" Text="查找" Width="60px" OnClick="BtnSelect_Click" />
                            <asp:Button ID="BtnInit" runat="server" Text="初始化" Width="60px" OnClick="BtnInit_Click"
                                OnClientClick="return confirm('是否确认初始化?')" />
                            <asp:Button ID="BtnSave" runat="server" Text="保存" Width="60px" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnDelete" runat="server" Text="删除" Width="60px" OnClick="BtnDelete_Click"
                                OnClientClick="return confirm('是否确认删除选中的数据?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="dataLabel" width="60px">
                            管理片区
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" RootValue="0" DisplayRoot="True" Width="200px" AlwaysSelectChildNode="True"
                                AutoPostBack="False" />
                        </td>
                        <td class="dataLabel" width="60px">
                            限制层级
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_CityLevel" runat="server" DataTextField="value" DataValueField="key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" width="60px">
                            职位
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" RootValue="1" DisplayRoot="False" Width="300px" AlwaysSelectChildNode="True" />
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="chb_ToPositionChild" runat="server" Text="包含子职位" Visible="false" />
                            <asp:CheckBox ID="CBAll" runat="server" Text="全选" OnCheckedChanged="CBAll_CheckedChanged"
                                AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" OnPageIndexChanging="gv_List_PageIndexChanging" DataKeyNames="ID,OrganizeCity,Position"
                    OnRowDeleting="gv_List_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CBSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" />
                        <asp:BoundField DataField="Position" HeaderText="职位" />
                        <asp:TemplateField HeaderText="预算人数">
                            <ItemTemplate>
                                <asp:TextBox ID="TBBudget" runat="server" Text='<%# Bind("BudgetNumber","{0:0.###}") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="上限人数">
                            <ItemTemplate>
                                <asp:TextBox ID="TBLimit" runat="server" Text='<%# Bind("NumberLimit","{0:0.###}") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="现有职位人数">
                            <ItemTemplate>
                                <asp:Label ID="lbActualNumber" Text='<%# GetActualNumber((int)DataBinder.Eval(Container,"DataItem.OrganizeCity"),(int)DataBinder.Eval(Container,"DataItem.Position")).ToString() %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
