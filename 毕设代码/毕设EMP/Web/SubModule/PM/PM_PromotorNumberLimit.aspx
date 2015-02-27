<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_PromotorNumberLimit.aspx.cs" Inherits="SubModule_PM_PM_PromotorNumberLimit" %>

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
                                导购员人数限定</h2>
                        </td>
                        <td>
                            管理片区
                        </td>
                        <td align="left">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" />
                        </td>
                        <td>
                            限制层级
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_CityLevel" runat="server" DataTextField="value" DataValueField="key">
                            </asp:DropDownList>
                        </td>
                        <td>
                            导购员类别
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="value" DataValueField="key">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="BtnInit" runat="server" Text="初始化" Width="60px" OnClick="BtnInit_Click"
                                OnClientClick="return confirm('是否确认初始化?')" />
                            <asp:Button ID="BtnSelect" runat="server" Text="查找" OnClick="BtnSelect_Click" Width="60px" />
                            <asp:Button ID="BtnSave" runat="server" Text="保存" OnClick="BtnSave_Click" Width="60px" />
                            <asp:Button ID="BtnDelete" runat="server" Text="批量删除" OnClientClick="return confirm(&quot;删除后数据无法恢复，是否确认删除该导购员限制?&quot;)"
                                OnClick="BtnDelete_Click" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="ID" AllowPaging="false" PageSize="15">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbx" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" />
                        <asp:BoundField DataField="Classify" HeaderText="导购员类别" />
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
                        <asp:TemplateField HeaderText="现有人数">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrent" runat="server" Text='<%# GetActualNumber((int)DataBinder.Eval(Container,"DataItem.OrganizeCity"),(int)DataBinder.Eval(Container,"DataItem.Classify")).ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:TextBox ID="TBRemark" runat="server" Width="400px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
