<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="TC_TargetEdit.aspx.cs" Inherits="SubModule_OA_TrackCard_TC_TargetEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="销售人员日跟踪表任务填报"></asp:Label>
                            </h2>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="dataLabel" width="60px">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" width="60px">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel" width="60px">
                                    销售人员
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="150px" />
                                </td>
                                <td class="dataLabel" width="60px">
                                    是否填报
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_IsSubmit" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="0">请选择...</asp:ListItem>
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="2">否</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Init" Width="80px" runat="server" Text="查看" OnClick="bt_Init_Click" />
                                    <asp:Button ID="bt_Save" Width="80px" runat="server" Text="保存目标" OnClick="bt_Save_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核确认"
                                        Width="80px" />
                                    <asp:Button ID="bt_Export" Width="80px" runat="server" Text="导出报表" OnClick="bt_Export_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                    <tr>
                        <td height="28px">
                            <h3>
                                日跟踪表任务填报
                            </h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID" AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx" runat="server" Visible='<%# DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="管理片区">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_OrganizeCity" Text='<%# MCSFramework.BLL.TreeTableBLL.GetFullPathName("Addr_OrganizeCity",(int)DataBinder.Eval(Container,"DataItem.OrganizeCity")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Staff" HeaderText="员工" SortExpression="Staff" />
                                <asp:BoundField DataField="Client" HeaderText="客户全称" SortExpression="Client" />
                                <asp:TemplateField HeaderText="客户简称">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_ShortName" Text='<%# DataBinder.Eval(Container,"DataItem[\"ShortName\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否促销店">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_IsPromote" Text='<%# DataBinder.Eval(Container,"DataItem[\"IsPromote\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="促销员">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_Promotor" Text='<%# DataBinder.Eval(Container,"DataItem[\"Promotor\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="门店容量(元)">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_Salesroom" Text='<%# DataBinder.Eval(Container,"DataItem[\"Salesroom\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="上月本品销量">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_PreMonthSalesVolume" Text='<%# DataBinder.Eval(Container,"DataItem[\"PreMonthSalesVolume\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否填报">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_IsSubmit" Text='<%# DataBinder.Eval(Container,"DataItem[\"IsSubmit\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月任务-销量(元)" SortExpression="Data01">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Data01" runat="server" Text='<%# Bind("Data01") %>' Width="60px"
                                            Enabled='<%# DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_Data01" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Data01"
                                            ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月任务-档案数(自抢档案)" SortExpression="Data02">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Data02" runat="server" Text='<%# Bind("Data02") %>' Width="60px"
                                            Enabled='<%# DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="tbx_Data02" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Data02"
                                            ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月任务-档案数(NE提供档案)" SortExpression="Data04">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Data04" runat="server" Text='<%# Bind("Data04") %>' Width="60px"
                                            Enabled='<%# DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                            runat="server" ControlToValidate="tbx_Data04" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_Data04"
                                            ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月任务-送货上门" SortExpression="Data03">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Data03" runat="server" Text='<%# Bind("Data03") %>' Width="60px"
                                            Enabled='<%# DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="tbx_Data03" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_Data03"
                                            ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否123柜台">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lb_Is123" Text='<%# DataBinder.Eval(Container,"DataItem[\"Is123\"]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                        <br />
                        <asp:CheckBox ID="cbx_CheckAll" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged" />
                        &nbsp;<span style="color: #FF0000; font-weight: bold">(在切换分页前，请确保已经点击了【保存】按钮，保存了当前页的数据。)</span>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Init" EventName="Click" />
                        <asp:PostBackTrigger ControlID="bt_Export" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
