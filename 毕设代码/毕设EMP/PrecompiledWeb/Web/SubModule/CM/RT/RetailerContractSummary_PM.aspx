<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerContractSummary_PM, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                预付管理费用（导购协议）统计汇总表
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
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
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                    <asp:Button runat="server" ID="bt_Approved" Text="审批通过" Width="70px" OnClientClick="return confirm('是否确认将该区域下所有工资单审批通过?')"
                                        OnClick="bt_Approved_Click" />
                                    <asp:Button runat="server" ID="bt_UnApproved" Text="审批不通过" Width="70px" OnClientClick="return confirm('是否确认将该区域下所有工资单全部设为审批不通过?')"
                                        OnClick="bt_UnApproved_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    查看层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    门店渠道
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RTChannel" DataTextField="Value" DataValueField="Key" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Selected="True" Value="1">待我审批的申请单</asp:ListItem>
                                        <asp:ListItem Value="4">已提交的申请单</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                
                                
                               <td></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 600px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="费用统计分析" Value="0" />
                                                <mcs:MCSTabItem Text="预付管理费用明细" Value="1" />
                                                <mcs:MCSTabItem Text="周期导购预付管理费用汇总表批复件" Value="4" Visible="false" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="OrganizeCity"
                                            AllowPaging="True" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging"
                                            BorderWidth="0px" GridLines="Both" CellPadding="1" BackColor="#CCCCCC" CellSpacing="1"
                                            CssClass="" TotalRecordCount="0" OnDataBound="gv_List_DataBound">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            
                                            <RowStyle BackColor="White" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" 
                                            AutoGenerateColumns="False" DataKeyNames="ID,ApproveTask" AllowPaging="True"
                                            PageSize="15" Visible="false" 
                                            onpageindexchanging="gv_ListDetail_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" onclick="javascript:SelectAll(this);">
                                                        </asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="120px"  HeaderText="审批意见" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbx_Remark" runat="server" Text='' Width="120">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="协议发起日期" DataField="协议发起日期" DataFormatString="{0:yyyy-MM-dd}"/>
                                                <asp:HyperLinkField ItemStyle-Wrap="false" HeaderText="协议ID" ControlStyle-CssClass="listViewTdLinkS1"  DataTextField="ID" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="RetailerContractDetail.aspx?ContractID={0}" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="门店代码" DataField="门店代码" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="卖场ID" DataField="卖场ID" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="协议类型" DataField="协议类型" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="大区" DataField="大区" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="营业部" DataField="营业部" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="办事处" DataField="办事处" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="卖场名称" DataField="卖场名称" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="KA系统" DataField="KA系统" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="门店渠道" DataField="门店渠道" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="门店分类" DataField="门店分类" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="市场类型" DataField="市场类型" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="经(分)销商" DataField="经(分)销商" />
                                                <asp:BoundField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="导购员数量" DataField="导购员数量" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="合同编码" DataField="合同编码" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="付款方式" DataField="付款方式" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="月目标销售额（元）" DataField="月目标销售额（元）" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="管理费经销商承担比例（%）" DataField="管理费经销商承担比例（%）" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="提成经销商承担比例（%）" DataField="提成经销商承担比例（%）" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="管理费（每人每月每店）" DataField="管理费（每人每月每店）" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="本店总管理费" DataField="本店总管理费" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="我司管理费合计" DataField="我司管理费合计" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="合同起始日期" DataField="合同起始日期" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="合同终止日期" DataField="合同终止日期" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="审批状态" DataField="审批状态" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="待审批人" DataField="待审批人" />
                                                <asp:BoundField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  HeaderText="备注" DataField="备注" />
                                                <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ApproveTask", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("ApproveTask").ToString()!="" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                       
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approved" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_UnApproved" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 55;
        divGridView.style.height = window.screen.availHeight - 400;            
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

