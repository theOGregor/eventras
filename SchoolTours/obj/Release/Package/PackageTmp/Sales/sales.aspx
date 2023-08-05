<%@ Page Title="Sales Central" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sales.aspx.cs" Inherits="SchoolTours.Sales.sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .main {
            height: calc(100vh - 200px);
            overflow-y: auto;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
   <%-- <script type="text/javascript">

        function preventMultipleSubmissions() {
            $('#<%=img_entity_save.ClientID %>').prop('disabled', true);
    $('#<%=img_person_save.ClientID %>').prop('disabled', true);
    $('#<%=img_save_hotlist.ClientID %>').prop('disabled', true);
    $('#<%=img_save_note.ClientID %>').prop('disabled', true);
    $('#<%=img_contract_save.ClientID %>').prop('disabled', true);
}

window.onbeforeunload = preventMultipleSubmissions;

    </script>--%>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('.phone_us').mask('000.000.0000');
        });

        //========================================================= entity
        function EntityBlur(e) {
            __doPostBack('<%=e_tag_new_search.UniqueID%>', "");
        }
        function EnterEntityEvent(e) {
            //debugger;
            // console.log($('#MainContent_e_entity_tag_descr').val());
            //console.log(e.keyCode);
            if (e.keyCode == 13) {
                // e.preventDefault();
                __doPostBack('<%=e_tag_new.UniqueID%>', "");
            }
            else {
                <%--setTimeout(function () {
                    __doPostBack('<%=e_tag_new_search.UniqueID%>', "");
                }, 4000);--%>
            }
        }
        //====================================================== person

        function PersonBlur(e) {
            __doPostBack('<%=person_tag_new_Search.UniqueID%>', "");
        }
        function EnterpersonEvent(e) {
            debugger;
            // console.log($('#MainContent_e_entity_tag_descr').val());
            // console.log(e.keyCode);
            if (e.keyCode == 13) {
                //e.preventDefault();
                __doPostBack('<%=person_tag_new.UniqueID%>', "");
           }
           else {
               <%--  __doPostBack('<%=person_tag_new_Search.UniqueID%>', "");--%>
           }
       }

    </script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/CustomJS/jquery.mask.min.js"></script>
    <script type="text/javascript">
        function showTimeZone(values) {
            debugger;
            var value = '';
            if (values == 'EST') {
                localStorage.widget = 'Eastern';
                value = 'Eastern';
            }
            else if (values == 'CST') {
                localStorage.widget = 'Central';
                value = 'Central';
            }
            else if (values == 'MST') {
                localStorage.widget = 'Mountain';
                value = 'Mountain';
            }
            else if (values == 'PST') {
                localStorage.widget = 'Arizona';
                value = 'Arizona';
            }
            else if (values == 'AKST') {
                localStorage.widget = 'Pacific';
                value = 'Pacific';
            }
            else if (values == 'HST') {
                localStorage.widget = 'Alaska';
                value = 'Alaska';
            }

            //alert(values);
            localStorage.widget = value;
            $('.div_world_clock_widget').removeClass('widgetActiveZone');
            $('#div_' + Value).addClass('widgetActiveZone');
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function shwMod(value) {
            debugger;
            if (value == "entity") {
                if ($('#MainContent_lbl_entity_info').css('display') == 'none') {
                    $('#MainContent_lbl_entity_info').show();
                }
                else {
                    $('#MainContent_lbl_entity_info').hide();

                }
            }
            else if (value == "person") {
                if ($('#MainContent_lbl_person_info').css('display') == 'none') {
                    $('#MainContent_lbl_person_info').show();
                }
                else {
                    $('#MainContent_lbl_person_info').hide();

                }
            }
        }
        function opentab() {
            debugger;
            var url = $('#MainContent_lbl_entity_Url').html().trim();
            if (url != '') {
                url = url.match(/^https?:/) ? url : '//' + url;
                window.open(url, '_blank');
            }
        }

    </script>
    <%--<form id="form1" runat="server" defaultbutton="btn_Enterkey">--%>

    <section class="main">
        <asp:HiddenField  ID="entity_id" runat="server"/>
        <asp:HiddenField  ID="person_id" runat="server"/>
        <asp:HiddenField  ID="hotlist_id" runat="server"/>
        <asp:HiddenField  ID="note_id" runat="server"/>
        <asp:HiddenField  ID="person_name" runat="server"/>
        <asp:HiddenField  ID="SaveCount" runat="server"/>

        <div class="row mx-0 align-items-stretch h-100">
            <div class="col-lg-3 col-md-4 mb-4">
                <div class="card bg-light-blue h-100 mb-0">
                    <div class="card-body p-2">
                        <div class="w-100 w-100 h-100 position-relative">
                            <div class="form-group mb-1">
                                <label for="" class="sr-only">Organization Name</label>
                                <asp:TextBox ID="input_entity_nm" runat="server" class="form-control form-control-sm" placeholder="Organization Name" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="form-group mb-1">
                                <label for="" class="sr-only">First or Last Name</label>
                                <asp:TextBox ID="input_person_nm" runat="server" class="form-control form-control-sm" placeholder="First or Last Name" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="form-group mb-1">
                                <label for="" class="sr-only">State</label>
                                <asp:TextBox ID="input_state" runat="server" class="form-control form-control-sm" placeholder="State" MaxLength="50"></asp:TextBox>

                            </div>
                            <div class="form-group mb-1">
                                <label for="" class="sr-only">tags</label>
                                <asp:ListBox ID="div_sel_tags" runat="server" class="form-control form-control-sm bg-secondary" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="tagRemove" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="form-group mb-1">
                                <label for="" class="sr-only">Add tags</label>
                                <asp:TextBox ID="input_tag_descr" runat="server" class="form-control form-control-sm" placeholder="Add tags" OnTextChanged="tagSearch" AutoPostBack="true"></asp:TextBox>

                                <asp:ListBox ID="div_add_tags" runat="server" class="form-control form-control-sm bg-primary border-0" OnSelectedIndexChanged="tagSelect" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="w-100 bot-fix">
                                <div class="mt-3 mb-0">
                                    <asp:Button ID="bttn_search" runat="server" Text="Search" class="btn btn-primary btn-sm btn-block" OnClick="bttn_search_Click" />
                                    <asp:Button ID="bttn_clear" runat="server" Text="Clear" class="btn btn-light btn-sm btn-block" OnClick="resetForm" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 mb-4" id="div_entity_panel" runat="server">
                <div class="card bg-primary h-100 mb-0">
                    <div class="card-body p-2">
                        <div class="w-100 w-100 h-100 position-relative" id="entity_list" runat="server">
                            <asp:ListBox ID="div_entity_list" runat="server" class="list-unstyled custom-list" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="div_entity_list_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            <div id="tooltip_container"></div>
                            <address class="text-active mb-5">
                                <h5 class="text-active">
                                    <asp:Label ID="lbl_entity_descr" runat="server" Text=""></asp:Label></h5>
                                <h6 class="text-active mb-4">
                                    <asp:Label ID="lbl_entity_address" runat="server" Text=""></asp:Label></h6>
                                <h6 class="text-active">
                                    <img src="../Content/images/icon_landline.png" alt="" width="25" title="landline">
                                    <asp:Label ID="lbl_entity_phone" runat="server" Text=""></asp:Label></h6>
                                <h6 class="text-active">
                                    <%--<asp:ImageButton ID="img_url" runat="server" src="../Content/images/icon_web.png" alt="" Width="25" title="Web" OnClick="img_url_Click" />--%>
                                    <img src="../Content/images/icon_web.png" alt="" width="25" title="Web" onclick="javascript:opentab();">
                                    <asp:Label ID="lbl_entity_Url" runat="server" Text=""></asp:Label></h6>
                                <h6 class="text-active px-2 py-4 bg-secondary border-0 border-rad">
                                    <asp:Label ID="div_entity_tags" runat="server" Text=""></asp:Label></h6>
                            </address>
                            <div class="d-flex align-items-center justify-content-between bot-fix">
                                <div>
                                    <a href="#" onclick="javascript:shwMod('entity');">
                                        <img src="../Content/images/icon_info.png" alt="" width="30" title="Info"></a>
                                    <asp:Label ID="lbl_entity_info" runat="server" Style="display: none; font-size: x-small;" class="text-active mr-5"></asp:Label>
                                </div>
                                <div>
                                    <asp:ImageButton ID="img_del_entity" runat="server" ImageUrl="../Content/images/icon_delete.png" data-placement="bottom" title="Delete Entity" Width="30" OnClick="delEntity" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    <asp:ImageButton ID="img_add_entity" runat="server" ImageUrl="../Content/images/icon_add.png" data-placement="bottom" title="Add Entity" Width="30" OnClick="addEntity" />
                                    <asp:ImageButton ID="img_edit_entity" runat="server" ImageUrl="../Content/images/icon_edit.png" data-placement="bottom" title="Edit Entity" Width="30" OnClick="editEntity" />
                                </div>
                            </div>
                        </div>
                        <div class="w-100 h-100 position-relative" id="new_entity_record" visible="false" runat="server">
                            <h5 class="text-active">
                                <asp:Label ID="lbl_entity_mode" runat="server" Text=""></asp:Label>
                            </h5>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Name of Entity</label>
                                        <asp:TextBox ID="input_entity_descr" runat="server" class="form-control form-control-sm" MaxLength="100" placeholder="Name of Entity"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Address</label>
                                        <asp:TextBox ID="input_entity_address" runat="server" TextMode="MultiLine" Rows="1" MaxLength="50" class="form-control form-control-sm" placeholder="Address"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_address" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">city</label>
                                        <asp:TextBox ID="input_entity_city" runat="server" class="form-control form-control-sm" MaxLength="20" placeholder="city"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_city" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">state</label>
                                        <asp:TextBox ID="input_entity_state" runat="server" class="form-control form-control-sm" MaxLength="5" placeholder="state"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_state" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Zip</label>
                                        <%--OnTextChanged="input_entity_zip_TextChanged" AutoPostBack="true"--%>
                                        <asp:TextBox ID="input_entity_zip" runat="server" class="form-control form-control-sm" MaxLength="10" placeholder="Zip" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_zip" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">phone</label>
                                        <asp:TextBox ID="input_entity_phone" runat="server" class="form-control form-control-sm phone_us" MaxLength="13" onkeypress="return isNumberKey(event)" placeholder="phone"></asp:TextBox>
                                        <%-- <asp:TextBox ID="input_entity_phone" runat="server" class="form-control form-control-sm" MaxLength="13" onkeypress="return isNumberKey(event)"  OnTextChanged="input_entity_phone_TextChanged" AutoPostBack="true" placeholder="phone"></asp:TextBox>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_entity" ControlToValidate="input_entity_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPhone" runat="server" ControlToValidate="input_entity_phone" ValidationGroup="validation_entity"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">URL</label>
                                        <asp:TextBox ID="input_entity_url" runat="server" class="form-control form-control-sm" MaxLength="100" placeholder="URL"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Tags</label>
                                        <asp:ListBox ID="e_entity_sel_tags" runat="server" class="form-control form-control-sm bg-secondary border-0" OnSelectedIndexChanged="tagRemove_entity" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1 position-relative">
                                        <label for="" class="col-form-label sr-only">Add Tags</label>
                                        <%--onkeyup="EnterEntityEvent(event)" onblur="javascript:EntityBlur(event)";--%>
                                        <%-- <asp:TextBox ID="e_entity_tag_descr" runat="server" AutoPostBack="true"  OnTextChanged="entity_tagSearch" placeholder="Add Tags" class="form-control form-control-sm"></asp:TextBox> --%>
                                        <asp:TextBox ID="e_entity_tag_descr" runat="server" placeholder="Add Tags" class="form-control form-control-sm" onkeyup="EnterEntityEvent(event)" onblur="javascript:EntityBlur(event)"></asp:TextBox>
                                        <asp:ImageButton ID="e_tag_new" runat="server" ImageUrl="../Content/images/add.png" alt="" OnClick="tagECreate" class="CustomAddIcon" />
                                        <asp:ImageButton ID="e_tag_new_search" runat="server" ImageUrl="../Content/images/add.png" alt="" OnClick="e_tag_new_search_Click" class="CustomAddIcon" Style="display: none;" />
                                        <asp:ListBox ID="e_entity_tag_assign" runat="server" class="list-unstyled custom-list" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="tagSelect_entity" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 bot-fix d-flex align-items-center justify-content-between">
                                <span class="text-active">
                                    <asp:Label ID="div_entity_mod_info" runat="server" Text=""></asp:Label>
                                </span>
                                <span>
                                    <asp:ImageButton ID="img_entity_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_Entity" />
                                    <asp:ImageButton ID="img_entity_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="addUpdateEntity" ValidationGroup="validation_entity" />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-5 col-md-4 mb-4" id="div_person_panel" runat="server">
                <div class="card bg-primary h-100 mb-0">
                    <div class="card-body p-2">
                        <div class="w-100 h-100 position-relative" id="person_list" runat="server">
                            <asp:ListBox ID="div_person_list" runat="server" class="list-unstyled custom-list" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="dtlPerson" AutoPostBack="true"></asp:ListBox>
                            <address class="text-active">
                                <h5 class="text-active">
                                    <asp:Label ID="lbl_person_Name" runat="server"></asp:Label></h5>
                                <h6 class="text-active mb-4">
                                    <asp:Label ID="lbl_person_address" runat="server"></asp:Label></h6>
                                <h6 class="text-active">
                                    <img src="../Content/images/icon_landline.png" alt="" width="25" title="landline">
                                    <asp:Label ID="lbl_person_office_phone" runat="server"></asp:Label></h6>
                                <h6 class="text-active">
                                    <asp:ImageButton ID="imgPersonMail" runat="server" ImageUrl="../Content/images/icon_mail.png" alt="" Width="25" title="Mail" OnClick="imgPersonMail_Click" />
                                    <%--<img src="../Content/images/icon_mail.png" alt="" width="25" title="Mail">--%>
                                    <asp:Label ID="lbl_person_eMail" runat="server"></asp:Label></h6>
                                <h6 class="text-active">
                                    <img src="../Content/images/icon_cellphone.png" alt="" width="25" title="Cellphone">
                                    <asp:Label ID="lbl_person_cell_phone" runat="server"></asp:Label></h6>
                                <h6 class="text-active px-2 py-4 bg-secondary border-0 border-rad">
                                    <asp:Label ID="div_person_tags" runat="server" Text=""> </asp:Label></h6>
                            </address>
                            <div class="d-flex align-items-center justify-content-between bot-fix">
                                <div>
                                    <a href="#" onclick="javascript:shwMod('person');">
                                        <img src="../Content/images/icon_info.png" alt="" width="30" title="Info"></a>
                                    <asp:Label ID="lbl_person_info" runat="server" Style="display: none; font-size: x-small;" class="text-active mr-5"></asp:Label>
                                </div>
                                <div>
                                    <asp:ImageButton ID="img_contract" runat="server" ImageUrl="../Content/images/icon_contract.png" data-placement="bottom" title="contract" Width="30" OnClick="showcontract" />
                                    <asp:ImageButton ID="img_hotlist" runat="server" ImageUrl="../Content/images/icon_hotlist.png" data-placement="bottom" title="Hotlist" Width="30" OnClick="showhotlist" />
                                    <asp:ImageButton ID="img_notes" runat="server" ImageUrl="../Content/images/icon_note.png" data-placement="bottom" title="Notes" Width="30" OnClick="showNote" />
                                    <asp:ImageButton ID="img_del_person" runat="server" ImageUrl="../Content/images/icon_delete.png" data-placement="bottom" title="Delete person" Width="30" OnClick="delPerson" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    <asp:ImageButton ID="img_add_person" runat="server" ImageUrl="../Content/images/icon_add.png" data-placement="bottom" title="Add person" Width="30" OnClick="addPerson" />
                                    <asp:ImageButton ID="img_edit_person" runat="server" ImageUrl="../Content/images/icon_edit.png" data-placement="bottom" title="Edit person" Width="30" OnClick="editPerson" />
                                </div>
                            </div>
                        </div>
                        <div class="w-100 h-100 position-relative" id="div_contract" runat="server" visible="false">
                            <h5 class="text-active d-flex justify-content-between align-items-center">
                                <asp:Label ID="lbl_contract_for" runat="server" Text="hotlist for Person"></asp:Label>
                                <asp:ImageButton ID="img_contract_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_contract" />
                            </h5>
                            <asp:ListBox ID="select_evt" runat="server" class="list-unstyled custom-list-hotlist" Style="background-color: #00a9ee; border: 0px; width: 100%;"></asp:ListBox>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_cost_descr" runat="server" MaxLength="100" class="form-control form-control-sm" TextMode="MultiLine" Rows="2" placeholder="cost descr"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="validation_contract" ControlToValidate="input_cost_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_cost_amt" runat="server" MaxLength="10" data-v-min="-999999999.0" data-v-max="999999999.0" data-m-dec="2" class="form-control form-control-sm number" placeholder="cost amount"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="validation_contract" ControlToValidate="input_cost_amt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 text-right bot-fix">
                                <asp:ImageButton ID="img_contract_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setTour" ValidationGroup="validation_contract" />
                            </div>
                        </div>
                        <div class="w-100 h-100 position-relative" id="div_hotlist" runat="server" visible="false">
                            <h5 class="text-active d-flex justify-content-between align-items-center">
                                <asp:Label ID="lbl_hotlist_for" runat="server" Text="hotlist for Person"></asp:Label>
                                <asp:ImageButton ID="img_cancel_hotlist" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_hotlist" />
                            </h5>

                            <asp:ListBox ID="div_hotlist_list" runat="server" class="list-unstyled custom-list-hotlist" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="dtlHotlist" AutoPostBack="true"></asp:ListBox>
                            <div class="form-row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_hotlist_date" runat="server" class="form-control form-control-sm" type="date" value="MM/dd/yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="validation_hotlist" ControlToValidate="input_hotlist_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_hotlist_time" runat="server" type="time" class="form-control form-control-sm"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="validation_hotlist" ControlToValidate="input_hotlist_time" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="select_hotlist_method" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" ValidationGroup="validation_hotlist" runat="server" ControlToValidate="select_hotlist_method" Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_hotlist_notes_txt" runat="server" TextMode="MultiLine" Rows="3" class="form-control form-control-sm" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="validation_hotlist" ControlToValidate="input_hotlist_notes_txt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 bot-fix d-flex align-items-center justify-content-between">
                                <span class="text-active">
                                    <asp:Label ID="div_hotlist_mod_info" runat="server" Text=""></asp:Label>
                                </span>
                                <span>
                                    <asp:ImageButton ID="img_delete_hotlist" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delHotlist" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    <asp:ImageButton ID="img_add_hotlist" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newHotlist" />
                                    <asp:ImageButton ID="img_save_hotlist" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="img_save_hotlist_Click" ValidationGroup="validation_hotlist" />
                                </span>
                            </div>
                        </div>
                        <div class="w-100 h-100 position-relative" id="div_notes" runat="server" visible="false">
                            <h5 class="text-active d-flex justify-content-between align-items-center">
                                <asp:Label ID="lbl_note_for" runat="server" Text=""></asp:Label>
                                <asp:ImageButton ID="img_cancel_note" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_note" />
                            </h5>
                            <asp:ListBox ID="div_notes_list" runat="server" class="list-unstyled custom-list-hotlist" OnSelectedIndexChanged="dtlNote" AutoPostBack="true" Style="background-color: #00a9ee; border: 0px; width: 100%;"></asp:ListBox>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:DropDownList ID="select_note_type" runat="server" class="custom-select" Style='height: 35px !important; line-height: 25px !important;'>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator17" Display="Dynamic" ValidationGroup="validation_note" runat="server" ControlToValidate="select_note_type" ErrorMessage="*" Class="validation-class"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="input_note_descr" runat="server" class="form-control form-control-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="validation_note" ControlToValidate="input_note_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 bot-fix d-flex align-items-center justify-content-between">
                                <span class="text-active">
                                    <asp:Label ID="div_note_mod_info" runat="server" Text=""></asp:Label>
                                </span>
                                <span>
                                    <asp:ImageButton ID="img_delete_note" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delNote" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    <asp:ImageButton ID="img_add_note" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newNote" />
                                    <asp:ImageButton ID="img_save_note" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="img_save_note_Click" ValidationGroup="validation_note" />
                                </span>
                            </div>
                        </div>
                        <div class="w-100 h-100 position-relative" id="new_person_record" runat="server" visible="false">
                            <h5 class="text-active">
                                <asp:Label ID="lbl_person_Mode" runat="server" Text="Add New Person"></asp:Label>
                            </h5>

                            <div class="form-row">
                                <div class="col-md-4">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Title</label>
                                        <asp:TextBox ID="input_person_greeting" runat="server" MaxLength="10" class="form-control form-control-sm" placeholder="Title"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_greeting" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">First Name</label>
                                        <asp:TextBox ID="input_given_name" runat="server" MaxLength="100" class="form-control form-control-sm" placeholder="First Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validation_person" ControlToValidate="input_given_name" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Last Name</label>
                                        <asp:TextBox ID="input_last_nm" runat="server" MaxLength="100" class="form-control form-control-sm" placeholder="Last Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="validation_person" ControlToValidate="input_last_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Address</label>
                                        <asp:TextBox ID="input_person_address" runat="server" MaxLength="100" class="form-control form-control-sm" placeholder="Address" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_address" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">City</label>
                                        <asp:TextBox ID="input_person_city" runat="server" MaxLength="20" class="form-control form-control-sm" placeholder="City"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_city" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">State</label>
                                        <asp:TextBox ID="input_person_state" runat="server" MaxLength="5" class="form-control form-control-sm" placeholder="State"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_state" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Zip</label>
                                        <%--OnTextChanged="input_person_zip_TextChanged" AutoPostBack="true"--%>
                                        <asp:TextBox ID="input_person_zip" runat="server" MaxLength="10" class="form-control form-control-sm" placeholder="Zip" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_zip" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Office Phone</label>
                                        <asp:TextBox ID="input_person_landline" runat="server" MaxLength="13" onkeypress="return isNumberKey(event)" class="form-control form-control-sm phone_us" placeholder="Office Phone"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_landline" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="input_person_landline" ValidationGroup="validation_person"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Cell Phone</label>
                                        <asp:TextBox ID="input_person_cell_phone" runat="server" MaxLength="13" onkeypress="return isNumberKey(event)" class="form-control form-control-sm phone_us" placeholder="Cell Phone"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_cell_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="input_person_cell_phone" ValidationGroup="validation_person"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Email Address</label>
                                        <asp:TextBox ID="input_person_eMail" runat="server" MaxLength="100" class="form-control form-control-sm" placeholder="Email Address"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="validation_person" ControlToValidate="input_person_eMail" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1">
                                        <label for="" class="col-form-label sr-only">Tags</label>
                                        <asp:ListBox ID="person_sel_tags" runat="server" class="form-control form-control-sm bg-secondary border-0" OnSelectedIndexChanged="tagRemove_person" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-1 position-relative">
                                        <label for="" class="col-form-label sr-only">Add Tags</label>
                                        <%--<asp:TextBox ID="person_tag_descr" runat="server" AutoPostBack="true" OnTextChanged="person_tagSearch" placeholder="Add Tags" class="form-control form-control-sm"></asp:TextBox>--%>
                                        <asp:TextBox ID="person_tag_descr" runat="server" placeholder="Add Tags" class="form-control form-control-sm" onkeyup="EnterpersonEvent(event)" onblur="javascript:PersonBlur(event);"></asp:TextBox>
                                        <asp:ImageButton ID="person_tag_new" runat="server" ImageUrl="../Content/images/add.png" alt="" OnClick="tagPCreate" class="CustomAddIcon" />
                                        <asp:ImageButton ID="person_tag_new_Search" runat="server" ImageUrl="../Content/images/add.png" alt="" OnClick="person_tag_new_Search_Click" class="CustomAddIcon" Style="display: none;" />
                                        <asp:ListBox ID="person_tag_assign" runat="server" class="list-unstyled custom-list" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="tagSelect_person" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 bot-fix d-flex align-items-center justify-content-between">
                                <span class="text-active">
                                    <asp:Label ID="div_person_mod_info" runat="server" Text=""></asp:Label>
                                </span>
                                <span>
                                    <asp:ImageButton ID="img_person_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_person" />
                                    <asp:ImageButton ID="img_person_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="addUpdateperson" ValidationGroup="validation_person" />
                                </span>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </section>
    <%-- </form>--%>
     <script src="../Scripts/autoNumeric-1.9.21.js"></script>
    <script type="text/javascript">
        $(".number").autoNumeric();
    </script>
</asp:Content>
