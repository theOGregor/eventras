<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pax.aspx.cs" Inherits="SchoolTours.Operations.pax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script src="../Scripts/CustomJS/jquery.mask.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('.phone_us').mask('000.000.0000');
        });
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script>
        function dragStart(e) {
            // Sets the operation allowed for a drag source
            e.dataTransfer.effectAllowed = "move";

            // Sets the value and type of the dragged data
            //alert(e.target.getAttribute("id"));
            e.dataTransfer.setData("Text", e.target.getAttribute("id"));
        }
        function dragOver(e) {

            // Prevent the browser default handling of the data
            e.preventDefault();
            e.stopPropagation();
        }
        function drop(e) {
            debugger;
            // Cancel this event for everyone else
            e.stopPropagation();
            e.preventDefault();

            // Retrieve the dragged data by type
            var data = e.dataTransfer.getData("Text");
            // alert(e.target.id + '~' + data);
            if (e.target.id == "") {
                alert('your drag value not drop properly. please try again.');
                return;
            }

            $.ajax({
                type: "POST",
                url: "pax.aspx/addupdateroom",
                contentType: "application/json; charset=utf-8",
                data: '{"id1":"' + e.target.id + '","id2":"' + data + '","tour_id":"' + $('#MainContent_OPS_tour_id').val() + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    location.reload(true);
                },
                failure: function (response) {

                }
            });

            // Append image to the drop box
            e.target.appendChild(document.getElementById(data));
        }
        function fun_getpaxdetails(value) {

            $.ajax({
                type: "POST",
                url: "pax.aspx/jquerypaxdetail",
                contentType: "application/json; charset=utf-8",
                data: '{"id1":"' + value + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    console.log(response.d);
                    var obj = JSON.parse(response.d);
                    $('#MainContent_OPS_pax_id').val(value);
                    $('#MainContent_input_given_nm').val(obj[0].given_nm);
                    $('#MainContent_input_last_nm').val(obj[0].last_nm);
                    $('#MainContent_select_role').val(obj[0].role_id);
                    $('#MainContent_select_age').val(obj[0].age_id);
                    $('#MainContent_select_sex').val(obj[0].sex_id);
                    if (obj[0].birth_date != "") {
                        var date = (obj[0].birth_date).split(' ');
                        var date1 = (date[0]).split('/');
                        var day = ("0" + date1[1]).slice(-2);
                        var month = ("0" + (date1[0])).slice(-2);
                        var today = date1[2] + "-" + (month) + "-" + (day);
                        $('#MainContent_input_birthdate').val(today);
                    }
                    if (obj[0].diet_id != "")
                        $('#MainContent_select_diet').val(obj[0].diet_id);
                    $('#MainContent_input_eMail').val(obj[0].eMail);
                    if (obj[0].payer_id != "0")
                        $('#MainContent_select_payer').val(obj[0].payer_id);
                    else
                        $('#MainContent_select_payer').val('Payer');
                    var str_phone = obj[0].phone;

                    if (str_phone != "") {
                        try {
                            str_phone = "" + str_phone.substring(0, 3) + "." + str_phone.substring(3, 6) + "." + str_phone.substring(6);
                            $('#MainContent_input_phone').val(str_phone);
                        } catch (err) {
                            $('#MainContent_input_phone').val(obj[0].phone);
                        }
                    }
                },
                failure: function (response) {
                    console.log(response.d);

                }
            });
        }
        function fun_delPaxRoom(room_id, pax_id) {

            $.ajax({
                type: "POST",
                url: "pax.aspx/jquerydelPaxRoom",
                contentType: "application/json; charset=utf-8",
                data: '{"room_id":"' + room_id + '","pax_id":"' + pax_id + '","tour_id":"' + $('#MainContent_OPS_tour_id').val() + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    location.reload(true);
                },
                failure: function (response) {
                    console.log(response.d);

                }
            });
        }
    </script>
    <div id="div_drag_drop" style="display: none;">
        <div id="test" class="dropBox" ondragover="dragOver(event);" ondrop="drop(event);">
            <!--Dropped image will be inserted here-->
        </div>

        <div id="test123" class="dropBox" ondragover="dragOver(event);" ondrop="drop(event);">
            <!--Dropped image will be inserted here-->
        </div>
        <div id="11" draggable="true" ondragstart="dragStart(event);">test </div>
        <div id="22" draggable="true" ondragstart="dragStart(event);">test 123 </div>
    </div>
    <section class="main pt-4">
        <asp:HiddenField ID="OPS_tour_id" runat="server" />
        <asp:HiddenField ID="OPS_pax_id" runat="server" />
        <asp:HiddenField ID="OPS_room_id" runat="server" />

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
                            <%-- <div class="form-group">
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
                                    <asp:ImageButton ID="img_pax" runat="server" ImageUrl="../Content/images/icon_pax_selected.png" data-placement="bottom" title="pax" Width="40" OnClick="img_pax_Click" /></li>
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
                            <asp:Label ID="div_tour_info" runat="server"></asp:Label></h6>
                    </div>
                    <div class="card-body">
                        <div class="w-100 h-100 position-relative">
                            <div class="row h-100">
                                <div class="col-md-4">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12  pb-2 mb-2">
                                            <div class="w-100 bg-secondary">
                                                <h6 class="text-dark clearfix mb-0 p-2">Participants List
														<span class="float-right">(<asp:Label ID="div_nr_pax" runat="server"></asp:Label>)</span>
                                                </h6>
                                            </div>
                                            <%--<asp:ListBox ID="select_pax" runat="server" class="list-unstyled mb-0 bg-white overflow-auto p-3 droptrue" Style="height: calc(100vh - 550px); width: 100%;" OnSelectedIndexChanged="dtlPax" AutoPostBack="true"></asp:ListBox>--%>
                                            <div id="div_pax" runat="server" style="height: calc(100vh - 550px); width: 100%; overflow: auto;"></div>

                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_given_nm" runat="server" class="form-control form-control-sm" placeholder="First Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_pax" ControlToValidate="input_given_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_last_nm" runat="server" class="form-control form-control-sm" placeholder="Last Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validation_pax" ControlToValidate="input_last_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_role" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_role" ErrorMessage="*" InitialValue="Role" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_age" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_age" ErrorMessage="*" InitialValue="Age" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_sex" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_sex" ErrorMessage="*" InitialValue="Sex" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_birthdate" runat="server" type="date" class="form-control form-control-sm" placeholder="Birthday"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validation_pax" ControlToValidate="input_birthdate" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_diet" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_diet" ErrorMessage="*" InitialValue="Diet" Class="validation-class"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_eMail" runat="server" class="form-control form-control-sm" placeholder="Email"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="input_eMail" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_payer" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_payer" ErrorMessage="*" InitialValue="Payer" Class="validation-class"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_phone" runat="server" class="form-control form-control-sm phone_us" placeholder="Phone"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPhone" runat="server" ControlToValidate="input_phone"
                                                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                                    ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delPax" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_add" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newPax" title="Add" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setPax" ValidationGroup="validation_pax" title="Save" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="w-100 h-100 position-relative" id="entity_list">
                                        <div class="card overflow-hidden border-0">
                                            <div class="card-header bg-secondary py-2" id="div_room_assign" runat="server">
                                                <h6 class="text-dark mb-0">Room Assignments
                                                    <ul class="list-unstyled mb-0 d-inline-flex float-right">
                                                        <li>Q=<asp:Label ID="lbl_quad_nr" runat="server"></asp:Label></li>
                                                        /<li>T=<asp:Label ID="lbl_triple_nr" runat="server"></asp:Label></li>
                                                        /<li>D=<asp:Label ID="lbl_double_nr" runat="server"></asp:Label></li>
                                                        /<li>S=<asp:Label ID="lbl_single_nr" runat="server"></asp:Label></li>
                                                        /<li>X=<asp:Label ID="lbl_other_nr" runat="server"></asp:Label></li>
                                                    </ul>
                                                </h6>
                                            </div>
                                            <div class="card-body">
                                                <div style="height: calc(100vh - 430px); overflow-y: auto; overflow-x: hidden;">
                                                    <div class="row">
                                                        <div id="div_Room" runat="server" class="col form-row connectedSortable">
                                                            <%-- <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6 mb-3">
                                                                <div class="card h-100 overflow-hidden border-0">
                                                                    <div class="h-100 bg-warning p-2">
                                                                        <p class="text-right text-white mb-0">Quad</p>
                                                                        <a href="javascript:void(0);" class="d-block">Robert R.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Jose B.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Samual T.</a>
                                                                        <a href="javascript:void(0);" class="d-block">George G.</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6 mb-3">
                                                                <div class="card h-100 overflow-hidden border-0">
                                                                    <div class="h-100 bg-primary p-2">
                                                                        <p class="text-right text-white mb-0">Triple</p>
                                                                        <a href="javascript:void(0);" class="d-block">Robert R.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Jose B.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Samual T.</a>
                                                                        <a href="javascript:void(0);" class="d-block">George G.</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6 mb-3">
                                                                <div class="card h-100 overflow-hidden border-0">
                                                                    <div class="h-100 bg-info p-2">
                                                                        <p class="text-right text-white mb-0">Quad</p>
                                                                        <a href="javascript:void(0);" class="d-block">Robert R.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Jose B.</a>
                                                                        <a href="javascript:void(0);" class="d-block">Samual T.</a>
                                                                        <a href="javascript:void(0);" class="d-block">George G.</a>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div class=" col-md-12 mt-3 bot-fix text-right dropdown d-inline-block">
                                            <a href="javascript:void(0);" class="d-inline-block" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <asp:ImageButton ID="img_mytour_itin" runat="server" ImageUrl="../Content/images/icon_print.png" alt="" Width="30" title="Print" />
                                            </a>
                                            <div class="dropdown-menu bg-light-blue p-2" aria-labelledby="dropdownMenuButton">
                                                <a class="dropdown-item p-0" href="javascript:void(0);">
                                                    <asp:Button class="btn btn-sm p-0 btn-block" ID="btn_Rooming_List" runat="server" Text="Rooming List" OnClick="btn_Rooming_List_Click" />
                                                </a>
                                                <%--data-toggle="modal" data-target="#attachitin"--%>
                                                <a class="dropdown-item p-0" href="javascript:void(0);">
                                                    <asp:Button ID="btn_Participant_List" class="btn btn-sm p-0 btn-block" runat="server" Text="Participant List" OnClick="btn_Participant_List_Click" />
                                                </a>
                                            </div>
                                        </div>
                                        <div class="col-md-12 mt-3 bot-fix text-right" style="display: none;">
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_print" runat="server" ImageUrl="../Content/images/icon_print.png" alt="" Width="30" OnClick="prnList" />

                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div_Print" runat="server" style="display: none;"></div>
        </div>
    </section>
</asp:Content>
