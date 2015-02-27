<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" stylesheettheme="basic" autoeventwireup="true" inherits="SubModule_OA_KPI_KPI_ScoreList, App_Web_6zjj_wzl" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            sState = theBox.checked;
            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                    if (elem[i].checked != sState) {
                        elem[i].click();
                    }
                }
            }
        }
    
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                员工KPI考核
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_downtemple" runat="server" Text="下载导入模版" PostBackUrl="~/SubModule/Reports/ReportViewer.aspx?Report=e28118d0-bf87-4af2-9153-fe04d801b4af"
                                Width="80px" />
                            <asp:Button ID="bt_ImportKPI" runat="server" Text="导入KPI" Width="60px" OnClick="bt_ImportKPI_Click" />
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td height="30px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr runat="server" id="tr_basicsearch">
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" DisplayRoot="True" />
                                </td>
                                <td align="left">
                                    考核方案
                                    <asp:DropDownList ID="ddl_KPI_Scheme" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    职位
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Position" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    员工
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="160" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="chkHeader" runat="server" Text="全选(片区内)" AutoPostBack="False" onclick="javascript:SelectAll(this);">
                                    </asp:CheckBox>
                                    <asp:Button ID="bt_Find" runat="server" Text="查 询" Width="60px" OnClick="bt_Find_Click" />
                                    <asp:Button ID="bt_Check" runat="server" Text="查看考核" Visible="false" Width="60px"
                                        OnClick="bt_Check_Click" />
                                    <asp:Button ID="bt_Save" runat="server" Text="保 存" Visible="false" Width="60px" OnClick="bt_Save_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="审 核" Width="60px" OnClick="bt_Approve_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="ID" AllowPaging="True"
                            PageSize="50" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" BorderWidth="0px"
                            GridLines="Both" CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" CssClass=""
                            TotalRecordCount="0" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="-" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.审核标志").ToString()=="未审核"%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Check" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_Save" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        <asp:PostBackTrigger ControlID="bt_Approve" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
