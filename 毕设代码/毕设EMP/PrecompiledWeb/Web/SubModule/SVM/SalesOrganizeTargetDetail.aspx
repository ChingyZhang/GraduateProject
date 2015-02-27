<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_SalesOrganizeTargetDetail, App_Web_nv-hfo9a" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160">
                            <h2>
                              办事处销售目标明细
                            </h2>
                        </td>
                        <td align="left">
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Approve" runat="server" Text="审 核" OnClick="bt_Approve_Click"
                                Width="60px" />
                            &nbsp;<asp:Button ID="bt_Save" runat="server" Text="保 存" OnClick="bt_Save_Click"
                                Width="60px" />&nbsp;
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提 交" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="dv_detail" runat="server" DetailViewCode="DV_SalesOrganizeTargetDetail">
                    
                </mcs:UC_DetailView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        明细列表</h3>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SVM_KeyProductTarget_Detail_ID,SVM_KeyProductTarget_Detail_Amount"
                                        Width="100%" PageSize="25" PanelCode="Panel_SalesOrganizeTargetDetail">
                                        <Columns>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                     <td align="right">    
                    </td>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">数据处理中，请稍候...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
