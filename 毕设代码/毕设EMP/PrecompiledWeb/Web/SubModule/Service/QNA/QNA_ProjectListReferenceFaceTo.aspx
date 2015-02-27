<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_ProjectListReferenceFaceTo, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="问卷列表页"></asp:Label></h2>
                        </td>
                        <td align="right">
               问卷录入日期范围：
                <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                        ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                问卷主题：<asp:TextBox ID="tbx_Search" Width="150" runat="server"></asp:TextBox>
                <asp:Button ID="btn_Search" OnClick="btn_Search_Click" runat="server" Width="60"
                    Text="查找" />
                            <asp:Button ID="bt_FillQuestionnair" runat="server" Text="填写问卷" 
                                onclick="bt_FillQuestionnair_Click" />
                            <asp:Button ID="bt_ScanQuestionnair" runat="server" Text="查看问卷结果" 
                                onclick="bt_ScanQuestionnair_Click"  Visible= "false"/>
                                
                               
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
              <td>
                        <mcs:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" 
                            SelectedIndex="0" Width="100%" 
                            onontabclicked="MCSTabControl2_OnTabClicked" >
                            <Items>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="进行中的问卷" Description=""
                                    Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="已完成的问卷" Description=""
                                    Value="1" Enable="True"></mcs:MCSTabItem>
                                  <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="已取消的问卷" Description=""
                                    Value="2" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="QNA_Project_ID" PageSize="15" Width="100%" PanelCode="Panel_QNA_ProjectList_01" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                                        <Columns>
                                              <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="已调研份数" SortExpression="ResultCount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ResultCount" runat="server" Text='<%# GetResultCount((int)Eval("QNA_Project_ID")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                              
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

