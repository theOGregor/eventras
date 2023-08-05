<%@ Page Title="" Language="C#" MasterPageFile="~/Mytour_Site.Master" AutoEventWireup="true" CodeBehind="mytour_dashboard.aspx.cs" Inherits="SchoolTours.Mytour.mytour_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .main {
            height: calc(100vh - 120px);
            overflow-y: auto;
        }
    </style>


    <div class="card bg-primary h-100 mb-0">

        <asp:HiddenField ID="person_id" runat="server" />
        <asp:HiddenField ID="tour_id" runat="server" />
        <asp:HiddenField ID="int_invoice_id" runat="server" />
        
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <div class="w-100 bg-blue p-2 text-center border-rad">
                        <h4 class="mb-0 text-white">Vital Information
                        </h4>
                    </div>
                    <div class="w-100 bg-white p-2 border-rad mt-2" style="min-height: 300px !important; max-height: 300px !important; overflow: auto;">
                        <asp:GridView runat="server" ID="gv_div_fin_info" DataKeyNames="inv_id"
                            CssClass="table table-borderless table-sm" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="false" Style="border: 0px;" OnRowCommand="gv_div_fin_info_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_current_inv" runat="server" Text='<%# Bind("current_inv") %>' CommandName="inv_id" CommandArgument='<%#Eval("inv_amt")+ ";" +Eval("inv_id")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_inv_amt" runat="server" Text='<% # Bind("inv_amt", "{0:n2}") %>' Style="float: right;"></asp:LinkButton>
                                        <%-- <asp:Label Text='<%# Eval("inv_amt").ToString() == "" ? " " : "$ " %>' runat="server" />
                                        <asp:LinkButton ID="lnk_inv_amt" runat="server" Text='<% # Bind("inv_amt","{0:n2}") %>' Style="float: right;"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col">
                    <div class="w-100 bg-blue p-2 text-center border-rad">
                        <h4 class="mb-0 text-white">Upcoming Dates
                        </h4>
                    </div>
                    <div class="w-100 bg-light-blue p-2 border-rad mt-2" style="min-height: 300px !important;">
                        <asp:GridView runat="server" ID="gv_div_contract_info"
                            CssClass="table table-borderless table-sm bg-light-blue" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="false" Style="border: 0px;">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_contract_date" runat="server" Text='<%# Bind("contract_date") %>'></asp:LinkButton>
                                        - 
                                                                <asp:LinkButton ID="lnk_contract_descr" runat="server" Text='<%# Bind("contract_descr") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days" runat="server" Text=" days" Style="float: right;"></asp:Label>
                                        <asp:LinkButton ID="lnk_days_nr" runat="server" Text='<%# Bind("days_nr") %>' Style="float: right; padding-right: 3%;"></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <p class="text-danger">
                        <asp:Label ID="div_notice" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="row bot-fix">
                <div class="col-12">
                    <ul class="list-unstyled d-flex justify-content-between">
                        <li>
                            <a href="javascript:void(0);" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <asp:ImageButton ID="img_mytour_itin" runat="server" alt="" Width="70" ImageUrl="../Content/images/mytour_itin.png" />
                                <small class="text-uppercase d-block mt-2 text-center">itinerary</small>
                            </a>
                            <div class="dropdown-menu bg-light-blue p-2" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item p-0" href="javascript:void(0);">
                                    <asp:Button class="btn btn-sm p-0 btn-block" ID="btn_ititinerary_view" runat="server" Text="View" OnClick="btn_ititinerary_view_Click" />
                                </a>
                                <a class="dropdown-item p-0" href="javascript:void(0);" data-toggle="modal" data-target="#attachititinerary">
                                    <asp:Button ID="Button3" class="btn btn-sm p-0 btn-block" runat="server" Text="Attach" />
                                </a>
                            </div>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_mytour_inv" runat="server" alt="" Width="70" ImageUrl="../Content/images/mytour_inv.png" OnClick="img_mytour_inv_Click" />
                                <small class="text-uppercase d-block mt-2 text-center">Invoices</small>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_mytour_stmt" runat="server" alt="" Width="70" ImageUrl="../Content/images/icon_generate.png" OnClick="img_mytour_stmt_Click" />
                                <small class="text-uppercase d-block mt-2 text-center">Statement</small>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:void(0);" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <asp:ImageButton ID="img_new_contract" runat="server" alt="" Width="70" ImageUrl="../Content/images/icon_contract.png" />
                                <small class="text-uppercase d-block mt-2 text-center">Contract</small>
                            </a>
                            <div class="dropdown-menu bg-light-blue p-2" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item p-0" href="javascript:void(0);">
                                    <asp:Button class="btn btn-sm p-0 btn-block" ID="btn_download_itin" runat="server" Text="View" OnClick="btn_download_itin_Click" />
                                </a>
                                <a class="dropdown-item p-0" href="javascript:void(0);" data-toggle="modal" data-target="#attachitin">
                                    <asp:Button ID="Button1" class="btn btn-sm p-0 btn-block" runat="server" Text="Attach" />
                                </a>
                            </div>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_mytour_pax" runat="server" alt="" Width="70" ImageUrl="../Content/images/mytour_pax.png" OnClick="img_mytour_pax_Click" />
                                <small class="text-uppercase d-block mt-2 text-center">Participants</small>
                            </a>
                        </li>
                        <li style="display: none;">
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_mytour_pmt" runat="server" alt="" Width="70" ImageUrl="../Content/images/mytour_itin.png" OnClick="img_mytour_pmt_Click" />
                                <small class="text-uppercase d-block mt-2 text-center">Payment</small>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <asp:ImageButton ID="img_mail" runat="server" alt="" Width="70" ImageUrl="../Content/images/icon_mail.png" OnClick="img_mail_Click" />
                                <small class="text-uppercase d-block mt-2 text-center">Contact</small>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="attachitin" tabindex="-1" role="dialog" aria-labelledby="attachitin" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header py-2">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Attach File  Itin</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label for="" class="col-form-label">File</label>
                        <asp:FileUpload ID="file_upload_itin" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Text="*" ErrorMessage="*" ControlToValidate="file_upload_itin" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" ValidationGroup="validation-itin" />
                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="file_upload_itin" runat="server" Display="Dynamic" ValidationGroup="validation-itin" Class="validation-class" />
                    </div>

                </div>
                <div class="modal-footer py-2">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btn_attach_itin" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="validation-itin" OnClick="btn_attach_itin_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="attachititinerary" tabindex="-1" role="dialog" aria-labelledby="attachititinerary" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header py-2">
                    <h5 class="modal-title" id="exampleModalCenterTitle1">Attach File  itinerary</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label for="" class="col-form-label">File</label>
                        <asp:FileUpload ID="file_upload_ititinerary" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Text="*" ErrorMessage="*" ControlToValidate="file_upload_ititinerary" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" ValidationGroup="validation-ititinerary" />
                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="file_upload_ititinerary" runat="server" Display="Dynamic" ValidationGroup="validation-ititinerary" Class="validation-class" />
                    </div>

                </div>
                <div class="modal-footer py-2">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btn_ititinerary" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="validation-ititinerary" OnClick="btn_ititinerary_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
