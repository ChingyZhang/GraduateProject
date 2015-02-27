<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoChangeHistory.ascx.cs"
    Inherits="Controls_InfoChangeHistory" %>
<style type="text/css">
    .style1
    {
        text-align: left;
        font-size: 12px;
        font-weight: normal;
        width: 70px;
    }
</style>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td height="28px">
                        <h3>
                            变更记录</h3>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="tabForm">
            <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                <tr>
                    <td class="dataLabel">
                        <asp:Label ID="lbl_Message" runat="server"></asp:Label>
                    </td>
                    <td class="style1">
                        变更字段
                    </td>
                    <td class="dataField">
                        <asp:DropDownList ID="ddl_FieldName" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="查  询" Width="80px" OnClick="btn_Search_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr height="1px">
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <mcs:UC_GridView ID="gv_List" runat="server" PageSize="15" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" Binded="False" ConditionString="" PanelCode="Panel_Info_ChangeHistory"
                TotalRecordCount="0">
                <EmptyDataTemplate>
                    无数据
                </EmptyDataTemplate>
                <Columns>                   
                </Columns>
            </mcs:UC_GridView>
        </td>
    </tr>
</table>
