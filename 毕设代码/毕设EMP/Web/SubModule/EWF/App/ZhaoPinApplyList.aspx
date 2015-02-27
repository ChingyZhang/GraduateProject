<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ZhaoPinApplyList.aspx.cs" Inherits="SubModule_EWF_App_PositionChangeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                招聘信息列表</h2>
                        </td>
                        <td class="dataLabel">
                            日期范围
                        </td>
                        <td>
                            <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="60px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                    ID="tbx_end" runat="server" onfocus="setday(this)" Width="60px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            管理片区
                        </td>
                        <td>
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="160px" />
                        </td>
                        <td class="dataLabel">
                            申请岗位
                        </td>
                        <td>
                            <mcs:MCSTreeControl ID="tr_Position" runat="server" DisplayRoot="True" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="150px" />
                        </td>
                        <td>
                            <asp:Button ID="bt_Find" runat="server" Text="查找" OnClick="bt_Find_Click" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="HR_ZhaoPinApply_ID" PanelCode="Panel_EWF_App_ZhaoPinApply" AllowPaging="True"
                            PageSize="15" Binded="False" ConditionString="" TotalRecordCount="0">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="HR_ZhaoPinApply_TaskID" DataNavigateUrlFormatString="../TaskDetail.aspx?TaskID={0}"
                                    Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1" />
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
