<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rev_lookup.aspx.cs" Inherits="SchoolTours.Sales.lookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
       <div id="div_All_panel" runat="server">
         
    <div style="height: calc(100vh - 185px);" class="py-4">
     <asp:HiddenField ID="entity_person_id" runat="server" />
            <div class="card mb-0 bg-primary p-3 h-100">
                <div class="d-flex flex-column justify-content-center align-items-center col-md-4 m-auto align-self-stretch h-100">
                    <div class="w-100">
                        <div class="form-group mb-5">
                            <asp:TextBox ID="input_lookup_value" runat="server" MaxLength="100" class="form-control" placeholder="Lookup Value"></asp:TextBox>

                        </div>
                    </div>
                    <div class="w-100 text-center mb-5">
                        <a href="javascript:void(0);">
                            <asp:ImageButton ID="img_cell_phone" runat="server" ImageUrl="../Content/images/icon_cellphone.png" alt="" OnClick="srchPhone" ToolTip="Cell Phone" />

                        </a>
                        <a href="javascript:void(0);" class="ml-5">
                            <asp:ImageButton ID="img_mail" runat="server" ImageUrl="../Content/images/icon_mail.png" alt="" OnClick="srchEMail"  ToolTip="Mail"/>

                        </a>
                    </div>
                    <div class="card w-100 bg-light-blue p-3 text-white">
                        <asp:ListBox ID="div_list" runat="server" class="list-unstyled custom-list" OnSelectedIndexChanged="div_entity_list_SelectedIndexChanged" Style="background-color: #007aab; color: #ffffff; border: 0px;" AutoPostBack="true"></asp:ListBox>
                        <div id="tooltip_container"></div>
                    </div>
                </div>
            </div>
        </div>

      
    </div>

  <div id="div_entity_panel" runat="server" class="col-lg-4 col-md-4 my-4 mx-auto">
      
            <div class="card bg-primary h-100 mb-0 overflow-hidden">
                     <div class="card-header bg-blue d-flex justify-content-between align-item-center">
                     <h5 class="my-0 text-white">
                                 <img src="../Content/images/icon_person.png" alt="" width="35">
                                <asp:Label ID="input_person_greeting" runat="server" Text=""></asp:Label>


                     </h5>                         
                                  <asp:ImageButton ID="img_entity_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" alt="" Width="30" OnClick="div_Hide_Entity" />
                           
                                                      
                                
                    </div>
                     <div class="card-body">
                    <div class="w-100 w-100 h-100 position-relative mb-3" id="entity_list" runat="server">
                      
                        <address class="text-active mb-5">
                                                     
                             <h5 class="text-active">  
                                <asp:Label ID="input_person_address" runat="server" Text=""></asp:Label>
                                 
                             </h5>
                               
                             <h5 class="text-active">                              
                            
                                <asp:Label ID="input_person_zip" runat="server" Text=""></asp:Label>
                                 
                               </h5>
                          
                              <div class="d-flex justify-content-start align-item-center" >
                                   <h6 class="text-active">
                                   <asp:ImageButton ID="img_person_landline" runat="server" src="../Content/images/icon_landline.png" alt="" width="25"/>
                                   <asp:Label ID="input_person_landline" runat="server" Text=""></asp:Label>
                                   </h6> 
                                
                                    <h6 class="text-active ml-3">
                                         <asp:ImageButton ID="img_person_cell_phone" runat="server"  src="../Content/images/icon_cellphone.png" alt="" width="25"/>
                                         <asp:Label ID="input_person_cell_phone" runat="server" Text=""></asp:Label>

                                    </h6>
                               </div>
                                      
                               
                            <h6 class="text-active">
                                <asp:ImageButton ID="img_person_eMail" runat="server" src="../Content/images/icon_mail.png" alt="" width="25" OnClick="img_person_eMail_Click"/>                               
                                <asp:Label ID="input_person_eMail" runat="server" Text=""></asp:Label>
                            </h6>
                        </address>

                    <%--</div>
                     <div class="w-100 h-100 position-relative" id="div_notes" runat="server">--%>
                             <h5 class="text-active d-flex justify-content-between align-items-center">                              
                                <img src="../Content/images/icon_note.png" alt="" width="25">
                            </h5>
                            <asp:ListBox ID="div_notes_list" runat="server" class="list-unstyled custom-list-hotlist"  Style="background-color: #00a9ee; border: 0px; width: 100%;"></asp:ListBox>
                                                     
                              
                           </div>
                             <div class="text-active px-2 py-4 bg-secondary border-0 border-rad">
                                    <div class="form-group mb-1">
                                         <asp:Label ID="div_tag_list" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                </div>
                     
            </div>
           
        </div>
     
</asp:Content>
