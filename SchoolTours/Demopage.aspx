<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demopage.aspx.cs" Inherits="SchoolTours.Demopage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function funfordefautenterkey1(btn, event) {
            if (document.all) {
                if (event.keyCode == 13) {
                    debugger;
                    event.returnValue = false;
                    event.cancel = true;
                    btn.click();
                }
            }
        }
        function funfordefautenterkey2(btn, event) {
            if (document.all) {
                if (event.keyCode == 13) {
                    debugger;
                    event.returnValue = false;
                    event.cancel = true;
                    btn.click();
                }
            }
        }
        function EnterEvent(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=Button3.UniqueID%>', "");
            }
        }
        function EnterEvent4(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=Button4.UniqueID%>', "");
            }
            function ButtonClick(event, button) {
                if (event.keyCode == 13) {
                    alert('Enter pressed!!!');
                    document.getElementById(btn_new).click();
                }
            }
        }
        $("#TextBox6").keypress(function (event) {
            if (e.keyCode == 13) {
                debugger;
                $.ajax({
                    type: "POST",
                    url: "Demopage2.aspx/EntityAddTag",
                    contentType: "application/json; charset=utf-8",
                    //data: '{"id1":"' + e.target.id + '","id2":"' + data + '"}',
                    dataType: "json",
                    //async:true,
                    success: function (response) {
                        //location.reload(true);
                    },
                    failure: function (response) {

                    }
                });
            }
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" Width="230px" MaxLength="50"
                Height="20px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Submit1" OnClick="ButtonIN_Click"></asp:Button>
            <br />
            <br />
            <asp:Label ID="message" runat="server"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="TextBox2" runat="server" Width="230px" MaxLength="50"
                Height="20px"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Submit2" OnClick="Button2_Click"></asp:Button>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>



        <asp:TextBox ID="TextBox3" ClientIDMode="Static" runat="server" onkeypress="return EnterEvent(event)"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" Style="display: none" Text="Button" />
        <asp:Label ID="Label2" runat="server"></asp:Label>

        <asp:Label ID="lbl" runat="server">-----------------------------------------------------------------------------</asp:Label>

        <asp:TextBox ID="TextBox4" ClientIDMode="Static" runat="server" onkeypress="return EnterEvent4(event)"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" Style="display: none" Text="Button" OnClick="Button4_Click" />

        <asp:Button ID="btnSend" runat="server" Text="send mail" OnClick="btnSend_Click" />

        <asp:Button ID="btn_mailKit" runat="server" Text="send mail" OnClick="btn_mailKit_Click" />


        <br />


        <asp:TextBox ID="TextBox5" runat="server" onkeypress="ButtonClick(event,'btn_new')"></asp:TextBox>
        <asp:Button ID="btn_new" runat="server" Text="Button" OnClick="btn_new_Click" />


        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    </form>
</body>
</html>
