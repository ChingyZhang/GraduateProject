<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_ReportViewer, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle"
                    height="28">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left">
                            <h2>
                                查看统计报表</h2>
                        </td>
                        <td align="left" class="dataLabel">
                            <asp:Label ID="lb_DataSetCacheTime" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="bt_ClearDataCache" runat="server" OnClick="bt_ClearDataCache_Click"
                                Text="清除缓存" Visible="false" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Button ID="bt_SaveDataCacache" runat="server" Text="保存历史快照" Width="80px" OnClick="bt_SaveDataCacache_Click"
                                        Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lb_PageInfo" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Button ID="bt_PrePage" runat="server" Text="上一页" Visible="false" OnClick="bt_PrePage_Click" />
                            <asp:Button ID="bt_NextPage" runat="server" Text="下一页" Visible="false" OnClick="bt_NextPage_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Refresh" runat="server" OnClick="bt_Refresh_Click" Text=" 查 询 "
                                Width="60px" />
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
                            <asp:Button ID="bt_ExprotList" runat="server" OnClick="bt_ExprotList_Click" Text="导出数据源"
                                ToolTip="导出Excel失效时使用此功能导出" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DataSetParamPanel ID="pl_Param" runat="server" Width="100%">
                        </mcs:UC_DataSetParamPanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Label ID="lb_ReportTitle" runat="server" Text="" Style="font-size: x-large;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Chart ID="Chart1" runat="server" EnableTheming="False" EnableViewState="True"
                    Height="600px" Width="1024px">
                    <Legends>
                    </Legends>
                    <Series>
                    </Series>
                    <ChartAreas>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="divGridView" style="overflow: scroll; height: 500px">
                    <mcs:UC_GridView runat="server" ID="GridView1" GridLines="Both" CssClass="" OnPageIndexChanging="GridView1_PageIndexChanging"
                        BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" EnableViewState="false">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="" />
                        <RowStyle CssClass="" Height="18px" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <AlternatingRowStyle BackColor="#FFFFFF" />
                        <SelectedRowStyle BackColor="#FFCC66"></SelectedRowStyle>
                        <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NumericFirstLast" Visible="false" />
                        <EmptyDataTemplate>
                            无数据
                        </EmptyDataTemplate>
                    </mcs:UC_GridView>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 50;      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
