<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="desktop_opi.aspx.cs" Inherits="SubModule_desktop_opi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="240px">
                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="1">
                                <Items>
                                    <mcs:MCSTabItem Text="默认桌面" NavigateURL="desktop.aspx" />
                                    <mcs:MCSTabItem Text="运营指标" Enable="false" />
                                    <mcs:MCSTabItem Text="日跟踪表进度" NavigateURL="OA/TrackCard/Desktop_TrackCard.aspx" Visible="false" />
                                </Items>
                            </mcs:MCSTabControl>
                        </td>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0" width="100%" style="border-bottom-style: solid;
                                border-bottom-color: #999999; border-bottom-width: 1px">
                                <tr>
                                    <td width="100px">
                                        &nbsp;
                                    </td>
                                    <td width="70px" class="dataLabel">
                                        管理片区
                                    </td>
                                    <td width="220px" class="dataField">
                                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" Width="180px" AutoPostBack="False" />
                                    </td>
                                    <td width="100px">
                                        <asp:ImageButton ID="bt_Confirm" runat="server" ImageUrl="~/Images/gif/gif-0024.gif"
                                            ImageAlign="AbsMiddle" OnClick="bt_Confirm_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                    <tr>
                        <td valign="top" width="380px" style="border-right-style: solid; border-right-width: 1px;
                            border-right-color: #C0C0C0;">
                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td height="28px">
                                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0" height="28px">
                                            <tr>
                                                <td width="32px">
                                                    <img width="32px" height="32px" src="../Images/gif/gif-0018.gif" />
                                                </td>
                                                <td valign="middle">
                                                    <h3>
                                                        客户及员工增减情况</h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <mcs:UC_GridView ID="gv_ClientCount" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="false" DataKeyNames="Classify">
                                                        <Columns>
                                                            <asp:BoundField DataField="Classify" HeaderText="项目" />
                                                            <asp:BoundField DataField="Increase" HeaderText="新增" />
                                                            <asp:BoundField DataField="Decrease" HeaderText="减少" />
                                                            <asp:BoundField DataField="Activity" HeaderText="总数" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            无数据
                                                        </EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Chart ID="chart_ClientCount" runat="server" EnableTheming="False" EnableViewState="True"
                                            Width="380px" Height="600px">
                                            <Legends>
                                                <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1">
                                                </asp:Legend>
                                            </Legends>
                                            <Series>
                                                <asp:Series Name="Promotor" ChartArea="ChartArea1" ChartType="Column" XValueMember="CityName"
                                                    YValueMembers="Activity" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder"
                                                    LegendText="导购员" XValueType="String" YValueType="Int32">
                                                </asp:Series>
                                                <asp:Series Name="Staff" ChartArea="ChartArea2" ChartType="Column" XValueMember="CityName"
                                                    YValueMembers="Activity" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder"
                                                    LegendText="员工" XValueType="String" YValueType="Int32">
                                                </asp:Series>
                                                <asp:Series Name="Retailer" ChartArea="ChartArea3" ChartType="Column" XValueMember="CityName"
                                                    YValueMembers="Activity" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder"
                                                    LegendText="零售商" XValueType="String" YValueType="Int32">
                                                </asp:Series>
                                                <asp:Series Name="Distributor" ChartArea="ChartArea4" ChartType="Column" XValueMember="CityName"
                                                    YValueMembers="Activity" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder"
                                                    LegendText="经销商" XValueType="String" YValueType="Int32">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <AxisY IsLabelAutoFit="False" Title="导购员" TitleFont="Microsoft Sans Serif, 6pt">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot" />
                                                        <LabelStyle Font="Microsoft Sans Serif, 6pt" />
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="True">
                                                        <MajorGrid Enabled="False" />
                                                    </AxisX>
                                                    <Position Height="23" Width="100" />
                                                </asp:ChartArea>
                                                <asp:ChartArea Name="ChartArea2">
                                                    <AxisY IsLabelAutoFit="False" Title="员工" TitleFont="Microsoft Sans Serif, 6pt">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 6pt"></LabelStyle>
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="True">
                                                        <MajorGrid Enabled="False"></MajorGrid>
                                                    </AxisX>
                                                    <Position Height="23" Width="100" Y="23" />
                                                </asp:ChartArea>
                                                <asp:ChartArea Name="ChartArea3">
                                                    <AxisY IsLabelAutoFit="False" Title="零售商" TitleFont="Microsoft Sans Serif, 6pt">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 6pt"></LabelStyle>
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="True">
                                                        <MajorGrid Enabled="False"></MajorGrid>
                                                    </AxisX>
                                                    <Position Height="23" Width="100" Y="46" />
                                                </asp:ChartArea>
                                                <asp:ChartArea Name="ChartArea4">
                                                    <AxisY IsLabelAutoFit="False" Title="经销商" TitleFont="Microsoft Sans Serif, 6pt">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 6pt"></LabelStyle>
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="True">
                                                        <MajorGrid Enabled="False"></MajorGrid>
                                                    </AxisX>
                                                    <Position Height="23" Width="100" Y="69" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="top">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0" height="28px">
                                            <tr>
                                                <td width="32px">
                                                    <img width="32px" height="32px" src="../Images/gif/gif-0272.gif" />
                                                </td>
                                                <td valign="middle">
                                                    <h3>
                                                        经营分析</h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <mcs:UC_GridView ID="gv_JXCSummary_DI" runat="server" Width="100%" AutoGenerateColumns="true"
                                                        AllowPaging="false" OnRowDataBound="gv_JXCSummary_DI_RowDataBound">
                                                        <Columns>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            无数据
                                                        </EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Chart ID="chart_JXCSummary" runat="server" EnableTheming="False" EnableViewState="True"
                                            Width="980px" Height="500px">
                                            <Legends>
                                                <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1" Title="图例">
                                                </asp:Legend>
                                            </Legends>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <AxisY IsLabelAutoFit="False">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot" />
                                                        <LabelStyle Font="Microsoft Sans Serif, 6pt" />
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="True" IsMarginVisible="false">
                                                        <MajorGrid LineColor="Silver" LineDashStyle="Dot" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Series>
                                                <asp:Series Name="BeginningInventory" ChartArea="ChartArea1" ChartType="Spline" XValueMember="MonthName"
                                                    YValueMembers="Value" IsValueShownAsLabel="true" LegendText="A.期初库存" XValueType="String"
                                                    YValueType="Double" BorderWidth="3" Color="#009933" Enabled="false">
                                                </asp:Series>
                                                <asp:Series Name="PurchaseVolume" ChartArea="ChartArea1" ChartType="Spline" XValueMember="MonthName"
                                                    YValueMembers="Value" IsValueShownAsLabel="true" LegendText="B.本期进货" XValueType="String"
                                                    YValueType="Double" BorderWidth="3" Color="Blue">
                                                </asp:Series>
                                                <asp:Series Name="SalesVolume" ChartArea="ChartArea1" ChartType="Spline" XValueMember="MonthName"
                                                    YValueMembers="Value" IsValueShownAsLabel="true" LegendText="C.本月销售" XValueType="String"
                                                    YValueType="Double" BorderWidth="3" Color="Black">
                                                </asp:Series>
                                                <asp:Series Name="EndingInventory" ChartArea="ChartArea1" ChartType="Spline" XValueMember="MonthName"
                                                    YValueMembers="Value" IsValueShownAsLabel="true" LegendText="D.期末盘存" XValueType="String"
                                                    YValueType="Double" BorderWidth="3" Color="Orange">
                                                </asp:Series>
                                                <asp:Series Name="ForcastVolumn" ChartArea="ChartArea1" ChartType="Spline" XValueMember="MonthName"
                                                    YValueMembers="Value" IsValueShownAsLabel="true" LegendText="E.预计销售" XValueType="String"
                                                    YValueType="Double" BorderWidth="3" Color="Red">
                                                </asp:Series>
                                            </Series>
                                        </asp:Chart>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
