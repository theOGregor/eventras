<%@ Page Title="" Language="C#" MasterPageFile="~/Mytour_Site.Master" AutoEventWireup="true" CodeBehind="mytour_pax.aspx.cs" Inherits="SchoolTours.Mytour.mytour_pax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            $('#ContentPlaceHolder1_room_id').val(e.target.id);
            $('#ContentPlaceHolder1_pax_id').val(data);
            $.ajax({
                type: "POST",
                url: "mytour_pax.aspx/addupdateroom",
                contentType: "application/json; charset=utf-8",
                data: '{"id1":"' + e.target.id + '","id2":"' + data + '","admin_ind":"' + $('#ContentPlaceHolder1_admin_ind').val() + '","tour_id":"' + $('#ContentPlaceHolder1_tour_id').val() + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    // location.reload(true);
                    MenuRedirect('mytour', 'mytour_pax');
                },
                failure: function (response) {

                }
            });

            // Append image to the drop box
            e.target.appendChild(document.getElementById(data));
        }
        function fun_getpaxdetails(value) {

            $('#ContentPlaceHolder1_pax_id').val(value);
            $.ajax({
                type: "POST",
                url: "mytour_pax.aspx/jquerypaxdetail",
                contentType: "application/json; charset=utf-8",
                data: '{"id1":"' + value + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    console.log(response.d);
                    var obj = JSON.parse(response.d);

                    $('#ContentPlaceHolder1_input_given_nm').val(obj[0].given_nm);
                    $('#ContentPlaceHolder1_input_last_nm').val(obj[0].last_nm);
                    $('#ContentPlaceHolder1_select_role').val(obj[0].role_id);
                    $('#ContentPlaceHolder1_select_age').val(obj[0].age_id);
                    $('#ContentPlaceHolder1_select_sex').val(obj[0].sex_id);
                    if (obj[0].birth_date != "") {
                        var date = (obj[0].birth_date).split(' ');
                        var date1 = (date[0]).split('/');
                        var day = ("0" + date1[1]).slice(-2);
                        var month = ("0" + (date1[0])).slice(-2);
                        var today = date1[2] + "-" + (month) + "-" + (day);
                        $('#ContentPlaceHolder1_input_birthdate').val(today);
                    }
                    if (obj[0].diet_id != "")
                        $('#ContentPlaceHolder1_select_diet').val(obj[0].diet_id);
                    $('#ContentPlaceHolder1_input_eMail').val(obj[0].eMail);
                    if (obj[0].payer_id != "")
                        $('#ContentPlaceHolder1_select_payer').val(obj[0].payer_id);
                    var str_phone = obj[0].phone;

                    if (str_phone != "") {
                        try {
                            str_phone = "" + str_phone.substring(0, 3) + "." + str_phone.substring(3, 6) + "." + str_phone.substring(6);
                            $('#ContentPlaceHolder1_input_phone').val(str_phone);
                        } catch (err) {
                            $('#ContentPlaceHolder1_input_phone').val(obj[0].phone);
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
                url: "mytour_pax.aspx/jquerydelPaxRoom",
                contentType: "application/json; charset=utf-8",
                data: '{"room_id":"' + room_id + '","pax_id":"' + pax_id + '","admin_ind":"' + $('#ContentPlaceHolder1_admin_ind').val() + '","tour_id":"' + $('#ContentPlaceHolder1_tour_id').val() + '"}',
                dataType: "json",
                //async:true,
                success: function (response) {
                    // location.reload(true);
                    MenuRedirect('mytour', 'mytour_pax');
                },
                failure: function (response) {
                    console.log(response.d);

                }
            });
        }
    </script>
    <style>
        .main {
            height: calc(100vh - 120px);
            overflow-y: auto;
        }
    </style>

    <div class="w-100 h-100">
        <asp:HiddenField ID="tour_id" runat="server" />
        <asp:HiddenField ID="pax_id" runat="server" />
        <asp:HiddenField ID="room_id" runat="server" />
        <asp:HiddenField ID="admin_ind" runat="server" />
        <div class="card bg-primary h-100 mb-0 overflow-hidden">
            <div class="card-header bg-blue">
                <h4 class="my-0 text-white">
                    <asp:Label ID="div_tour_info" runat="server" Text="Manage Participants and rooming listing"></asp:Label>
                </h4>
            </div>
            <div class="card-body">
                <div class="w-100 h-100 position-relative">
                    <div class="row h-100">
                        <div class="col-md-4">
                            <div class="form-row h-100 position-relative align-content-start">
                                <div class="col-md-12  pb-2 mb-2">
                                    <div class="w-100 bg-secondary">
                                        <h6 class="text-dark clearfix mb-0 p-2">Participants List
										<span class="float-right">
                                            <asp:Label ID="div_nr_pax" runat="server" Text=""></asp:Label></span>
                                        </h6>
                                    </div>
                                    <div id="div_pax" runat="server" style="height: calc(100vh - 550px); width: 100%; overflow: auto;"></div>
                                    <%-- <ul class="list-unstyled mb-0 bg-white overflow-auto" style="height: calc(100vh - 550px);">
                                        <li class="px-2 py-1 text-dark">Willioms Scot (Student)
                                        </li>
                                        <li class="px-2 py-1 text-dark">Willioms Scot (Student)
                                        </li>
                                        <li class="px-2 py-1 text-dark">Willioms Scot (Student)
                                        </li>
                                        <li class="px-2 py-1 text-dark">Willioms Scot (Student)
                                        </li>
                                    </ul>--%>
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

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group mb-2">
                                        <asp:DropDownList ID="select_diet" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_diet" ErrorMessage="*" InitialValue="Diet" Class="validation-class"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_pax" ControlToValidate="select_payer" ErrorMessage="*" InitialValue="Payer" Class="validation-class"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group mb-2">
                                        <asp:TextBox ID="input_phone" runat="server" class="form-control form-control-sm" placeholder="Phone" AutoPostBack="true" OnTextChanged="input_phone_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 mt-3 bot-fix text-right">
                                <a href="javascript:void(0);" class="d-inline-block">
                                    <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delPax" />
                                </a>
                                <a href="javascript:void(0);" class="d-inline-block">
                                    <asp:ImageButton ID="img_add" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newPax" />
                                </a>
                                <a href="javascript:void(0);" class="d-inline-block">
                                    <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setPax" ValidationGroup="validation_pax" />
                                </a>
                            </div>

                        </div>
                        <div class="col-md-8">
                            <div class="w-100 h-100 position-relative" id="entity_list">
                                <div class="card overflow-hidden border-0">
                                    <div class="card-header bg-secondary py-2">
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
                                        <div style="height: calc(100vh - 380px); overflow-y: auto; overflow-x: hidden;">
                                            <div class="row">
                                                <div id="div_Room" runat="server" class="col form-row connectedSortable">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 mt-3 bot-fix text-right">
                                    <a href="javascript:void(0);" class="d-inline-block">
                                        <asp:ImageButton ID="img_cancel" runat="server" ImageUrl="../Content/images/icon_back.png" title="back" alt="" Width="30" OnClick="goHome" />
                                    </a>
                                    <a href="javascript:void(0);" class="d-inline-block">
                                        <asp:ImageButton ID="img_print" runat="server" ImageUrl="../Content/images/icon_print.png" title="save" alt="" Width="30" OnClick="prnList" />
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
