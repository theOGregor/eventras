<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SchoolTours.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <style>
        .custom-popup {
            position: fixed;
            background: rgba(34, 45, 50, 0.78);
            top: 0;
            left: 0;
            width: 100%;
            z-index: 99999;
            height: 100%;
            /*display: flex;*/
            justify-content: center;
            align-items: center;
        }
    </style>
  
    <div id="div_all_panel" runat="server">

        <section class="main overflow-auto" style="height: calc(100vh - 200px);">
            <div class="row mx-0 align-items-stretch h-100">
                <div class="col-md-6 mb-4">
                    <div class="card bg-primary h-100 mb-0">
                        <div class="card-body">
                            <asp:LinkButton runat="server" ID="lnk_c_invoice" class="d-flex align-items-center h-100" OnClick="lnk_c_invoice_Click" ToolTip="Current Invoices">
                                <%--<a href="javascript:void(0);" data-toggle="modal" data-target="#div_current_inv_summary" class="d-flex align-items-center h-100">--%>
                                <div class="d-flex flex-row">
                                    <div class="round align-self-center round-success">
                                        <img src="Content/images/icon_inv.png" alt="" width="60">
                                    </div>
                                    <div class="ml-3 align-self-center">
                                        <h5 class="text-active font-weight-bold text-uppercase">Current Invoices</h5>
                                        <p class="text-active m-b-0">
                                            You have
                                        <asp:Label ID="lbl_current_inv" runat="server"></asp:Label>
                                            invoices to send
                                        </p>
                                    </div>
                                </div>
                            </asp:LinkButton>
                            <%--</a>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 mb-4">
                    <div class="card bg-primary h-100 mb-0">
                        <div class="card-body">
                            <%--<a href="javascript:void(0);" data-toggle="modal" data-target="#div_hotlist_summary" class="d-flex align-items-center h-100">--%>
                            <asp:LinkButton runat="server" ID="lnk_Hotlist" class="d-flex align-items-center h-100" OnClick="lnk_Hotlist_Click" ToolTip="Hotlist">
                                <div class="d-flex flex-row">
                                    <div class="round align-self-center round-success">
                                        <img src="Content/images/icon_person.png" alt="" width="60">
                                    </div>
                                    <div class="ml-3 align-self-center">
                                        <h5 class="text-active font-weight-bold text-uppercase">Hotlist</h5>
                                        <p class="text-active m-b-0">
                                            You have
                                        <asp:Label ID="lbl_hotlist" runat="server"></asp:Label>
                                            people to contact
                                        </p>
                                    </div>
                                </div>
                            </asp:LinkButton>
                            <%--</a>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 mb-4">
                    <div class="card bg-primary h-100 mb-0">
                        <div class="card-body">
                            <%--<a href="javascript:void(0);" data-toggle="modal" data-target="#div_past_inv_summary" class="d-flex align-items-center h-100">--%>
                            <asp:LinkButton runat="server" ID="lnk_PastDue_inv" class="d-flex align-items-center h-100" OnClick="lnk_PastDue_inv_Click" ToolTip="Past Due Invoices">
                                <div class="d-flex flex-row">
                                    <div class="round align-self-center round-success">
                                        <img src="Content/images/icon_past_inv.png" alt="" width="60">
                                    </div>
                                    <div class="ml-3 align-self-center">
                                        <h5 class="text-active font-weight-bold text-uppercase">Past Due Invoices</h5>
                                        <p class="text-active m-b-0">
                                            You have
                                        <asp:Label ID="lbl_past_inv" runat="server"></asp:Label>
                                            invoices past due
                                        </p>
                                    </div>
                                </div>
                            </asp:LinkButton>
                            <%--</a>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 mb-4">
                    <div class="card bg-primary h-100 mb-0">
                        <div class="card-body">
                            <%-- <asp:LinkButton ID="lnk_contract" runat="server" OnClick="shwPanel_contract">--%>

                            <asp:LinkButton runat="server" ID="lnk_Contract_Thresholds" class="d-flex align-items-center h-100" OnClick="lnk_Contract_Thresholds_Click" ToolTip="DEADLINES">
                                <%--<a href="javascript:void(0);" data-toggle="modal" data-target="#div_contract_summary" class="d-flex align-items-center h-100">--%>
                                <div class="d-flex flex-row">
                                    <div class="round align-self-center round-success">
                                        <img src="Content/images/icon_threshold.png" alt="" width="60">
                                    </div>
                                    <div class="ml-3 align-self-center">
                                        <h5 class="text-active font-weight-bold text-uppercase">DEADLINES</h5>
                                        <p class="text-active m-b-0">
                                            You
                                            <asp:Label ID="lbl_contract" runat="server"></asp:Label>
                                            Deadlines
                                        </p>
                                    </div>
                                </div>
                            </asp:LinkButton>
                            <%--</a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <%--<div class="modal fade" id="div_current_inv_summary" tabindex="-1" role="dialog" aria-labelledby="div_current_inv_summary" style="display: none;" aria-hidden="true">--%>
        <div class="custom-popup" id="div_current_inv_summary" runat="server" visible="false" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg mw-98" role="document">
                <div class="modal-content bg-primary">
                    <div class="modal-body inv-ht">
                        <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img src="Content/images/icon_cancel.png" alt="" width="30px">
                        </button>--%>
                        <asp:ImageButton ID="btn_close_1" class="close" runat="server" ImageUrl="Content/images/icon_cancel.png" alt="" Width="30px" OnClick="btn_close_Click" ToolTip="Cancel" />
                        <div class="row">
                            <div class="col-auto">
                                <img src="Content/images/icon_inv.png" alt="" width="60">
                            </div>
                            <div class="col">
                                <h5 class="mb-4 col modal-title text-uppercase font-weight-bold text-active">Current Invoices
                                </h5>
                                <div class="table-responsive table table-borderless">
                                    <asp:GridView runat="server" ID="gv_curr_inv" DataKeyNames="inv_id"
                                        CssClass="table table-bordered" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="False" Style="border: 0px;" OnRowCommand="gv_curr_inv_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_landline" runat="server" ImageUrl="~/Content/images/icon_mail.png" Width="30" CommandName="sndItem" CommandArgument='<%#Eval("inv_id") + ";" +Eval("ind_ind")%>' ToolTip="Mail" />
                                                    <asp:ImageButton ID="img_mail" runat="server" ImageUrl="~/Content/images/icon_print.png" Width="30" ToolTip="Print" CommandName="prnItem" CommandArgument='<%#Eval("inv_id") + ";" +Eval("ind_ind")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<%# Eval("inv_id") %>/////<%#Eval("IdTemplate") + ";" +Eval("EntityId")%>--%>

                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_entity_nm" runat="server" Text='<%# Bind("entity_nm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_due_dt" runat="server" Text=' <%# Bind("inv_amt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_contract_descr" runat="server" Text='<%# Bind("inv_dt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ind_ind" runat="server" Text='<%# Bind("ind_ind") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <%--<div class="modal fade" id="div_past_inv_summary" tabindex="-1" role="dialog" aria-labelledby="div_past_inv_summary" aria-hidden="true">--%>
        <div class="custom-popup" id="div_past_inv_summary" runat="server" visible="false" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg mw-98" role="document">
                <div class="modal-content bg-primary">
                    <div class="modal-body inv-ht">
                        <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img src="Content/images/icon_cancel.png" alt="" width="30px">
                        </button>--%>
                        <asp:ImageButton ID="ImageButton1" class="close" runat="server" ImageUrl="Content/images/icon_cancel.png" alt="" Width="30px" OnClick="btn_close_Click" ToolTip="Cancel" />
                        <div class="row">
                            <div class="col-auto">
                                <img src="Content/images/icon_past_inv.png" alt="" width="60">
                            </div>
                            <div class="col-auto">
                                <h5 class="mb-4 col modal-title text-uppercase font-weight-bold text-active">Past Due Invoices
                                </h5>
                                <div class="table-responsive table table-borderless">
                                    <asp:GridView runat="server" ID="gv_past_inv" DataKeyNames="x_id"
                                        CssClass="table table-bordered" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="False" Style="border: 0px;" OnRowCommand="gv_past_inv_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_landline" runat="server" ImageUrl="~/Content/images/icon_mail.png" Width="30" CommandName="sndItem" CommandArgument='<%#Eval("x_id") + ";" +Eval("ind_ind")%>' ToolTip="Mail" />
                                                    <asp:ImageButton ID="img_mail" runat="server" ImageUrl="~/Content/images/icon_print.png" Width="30" ToolTip="Print" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_entity_nm" runat="server" Text='<%# Bind("x_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_due_dt" runat="server" Text=' <%# Bind("amt_due") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_contract_descr" runat="server" Text='<%# Bind("due_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- <asp:GridView runat="server" ID="gv_past_inv" DataKeyNames="inv_id"
                                    CssClass="table table-bordered" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="False" Style="border: 0px;">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" SortExpression="">
                                            <ItemTemplate>
                                                <asp:Image ID="img_landline" runat="server" ImageUrl="~/Content/images/icon_mail.png" Width="30" />
                                                <asp:Image ID="img_mail" runat="server" ImageUrl="~/Content/images/icon_print.png" Width="30" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" SortExpression="">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_entity_nm" runat="server" Text='<%# Bind("entity_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" SortExpression="">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_due_dt" runat="server" Text=' <%# Bind("inv_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" SortExpression="">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_contract_descr" runat="server" Text='<%# Bind("inv_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%--<div class="modal fade" id="div_hotlist_summary" tabindex="-1" role="dialog" aria-labelledby="div_hotlist_summary" aria-hidden="true">--%>
        <div class="custom-popup" id="div_hotlist_summary" runat="server" visible="false" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg mw-98" role="document">
                <div class="modal-content bg-primary">

                    <div class="modal-body inv-ht">
                        <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img src="Content/images/icon_cancel.png" alt="" width="30px">
                        </button>--%>
                        <asp:ImageButton ID="ImageButton2" class="close" runat="server" ImageUrl="Content/images/icon_cancel.png" alt="" Width="30px" OnClick="btn_close_Click" ToolTip="Cancel" />
                        <div class="row">
                            <div class="col-auto">
                                <img src="Content/images/icon_person.png" alt="" width="60">
                            </div>
                            <div class="col-auto">
                                <h5 class="mb-4 col modal-title text-uppercase font-weight-bold text-active">Hotlist for
                                <asp:Label ID="lblHotlistDate" runat="server" Text="11/20/2019"></asp:Label>
                                </h5>
                                <div class="table-responsive table table-borderless">
                                    <asp:GridView runat="server" ID="gv_hotlist" DataKeyNames="hotlist_id"
                                        CssClass="table table-bordered" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="False" Style="border: 0px;" OnRowCommand="gv_hotlist_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_landline" runat="server" ImageUrl="~/Content/images/icon_landline.png" Width="30" Visible='<%# Eval("method").ToString() == "Call Office" ? true : false %>' ToolTip="landline" />
                                                    <asp:ImageButton ID="img_mail" runat="server" ImageUrl="~/Content/images/icon_mail.png" Width="30" Visible='<%# Eval("method").ToString() == "eMail" ? true : false %>' CommandName="sndItem" CommandArgument='<%#Eval("person_id")%>' ToolTip="Mail" />
                                                    <asp:ImageButton ID="img_cellphone" runat="server" ImageUrl="~/Content/images/icon_cellphone.png" Width="30" Visible='<%# Eval("method").ToString() == "Call Cell" ? true : false %>' ToolTip="cellphone" />
                                                    <asp:ImageButton ID="img_delete" runat="server" ImageUrl="~/Content/images/icon_delete.png" Width="30" CommandName="cancelHotlist" CommandArgument='<%#Eval("hotlist_id")%>' ToolTip="delete" />
                                                    <%--<asp:ImageButton ID="img_save" runat="server" ImageUrl="~/Content/images/icon_save.png" Width="30" />--%>
                                                    <asp:ImageButton ID="img_reschedule" runat="server" ImageUrl="~/Content/images/icon_reschedule.png" Width="30" CommandName="hotlist_id" CommandArgument='<%#Eval("hotlist_id")+";" +Eval("method_id")%>' ToolTip="reschedule Hotlist" />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <%-- <asp:Label ID="lbl_person_nm" runat="server" Text='<%# Bind("person_nm")%>' CommandName="sndpersonId" CommandArgument='<%#Eval("hotlist_id") + ";" +Eval("entity_person_id")%>'></asp:Label>--%>
                                                    <asp:LinkButton ID="lbl_person_nm" runat="server" Text='<%# Bind("person_nm")%>' CommandName="sndpersonId" CommandArgument='<%#Eval("person_id")%>'></asp:LinkButton>

                                                    /
                                                    <asp:LinkButton ID="lbl_entity_nm" runat="server" Text='<%# Bind("entity_nm") %>' CommandName="sndpersonId" CommandArgument='<%#Eval("person_id")%>'></asp:LinkButton>
                                                    
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>  

                                                     <asp:LinkButton ID="lbl_notes_txt" runat="server" Text='<%# Bind("notes_txt") %>' CommandName="sndpersonId" CommandArgument='<%#Eval("person_id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%--<div class="modal fade" id="div_contract_summary" tabindex="-1" role="dialog" aria-labelledby="div_contract_summary" aria-hidden="true">--%>
        <div class="custom-popup" id="div_contract_summary" runat="server" visible="false" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg mw-98" role="document">
                <div class="modal-content bg-primary">
                    <div class="modal-body inv-ht">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img src="Content/images/icon_cancel.png" alt="" width="30px">
                        </button>--%>
                        <asp:ImageButton ID="ImageButton3" class="close" runat="server" ImageUrl="Content/images/icon_cancel.png" alt="" Width="30px" OnClick="btn_close_Click" ToolTip="Cancel" />
                        <div class="row">
                            <div class="col-auto">
                                <img src="Content/images/icon_threshold.png" alt="" width="60">
                            </div>
                            <div class="col-auto">
                                <h5 class="col mb-4 modal-title text-uppercase font-weight-bold text-active">DEADLINES
                                </h5>
                                <div class="table-responsive table table-borderless">
                                    <asp:GridView runat="server" ID="gv_contract" DataKeyNames="contract_id"
                                        CssClass="table table-bordered" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="False" Style="border: 0px;" OnRowCommand="gv_contract_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_landline" runat="server" ImageUrl="~/Content/images/icon_landline.png" Width="30" ToolTip="landline" />
                                                    <asp:ImageButton ID="img_mail" runat="server" ImageUrl="~/Content/images/icon_mail.png" Width="30" CommandName="sndItem" CommandArgument='<%#Eval("contract_id")%>' ToolTip="Mail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_entity_nm" runat="server" Text='<%# Bind("entity_nm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_due_dt" runat="server" Text='<%# Bind("due_dt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_contract_descr" runat="server" Text='<%# Bind("contract_descr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_phone" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="custom-popup" id="div_Re_contact_popup" runat="server" visible="false">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content bg-light-blue">
                    <div class="modal-body">
                        <h5 class="text-uppercase font-weight-bold text-active">Reschedule Call</h5>
                        <div class="d-flex align-items-center">
                            <asp:HiddenField ID="hdnhotlist_id" runat="server" />
                            <asp:TextBox ID="input_nbr" runat="server" Class="form-control w-25" type="number" min="1" Max="10" MaxLength="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_schedHotlist" ControlToValidate="input_nbr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                            <asp:DropDownList ID="select_hotlist_method" runat="server" class="custom-select custom-select-sm" Style="height: 35px !important; line-height: 25px !important;" ValidationGroup="validation_schedHotlist">
                                <%--<asp:ListItem Value="1">Hour</asp:ListItem>--%>
                                <asp:ListItem Value="24">Day</asp:ListItem>
                                <asp:ListItem Value="168">Week</asp:ListItem>
                                <asp:ListItem Value="5040 ">Month</asp:ListItem>
                            </asp:DropDownList>

                            <span class="d-flex align-items-center ml-2">
                                <asp:ImageButton ID="img_Cancel_hotlist" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="img_Cancel_hotlist_Click" ToolTip="Cancel" />
                                <asp:ImageButton ID="img_delete_hotlist" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="30" OnClick="img_delete_hotlist_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" ToolTip="Delete" />
                                <%--<asp:ImageButton ID="img_add_hotlist" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" class="ml-1" OnClick="schedHotlist" ValidationGroup="validation_schedHotlist" />--%>
                                <asp:ImageButton ID="img_add_hotlist" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="30" class="ml-1" OnClick="schedHotlist" ValidationGroup="validation_schedHotlist" ToolTip="Save" />
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="custom-popup" id="div_entity_panel" runat="server" visible="false">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content bg-light-blue  h-100 mb-0 overflow-hidden">
                    <div class="card-header bg-blue d-flex justify-content-between align-item-center">
                        <h5 class="my-0 text-white">
                            <img src="../Content/images/icon_person.png" alt="" width="35">
                            <asp:Label ID="input_person_greeting" runat="server" Text=""></asp:Label>
                        </h5>
                        <asp:ImageButton ID="img_entity_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_Entity" />
                    </div>
                    <div class="modal-body">

                        <div class="w-100 w-100 h-100 position-relative mb-3" id="entity_list" runat="server">

                            <%--<address class="text-active mb-5">--%>

                            <h5 class="text-active">
                                <asp:Label ID="input_person_address" runat="server" Text=""></asp:Label>
                            </h5>

                            <h5 class="text-active">
                                <asp:Label ID="input_person_zip" runat="server" Text=""></asp:Label>
                            </h5>

                            <div class="d-flex justify-content-start align-item-center">
                                <h6 class="text-active">
                                    <asp:ImageButton ID="img_person_landline" runat="server" src="../Content/images/icon_landline.png" alt="" Width="25" ToolTip="Landline" />
                                    <asp:Label ID="input_person_landline" runat="server" Text=""></asp:Label>
                                </h6>

                                <h6 class="text-active ml-3">
                                    <asp:ImageButton ID="img_person_cell_phone" runat="server" src="../Content/images/icon_cellphone.png" alt="" Width="25" ToolTip="Cell Phone" />
                                    <asp:Label ID="input_person_cell_phone" runat="server" Text=""></asp:Label>
                                </h6>
                            </div>


                            <h6 class="text-active">
                                <asp:ImageButton ID="img_person_eMail" runat="server" src="../Content/images/icon_mail.png" alt="" Width="25" ToolTip="Mail" OnClick="img_person_eMail_Click" />
                                <asp:Label ID="input_person_eMail" runat="server" Text=""></asp:Label>
                            </h6>
                            <%-- </address>--%>

                            <h5 class="text-active d-flex justify-content-between align-items-center">
                                <img src="../Content/images/icon_note.png" alt="" width="25" title="Notes">
                            </h5>
                            <asp:ListBox ID="div_notes_list" runat="server" class="list-unstyled custom-list-hotlist" Style="background-color: #00a9ee; border: 0px; width: 100%;"></asp:ListBox>
                            <div class="text-active px-2 py-4 bg-secondary border-0 border-rad">
                                <div class="form-group mb-1">
                                    <asp:Label ID="div_tag_list" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
    <script type="text/javascript">
        
    </script>
</asp:Content>
