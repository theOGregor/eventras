<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mailer.aspx.cs" Inherits="SchoolTours.mailer" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdn.ckeditor.com/4.14.1/standard/ckeditor.js"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    var referrer = document.referrer;
        //    alert(referrer);
        //});
        function funSuccessfullyMailSend() {
            $("#exampleModal").show();
        }
        $(document).ready(function () {
            $('#MainContent_file_upload').change(function () {
                var path = $(this).val();
                if (path != '' && path != null) {
                    var q = path.substring(path.lastIndexOf('\\') + 1);
                    $('#MainContent_div_attach').html(q);
                }
            });
        });
    </script>
    <style>
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
    </style>

    <div style="height: calc(100vh - 125px);" class="py-4">
        <div class="card mb-0 bg-primary p-3 h-100 position-relative">
            <div class="row">
                <div class="col-md-6">
                    <h6 class="text-active pt-2 mb-0">
                        <asp:Label ID="lbl_recipient_descr" runat="server" Text=""></asp:Label>

                    </h6>

                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:DropDownList ID="sending_eMail" runat="server" class="custom-select" Style='height: 35px !important; line-height: 25px !important;'></asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="Select" ID="RequiredFieldValidator17" Display="Dynamic" ValidationGroup="validation_send" runat="server" ControlToValidate="sending_eMail" ErrorMessage="*" Class="validation-class"></asp:RequiredFieldValidator>

                        <%--<asp:TextBox ID="txt_recipient_eMail" runat="server" Text="" class="form-control" placeholder="Form Division" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_send" ControlToValidate="txt_recipient_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox ID="input_subject" runat="server" MaxLength="100" Class="form-control" ValidationGroup="send_validation" placeholder="Subject"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_send" ControlToValidate="input_subject" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox ID="input_copy" runat="server" class="form-control" placeholder="Courtesy Copy" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <%-- <div class="form-group" id="input_message" runat="server" textmode="MultiLine"></div>--%>
                    <asp:TextBox class="form-group" ID="input_message" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <script type="text/javascript" lang="javascript">
                        CKEDITOR.replace('<%=input_message.ClientID%>');
                    </script>
                </div>


                <div class="col-md-12 bot-fix mb-3">
                    <div class="d-flex justify-content-between">

                        <div class="image-upload">
                            <label for="file_upload">
                                <img src="Content/images/icon_attach.png" alt="" width="30">
                            </label>
                            <asp:FileUpload ID="file_upload" runat="server" Class="file-upload" />
                            <asp:Label ID="div_attach" runat="server" Class="text-active"></asp:Label>
                        </div>
                        <asp:Button ID="bttn_send_mail" runat="server" OnClick="sndMail" Text="Send" class="btn btn-primary btn-sm" ValidationGroup="validation_send" />

                        <%--data-toggle="modal" data-target="#exampleModal"--%>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal -->
    <div id="exampleModal" style="display: none;">
        <div class="modal-dialog" role="document" style="position: absolute; top: 2%; left: 34%; width: 500px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirmation</h5>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>--%>
                </div>
                <div class="modal-body">
                    Mail sent successfully
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>--%>
                    <asp:Button ID="bttn_Ok" runat="server" OnClick="bttn_Ok_Click" Text="Ok" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            $("#<%= bttn_send_mail.ClientID %>").click(function () {
                CKEDITOR.instances["<%= input_message.ClientID %>"].updateElement();
            });
        });
    </script>
</asp:Content>

