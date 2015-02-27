<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_SalaryGenerate, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
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
                                        生成导购员工资</h2>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        查询条件</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm" align="center">
                        <table cellpadding="0" cellspacing="0" border="0" width="50%" height="100">
                            <tr>
                                <td class="dataLabel" height="28">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" AutoPostBack="False" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="220px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28">
                                    月份
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28">
                                    经销商
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="280px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="bt_Generate" runat="server" Text="生成工资" Width="80px" OnClick="bt_Generate_Click"  Enabled="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" height="70" style="color: #FF0000">
                                    注：在生成导购员工资之前，请先确认以下事项：<br />
                                    1.各导购门店销售录入并完成审核。<br />
                                    2.将已离职导购员，设为离职。<br />
                                    3.已录入【导购员奖惩及调整项】，并完成审核。
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle"
                align="center" runat="server" id="gv_Table" visible="false">
                <tr>
                    <td class="h3Row" height="30">
                        <h2>
                            薪酬信息不符合导购</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                            PageIndex="10">
                            <Columns>
                                <asp:BoundField DataField="Promotor" HeaderText="促销员ID" ItemStyle-Width="100" />
                                <asp:HyperLinkField ControlStyle-CssClass="listViewTdLinkS1" DataNavigateUrlFields="Promotor"
                                    DataNavigateUrlFormatString="PM_PromotorDetail.aspx?PromotorID={0}" DataTextField="Name"
                                    HeaderText="促销员姓名" Target="_blank" ItemStyle-Width="160">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="所在门店" ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:Literal ID="lt_PromotorInClient" runat="server" Text='<%#PromotorInClient((int)Eval("Promotor")) %>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Err" HeaderText="异常原因" />
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
