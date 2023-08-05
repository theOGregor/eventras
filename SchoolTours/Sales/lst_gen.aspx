<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lst_gen.aspx.cs" Inherits="SchoolTours.Sales.lst_gen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div class="card mb-0 my-2 bg-primary p-3 h-100">
				<div class="row align-items-center">
					<div class="d-flex flex-column col-md-10 h-100">
						<div class="row">
							<div class="col-md-4">
                                 <label for="" class="" style="color:black;margin-left: 10px;"><b>STATE(S)</b></label>
								<div class="card bg-secondary mb-0">
									<div class="card-body">
										<p class="text-active mb-0">
                                            <asp:ListBox ID="div_state_list" runat="server" class="form-control form-control-sm bg-secondary border-0  custom-list" OnSelectedIndexChanged="tagRemove_State" AutoPostBack="true"></asp:ListBox>
										</p>
									</div>
								</div>
								<div class="form-group position-relative">
                                    <%--placeholder="State"--%>
                                    <asp:TextBox ID="input_state" runat="server" class="form-control"  MaxLength="5"></asp:TextBox>
                                    <asp:ImageButton ID="ListGen_tag_new" runat="server" ImageUrl="../Content/images/add.png" alt=""  class="CustomAddIcon" OnClick="ListGen_tag_new_Click" />
									
								</div>
							</div>
							<div class="col-md-4">
                                <label for="" class="" style="color:black;margin-left: 10px;"><b>ORGANIZATION TAGS</b></label>
								<div class="card bg-secondary mb-0">
									<div class="card-body P-2">
										<asp:ListBox ID="entity_sel_tags" runat="server" class="form-control form-control-sm bg-secondary border-0 custom-list"  OnSelectedIndexChanged="tagRemove_entity" AutoPostBack="true"></asp:ListBox>
									</div>
								</div>
								<div class="form-group">
                                    <%--placeholder="entity_Tag"--%>
                                    <asp:TextBox ID="input_entity_tag_descr" runat="server" class="form-control"  AutoPostBack="true" OnTextChanged="input_entity_tag_descr_TextChanged"></asp:TextBox>
                                    <asp:ListBox ID="entity_tag_assign" runat="server" class="list-unstyled custom-list bg-light-blue border-0 w-100 " OnSelectedIndexChanged="tagSelect_entity" AutoPostBack="true"></asp:ListBox>
                                     <div id="tooltip_container"></div>
								</div>
							</div>
							<div class="col-md-4">
                                <label for="" class="" style="color:black;margin-left: 10px;"><b>INDIVIDUAL TAGS</b></label>
								<div class="card bg-secondary mb-0">
									<div class="card-body">
                                        <asp:ListBox ID="person_sel_tags" runat="server" class="form-control form-control-sm bg-secondary border-0 custom-list" OnSelectedIndexChanged="tagRemove_person" AutoPostBack="true"></asp:ListBox>
									</div>
								</div>
								<div class="form-group">
                                    <%--placeholder="person_Tag"--%>
                                     <asp:TextBox ID="input_preson_tag_descr" runat="server" class="form-control"  AutoPostBack="true" OnTextChanged="input_preson_tag_descr_TextChanged"></asp:TextBox>
                                    <asp:ListBox ID="person_tag_assign" runat="server" class="list-unstyled bg-light-blue border-0 custom-list w-100" OnSelectedIndexChanged="tagSelect_person" AutoPostBack="true"></asp:ListBox>
                                     <div id="tooltip_container1"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-2 text-center">
						<a href="javascript:void(0);">
                            <asp:ImageButton ID="img_entity_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" OnClick="clearFields" ToolTip="cancel"/>
						</a>
						<a href="javascript:void(0);" class="ml-4">
                             <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" OnClick="genList" ToolTip="Generate List"/>
						</a>
					</div>
				</div>
				<div class="row align-items-center">
					<div class="d-flex flex-column col-md-10 h-100">
						<div class="card mb-1 px-3 py-2 bg-secondary">
							<h6 class="mb-0 text-dark"><asp:Label ID="div_nbr_items" runat="server" Text=""></asp:Label></h6>
						</div>
						<div class="card px-3 py-2">
                            <asp:ListBox ID="div_list" runat="server" class="list-unstyled mb-0"></asp:ListBox>
						</div>
					</div>
					<div class="col-md-2 text-center">
						<a href="javascript:void(0);">
                            <asp:ImageButton ID="img_print" runat="server" ImageUrl="../Content/images/icon_print.png" alt=""  OnClick="prnList" ToolTip="Print"/>
							</a>
						<a href="javascript:void(0);" class="ml-4">
                            <asp:ImageButton ID="div_mail" runat="server" ImageUrl="../Content/images/icon_mail.png" alt="" OnClick="mailList" ToolTip="Mail"/>
						</a>
					</div>
				</div>
			</div>
</asp:Content>
