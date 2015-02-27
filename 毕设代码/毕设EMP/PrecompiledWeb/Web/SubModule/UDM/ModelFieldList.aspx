<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_ModelFieldList, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="T_PageContent">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                数据表字段列表
                            </h2>
                        </td>
                        <td align="left">
                            所属表:<asp:Label ID="lbl_TableName" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Text="新增" OnClick="bt_OK_Click" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1">
                    <Items>
                        <mcs:MCSTabItem Text="数据表信息" Value="0" />
                        <mcs:MCSTabItem Text="表包含字段" Value="1" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="ID" AutoGenerateColumns="False"
                    OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible=false />
                        <asp:BoundField DataField="FieldName" HeaderText="字段名称" />
                        <asp:BoundField DataField="DisplayName" HeaderText="显示名称" />
                        <asp:BoundField DataField="DataType" HeaderText="数据类型" />
                        <asp:BoundField DataField="DataLength" HeaderText="数据长度" />
                        <asp:BoundField DataField="Flag" HeaderText="实体字段标记" />
                        <asp:BoundField DataField="Position" HeaderText="扩展字段位置" />
                        <asp:BoundField DataField="RelationType" HeaderText="关联方式" />
                        <asp:BoundField DataField="RelationTableName" HeaderText="关联项目名" />
                        <asp:CommandField SelectText="详细信息" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
