<%@ Page Title="Add page content" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddPageContent.aspx.cs" Inherits="DS.UI.Administration.DSWS.AddPageContent" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         .table-bordered tr th{
            background-color: #23282C;
            color: white;
        }
         .padingtable{
             margin-top:0px;
         }
         .padingtable th:nth-child(7),th:nth-child(6),td:nth-child(7),td:nth-child(6){
             text-align:center;   
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
                <li class="active">Add Page Content</li>
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
                <h4 class="text-right">Page Content List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <%--<div class="col-md-2"></div>--%>
            
         
                    <div class="col-md-7">
                        <div class="tgPanel">
                           <asp:GridView ID="gvNoticeList" CssClass="table table-bordered padingtable"  DataKeyNames="PageID,Image,Status" AutoGenerateColumns="False" runat="server" OnRowCommand="gvNoticeList_RowCommand">
                                <PagerStyle CssClass="gridview" />
                               <Columns>
                                   <asp:BoundField HeaderText="SL"  DataField="SL"/>
                                   <asp:BoundField HeaderText="Page"  DataField="Page"/>
                                   <asp:BoundField HeaderText="Title"  DataField="Title"/>
                                   <asp:BoundField HeaderText="Details" DataField="TextContent" />
                                   <asp:BoundField HeaderText="Status" DataField="Status" />
                            <%--      <asp:TemplateField  HeaderText="Image">
                  <ItemTemplate>
                      <asp:Image ID="imgAlbum" Height="50px" ImageUrl='<%# Bind("Image")%>' runat="server" />
                  </ItemTemplate>
              </asp:TemplateField>--%>
                                  <%-- <asp:BoundField HeaderText="Entry Date" DataField="NEntryDate" />--%>
                                   <%--<asp:BoundField HeaderText="Published Date" DataField="NPublishedDate" />--%>
                                <%--    <asp:TemplateField HeaderText="Published Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPublishDate" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "PublishedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  --%>                      
                                   <%--<asp:ButtonField  CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit"  ControlStyle-CssClass="btn btn-success" />--%>
                                 <asp:TemplateField HeaderText="Edit">                                    
                                     <ItemTemplate >
                                         <asp:Button ID="btnChange" runat="server"  Text="Edit" CommandName="Change" CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" >                                      
                                     <ItemTemplate >
                                         <asp:Button ID="btnRemove" runat="server" Text="Delete" CssClass ="btn btn-danger"  CommandName="Remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');"  />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                               </Columns>
                           </asp:GridView> 
                            </div>

                    </div>
            <div class="col-md-5">
                <div class="tgPanelHead">Add Special Description</div>   
                        
                      <div class="tgPanel"> 
                           <div class="row tbl-controlPanel">
                            <div class="col-sm-10 col-sm-offset-1">
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Page</label>
                                    <div class="col-sm-8" id="tdType">
                                        <asp:DropDownList runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" ID="ddlPageList">
                                                                                                                                                                                                                        
                                        </asp:DropDownList>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Title</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtNSubject" runat="server" ClientIDMode="Static" class="input controlLength form-control" ></asp:TextBox> 
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Details</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtNDetails" runat="server" Height="180px" ClientIDMode="Static" class="input controlLength form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                  </div>
                                  
                                  <div class="row tbl-controlPanel">
                                      <label class="col-sm-4"></label>
                                      <div class="col-sm-8">
                                          <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/dsimages/blank-frame.png" />
                                            <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                  </div>
                                  </div>
                                 <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Status
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:CheckBox runat="server" ID="ckbStatus" Checked="true" ClientIDMode="Static" />
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <div class="col-sm-offset-4 col-sm-8">
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClick="btnSubmit_Click" />
                                        &nbsp;<asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click"/>
                                        <%--<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />--%>
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
                 searchTable($(this).val(), 'MainContent_gvNoticeList', '');
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
            if (validateText('txtNSubject', 1, 50, 'Enter Valid Notice Subject') == false) return false;
            if (validateText('txtNDetails', 1, 2000, 'Enter Valid Notice Details') == false) return false;
            if (validateText('txtPublishdate', 1, 10, 'Enter Valid Publish Date') == false) return false;
             return true;
         }
    </script>
</asp:Content>
