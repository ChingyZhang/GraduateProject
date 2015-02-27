<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerPictureList, App_Web_bemqpquv" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                客户附件及图片列表
                            </h2>
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
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="dataLabel">
                                        客户
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="PopSearch/Search_SelectClient.aspx"
                                                    Width="300px" onselectchange="select_Retailer_SelectChange" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="dataLabel">
                                        上传日期
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begin"
                                            Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        至<asp:TextBox ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_end"
                                            Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="dataLabel">
                                        附件名称
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_FindName" runat="server"></asp:TextBox>
                                        <asp:CheckBox ID="cb_OnlyPic" runat="server" Text="仅图片" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Find" runat="server" Text="查  找" Width="60px" OnClick="bt_Find_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td>
        <h2 style="font-size:large">
        <asp:Label ID="lbl_message" runat="server"></asp:Label></h2>
        </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="30" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
