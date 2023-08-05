<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="billing_ind.aspx.cs" Inherits="SchoolTours.Operations.billing_ind" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
     <style>
        #MainContent_gv_div_reminder td{
            padding-top:0px;
            padding-bottom:0px;
        }
    </style>
    <section class="main pt-4">
         <asp:HiddenField ID="OPS_tour_id" runat="server" />
        <asp:HiddenField ID="OPS_inv_id" runat="server" />
        <asp:HiddenField ID="OPS_pmt_id" runat="server" />
        <asp:HiddenField ID="OPS_pax_id" runat="server" />
        <div class="row mx-0 align-items-stretch h-100">
            <div class="col-lg-3 col-md-4">
                <div class="card bg-light-blue h-100 mb-0">
                    <div class="card-body">
                        <div class="position-relative h-100">
                            <div class="form-group mb-1">
                                <asp:Label ID="lbl_base" runat="server"></asp:Label>
                                <asp:DropDownList ID="select_div" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-8">
                                    <label for="" class="sr-only">Operators</label>
                                    <asp:DropDownList ID="select_emp" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="" class="sr-only">Year</label>
                                    <asp:DropDownList ID="select_year" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group mb-1">
                                <asp:DropDownList ID="select_evt_serach" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="srchEvts" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group mb-1">
                                <asp:ListBox ID="select_tour" runat="server" class="form-control form-control-sm" Style="border: 0px; width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="getTourType" Rows="12"></asp:ListBox>
                                <div id="tooltip_container"></div>
                            </div>
                            <%--<div class="form-group">
                                <label for="" class="sr-only">Division</label>
                                <select name="" id="" class="custom-select custom-select-sm">
                                    <option value="">Select Division</option>
                                </select>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-8">
                                    <label for="" class="sr-only">Operators</label>
                                    <select name="" id="" class="custom-select custom-select-sm">
                                        <option value="">Select Operators</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="" class="sr-only">Year</label>
                                    <select name="" id="" class="custom-select custom-select-sm">
                                        <option value="">Select Year</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="" class="sr-only">Event</label>
                                <select name="" id="" class="custom-select custom-select-sm">
                                    <option value="">Select Event</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="" class="sr-only">Note</label>
                                <textarea name="" id="" rows="6" class="form-control form-control-sm"></textarea>
                            </div>--%>
                            <ul class="list-unstyled d-flex bot-fix justify-content-between mb-0">
                                <li>
                                    <asp:ImageButton ID="img_details" runat="server" ImageUrl="../Content/images/icon_details.png" data-placement="bottom" title="details" Width="40" OnClick="img_details_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_pax" runat="server" ImageUrl="../Content/images/icon_pax.png" data-placement="bottom" title="pax" Width="40" OnClick="img_pax_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_billing" runat="server" ImageUrl="../Content/images/icon_inv_selected.png" data-placement="bottom" title="billing" Width="40" OnClick="img_billing_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_contracts" runat="server" ImageUrl="../Content/images/icon_threshold.png" data-placement="bottom" title="Deadlines" Width="40" OnClick="img_contracts_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_cost" runat="server" ImageUrl="../Content/images/icon_costs.png" data-placement="bottom" title="Costs" Width="40" OnClick="img_cost_Click" /></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-8">
                <div class="card bg-primary h-100 mb-0 overflow-hidden">
                    <div class="card-header bg-blue">
                        <h6 class="mb-0 text-white">
                            <asp:Label ID="div_tour_info" runat="server"></asp:Label>
                            <%--<button class="btn btn-sm bg-white float-right">PAX</button>--%>
                            <%--<asp:Button ID="input_pax_nr" runat="server" Class="form-control float-right w-25" OnClick="setPaxNr" Text="Pax"></asp:Button>--%>
                            <asp:DropDownList ID="select_pax" Class="custom-select custom-select-sm float-right w-25" runat="server" OnSelectedIndexChanged="select_pax_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </h6>
                    </div>
                    <div class="card-body">
                        <div class="w-100 h-100 position-relative">
                            <div class="row h-100">
                                <div class="col-md-6">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="w-100 bg-secondary">
                                                <h6 class="text-dark clearfix mb-0 p-2">Group Invoices
                                                </h6>
                                            </div>
                                            <asp:ListBox ID="select_inv" runat="server" class="list-unstyled mb-0 bg-white overflow-auto p-3" Style="height: calc(100vh - 580px); width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="dtlInv"></asp:ListBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_due_date" runat="server" type="date" class="form-control form-control-sm" placeholder="Due Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_inv" ControlToValidate="input_due_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_ratio" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_inv" ControlToValidate="select_ratio" ErrorMessage="*" InitialValue="Ratio" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_inv_amt" runat="server" class="form-control form-control-sm number" placeholder="Amount" MaxLength="10" data-v-min="-999999999.0" data-v-max="999999999.0" data-m-dec="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_inv" ControlToValidate="input_inv_amt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_pmt_per" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validation_inv" ControlToValidate="select_pmt_per" ErrorMessage="*" InitialValue="Per" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_inv_memo" runat="server" class="form-control form-control-sm" placeholder="Memo"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-auto ml-auto mt-1">
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_del_inv" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delInv" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_add_inv" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newInv" /></a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_save_inv" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setInv" ValidationGroup="validation_inv" /></a>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive overflow-auto bg-light-blue h-100 mb-2">
                                                <asp:GridView runat="server" ID="gv_div_reminder" DataKeyNames="sorter"
                                                    CssClass="table table-borderless table-sm bg-light-blue h-75" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="false" Style="border: 0px;">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="page" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk_reminder_descr" runat="server" Text='<%# Bind("reminder_descr") %>' ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="page" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__page_nm" runat="server" Text='<%# Bind("reminder_amt") %>' Style="float:right;padding-right:3%;"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <asp:ListBox ID="div_reminder" Visible="false" runat="server" class="list-unstyled mb-0 p-2 w-100 bg-light-blue mb-2 border-rad"></asp:ListBox>

                                        </div>
                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_mail_inv" runat="server" ImageUrl="../Content/images/icon_mail.png" alt="" Width="30" OnClick="img_mail_inv_Click" titel="Mail" OnClientClick="showProgress();"/>
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <img src="images/icon_print.png" alt="" width="30">
                                                <asp:ImageButton ID="img_print_inv" runat="server" ImageUrl="../Content/images/icon_print.png" alt="" Width="30" OnClick="img_print_inv_Click" titel="Print" OnClientClick="showProgress();" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12  p-0 mb-2 border-rad overflow-hidden bg-white">
                                            <div class="w-100 bg-secondary">
                                                <h6 class="text-dark clearfix mb-0 p-2">Group Payments
                                                </h6>
                                            </div>
                                            <div class="table-responsive overflow-auto bg-white" style="height: calc(100vh - 460px);">
                                                <asp:ListBox ID="select_pmt" runat="server" class="list-unstyled mb-0 bg-white overflow-auto p-3" Style="height: calc(800vh - 400px); width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="dtlPmt"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_pmt_date" runat="server" class="form-control form-control-sm" placeholder="Due Date" Type="date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_pmt_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_pmt_method" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_pmt" ControlToValidate="select_pmt_method" ErrorMessage="*" InitialValue="Type" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_pmt_amt" runat="server" class="form-control form-control-sm number" placeholder="Amount" MaxLength="10" data-v-min="-999999999.0" data-v-max="999999999.0" data-m-dec="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_pmt" ControlToValidate="input_pmt_amt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_pmt_memo" runat="server" class="form-control form-control-sm" placeholder="Memo" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_trans_nr" runat="server" class="form-control form-control-sm" placeholder="Transaction nbr" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <a href="javascript:void(0);" class="d-inline-block float-left">
                                                <asp:ImageButton ID="img_stmt" runat="server" ImageUrl="../Content/images/icon_generate.png" alt="" Width="30" OnClick="genStmt" title="Statement" OnClientClick="showProgress();"/>
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_del_pmt" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delPmt" title="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_new_pmt" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newPmt" title="Add"/>
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_sav_pmt" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setPmt" ValidationGroup="validation_pmt" title="Save" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
      <script src="../Scripts/autoNumeric-1.9.21.js"></script>
    <script type="text/javascript">
        $(".number").autoNumeric();
    </script>
</asp:Content>
