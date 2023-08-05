<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mass_mailer.aspx.cs" Inherits="SchoolTours.mass_mailer" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdn.ckeditor.com/4.14.1/standard/ckeditor.js"></script>

    <style>
        .recepientList li {
            margin-bottom: 5px;
        }

            .recepientList li a {
                background: #007BAB;
                border-radius: 8px;
                padding: 8px 15px;
                display: block;
                color: #fff;
            }

        .image-upload > .file-upload {
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 9999;
            opacity: 0;
        }

        .note-link-popover {
            display: none;
        }

        .note-editable {
            height: 300px;
        }
    </style>
    <script>
        var int = 1;
        //$("MainContent_input_mail_txt").focus(function () {
        //    checkid(0);
        //});
        function myFunction(value) {
            console.log(int + '~' + value);
            if (int == 0) {
                CKEDITOR.instances["<%= input_mail_txt.ClientID %>"].insertText(value);
                //int == 0
            }
            if (int == 1) {
                //if (value != "[Sender First Name]" && value != "[Sender Last Name]" && value != "[Sender Organization]" && value != "[Sender eMail Address]" && value != "[Sender Phone]" && value != "[Sender Website]") {
                document.getElementById("MainContent_input_subject").value = document.getElementById("MainContent_input_subject").value + '' + value;
                //}
                //int = 0;
            }
            //int = 0;
        }
        function SetCursorToTextEnd(input_mail_txt) {
            //debugger;
            var text = document.getElementById(input_mail_txt);
            if (text != null && text.value.length > 0) {
                if (text.createTextRange) {
                    var FieldRange = text.createTextRange();
                    FieldRange.moveStart('character', text.value.length);
                    FieldRange.collapse();
                    FieldRange.select();
                }
            }
        }
        $(document).ready(function () {
            CKEDITOR.on('instanceReady', function (evt) {
                var editor = evt.editor;
                console.log('The editor named ' + editor.name + ' is now ready');

                editor.on('focus', function (e) {
                    // console.log('The editor named ' + e.editor.name + ' is now focused');
                    int = 0;
                });
                editor.on('click', function (e) {
                    //console.log('The editor named ' + e.editor.name + ' is now clicked');
                    int = 0;
                });
                //$(evt.editor.container.$).find('iframe').contents().click(function () {
                //    debugger;
                //    int = 0;
                //});
                //$(evt.editor.container.$).find('iframe').contents().focus(function () {
                //    debugger;
                //    int = 0;
                //});
            });
            
        });
        $(document).ready(function () {
           
            $('#MainContent_file_upload').change(function (e) {
                debugger;
                var qq = '';
                var files = e.target.files;
                for (var i = 0; i < files.length; i++) {
                    qq += files[i].name + " ,";
                }
                if (qq != "")
                    qq = (qq.slice(0, -1));
                var filename = $(this).val().split('\\').pop();
                var path = $(this).val();
                if (path != '' && path != null) {
                    var q = path.substring(path.lastIndexOf('\\') + 1);
                    $('#MainContent_div_attach').html(q);
                }
                $('#MainContent_div_attach').html(qq);
            });

            $('input[name=notes]').keydown(function (e) {
                debugger;
                var code = e.keyCode || e.which;

                if (code === 9) {
                    e.preventDefault();
                    myFunction();
                    alert('it works!');
                }
            });
        });
        
        function checkid(val) {
            //console.log(val);
            int = val;
        }
      
        function disableBtn(btnID, newText) {
            var btn = document.getElementById(btnID);
            if (document.getElementById("MainContent_input_subject").value != "" && document.getElementById("MainContent_select_div_id").value != "Select") {
                setTimeout("setImage('" + btnID + "')", 10);
                btn.disabled = true;
                btn.value = newText;
            }
        }
        function setImage(btnID) {
            var btn = document.getElementById(btnID);
            btn.style.background = 'url(12501270608.gif)';
        }
    </script>
    <div class="container-fluid">
        <section class="main">
            <div class="row mx-0 align-items-stretch h-100 pt-4">
                <div class="col-lg-12 col-md-12">
                    <div class="card bg-primary h-100 mb-0">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-5">
                                    <%--<h4 class="text-active">This Mailing Contains <span id="div_nbr_items" runat="server"></span> Recipient</h4>--%>
                                    <h4 class="text-active"><span id="div_nbr_items" runat="server"></span></h4>
                                </div>
                                <div class="col-lg-8 col-md-7">
                                    <div class="row">
                                        <div class="col-lg-6 ml-auto">
                                            <div class="form-group">
                                                <asp:DropDownList ID="select_div_id" runat="server" class="custom-select">
                                                    <asp:ListItem>Select Sending Div</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_mass_mailer" ControlToValidate="select_div_id" ErrorMessage="*" InitialValue="Select" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-4 col-md-5">
                                    <div>
                                        <ul class="list-unstyled recepientList">
                                            <li><a href="#" ondblclick="myFunction('[Recipient First Name]');">Recipient First Name</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Recipient Last Name]');">Recipient Last Name</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Recipient Title]');">Recipient Title</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Recipient Organization]');">Recipient Organization</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender First Name]');">Sender First Name</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender Last Name]');">Sender Last Name</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender Organization]');">Sender Organization</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender eMail Address]');">Sender eMail Address</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender Phone]');">Sender Phone</a></li>
                                            <li><a href="#" ondblclick="myFunction('[Sender Website]');">Sender Website</a></li>
                                        </ul>
                                    </div>
                                    <div class="image-upload position-relative" style="display: block;">
                                        <label for="file_upload">
                                            <img src="Content/images/icon_attach.png" alt="" width="30">
                                        </label>
                                        <asp:FileUpload ID="file_upload" runat="server" Class="file-upload" AllowMultiple="true" />
                                        <asp:Label ID="div_attach" runat="server" Class="text-active"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-md-7">
                                    <div class="form-row">
                                        <div class="col-md-12">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_subject"  runat="server" Class="form-control" MaxLength="100" placeholder="Subject" onClick="checkid(1);" name="notes"></asp:TextBox>
                                                <%-- <script type="text/javascript" lang="javascript">
                                                    CKEDITOR.replace('<%=input_subject.ClientID%>');
                                                </script>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidato2" runat="server" ValidationGroup="validation_mass_mailer" ControlToValidate="input_subject" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                                <div id="Div_mailbody" class="sectionBox">
                                                    <asp:TextBox ID="input_mail_txt" Class="text_areas"  runat="server" TextMode="MultiLine" onClick="checkid(0);"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_mass_mailer" ControlToValidate="input_mail_txt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                                    <script type="text/javascript" lang="javascript">
                                                        CKEDITOR.replace('<%=input_mail_txt.ClientID%>');
                                                        //debugger;
                                                        //CKEDITOR.on('instanceReady', function (evt) {
                                                        //    $(evt.editor.container.$).find('iframe').contents().click(function () {
                                                        //        alert('Clicked!');
                                                        //    });
                                                        //});
                                                    </script>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-right">
                                            <asp:Button ID="bttn_send_mail" runat="server" class="btn btn-md btn-primary" Text="Send" OnClick="sndMail" ValidationGroup="validation_mass_mailer" OnClientClick="disableBtn(this.id, 'Send...')" UseSubmitBehavior="false" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>

    <script>
        //CKEDITOR.replace('MainContent_input_mail_txt');
        $(document).ready(function () {
            $("#<%= bttn_send_mail.ClientID %>").click(function () {
                CKEDITOR.instances["<%= input_mail_txt.ClientID %>"].updateElement();
            });
        });
    </script>
</asp:Content>
