<%@ Page Title="" Language="C#" MasterPageFile="~/Mytour_Site.Master" AutoEventWireup="true" CodeBehind="mytour_mail.aspx.cs" Inherits="SchoolTours.Mytour.mytour_mail" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdn.ckeditor.com/4.14.1/standard/ckeditor.js"></script>
    <script type="text/javascript">
        function funSuccessfullyMailSend() {
            $("#exampleModal").show();
        }
        $(document).ready(function () {
            $('#ContentPlaceHolder1_file_upload').change(function () {
                var path = $(this).val();
                if (path != '' && path != null) {
                    var q = path.substring(path.lastIndexOf('\\') + 1);
                    $('#ContentPlaceHolder1_div_attach').html(q);
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

        .main {
            height: calc(100vh - 120px);
            overflow-y: auto;
        }
    </style>
    <div class="col h-100">
        <div class="card bg-primary h-100 mb-0 overflow-hidden">
            <asp:HiddenField ID="tour_id" runat="server" />
            <asp:HiddenField ID="person_id" runat="server" />
            <div class="card-header bg-blue">
                <h4 class="my-0 text-white">Send an Mail</h4>
            </div>
            <div class="card-body">
                <div class="row h-100 position-relative mx-auto">
                    <div class="w-100 h-100 align-content-center">
                        <div class="w-100">
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:Label ID="div_to_nm" runat="server" Class="text-dark font-weight-bold" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_subject" class="form-control" runat="server" placeholder="Subject" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_send" ControlToValidate="input_subject" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <%--<asp:TextBox class="form-group summernote" ID="input_body" runat="server" TextMode="MultiLine" Style="display:none;"></asp:TextBox>--%>
                                        <asp:TextBox ID="input_body" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_mass_mailer" ControlToValidate="input_body" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        <script type="text/javascript" lang="javascript">
                                            CKEDITOR.replace('<%=input_body.ClientID%>');
                                        </script>
                                        <%--<div class="form-group summernote" id="input_body" runat="server"></div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 bot-fix">
                            <div class="d-flex justify-content-between">
                                <div class="image-upload position-relative w-100">
                                    <label for="file_upload" class="mb-0">
                                        <img src="../Content/images/icon_attach.png" alt="" width="40">
                                    </label>
                                    <asp:FileUpload ID="file_upload" runat="server" Class="file-upload" />
                                    <asp:Label ID="div_attach" runat="server" Class="text-active"></asp:Label>
                                </div>
                                <div class="w-100 text-right mb-3">
                                    <a href="javascript:void(0);">
                                        <asp:ImageButton ID="img_cancel" runat="server" ImageUrl="../Content/images/icon_back.png" title="back" alt="" Width="40" OnClick="goHome" />
                                    </a>
                                    <a href="javascript:void(0);">
                                        <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" title="save" alt="" Width="40" OnClick="sndMail" ValidationGroup="validation_send" />
                                    </a>
                                </div>
                                <%--data-toggle="modal" data-target="#exampleModal"--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
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
                    mail was sent successfully
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
        //CKEDITOR.replace('MainContent_input_mail_txt');
        $(document).ready(function () {
            $("#<%= img_save.ClientID %>").click(function () {
                CKEDITOR.instances["<%= input_body.ClientID %>"].updateElement();
            });
        });
    </script>
</asp:Content>
