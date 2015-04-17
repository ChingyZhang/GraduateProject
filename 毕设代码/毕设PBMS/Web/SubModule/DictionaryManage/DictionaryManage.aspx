<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System" %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" StylesheetTheme="basic"
    AutoEventWireup="true" CodeFile="DictionaryManage.aspx.cs" Inherits="SubModule_DictionaryManage_DictionaryManage" %>

<script id="Script1" runat="server">
    private string GetDicUpdate(string atid, string typeid)
    {
        return ("DictionaryManage.aspx?ATID=" + atid + "&TypeID=" + typeid);
    }
		
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table4" class="moduleTitle">
        <tr>
            <td align="right" width="30">
                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
            </td>
            <td align="left" width="90%">
                <h2>
                    系统字典信息维护</h2>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                字典类型<asp:DropDownList ID="ddl_DicType" runat="server" DataValueField="ID" DataTextField="Name"
                    AutoPostBack="True" OnSelectedIndexChanged="ddl_DicType_SelectedIndexChanged">
                </asp:DropDownList>
                编号:<asp:TextBox ID="tbx_SelfNo" runat="server" Width="50px"></asp:TextBox>
                <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="不能为空"
                            ControlToValidate="tbx_SelfNo" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" 
                    ControlToValidate="tbx_SelfNo" Display="Dynamic" ErrorMessage="必须为数字" 
                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="1"></asp:CompareValidator>
                编码名称:<asp:TextBox ID="tbx_name" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="不能为空"
                            ControlToValidate="tbx_name"></asp:RequiredFieldValidator>
                <asp:CheckBox ID="cb_Enabled" runat="server" Text="启用" TextAlign="Left" Checked="True" />
                描述:<asp:TextBox ID="tbx_Description" runat="server" Width="180px"></asp:TextBox>
                <asp:Button ID="bt_Save" runat="server" Text="添加" OnClick="bt_Save_Click" 
                    Width="60px" ValidationGroup="1">
                </asp:Button>&nbsp;
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="ID" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" PageSize="15"
                    Width="100%" OnRowDeleting="gv_List_RowDeleting" 
                    onpageindexchanging="gv_List_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Code" HeaderText="编码" SortExpression="Code" />
                        <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                        <asp:BoundField DataField="Enabled" HeaderText="启用标志" SortExpression="Enabled" />
                        <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" />
                        <asp:ButtonField CommandName="Select" Text="修改" ControlStyle-CssClass="listViewTdLinkS1" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="删除" OnClientClick="javascript:return confirm('确认是否删除?');" CssClass="listViewTdLinkS1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                合计记录条数：<font color="red"><asp:Label ID="lb_rowcount" runat="server"></asp:Label></font>
                转到第<asp:TextBox ID="tbx_PageGo" runat="server" Width="30px"></asp:TextBox>页
                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="tbx_PageGo"
                    Display="Dynamic" ErrorMessage="必须为数字格式" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                <asp:Button ID="bt_PageOk" runat="server" Text="确定" OnClick="bt_PageOk_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
