<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeWriteoffDetail0.aspx.cs" Inherits="SubModule_FNA_FeeWriteoff_FeeWriteoffDetail0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="新费用核销"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="tabForm">
                        <table border="0" cellpadding="0" cellspacing="0" width="600px">
                            <tr>
                                <td rowspan="3" style="background: yellow">
                                    1-选择费用申请对象
                                </td>
                                <td class="dataLabel" height="28px">
                                    费用申请为经销商
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSSelectControl ID="select_ApplyClient" runat="server" Width="280px" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,3)\"'
                                        OnSelectChange="select_ApplyClient_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    &nbsp;
                                </td>
                                <td align="left" class="dataField">
                                    <asp:Label ID="lbl_MasterApplyClient" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="30">
                                    &nbsp;&nbsp;费用申请为员工
                                </td>
                                <td align="left">
                                    <mcs:MCSSelectControl ID="select_applyStaff" runat="server" Width="200px" PageUrl='~/SubModule/StaffManage/Pop_Search_Staff.aspx'
                                        OnSelectChange="select_applyStaff_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr style="filter: progid:DXImageTransform.Microsoft.Shadow(color:#987cb9,direction:145,strength:15)"
                                        width="100%" color="#987cb9" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="3" style="background: yellow">
                                    2-选择费用代垫对象
                                </td>
                                <td class="dataLabel" height="28px">
                                    费用代垫为经销商
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" Width="280px" OnSelectChange="select_Client_SelectChange"
                                        PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,3)\"' />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    &nbsp;
                                </td>
                                <td align="left" class="dataField">
                                    <asp:Label ID="lb_MasterAccountName" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;费用代垫为员工
                                </td>
                                <td align="left">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" Width="200px" PageUrl='~/SubModule/StaffManage/Pop_Search_Staff.aspx'
                                        OnSelectChange="select_Staff_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr style="filter: progid:DXImageTransform.Microsoft.Shadow(color:#987cb9,direction:145,strength:15)"
                                        width="100%" color="#987cb9" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="4" style="background: yellow">
                                    3-其它选择项目
                                </td>
                                <td class="dataLabel" height="28px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                ParentColumnName="SuperID" Width="180px" AutoPostBack="False" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    是否有申请单
                                </td>
                                <td align="left" class="dataField">
                                    <asp:RadioButtonList ID="rbl_HasFeeApply" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_HasFeeApply_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="Y">有申请单,需先申请费用后核销</asp:ListItem>
                                        <asp:ListItem Value="N">无申请单,依实际发生实报实销</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr_FeeType" runat="server" visible="false">
                                <td class="dataLabel" height="28px">
                                    费用类型
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="rbl_FeeType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    抵货款类型
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_InvoiceClassAB" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:ImageButton ID="bt_Confirm" runat="server" ImageUrl="~/Images/gif/gif-0024.gif"
                                        OnClick="bt_Confirm_Click" ImageAlign="AbsMiddle" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color:Red; text-align:left">
                                请操作人员参照以下方式操作:<br />
                                方式1-适用常规市场代垫费用核销：即费用申请为A经销商，费用核销为A经销商时（即费用申请与费用代垫为同<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 一经销商），只需选择A经销商名 
                                    称（或A经销商其它子户头）；<br />
                                方式2-适用KA类市场费用核销：即费用申请为经销商，费用核销为责任业代时（即责任业代代垫），需同时选择<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;经销商名称、责任业代名称；<br />
                                方式3-适用部门费用核销：即费用申请为A责任业代，费用核销为A或B责任业代时（即责任业代代垫），只需选<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;择责任业代名称；<br />
                                方式4-适用①统仓配送类型代垫费用或②经销商销户由另一经销商核销费用：即费用申请为A经销商，费用核销<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为B经销商（B经销商非A经销商子 户头）时，只需选择费用申请为A经销商名称，费用核销为B经销商（<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;或B经销商其它子户头）。

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
