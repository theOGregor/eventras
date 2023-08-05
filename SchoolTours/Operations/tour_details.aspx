<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tour_details.aspx.cs" Inherits="SchoolTours.Operations.tour_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        //function SetTarget() {
        //    document.forms[0].target = "_blank";
        //}
    </script>
    <section class="main pt-4">
        <asp:HiddenField ID="pmt_plan_ind_id" runat="server" />
        <asp:HiddenField ID="pax_ind_id" runat="server" />
        <asp:HiddenField ID="flying_ind_id" runat="server" />
        <asp:HiddenField ID="cmg_bus_ind_id" runat="server" />
        <asp:HiddenField ID="inv_type_ind_id" runat="server" />
        <asp:HiddenField ID="final_ind_id" runat="server" />
        <asp:HiddenField ID="options_nr" runat="server" />
        <asp:HiddenField ID="note_id" runat="server" />


        <asp:HiddenField ID="person_id" runat="server" />

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
                                <asp:DropDownList ID="select_div" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-8">
                                    <label for="" class="sr-only">Operators</label>
                                    <asp:DropDownList ID="ddl_op" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="" class="sr-only">Year</label>
                                    <asp:DropDownList ID="select_year" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="" class="sr-only">Event</label>
                                <asp:DropDownList ID="ddl_e" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="" class="sr-only">Note</label>
                                <asp:ListBox ID="select_tour" runat="server" class="form-control form-control-sm" Style="border: 0px; width: 100%;"></asp:ListBox>
                            </div>--%>
                            <ul class="list-unstyled d-flex bot-fix justify-content-between mb-0">
                                <li>
                                    <asp:ImageButton ID="img_details" runat="server" ImageUrl="../Content/images/icon_details_selected.png" data-placement="bottom" title="details" Width="40" OnClick="img_details_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_pax" runat="server" ImageUrl="../Content/images/icon_pax.png" data-placement="bottom" title="pax" Width="40" OnClick="img_pax_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_billing" runat="server" ImageUrl="../Content/images/icon_inv.png" data-placement="bottom" title="billing" Width="40" OnClick="img_billing_Click" /></li>
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
                        <h6 class="my-0 text-white">
                            <asp:Label ID="div_tour_info" runat="server"></asp:Label>
                            <asp:ImageButton ID="img_mytour" runat="server" ImageUrl="../Content/images/mytour.png" data-placement="bottom" title="My Tour" Width="30" OnClick="img_mytour_Click" Style="float: right;" OnClientClick="SetTarget();" />
                        </h6>
                    </div>

                    <div class="card-body py-2">
                        <div class="w-100 h-100 position-relative">
                            <div class="row h-100">
                                <div class="col-md-6">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_evt" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_tour" ControlToValidate="select_evt" ErrorMessage="*" InitialValue="Select" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_group_nm" runat="server" class="form-control form-control-sm" placeholder="Group Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_tour" ControlToValidate="input_group_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_start_date" runat="server" type="date" class="form-control form-control-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_tour" ControlToValidate="input_start_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_producer" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validation_tour" ControlToValidate="select_producer" ErrorMessage="*" InitialValue="Producer" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_end_date" runat="server" type="date" class="form-control form-control-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_tour" ControlToValidate="input_end_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="validation_tour" ForeColor="Red" runat="server" ControlToValidate="input_start_date" ControlToCompare="input_end_date" Operator="LessThan" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_operator" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-row">
                                                <div class="col-2">
                                                    <div class="form-group mb-2" title="PAX">
                                                        <asp:TextBox ID="input_pax_nr" runat="server" class="form-control form-control-sm number"  placeholder="PAX" data-v-max="999" data-m-dec="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                    <div class="form-group mb-2" title="Free Trips">
                                                        <asp:TextBox ID="input_free_trip_nr" runat="server" class="form-control form-control-sm" placeholder="Free"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Plan Approved">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="pmt_plan_ind" runat="server" AutoPostBack="true" OnCheckedChanged="pmt_plan_ind_CheckedChanged" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Rooming">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="pax_ind" runat="server" AutoPostBack="true" OnCheckedChanged="pax_ind_CheckedChanged" />
                                                            <span class="slider slider2 round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Air">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="flying_ind" runat="server" AutoPostBack="true" OnCheckedChanged="flying_ind_CheckedChanged" />
                                                            <span class="slider slider3 round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-row">
                                                <div class="col-2">
                                                    <div class="form-group mb-2" title="Driver Rooms">
                                                        <asp:TextBox ID="input_driver_nr" runat="server" class="form-control form-control-sm" placeholder="DRV"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Bus">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="cmg_bus_ind" runat="server" AutoPostBack="true" OnCheckedChanged="cmg_bus_ind_CheckedChanged" />
                                                            <span class="slider slider4 round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Group">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="inv_type_ind" runat="server" AutoPostBack="true" OnCheckedChanged="inv_type_ind_CheckedChanged" />
                                                            <span class="slider slider5 round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group mb-2" title="Toggle Final">
                                                        <label class="switch">
                                                            <asp:CheckBox ID="final_ind" runat="server" AutoPostBack="true" OnCheckedChanged="final_ind_CheckedChanged" />
                                                            <span class="slider slider6 round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group mb-2">
                                                <asp:ListBox ID="div_reminder" runat="server" class="form-control form-control-sm bg-light-blue" Style="border: 0px; width: 100%; color: black;"></asp:ListBox>
                                                <%--<textarea name=""  rows="3" class="bg-light-blue form-control form-control-sm border-0" spellcheck="false"></textarea>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-12 mt-3 bot-fix">
                                            <a href="javascript:void(0);" class="d-inline-block" title="Info">
                                                <asp:ImageButton ID="img_info" runat="server" ImageUrl="../Content/images/icon_info.png" alt="" Width="30" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block" title="Delete">
                                                <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delTour" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            </a>
                                            <div class="dropdown d-inline-block">
                                                <a href="javascript:void(0);" class="d-inline-block" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" title="Show Menu">
                                                    <asp:ImageButton ID="img_mytour_itin" runat="server" ImageUrl="../Content/images/mytour_itin.png" alt="" Width="30" />
                                                </a>
                                                <div class="dropdown-menu bg-light-blue p-2" aria-labelledby="dropdownMenuButton">
                                                    <a class="dropdown-item p-0" href="javascript:void(0);">
                                                        <asp:Button class="btn btn-sm p-0 btn-block" ID="btn_download_itin" runat="server" Text="View" OnClick="btn_download_itin_Click" />
                                                    </a>
                                                    <a class="dropdown-item p-0" href="javascript:void(0);" data-toggle="modal" data-target="#attachitin">
                                                        <asp:Button ID="Button1" class="btn btn-sm p-0 btn-block" runat="server" Text="Attach" />
                                                    </a>
                                                </div>
                                            </div>

                                            <div class="dropdown d-inline-block">
                                                <a href="javascript:void(0);" class="d-inline-block" id="dropdownMenuButton2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" title="Show Menu">
                                                    <asp:ImageButton ID="img_new_contract" runat="server" ImageUrl="../Content/images/icon_contract.png" alt="" Width="30" />
                                                </a>
                                                <div class="dropdown-menu bg-light-blue p-2" aria-labelledby="dropdownMenuButton">
                                                    <a class="dropdown-item p-0" href="javascript:void(0);">
                                                        <asp:Button ID="btn_download_ctrc" class="btn btn-sm p-0 btn-block" runat="server" Text="View" OnClick="btn_download_ctrc_Click" />
                                                    </a>
                                                    <a class="dropdown-item p-0" href="javascript:void(0);" data-toggle="modal" data-target="#attachctrc">
                                                        <asp:Button ID="btn" class="btn btn-sm p-0 btn-block" runat="server" Text="Attach" /></a>
                                                </div>
                                            </div>


                                            <a href="javascript:void(0);" class="d-inline-block" title="landline">
                                                <asp:ImageButton ID="img_landline" runat="server" ImageUrl="../Content/images/icon_landline.png" alt="" Width="30" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block" title="Mail">
                                                <asp:ImageButton ID="img_mail" runat="server" ImageUrl="../Content/images/icon_mail.png" alt="" Width="30" OnClick="sndMail" />
                                            </a>

                                            <a href="javascript:void(0);" class="d-inline-block float-right" style="padding-left: 5px;" title="Send Invitation">
                                                <asp:ImageButton ID="img_invite" runat="server" ImageUrl="../Content/images/invite.png" alt="" Width="30" OnClick="sndInv" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block float-right" title="Save">
                                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setTour" ValidationGroup="validation_tour" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="w-100 h-100 position-relative" id="entity_list">
                                        <asp:ListBox ID="select_notes" runat="server" class="list-unstyled custom-list mb-3" Style="border: 0px; width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="dtlNote"></asp:ListBox>
                                        <div class="form-group position-relative">
                                            <asp:DropDownList ID="select_note_type" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_note" ControlToValidate="select_note_type" ErrorMessage="*" InitialValue="Select" Class="validation-class"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group position-relative">
                                            <asp:TextBox ID="input_note_txt" runat="server" TextMode="MultiLine" Rows="3" class="form-control form-control-sm"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_note" ControlToValidate="input_note_txt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />

                                        </div>
                                        <div class="d-flex align-items-center justify-content-between bot-fix">
                                            <div>
                                                <asp:Label ID="div_mod_info" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <a href="javascript:void(0);" data-toggle="tooltip" data-placement="bottom" title="Delete Note">
                                                    <asp:ImageButton ID="img_Note_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delNote" OnClientClick="return confirm('Are you sure you want to delete this record?');" />

                                                </a>
                                                <a href="javascript:void(0);" data-toggle="tooltip" data-placement="bottom" title="Add Note">
                                                    <asp:ImageButton ID="img_Note_add" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newNote" />

                                                </a>
                                                <a href="javascript:void(0);" data-toggle="tooltip" data-placement="bottom" title="Save Note">
                                                    <asp:ImageButton ID="img_Note_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setNote" ValidationGroup="validation_note" />

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
        </div>
    </section>

    <!-- Modal -->
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
    <div class="modal fade" id="attachctrc" tabindex="-1" role="dialog" aria-labelledby="attachctrc" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header py-2">
                    <h5 class="modal-title" id="exampleModalCenterTitle1">Attach File Ctrc</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label for="" class="col-form-label">File</label>
                        <asp:FileUpload ID="file_upload_ctrc" runat="server" />
                        <asp:RegularExpressionValidator ID="regpdf" Text="*" ErrorMessage="*" ControlToValidate="file_upload_ctrc" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" ValidationGroup="validation-ctrc" Class="validation-class" />
                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="file_upload_ctrc" runat="server" Display="Dynamic" ValidationGroup="validation-ctrc" Class="validation-class" />
                    </div>

                </div>
                <div class="modal-footer py-2">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btn_attach_ctrc" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="validation-ctrc" OnClick="btn_attach_ctrc_Click" />
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/autoNumeric-1.9.21.js"></script>
    <script type="text/javascript">
        $(".number").autoNumeric();
    </script>
</asp:Content>
