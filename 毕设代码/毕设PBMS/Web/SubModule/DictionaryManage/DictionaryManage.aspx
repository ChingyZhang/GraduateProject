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
                    ϵͳ�ֵ���Ϣά��</h2>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                �ֵ�����<asp:DropDownList ID="ddl_DicType" runat="server" DataValueField="ID" DataTextField="Name"
                    AutoPostBack="True" OnSelectedIndexChanged="ddl_DicType_SelectedIndexChanged">
                </asp:DropDownList>
                ���:<asp:TextBox ID="tbx_SelfNo" runat="server" Width="50px"></asp:TextBox>
                <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="����Ϊ��"
                            ControlToValidate="tbx_SelfNo" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" 
                    ControlToValidate="tbx_SelfNo" Display="Dynamic" ErrorMessage="����Ϊ����" 
                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="1"></asp:CompareValidator>
                ��������:<asp:TextBox ID="tbx_name" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="����Ϊ��"
                            ControlToValidate="tbx_name"></asp:RequiredFieldValidator>
                <asp:CheckBox ID="cb_Enabled" runat="server" Text="����" TextAlign="Left" Checked="True" />
                ����:<asp:TextBox ID="tbx_Description" runat="server" Width="180px"></asp:TextBox>
                <asp:Button ID="bt_Save" runat="server" Text="���" OnClick="bt_Save_Click" 
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
                        <asp:BoundField DataField="Code" HeaderText="����" SortExpression="Code" />
                        <asp:BoundField DataField="Name" HeaderText="����" SortExpression="Name" />
                        <asp:BoundField DataField="Enabled" HeaderText="���ñ�־" SortExpression="Enabled" />
                        <asp:BoundField DataField="Description" HeaderText="����" SortExpression="Description" />
                        <asp:ButtonField CommandName="Select" Text="�޸�" ControlStyle-CssClass="listViewTdLinkS1" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="ɾ��" OnClientClick="javascript:return confirm('ȷ���Ƿ�ɾ��?');" CssClass="listViewTdLinkS1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                �ϼƼ�¼������<font color="red"><asp:Label ID="lb_rowcount" runat="server"></asp:Label></font>
                ת����<asp:TextBox ID="tbx_PageGo" runat="server" Width="30px"></asp:TextBox>ҳ
                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="tbx_PageGo"
                    Display="Dynamic" ErrorMessage="����Ϊ���ָ�ʽ" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                <asp:Button ID="bt_PageOk" runat="server" Text="ȷ��" OnClick="bt_PageOk_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
