<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contract.aspx.cs" Inherits="SchoolTours.Operations.contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        function shwMod() {
            if ($('#MainContent_lbl_mod_details').css('display') == 'none') {
                $('#MainContent_lbl_mod_details').show();
            }
            else {
                $('#MainContent_lbl_mod_details').hide();
            }
        }
        function chgDir(value) {
            if (value == 'plus') {
                $('#MainContent_toggle_plus').hide();
                $('#MainContent_toggle_minus').show();
            }
            else {
                $('#MainContent_toggle_plus').show();
                $('#MainContent_toggle_minus').hide();
            }
        }
    </script>
    <section class="main pt-4">
        <asp:HiddenField id="contract_id" runat="server"/>
        <asp:HiddenField id="nr_days" runat="server"/>
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
                                    <asp:ImageButton ID="img_pax" runat="server" ImageUrl="../Content/images/icon_pax.png" data-placement="bottom" title="pax" Width="40" OnClick="img_pax_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_billing" runat="server" ImageUrl="../Content/images/icon_inv.png" data-placement="bottom" title="billing" Width="40" OnClick="img_billing_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_contracts" runat="server" ImageUrl="../Content/images/icon_threshold_selected.png" data-placement="bottom" title="Deadlines" Width="40" OnClick="img_contracts_Click" /></li>
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
                                <div class="col-md-12">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12  py-2 mb-2 overflow-hidden">
                                            <div class="table-responsive overflow-auto bg-white border-rad" style="height: calc(100vh - 430px);">
                                                <asp:GridView runat="server" ID="gv_contract" DataKeyNames="contract_id" OnRowCommand="gv_contract_RowCommand"
                                                    CssClass="table table-borderless table-sm bg-white" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;"
                                                    OnPageIndexChanging="gv_contract_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__Date" runat="server" Text='<%# Bind("contract_date") %>' CommandName="contract" CommandArgument='<%#Eval("contract_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__Type" runat="server" Text='<%# Bind("ref_descr") %>' CommandName="contract" CommandArgument='<%#Eval("contract_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contract Description" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__Contract_Description" runat="server" Text='<%# Bind("contract_descr") %>' CommandName="contract" CommandArgument='<%#Eval("contract_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__Status" runat="server" Text='<%# Bind("status_ind") %>' CommandName="contract" CommandArgument='<%#Eval("contract_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-auto">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_date_type" runat="server" class="custom-select custom-select-sm" OnSelectedIndexChanged="shwDtCtrl" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_contract" ControlToValidate="select_date_type" ErrorMessage="*" InitialValue="Date Type" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-auto">
                                            <div class="form-group mb-2" id="div_input_date" runat="server" visible="true">
                                                <asp:TextBox ID="input_date" runat="server" type="date" class="form-control form-control-sm" placeholder="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validation_contract" ControlToValidate="input_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                            <div class="d-flex align-items-center" id="div_plusMin_toggle" runat="server" visible="false">
                                                <div class="form-group mb-2 pr-3">
                                                    <asp:ImageButton ID="toggle_plus" runat="server" ImageUrl="../Content/images/toggle_plus.png" Visible="false" Width="15" OnClick="toggle_plus_Click" />
                                                    <asp:ImageButton ID="toggle_minus" runat="server" ImageUrl="../Content/images/toggle_minus.png" Width="15" OnClick="toggle_minus_Click" />
                                                </div>
                                                <div class="form-group mb-2">
                                                    <%--OnClientClick="javascript:chgDir('plus');" --%>
                                                    <%--OnClientClick="javascript:chgDir('minus');"--%>
                                                    <asp:TextBox ID="input_nr_days" runat="server" class="form-control form-control-sm" placeholder="Days" MaxLength="3" Type="number"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_contract_type" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_contract" ControlToValidate="select_contract_type" ErrorMessage="*" InitialValue="Contract Type" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_contract_descr" runat="server" class="form-control form-control-sm" placeholder="Contract Description" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_contract" ControlToValidate="input_contract_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-auto ml-auto">
                                            <asp:CheckBox ID="chk_status" runat="server" AutoPostBack="true" /><span class="text-white pl-2">Alert</span>
                                            <%--<div class="custom-control custom-checkbox mt-2">
                                                <asp:CheckBox ID="chk_status" runat="server" class="custom-control-input" AutoPostBack="true"/> 
                                                <label class="custom-control-label" for="Alerted">Alerted</label>
                                            </div>--%>
                                        </div>


                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <a href="javascript:void(0);" class="d-inline-block float-left" onclick="javascript:shwMod();" title="Info">
                                                <img src="../Content/images/icon_info.png" alt="" width="30">
                                                <asp:Label ID="lbl_mod_details" runat="server" Style="display: none; font-size: x-small;" class="text-active mr-5"></asp:Label>
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_del" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delContract" title="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            </a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newContract" title="Add"/></a>
                                            <a href="javascript:void(0);" class="d-inline-block">
                                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setContract" ValidationGroup="validation_contract"  title="Save"/></a>
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
</asp:Content>
