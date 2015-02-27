<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_FlowAppInitList, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 355px">
                            <h2>
                                发起新流程申请列表</h2>
                        </td>
                        <td align="right">
                            流程名称:
                            <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="ID" PageSize="15" Width="100%" Binded="False">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="流程名称" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="流程描述" SortExpression="Description" />
                        <asp:BoundField DataField="InsertTime" HeaderText="流程创建时间" SortExpression="InsertTime" />
                        <asp:BoundField DataField="InsertStaff" HeaderText="创建人" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "Apply.aspx?AppID={0}") %>'
                                    Text="发起流程" ></asp:HyperLink>
                            </ItemTemplate>
                            <ControlStyle CssClass="listViewTdLinkS1" />
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        无数据
                    </EmptyDataTemplate>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
