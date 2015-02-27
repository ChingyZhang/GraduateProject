<%@ control language="C#" autoeventwireup="true" inherits="Controls_UploadFile, App_Web_3-xbnske" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr runat="server" id="tr_Upload" visible="<%# CanUpload %>">
        <td>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td height="28px">
                                    <h3>
                                        上传附件</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel">
                                    选择附件
                                </td>
                                <td class="dataField">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" />
                                </td>
                                <td class="dataLabel">
                                    名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    描述
                                </td>
                                <td class="dataField">
                                    <span style="color: #ff0000"></span><span style="color: #ff0000"></span>
                                    <asp:TextBox ID="tbx_Description" runat="server" Width="180px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    <asp:CheckBox ID="cb_AutoCompress" Text="自动压缩图片" runat="server" Checked="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Add" runat="server" Text="上 传" Width="60" OnClick="btn_Add_Click"
                                        ValidationGroup="9" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr runat="server" id="tr_Title">
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                <tr>
                    <td height="28px">
                        <h2>
                            附件列表</h2>
                    </td>
                    <td align="right">
                        <asp:Button ID="bt_SetFirstPicture" runat="server" Text="设为首要图片" Width="80px" OnClick="bt_SetFirstPicture_Click"
                            Visible="<%# CanUpload %>" />
                        <asp:Button ID="bt_Delete" runat="server" Text="删除附件" Width="80px" OnClick="bt_Delete_Click"
                            Visible="<%# CanDelete %>" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr runat="server" id="tr_List2" visible="<%# CanViewList %>">
        <td align="left" class="tabForm">
            <asp:ListView ID="lv_list" runat="server" GroupItemCount="5" DataSourceID="ObjectDataSource1"
                DataKeyNames="ID">
                <LayoutTemplate>
                    <table cellpadding="2" width="100%" align="left">
                        <tr runat="server" id="groupPlaceholder">
                        </tr>
                        <tr>
                            <td style="text-align: center; color: #FF0000" colspan="5">
                                <asp:DataPager runat="server" ID="DataPager" PageSize="10">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="首页" ButtonType="Link" ShowFirstPageButton="true"
                                            ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonCount="5" NextPageText=">>" PreviousPageText="<<" />
                                        <asp:NextPreviousPagerField LastPageText="末页" ButtonType="Link" ShowLastPageButton="true"
                                            ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr style="height: 80px">
                        <td runat="server" id="itemPlaceholder">
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td valign="top" align="center" runat="server">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="ProductLink" Enabled='<%#CanDownLoad %>' runat="server" 
                                        Text='<% #Eval("UploadUser")+"上传于"+Eval("UploadTime")%>' NavigateUrl='<%#Bind("GUID","~/SubModule/DownloadAttachment.aspx?GUID={0}")%>'
                                        ImageUrl='<%# PreViewImagePath((int)DataBinder.Eval(Container,"DataItem.ID"))%>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Text='☆' Visible='<%# DataBinder.Eval(Container,"DataItem[\"IsFirstPicture\"]").ToString()=="Y"%>' />
                                    <asp:CheckBox ID="cb_ck" runat="server" />
                                    <asp:Label ID="lb_name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                    (<asp:Label ID="Label1" runat="server" Text='<%#Bind("FileSize")%>'></asp:Label>KB)
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lb_description" runat="server" Width="200px" Text='<%#Bind("Description")%>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAttachmentList"
                TypeName="MCSFramework.BLL.Pub.ATMT_AttachmentBLL">
                <SelectParameters>
                    <asp:Parameter Name="RelateType" Type="Int32" />
                    <asp:Parameter Name="RelateID" Type="Int32" />
                    <asp:Parameter Name="Begintime" Type="DateTime" DefaultValue="2000-01-01" />
                    <asp:Parameter Name="Endtime" Type="DateTime" DefaultValue="2100-01-01" />
                    <asp:Parameter Name="extcondition" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
