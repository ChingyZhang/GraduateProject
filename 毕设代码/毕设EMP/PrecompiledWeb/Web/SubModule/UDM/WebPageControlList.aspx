<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_WebPageControlList, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

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
                                Web页面控件列表
                            </h2>
                        </td>
                        <td align="left">
                            所属页面:<asp:Label ID="lbl_WebPage" runat="server"></asp:Label>
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
                        <mcs:MCSTabItem Text="WEB页面信息" Value="0" />
                        <mcs:MCSTabItem Text="页面包含控件" Value="1" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="ID" AutoGenerateColumns="False"
                    OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                    Binded="False" ConditionString="" TotalRecordCount="0">
                    <Columns>
                        <asp:BoundField DataField="ControlName" HeaderText="控件名称" />
                        <asp:BoundField DataField="ControlIndex" HeaderText="控件索引" />
                        <asp:BoundField DataField="Text" HeaderText="控件文本" />
                        <asp:BoundField DataField="VisibleActionCode" HeaderText="可见权限代码" />
                        <asp:BoundField DataField="EnableActionCode" HeaderText="可编辑权限代码" />
                        <asp:BoundField DataField="Description" HeaderText="描述" />
                        <asp:BoundField DataField="ControlType" HeaderText="控件类型" />
                        <asp:CommandField SelectText="详细信息" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                        </asp:CommandField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="删除" OnClientClick='return confirm("是否确认删除该控件?")'></asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:TemplateField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
