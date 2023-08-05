<%@ Page Title="" Language="C#" MasterPageFile="~/Mytour_Site.Master" AutoEventWireup="true" CodeBehind="mytour_pmt.aspx.cs" Inherits="SchoolTours.Mytour.mytour_pmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isNumberFloat() {
            $('#ContentPlaceHolder1_input_amt').val($('#ContentPlaceHolder1_input_amt').val().replace(/[^0-9\.]/g, ''));
            if ((event.which != 46 || $('#ContentPlaceHolder1_input_amt').val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        }
    </script>
    <style>
        .main {
            height: calc(100vh - 120px);
            overflow-y: auto;
        }
    </style>
    <div class="w-100 h-100">


        <div class="card bg-primary h-100 mb-0 overflow-hidden">

            <asp:HiddenField ID="tour_id" runat="server" />
            <asp:HiddenField ID="person_id" runat="server" />
            <asp:HiddenField ID="PaymentJson" runat="server" />

            <div class="card-header bg-blue">
                <h4 class="my-0 text-white">Make a Payment</h4>
            </div>
            <div class="card-body">
                <div class="col-md-9 h-100 position-relative mx-auto">
                    <div class="row h-100 align-content-center">
                        <div class="w-100">
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <div class=" pl-0">
                                            <asp:RadioButton ID="radio_amt_inv" runat="server" GroupName="pmt" Checked="true" OnCheckedChanged="radio_amt_inv_CheckedChanged" AutoPostBack="true" />
                                            <%--<input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">--%>
                                            <label class="text-dark" for="customRadio1">Pay Invoice Amount</label>
                                        </div>
                                        <div class=" pl-0">
                                            <asp:RadioButton ID="radio_amt_other" runat="server" GroupName="pmt" OnCheckedChanged="radio_amt_other_CheckedChanged" AutoPostBack="true" />
                                            <%--<input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">--%>
                                            <label class="text-dark" for="customRadio2">Pay Other Amount</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_amt" runat="server" class="form-control allownumericwithdecimal" placeholder="Payment Amount" MaxLength="10" onkeypress="return isNumberFloat()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_amt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_nm" runat="server" class="form-control" placeholder="Name on Card" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_card_nr" runat="server" class="form-control" placeholder="Card Number" MaxLength="20" onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_card_nr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />

                                    </div>
                                </div>
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_address" runat="server" class="form-control" placeholder="Billing Address" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_address" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-5">
                                    <div class="form-row">
                                        <div class="col">
                                            <div class="form-group">
                                                <asp:TextBox ID="input_exp" runat="server" class="form-control" placeholder="Exp(YYYY-MM)" MaxLength="7"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_exp" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="form-group">
                                                <asp:TextBox ID="input_cvv" runat="server" class="form-control" placeholder="CVV" MaxLength="3" onkeypress="return isNumberFloat()"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_cvv" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_zip" runat="server" class="form-control" placeholder="Zip" AutoPostBack="true" OnTextChanged="input_zip_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_zip" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_city" runat="server" class="form-control" placeholder="City"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_city" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_state" runat="server" class="form-control" placeholder="State"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_state" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="w-100 text-right mb-3">
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_cancel" runat="server" ImageUrl="../Content/images/icon_back.png" title="back" alt="" Width="40" OnClick="goHome" />
                            </a>
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" title="save" alt="" Width="40" OnClick="setPmt" ValidationGroup="validation_pmt" />
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

