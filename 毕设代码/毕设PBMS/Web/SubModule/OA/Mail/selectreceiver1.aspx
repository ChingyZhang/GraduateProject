<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectreceiver1.aspx.cs"
    Inherits="SubModule_OA_Mail_selectreceiver1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <base target="_self" />
</head>
<body onload="PopulateData()">

    <script language="javascript">
        function RemoveItem(ControlName) {
            Control = null;
            switch (ControlName) {
                case "btnReceSendToLeft":
                    Control = eval("document.forms[0].listSendTo");
                    break;
                case "btnCcSendToLeft":
                    Control = eval("document.forms[0].listCcTo");
                    break;
                case "btnBccSendToLeft":
                    Control = eval("document.forms[0].listBccTo");
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
                case "btnCcSendToRight":
                    Control = eval("document.forms[0].listCcTo");
                    break;
                case "btnBccSendToRight":
                    Control = eval("document.forms[0].listBccTo");
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
            document.forms[0].btnCcSendToRight.disabled = false;
            document.forms[0].btnBccSendToRight.disabled = false;
        }

        function setStatusleft() {
            document.forms[0].btnReceSendToLeft.disabled = false;
            document.forms[0].btnCcSendToLeft.disabled = false;
            document.forms[0].btnBccSendToLeft.disabled = false;
        }

        function ReturnValue() {
            if (window.dialogArguments != null) {
                var parwin = window.dialogArguments;
            }

            var listSendToTxtStr = "";
            var listSendToValueStr = "";
            var listCcToTxtStr = "";
            var listCcToValueStr = "";
            var listBccToTxtStr = "";
            var listBccToValueStr = "";
            var listSendToCompleteStr = "";

            listSendTo = eval("document.forms[0].listSendTo");
            listCcTo = eval("document.forms[0].listCcTo");
            listBccTo = eval("document.forms[0].listBccTo");


            for (i = 0; i < listSendTo.length; i++) {
                listSendToTxtStr += listSendTo.options[i].text + ", ";
                listSendToValueStr += listSendTo.options[i].value + ", ";
            }
            //            parwin.document.forms[0].txtSendTo.value = listSendToTxtStr;
            //            parwin.document.forms[0].hdnTxtSendTo.value = listSendToValueStr;


            for (i = 0; i < listCcTo.length; i++) {
                listCcToTxtStr += listCcTo.options[i].text + ", ";
                listCcToValueStr += listCcTo.options[i].value + ", ";
            }
            //            parwin.document.forms[0].txtCcTo.value = listCcToTxtStr;
            //            parwin.document.forms[0].hdnTxtCcTo.value = listCcToValueStr;


            for (i = 0; i < listBccTo.length; i++) {
                listBccToTxtStr += listBccTo.options[i].text + ", ";
                listBccToValueStr += listBccTo.options[i].value + ", ";
            }
            //            parwin.document.forms[0].txtBccTo.value = listBccToTxtStr;
            //            parwin.document.forms[0].hdnTxtBccTo.value = listBccToValueStr;

            window.returnValue = listSendToTxtStr + "|" + listSendToValueStr + "|" + listCcToTxtStr + "|" + listCcToValueStr + "|" + listBccToTxtStr + "|" + listBccToValueStr;
            window.close();
        }
        function PopulateData() {
            if (window.dialogArguments != null) {

                var parwin = window.dialogArguments;
                if (parwin.document.all.hdnTxtSendTo.value != "") {
                    Control = eval("document.forms[0].listSendTo");
                    var SendToValueArray = parwin.document.all.hdnTxtSendTo.value.split(",");
                    var SendToTxtArray = parwin.document.all.txtSendTo.value.split(",");
                    for (i = 0; i < SendToValueArray.length - 1; i++) {
                        Control.add(new Option(SendToTxtArray[i].trim(), SendToValueArray[i].trim()));
                    }
                }

                if (parwin.document.all.hdnTxtCcTo.value != "") {
                    Control = eval("document.forms[0].listCcTo");
                    var CcToValueArray = parwin.document.all.hdnTxtCcTo.value.split(",");
                    var CcToTxtArray = parwin.document.all.txtCcTo.value.split(",");
                    for (i = 0; i < CcToValueArray.length - 1; i++) {
                        Control.add(new Option(CcToTxtArray[i].trim(), CcToValueArray[i].trim()));
                    }
                }

                if (parwin.document.all.hdnTxtSendTo.value != "") {
                    Control = eval("document.forms[0].listBccTo");
                    var BccToValueArray = parwin.document.all.hdnTxtBccTo.value.split(",");
                    var BccToTxtArray = parwin.document.all.txtBccTo.value.split(",");
                    for (i = 0; i < BccToValueArray.length - 1; i++) {
                        Control.add(new Option(BccToTxtArray[i].trim(), BccToValueArray[i].trim()));
                    }
                }


            }
        }	
    </script>

    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <select id="listBccTo" style="z-index: 110; left: 376px; width: 181px; position: absolute;
        top: 293px; height: 105px" multiple size="6" name="listBccTo">
    </select>
    <input class="button" style="z-index: 109; left: 258px; width: 81px; position: absolute;
        top: 300px; height: 24px" onclick="AddItem(this.name)" type="button" value=">>>>"
        name="btnBccSendToRight" />
    <input class="button" style="z-index: 108; left: 258px; width: 81px; position: absolute;
        top: 324px; height: 24px" onclick="RemoveItem(this.name)" type="button" value="<<<<"
        name="btnBccSendToLeft" />
    <select id="listCcTo" style="z-index: 107; left: 375px; width: 181px; position: absolute;
        top: 168px; height: 92px" multiple size="5" name="listCcTo">
    </select>
    <input class="button" style="z-index: 106; left: 256px; width: 81px; position: absolute;
        top: 185px; height: 24px" onclick="AddItem(this.name)" type="button" value=">>>>"
        name="btnCcSendToRight" />
    <input class="button" style="z-index: 105; left: 256px; width: 81px; position: absolute;
        top: 209px; height: 24px" onclick="RemoveItem(this.name)" type="button" value="<<<<"
        name="btnCcSendToLeft">
    <select id="listSendTo" style="z-index: 104; left: 374px; width: 182px; position: absolute;
        top: 43px; height: 90px;" multiple size="5" name="listSendTo">
    </select>
    <input class="button" style="z-index: 103; left: 256px; width: 81px; position: absolute;
        top: 59px; height: 24px" onclick="AddItem(this.name)" type="button" value=">>>>"
        name="btnReceSendToRight">
    <input class="button" style="z-index: 102; left: 256px; width: 81px; position: absolute;
        top: 83px; height: 24px" onclick="RemoveItem(this.name)" type="button" value="<<<<"
        name="btnReceSendToLeft" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
        <asp:Label ID="Label1" runat="server" Text="按名字:" Style="z-index: 101;
                left: 20px; position: absolute; top: 53px; width: 42px;" 
                Height="20px"></asp:Label>
         <asp:TextBox ID="tbx_Name" runat="server" Style="z-index: 101;
                left: 73px; position: absolute; top: 53px; width: 122px;" 
                Height="20px"></asp:TextBox>
                <asp:Button ID="bt_serch" runat="server" Text="搜索" Style="z-index: 101;
                left: 200px; position: absolute; top: 53px; width: 42px;" 
                Height="20px" onclick="bt_serch_Click" />
            <asp:DropDownList ID="listAccount" ondblclick="AddItem('btnReceSendToRight')" Style="z-index: 101;
                left: 73px; position: absolute; top: 83px" runat="server" Width="148px" Height="356px"
                multiple onchange="setStatusright()" DataTextField="RealName" DataValueField="RealName">
            </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tr_Position" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Label ID="lblReceiver" Style="z-index: 111; left: 375px; position: absolute;
        top: 18px" runat="server" Font-Size="X-Small">收件人</asp:Label>
    <asp:Label ID="lblCc" Style="z-index: 112; left: 376px; position: absolute; top: 143px"
        runat="server" Font-Size="X-Small">抄送人</asp:Label>
    <asp:Label ID="lblBcc" Style="z-index: 113; left: 380px; position: absolute; top: 272px"
        runat="server" Font-Size="X-Small">秘抄人</asp:Label>
    <input class="button" style="z-index: 116; left: 221px; width: 61px; position: absolute;
        top: 421px; height: 24px" onclick="ReturnValue()" type="button" value="确定" />
    <input class="button" style="z-index: 117; left: 356px; width: 61px; position: absolute;
        top: 421px; height: 24px" onclick="window.close()" type="button" value="取消" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:DropDownList ID="tr_Position" runat="server" AutoPostBack="true" Style="z-index: 111;
                left: 72px; position: absolute; top: 18px; right: 732px;" DataTextField="Name" Width="150px"
                DataValueField="ID" OnSelectedIndexChanged="tr_Position_SelectedIndexChanged">
            </asp:DropDownList>
            <%--   <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                Width="200px" AutoPostBack="true" ParentColumnName="SuperID" RootValue="0" Style="z-index: 111;
                left: 55px; position: absolute; top: 18px; right: 732px;" OnSelected="tr_Position_Selected">
            </mcs:MCSTreeControl>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        <asp:Label ID="lblReceiver0" Style="z-index: 111; left: 10px; position: absolute;
            top: 22px" runat="server" Font-Size="X-Small">员工职务</asp:Label>
    </p>
    <asp:CheckBox ID="cb_IncludeChild" runat="server" Checked="True" Style="color: #FF0000;
        z-index: 111; left: 260px; position: absolute; top: 18px" Text="包含下级职位" />
    </form>
</body>
</html>
