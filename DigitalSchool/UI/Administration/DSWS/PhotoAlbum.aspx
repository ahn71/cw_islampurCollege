<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="PhotoAlbum.aspx.cs" Inherits="DS.UI.Administration.DSWS.PhotoAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }

        .table-bordered tr th {
            background-color: #23282C;
            color: white;
        }

        .padingtable {
            margin-top: 0px;
        }
         .padingtable  th:nth-child(5), th:nth-child(6), th:nth-child(7), td:nth-child(5), td:nth-child(6), td:nth-child(7){
               text-align:center;  
               width:50px;            
            }  
          .itemcss {
          width:50px;
          text-align:center;
        }
        .chkbox label {
            margin-left: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
               <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>
                <li class="active">Album</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>



    <div class="">
        <div class="row">
            <div class="col-md-6">
                 <div class="dataTables_filter_New" style="float: right;margin-right:-223px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
            </div>
                <h4 class="text-right">Album List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row" style="border-bottom:2px solid black;">
            <%--<div class="col-md-2"></div>--%>
            <div class="col-md-8">
                <div class="tgPanel">
                <asp:GridView ID="gvAlbumInfo" CssClass="table table-bordered padingtable" DataKeyNames="PASL,IsActive" AutoGenerateColumns="False" runat="server" AllowPaging="true" PageSize="5" OnRowCommand="gvAlbumInfo_RowCommand" OnRowDeleting="gvAlbumInfo_RowDeleting" OnPageIndexChanging="gvAlbumInfo_PageIndexChanging">
                    <PagerStyle CssClass="gridview" />
                    <Columns>
                        <asp:BoundField HeaderText="SL" DataField="slNo" />
                        <asp:BoundField HeaderText="Album Name" DataField="AlbumName" />
                        <asp:BoundField HeaderText="Notes" DataField="Notes" />
                        <asp:TemplateField HeaderText="Active" ItemStyle-CssClass="itemcss">                                      
                                     <ItemTemplate  >
                                        <asp:CheckBox ID="chkIsActive"  Enabled="false"  runat="server" Checked='<%#bool.Parse(Eval("IsActive").ToString())%>' />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                        <asp:ButtonField CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btn btn-success" />
                        <%-- <asp:TemplateField HeaderText="Edit">                                    
                                     <ItemTemplate >
                                         <asp:Button ID="btnEdit" runat="server"  Text="Edit" CommandName="Edit" CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
            <div class="col-md-4">
                <div class="tgPanelHead">Album Info</div>
                <div class="tgPanel">
                       <div class="row tbl-controlPanel">
                            <div class="col-sm-10 col-sm-offset-1">
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Album Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtAlbumName" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Notics</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtNotes" runat="server" ClientIDMode="Static" class="input controlLength form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4"></label>
                                    <div class="col-sm-8">
                                        <asp:CheckBox ID="chkIsActive" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Is Active" />
                                    </div>
                                  </div>
                                   
                                  <div class="row tbl-controlPanel">
                                    <div class="col-sm-offset-4 col-sm-8">
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                     &nbsp;<asp:Button runat="server" Text="Clear" ID="btnClear" ClientIDMode="Static" OnClick="btnClear_Click" CssClass="btn btn-default"/>
                                    </div>
                                  </div>
                            </div>
                        </div>
                   
                </div>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right">Album Details List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <%--<div class="col-md-2"></div>--%>
            <div class="col-md-8">
                <div class="tgPanel">
                <asp:GridView ID="gvAlbumDetails" CssClass="table table-bordered padingtable" DataKeyNames="SL,imgLocation,PASL" AutoGenerateColumns="False" runat="server" AllowPaging="true" PageSize="5" OnRowCommand="gvAlbumDetails_RowCommand" OnRowDeleting="gvAlbumDetails_RowDeleting" OnPageIndexChanging="gvAlbumDetails_PageIndexChanging">
                    <PagerStyle CssClass="gridview" />
                    <Columns>
                        <asp:BoundField HeaderText="SL" DataField="slNo" />
                        <asp:BoundField HeaderText="Album" DataField="AlbumName" />
                        <asp:BoundField HeaderText="Title" DataField="Title" />
                        <asp:BoundField HeaderText="Short Description" DataField="Description" />
                        <asp:TemplateField  HeaderText="Image">
                  <ItemTemplate>
                      <asp:Image ID="imgAlbum" Height="50px" ImageUrl='<%# Bind("imgLocation")%>' runat="server" />
                  </ItemTemplate>
              </asp:TemplateField>
                        <asp:ButtonField CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btn btn-success" />
                        <%-- <asp:TemplateField HeaderText="Edit">                                    
                                     <ItemTemplate >
                                         <asp:Button ID="btnEdit" runat="server"  Text="Edit" CommandName="Edit" CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
            <div class="col-md-4">
                <div class="tgPanelHead">Album Details
                </div>
                <div class="tgPanel">
                    
                    <div class="row" style="margin-left:10px">
                         
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-10 col-sm-offset-1">
                              <div class="row tbl-controlPanel">
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4"></label>
                                    <div class="col-sm-8">
                                       <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/dsimages/blank-frame.png" />  <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" onclick="" onchange="previewFile()" Width="122px" ClientIDMode="Static" />                                
                             
                                    </div>
                                  </div>
                                <label class="col-sm-4">Album Name</label>
                                <div class="col-sm-8" id="tdType">
                                    <asp:DropDownList runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" ID="ddlAlbumName" Font-Names="SutonnyMJ">                                                                                                              
                                        </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Title</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Short Description</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShortDes" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                </div>
                              </div>
                               
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button CssClass="btn btn-primary" ID="btnSaveAD" runat="server" Text="Save" ClientIDMode="Static"
                                     OnClick="btnSaveAD_Click"  />
                                     &nbsp;<asp:Button runat="server" Text="Clear" ID="btnClearAD" ClientIDMode="Static" CssClass="btn btn-default" OnClick="btnClearAD_Click"/>
                                </div>
                              </div>
                        </div>
                    </div>
                                    
                </div>
                   
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
      <script type="text/javascript">
          $(document).ready(function () {
              $(document).on("keyup", '.Search_New', function () {
                  searchTable($(this).val(), 'MainContent_gvAlbumInfo', '');
              });
          });
          function previewFile() {
              var preview = document.querySelector('#<%=imgProfile.ClientID %>');
             var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
             var reader = new FileReader();

             reader.onloadend = function () {
                 preview.src = reader.result;
             }

             if (file) {
                 reader.readAsDataURL(file);
             } else {
                 preview.src = "";
             }
         }
         function validateInputs() {
             if (validateText('txtAlbumName', 1, 50, 'Enter Valid Album Name') == false) return false;            
             return true;
         }
      </script>
</asp:Content>
