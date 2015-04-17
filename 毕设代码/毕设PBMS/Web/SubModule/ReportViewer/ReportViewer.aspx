<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="SubModule_ReportViewer_ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .main {
            min-width: 800px;
            max-width: 1024px;
            margin: 0px auto;
            /*background: rgb(255,255,255);*/
            background: #f3faff;
            overflow: auto;
        }

            .main div {
                overflow: auto;
                padding: 0px 0px;
                margin: 0px;
                border: 0px solid #ccc;
            }

        .footer {
            min-width: 800px;
            max-width: 1024px;
            text-align: center;
            background: #ddd;
            border-top: 1px solid #ccc;
            margin: 0px auto;
            padding: 0px;
            overflow: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <div class="main">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr runat="server" visible="false">
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle"
                            height="28">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left">
                                    <h2>查看统计报表</h2>
                                </td>
                                <td align="left" class="dataLabel">
                                    <asp:Label ID="lb_DataSetCacheTime" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="bt_ClearDataCache" runat="server" OnClick="bt_ClearDataCache_Click"
                                        Text="清除缓存" Visible="false" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lb_PageInfo" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Button ID="bt_PrePage" runat="server" Text="上一页" Visible="false" OnClick="bt_PrePage_Click" />
                                    <asp:Button ID="bt_NextPage" runat="server" Text="下一页" Visible="false" OnClick="bt_NextPage_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Refresh" runat="server" OnClick="bt_Refresh_Click" Text=" 查 看 "
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
                <tr runat="server" visible="false">
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
                        <div>
                            <asp:Chart ID="Chart1" runat="server" EnableTheming="False" EnableViewState="True"
                                Height="600px" Width="1024px">
                                <Legends>
                                </Legends>
                                <Series>
                                </Series>
                                <ChartAreas>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div id="divGridView">
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
        </div>
        <div class="footer">
            <p>
                @ 2004-2011 <a class="copyRightLink" href="http://www.meichis.com" style="color: #990033"
                    target="_blank">南京美驰资讯科技开发有限公司 </a>版权所有.
            </p>
        </div>
    </form>
</body>
</html>
