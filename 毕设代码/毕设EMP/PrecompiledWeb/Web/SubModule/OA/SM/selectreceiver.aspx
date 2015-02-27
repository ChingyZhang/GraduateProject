<%@ page language="C#" autoeventwireup="true" inherits="SubModule_OA_Mail_selectreceiver1, App_Web_mlnd3-hy" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <base target="_self" />

    <script language="javascript">
        function RemoveItem(ControlName) {
            Control = null;
            switch (ControlName) {
                case "btnReceSendToLeft":
                    Control = eval("document.forms[0].listSendTo");
                    break;

            }

            listAccount = eval("document.forms[0].listAccount");
            var j = Control.length;
            if (j == 0) return;
            for (j; j > 0; j--) {
                if (Control.options[j - 1].selected == true) {
                    listAccount.add(new Option(Control[j - 1].text, Control.options[j - 1].value));
                    Control.remove(j - 1);
                }
            }

        }

        function AddItem(ControlName) {
            Control = null;
            switch (ControlName) {
                case "btnReceSendToRight":
                    Control = eval("document.forms[0].listSendTo");
                    break;

            }

            var i = 0;
            listAccount = eval("document.forms[0].listAccount");
            var j = listAccount.length;
            for (j; j > 0; j--) {
                if (listAccount.options[j - 1].selected == true) {

                    Control.add(new Option(listAccount[j - 1].text, listAccount.options[j - 1].value));
                    listAccount.remove(j - 1)
                }
            }

        }

        function setStatusright() {
            document.forms[0].btnReceSendToRight.disabled = false;
            //            document.forms[0].btnCcSendToRight.disabled = false;
            //            document.forms[0].btnBccSendToRight.disabled = false;
        }

        function setStatusleft() {
            document.forms[0].btnReceSendToLeft.disabled = false;
            //            document.forms[0].btnCcSendToLeft.disabled = false;
            //            document.forms[0].btnBccSendToLeft.disabled = false;
        }

        function ReturnValue() {
            if (window.dialogArguments != null) {
                var parwin = window.dialogArguments;
            }

            var listSendToTxtStr = "";
            var listSendToValueStr = "";
            //            var listCcToTxtStr = "";
            //            var listCcToValueStr = "";
            //            var listBccToTxtStr = "";
            //            var listBccToValueStr = "";
            //            var listSendToCompleteStr = "";

            listSendTo = eval("document.forms[0].listSendTo");
            //            listCcTo = eval("document.forms[0].listCcTo");
            //            listBccTo = eval("document.forms[0].listBccTo");


            for (i = 0; i < listSendTo.length; i++) {
                listSendToTxtStr += listSendTo.options[i].text + ",";
                listSendToValueStr += listSendTo.options[i].value + ",";
            }
            //            parwin.document.forms[0].txtSendTo.value = listSendToTxtStr;
            //            parwin.document.forms[0].hdnTxtSendTo.value = listSendToValueStr;


            //            for (i = 0; i < listCcTo.length; i++) {
            //                listCcToTxtStr += listCcTo.options[i].text + ",";
            //                listCcToValueStr += listCcTo.options[i].value + ",";
            //            }
            //            parwin.document.forms[0].txtCcTo.value = listCcToTxtStr;
            //            parwin.document.forms[0].hdnTxtCcTo.value = listCcToValueStr;


            //            for (i = 0; i < listBccTo.length; i++) {
            //                listBccToTxtStr += listBccTo.options[i].text + ",";
            //                listBccToValueStr += listBccTo.options[i].value + ",";
            //            }
            //            parwin.document.forms[0].txtBccTo.value = listBccToTxtStr;
            //            parwin.document.forms[0].hdnTxtBccTo.value = listBccToValueStr;

            window.returnValue = listSendToTxtStr + "|" + listSendToValueStr + "|";
            window.close();
        }		
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <select id="listSendTo" style="z-index: 104; left: 374px; width: 182px; position: absolute;
        top: 78px; height: 357px;" multiple size="5" name="listSendTo">
    </select>
    <input class="button" style="z-index: 103; left: 256px; width: 81px; position: absolute;
        top: 135px; height: 24px" onclick="AddItem(this.name)" type="button" value=">>>>"
        name="btnReceSendToRight">
    <input class="button" style="z-index: 102; left: 256px; width: 81px; position: absolute;
        top: 177px; height: 24px" onclick="RemoveItem(this.name)" type="button" value="<<<<"
        name="btnReceSendToLeft" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="listAccount" ondblclick="AddItem('btnReceSendToRight')" Style="z-index: 101;
                left: 73px; position: absolute; top: 78px" runat="server" Width="148px" Height="356px"
                multiple onchange="setStatusright()" DataTextField="RealName" DataValueField="RealName">
            </asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblReceiver" Style="z-index: 111; left: 375px; position: absolute;
        top: 58px" runat="server">收件人</asp:Label>
    <input class="button" style="z-index: 116; left: 221px; width: 61px; position: absolute;
        top: 472px; height: 24px" onclick="ReturnValue()" type="button" value="确定" />
    <input class="button" style="z-index: 117; left: 356px; width: 61px; position: absolute;
        top: 475px; height: 24px" onclick="window.close()" type="button" value="取消" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <p>
                <asp:DropDownList ID="ddl_Position" DataTextField="Name" DataValueField="ID" runat="server"
                    Style="z-index: 111; left: 70px; position: absolute; top: 18px; right: 843px;"
                    AutoPostBack="True" OnSelectedIndexChanged="ddl_Position_SelectedIndexChanged"
                    Width="200px">
                </asp:DropDownList>
            </p>
            <p>
                <asp:Label ID="lblReceiver0" Style="z-index: 111; left: 12px; position: absolute;
                    top: 23px" runat="server">员工职务</asp:Label>
            </p>
            <asp:CheckBox ID="cb_IncludeChild" runat="server" Checked="True" Style="color: #FF0000;
                z-index: 111; left: 277px; position: absolute; top: 18px" Text="包含下级职位" />
            <p>
                <asp:Label ID="Label1" runat="server" Text="按名字:" Style="z-index: 101; left: 20px;
                    position: absolute; top: 53px; width: 42px;" Height="20px"></asp:Label>
                <asp:TextBox ID="tbx_Name" runat="server" Style="z-index: 101; left: 73px; position: absolute;
                    top: 53px; width: 122px;" Height="20px"></asp:TextBox>
                <asp:Button ID="bt_serch" runat="server" Text="搜索" Style="z-index: 101; left: 200px;
                    position: absolute; top: 53px; width: 42px;" Height="20px" OnClick="bt_serch_Click" />
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
