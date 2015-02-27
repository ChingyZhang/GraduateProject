<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_RM_RetailerList, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="smallmap" style="width: 280px; height: 180px; position: absolute; border: outset;
        float: right; display: none">
    </div>

    <script language="javascript" type="text/javascript">
        mapObj = new AMap.Map("smallmap");
        function showmap(a, x, y) {
            if (a == "")
                return;
            else {
                mapObj.setZoomAndCenter(9, new AMap.LngLat(x, y));
                var marker = new AMap.Marker({
                    position: new AMap.LngLat(x, y), //基点位置
                    offset: new AMap.Pixel(-14, -34), //相对于基点的位置
                    draggable: false,
                    icon: new AMap.Icon({  //复杂图标
                        size: new AMap.Size(27, 36), //图标大小
                        //size:small;
                        image: "http://api.amap.com/webapi/static/Images/custom_a_j.png", //大图地址
                        imageOffset: new AMap.Pixel(-28, 0)//相对于大图的取图位置
                    })
                });
                marker.setContent("<img src='http://api.amap.com/webapi/static/Images/0.png'/><span>" + a + "</span>");
                mapObj.addOverlays(marker);
                document.getElementById("smallmap").style.display = "block";
            }
        }
        function hidemap() {
            document.getElementById("smallmap").style.display = "none";
            mapObj.removeOverlays();
        }
        function locate(e) {
            e = e || window.event;
            var divTips = document.getElementById("smallmap");
            divTips.style.top = e.clientY + document.body.scrollTop + document.documentElement.scrollTop + 10 + "px";
            divTips.style.left = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft - 290 + "px";
        }
        document.getElementById("smallmap").style.display = "none";
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
                                零售商列表<asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
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
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td height="30px">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" id="tr_basicsearch">
                        <td>
                            管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName" Selected="True">门店全称</asp:ListItem>
                                        <asp:ListItem Value="MCS_SYS.dbo.Org_Staff.RealName">销售代表</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum">电话号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">客户地址</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">门店编号</asp:ListItem>
                                    </asp:DropDownList>
                                    相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_SearchType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            门店分类
                            <asp:DropDownList ID="ddl_RTClassify" DataTextField="Value" DataValueField="Key"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            市场类型
                            <asp:DropDownList ID="ddl_MarketType" DataTextField="Value" DataValueField="Key"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td nowrap="nowrap">
                            门店渠道
                            <asp:DropDownList ID="ddl_RTChannel" DataTextField="Value" DataValueField="Key" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 活跃标志
                            <asp:DropDownList ID="ddl_ActiveFlag" DataTextField="Value" DataValueField="Key"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <%--是否促销店
                            <asp:DropDownList ID="ddl_IsPromote" DataTextField="Value" DataValueField="Key"
                                runat="server">
                            </asp:DropDownList>
                            是否返利店
                            <asp:DropDownList ID="ddl_IsRebate" DataTextField="Value" DataValueField="Key"
                                runat="server">
                            </asp:DropDownList>--%>
                            审核标志
                            <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="快捷查询" Width="60px" OnClick="bt_Find_Click" />
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="CM_Client_ID" PageSize="15" Width="100%"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" PanelCode="Page_RT_RetailerList"
                            OnRowDataBound="gv_List_RowDataBound">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1">
                                </asp:CommandField>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="RetailerDetail.aspx?ClientID={0}"
                                    DataTextField="CM_Client_FullName" HeaderText="门店全称" ControlStyle-CssClass="listViewTdLinkS1"
                                    SortExpression="CM_Client_FullName" />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/FNA/FeeApplyOrWriteoffByClientList.aspx?ClientID={0}"
                                    Text="费用" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/SalesVolumeList.aspx?Type=2&SellInClientID={0}"
                                    Text="进货" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/SalesVolumeList.aspx?Type=3&SellOutClientID={0}"
                                    Text="销售" ControlStyle-CssClass="listViewTdLinkS1" />
                                <%-- <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/InventoryList.aspx?ClientID={0}"
                                    Text="库存" ControlStyle-CssClass="listViewTdLinkS1"  />   --%>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/InventoryDifferencesList.aspx?ClientID={0}"
                                    Text="盘点" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/ClassifyForcast.aspx?ClientID={0}"
                                    Text="预估" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_attach" runat="server" NavigateUrl='<%# Bind("CM_Client_ID","../ClientPictureList.aspx?ClientID={0}")%>'
                                            Text="附件管理" CssClass="listViewTdLinkS1">
                                        </asp:HyperLink>
                                        <a onmouseout="hidemap()" style="cursor: pointer; color: #990033" href='../Map/ClientInMap.aspx?ClientID=<%#Eval("CM_Client_ID") %>'
                                            target="_blank">地图</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
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
