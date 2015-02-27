<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_PanelList, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                页面包含板块列表
                            </h2>
                        </td>
                        <td align="right">
                            显示形式：<asp:DropDownList ID="ddl_DisplayType" runat="server">
                                <asp:ListItem Value="1">明细视图</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2">列表视图</asp:ListItem>
                                <asp:ListItem Value="0">全部</asp:ListItem>
                            </asp:DropDownList>
                            关键字:<asp:TextBox ID="tbx_Find" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                            <asp:Button ID="bt_OK" runat="server" Text="新 增" OnClick="bt_OK_Click" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="gv_List_PageIndexChanging"
                    PageSize="15" onrowdeleting="gv_List_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible=false />
                        <asp:BoundField DataField="Code" HeaderText="板块代码" />
                        <asp:BoundField DataField="Name" HeaderText="板块名称" />
                        <asp:BoundField DataField="DetailViewID" HeaderText="所属详细视图" />
                        <asp:BoundField DataField="SortID" HeaderText="显示顺序" />
                        <asp:BoundField DataField="Enable" HeaderText="可见标志" />
                        <asp:BoundField DataField="Description" HeaderText="描述" />
                        <asp:BoundField DataField="DisplayType" HeaderText="显示形式" />
                        <asp:BoundField DataField="FieldCount" HeaderText="每行列数" />
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PanelDetail.aspx?PanelID={0}"
                            Text="详细信息" ControlStyle-CssClass="listViewTdLinkS1"  />
                        <asp:CommandField DeleteText="删除" ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
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
