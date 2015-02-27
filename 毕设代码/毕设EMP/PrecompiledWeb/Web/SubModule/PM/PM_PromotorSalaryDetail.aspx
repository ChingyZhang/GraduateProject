<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_PromotorSalaryDetail, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 220px">
                                    <h2>
                                        导购员薪酬定义详细信息
                                    </h2>
                                </td>
                                <td>
                                 <a href="SalaryModelFieldDescribe.htm"><h3>提报说明</h3></a> 
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Width="60" Text="删除" OnClientClick="return confirm('是否确认删除该薪酬？')"
                                        OnClick="bt_Delete_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Width="60px" Text="审 核" OnClick="bt_Approve_Click"
                                        OnClientClick="return confirm(&quot;确定将该员工审核通过?&quot;)" Visible="false" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="提交审批" Width="60px" OnClick="bt_Submit_Click"
                                        OnClientClick="return confirm(&quot;确定发起申请薪酬流程?&quot;)" />                                  
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
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                        SelectedIndex="1" Width="100%">
                                        <Items>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="基本资料" Description=""
                                                Value="0" Enable="True"></mcs:MCSTabItem>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="薪酬定义" Description=""
                                                Value="1" Enable="True"></mcs:MCSTabItem>
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="Page_PM_003">
                                                        </mcs:UC_DetailView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tbl_Promotor" cellspacing="0" cellpadding="0" width="100%" border="0"
                                                    runat="server">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td nowrap>
                                                                        <h3>
                                                                            导购员的薪酬定义列表
                                                                        </h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Button ID="bt_Add" runat="server" Text="新增薪酬" Width="60px" Visible="true" UseSubmitBehavior="False"
                                                                            OnClick="bt_Add_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_list" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                DataKeyNames="PM_PromotorSalary_ID" PanelCode="Panel_PM_PromotorSalaryDefine_List"
                                                                OnSelectedIndexChanging="gv_list_SelectedIndexChanging">
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Link" ControlStyle-CssClass="listViewTdLinkS1" ShowSelectButton="True"
                                                                        SelectText="查看详细" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.PM_PromotorSalary_ApproveTask").ToString()%>'
                                                                                 Text='审批记录' Visible='<%# DataBinder.Eval(Container,"DataItem.PM_PromotorSalary_ApproveTask").ToString()!="" %>'
                                                                                CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    无数据</EmptyDataTemplate>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
