<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cost_grp.aspx.cs" Inherits="SchoolTours.Operations.cost_grp" %>

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
    </script>
    <section class="main pt-4">
        <asp:HiddenField ID="OPS_tour_id" runat="server" />
        <asp:HiddenField ID="OPS_cost_id" runat="server" />

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
                                    <asp:ImageButton ID="img_billing" runat="server" ImageUrl="../Content/images/icon_inv.png" data-placement="bottom" title="billing" Width="40" OnClick="img_billing_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_contracts" runat="server" ImageUrl="../Content/images/icon_threshold.png" data-placement="bottom" title="Deadlines" Width="40" OnClick="img_contracts_Click" /></li>
                                <li>
                                    <asp:ImageButton ID="img_cost" runat="server" ImageUrl="../Content/images/icon_costs_selected.png" data-placement="bottom" title="Costs" Width="40" OnClick="img_cost_Click" /></li>
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
                        </h6>
                    </div>
                    <div class="card-body">
                        <div class="w-100 h-100 position-relative">
                            <div class="row h-100">
                                <div class="col-md-12">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12  py-2 mb-2 overflow-hidden">
                                            <div class="table-responsive overflow-auto bg-white border-rad" style="height: calc(100vh - 470px);">
                                                <asp:GridView runat="server" ID="gv_group_cost" DataKeyNames="cost_id" OnRowCommand="gv_group_cost_RowCommand"
                                                    CssClass="table table-borderless table-sm bg-white" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;"
                                                    OnPageIndexChanging="gv_group_cost_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cost Date" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__Date" runat="server" Text='<%# Bind("cost_date") %>' CommandName="cost" CommandArgument='<%#Eval("cost_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost Description" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__cost_descr" runat="server" Text='<%# Bind("cost_descr") %>' CommandName="cost" CommandArgument='<%#Eval("cost_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost Amount" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk_cost_amt" runat="server" Text='<%# Bind("cost_amt","{0:N2}") %>' CommandName="cost" CommandArgument='<%#Eval("cost_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="	Cost Category" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__cost_cate" runat="server" Text='<%# Bind("category_descr") %>' CommandName="cost" CommandArgument='<%#Eval("cost_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Type" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk__pay_type" runat="server" Text='<%# Bind("pmt_type_descr") %>' CommandName="cost" CommandArgument='<%#Eval("cost_id")%>'></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_cost_descr" runat="server" class="form-control form-control-sm" placeholder="Cost Description" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_cost" ControlToValidate="input_cost_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_category" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_cost" ControlToValidate="select_category" ErrorMessage="*" InitialValue="Cost Category" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="form-group mb-2">
                                                <asp:TextBox ID="input_cost_amt" runat="server" class="form-control form-control-sm number" placeholder="Amount" MaxLength="10" data-v-min="-999999999.0" data-v-max="999999999.0" data-m-dec="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_cost" ControlToValidate="input_cost_amt" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>

                                        <div class="col">
                                            <div class="form-group mb-2">
                                                <asp:DropDownList ID="select_pmt_per" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_cost" ControlToValidate="select_pmt_per" ErrorMessage="*" InitialValue="Per" Class="validation-class"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="w-100"></div>
                                        <div class="w-100 form-row d-flex align-items-end position-relative mt-2">
                                            <div class="col-md-4">
                                                <div class="table-responsive overflow-auto bg-light-blue h-75 mb-2">
                                                    <asp:GridView runat="server" ID="gv_cost_summary"
                                                        CssClass="table table-borderless table-sm bg-light-blue" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="false" Style="border: 0px;">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="page" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_reminder_descr" runat="server" Text='<%# Bind("Name") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="page" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk__page_nm" runat="server" Text='<%# Bind("Amount") %>' Style="float: right; padding-right: 3%;"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <%-- <div class="w-100 bg-light-blue border-rad">
                                                    <asp:ListBox ID="div_cost_summary" runat="server" class="list-unstyled mb-0 p-2 bg-light-blue border-rad" Style="width: 100%;"></asp:ListBox>
                                                </div>--%>
                                            </div>

                                            <div class="col-md-8 mt-3 text-right">
                                                <a href="javascript:void(0);" class="d-inline-block float-left" onclick="javascript:shwMod();" title="Info">
                                                    <img src="../Content/images/icon_info.png" alt="" width="30">
                                                    <asp:Label ID="lbl_mod_details" runat="server" Style="display: none; font-size: x-small;" class="text-active mr-5"></asp:Label>
                                                </a>
                                                <asp:ImageButton ID="img_del" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="delCost" title="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="30" OnClick="newCost" title="Add" />
                                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" OnClick="setCost" ValidationGroup="validation_cost" title="Save" />

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
    <script src="../Scripts/autoNumeric-1.9.21.js"></script>
    <script type="text/javascript">
        $(".number").autoNumeric();
    </script>
</asp:Content>
