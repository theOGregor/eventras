<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Defect.aspx.cs" Inherits="SchoolTours.Defect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="main pt-4">
        <div class="row mx-0 align-items-stretch h-100">
            <div class="col-lg-12 col-md-12">
                <div class="card bg-primary h-100 mb-0 overflow-hidden">
                    <div class="card-body">
                        <div class="w-100 h-100 position-relative">
                            <div class="row h-100">
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">
                                                <asp:ListBox ID="div_defect" runat="server" class="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="dtlDefect"
                                                    Style="height: calc(100vh - 36rem); overflow: auto;"></asp:ListBox>
                                                <%--<select multiple="" class="form-control" id=""
														style="height: calc(100vh - 36rem); overflow: auto;">
														<option>1</option>
														<option>2</option>
														<option>3</option>
														<option>4</option>
														<option>5</option>
													</select>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">
                                                <%--<input type="text" class="form-control"value="Unable to add tags to person">--%>
                                                <asp:TextBox ID="input_defect_title" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_Defect" ControlToValidate="input_defect_title" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">
                                                <%--<textarea name="" id="" rows="6"
														class="form-control">Notes Here</textarea>--%>

                                                <asp:TextBox ID="input_defect_descr" runat="server" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_Defect" ControlToValidate="input_defect_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>

                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <asp:ImageButton ID="bttn_new_defect" runat="server" ImageUrl="Content/images/icon_add.png" data-placement="bottom" title="Save" Width="40" OnClick="newDefect" />
                                            <asp:ImageButton ID="bttn_save_defect" runat="server" ImageUrl="Content/images/icon_save.png" data-placement="bottom" title="Save" Width="40" OnClick="setDefect" ValidationGroup="validation_Defect"  />


                                            <%--<a href="javascript:void(0);" class="d-inline-block"><img
														src="images/icon_add.png" alt="" width="30"></a>
												<a href="javascript:void(0);" class="d-inline-block"><img
														src="images/icon_save.png" alt="" width="30"></a>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="form-row h-100 position-relative align-content-start">
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">

                                                <asp:ListBox ID="div_memo" runat="server" class="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="dtlMemo"
                                                    Style="height: calc(100vh - 36rem); overflow: auto;"></asp:ListBox>
                                                <%--<select multiple="" class="form-control" id=""
														style="height: calc(100vh - 36rem); overflow: auto;">
														<option>1</option>
														<option>2</option>
														<option>3</option>
														<option>4</option>
														<option>5</option>
													</select>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">
                                                <asp:DropDownList ID="select_status" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="Select" ID="Req_ID" Display="Dynamic" ValidationGroup="validation_memo" runat="server" ControlToValidate="select_status" Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                                                <%--<select name="" id="" class="custom-select">
														<option value="">Select</option>
													</select>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-12 px-2 pb-2 pt-0 mb-2">
                                            <div class="form-group radius-md overflow-hidden">
                                                <%--<textarea name="" id="" rows="6"
														class="form-control">Notes Here</textarea>--%>

                                                <asp:TextBox ID="input_memo_descr" runat="server" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_memo" ControlToValidate="input_memo_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            </div>
                                        </div>

                                        <div class="col-md-12 mt-3 bot-fix text-right">
                                            <asp:ImageButton ID="bttn_del_memo" runat="server" ImageUrl="Content/images/icon_delete.png" data-placement="bottom" title="Save" Width="40" OnClick="delMemo" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Content/images/icon_add.png" data-placement="bottom" title="Save" Width="40" OnClick="newMemo" />
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="Content/images/icon_save.png" data-placement="bottom" title="Save" Width="40" OnClick="setMemo" ValidationGroup="validation_memo" />

                                            <%--<a href="javascript:void(0);" class="d-inline-block"><img
														src="images/icon_delete.png" alt="" width="30"></a>
												<a href="javascript:void(0);" class="d-inline-block"><img
														src="images/icon_add.png" alt="" width="30"></a>
												<a href="javascript:void(0);" class="d-inline-block"><img
														src="images/icon_save.png" alt="" width="30"></a>--%>
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
