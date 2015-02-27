<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="PM_PM_PromotorList, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

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
                                导购员列表</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="快捷查询" Description=""
                            Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr runat="server" id="tr_basicsearch">
                                <td width="260px">
                                    管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" />
                                </td>
                                <td>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_Promotor.dbo.PM_Promotor.Name">姓名</asp:ListItem>
                                        <asp:ListItem Value="MCS_Promotor.dbo.PM_Promotor.Code">代码</asp:ListItem>
                                        <asp:ListItem Value="MobileNumber">手机号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_Promotor.dbo.PM_Promotor.ExtPropertys,'|',16)">固定电话</asp:ListItem>
                                        <asp:ListItem Value="MCS_SYS.dbo.UF_Spilt(MCS_Promotor.dbo.PM_Promotor.ExtPropertys,'|',1)">身份证号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">所在终端门店代码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName">所在终端门店名称</asp:ListItem>
                                    </asp:DropDownList>
                                    相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    审核标志
                                    <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                    在职标志
                                    <asp:DropDownList ID="ddl_Dimission" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                    <asp:Button ID="bt_Find" runat="server" Text="快捷查询" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" DataKeyNames="PM_Promotor_ID"
                            PanelCode="Panel_PM_List_001" AllowPaging="True" PageSize="15" AllowSorting="true">
                            <Columns>
                                <asp:HyperLinkField HeaderText="导购员姓名" DataNavigateUrlFields="PM_Promotor_ID" 
                                    DataNavigateUrlFormatString="PM_PromotorDetail.aspx?PromotorID={0}" DataTextField="PM_Promotor_Name"
                                    ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="所在门店">
                                    <ItemTemplate>
                                        <asp:Literal ID="lt_PromotorInClient" Text='<%#PromotorInClient((int)Eval("PM_Promotor_ID")) %>'
                                            runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField Text="所在门店进销存" DataNavigateUrlFields="PM_Promotor_ID" DataNavigateUrlFormatString="PromotorJXCList.aspx?PromotorID={0}"
                                    ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="100px" Visible="false">
                                    <ItemStyle Width="100px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField HeaderText="" DataNavigateUrlFields="PM_Promotor_ID" 
                                    DataNavigateUrlFormatString="PM_SalaryDetail_Search.aspx?ID={0}" Text="查看工资"
                                    ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
