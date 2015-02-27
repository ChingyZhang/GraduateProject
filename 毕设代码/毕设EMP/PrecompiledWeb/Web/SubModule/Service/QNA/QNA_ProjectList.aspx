<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_ProjectList, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

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
               
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="QNA_Project_ID" PageSize="15" Width="100%" 
                                        PanelCode="Panel_QNA_ProjectList_01" 
                                        onselectedindexchanged="gv_List_SelectedIndexChanged">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="QNA_Project_ID" DataNavigateUrlFormatString="QNA_ProjectDetail.aspx?ID={0}"
                                                DataTextField="QNA_Project_Name" HeaderText="问卷名称" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="已调研份数" SortExpression="ResultCount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ResultCount" runat="server" Text='<%# GetResultCount((int)Eval("QNA_Project_ID")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField DataNavigateUrlFields="QNA_Project_ID" DataNavigateUrlFormatString="QNA_ProjectStatistics.aspx?Project={0}"
                                                Text="查看调研结果" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:HyperLinkField>
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
