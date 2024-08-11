<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddSpesialDescription.aspx.cs" Inherits="DS.UI.Administration.DSWS.AddSpesialDescription" %>
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
                <li class="active">Add Special Description</li>
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
                <h4 class="text-right">Special Description List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <%--<div class="col-md-2"></div>--%>
            
         
                    <div class="col-md-7">
                        <div class="tgPanel">
                           <asp:GridView ID="gvNoticeList" CssClass="table table-bordered padingtable"  DataKeyNames="DSL,Image" AutoGenerateColumns="False" runat="server" AllowPaging="True" PageSize="4"  OnPageIndexChanging="gvNoticeList_PageIndexChanging" OnRowCommand="gvNoticeList_RowCommand" OnRowDeleting="gvNoticeList_RowDeleting">
                                <PagerStyle CssClass="gridview" />
                               <Columns>
                                   <asp:BoundField HeaderText="SL"  DataField="Sl"/>
                                   <asp:BoundField HeaderText="Type"  DataField="Type"/>
                                   <asp:BoundField HeaderText="Subject"  DataField="Subject"/>
                                   <asp:BoundField HeaderText="Description" DataField="Details" />
                                  <%-- <asp:BoundField HeaderText="Entry Date" DataField="NEntryDate" />--%>
                                   <%--<asp:BoundField HeaderText="Published Date" DataField="NPublishedDate" />--%>
                                    <asp:TemplateField HeaderText="Published Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPublishDate" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "PublishedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                        
                                   <asp:ButtonField  CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit"  ControlStyle-CssClass="btn btn-success" />
                                <%-- <asp:TemplateField HeaderText="Edit">                                    
                                     <ItemTemplate >
                                         <asp:Button ID="btnEdit" runat="server"  Text="Edit" CommandName="Edit" CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Delete" >                                      
                                     <ItemTemplate >
                                         <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass ="btn btn-danger"  CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');"  />
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
                                    <label class="col-sm-4">Type</label>
                                    <div class="col-sm-8" id="tdType">
                                        <asp:DropDownList runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" ID="ddlSDType" Font-Names="SutonnyMJ">
                                        <asp:ListItem Value="হোম">হোম</asp:ListItem>                      
                                        <asp:ListItem Value="পঠিত বিষয়াবলী">পঠিত বিষয়াবলী</asp:ListItem>                                           
                                         <asp:ListItem Value="শরীর চর্চা ও স্যানিটেশন">শরীর চর্চা ও স্যানিটেশন</asp:ListItem>  
                                        <asp:ListItem Value="কম্পিউটার ব্যবহার">কম্পিউটার ব্যবহার</asp:ListItem>                                           
                                         <asp:ListItem Value="প্রশংসাপত্র / টি সি">প্রশংসাপত্র / টি সি</asp:ListItem>  
                                        <asp:ListItem Value="নীতিমালা ও সার্কুলার">নীতিমালা ও সার্কুলার</asp:ListItem>                                           
                                         <asp:ListItem Value="খেলার মাঠ">খেলার মাঠ</asp:ListItem>  
                                        <asp:ListItem Value="আমাদের সফলতা">আমাদের সফলতা</asp:ListItem>                                                                                                                                                                                   
                                        </asp:DropDownList>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Subject</label>
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
                                      <label class="col-sm-4">Publish Date</label>
                                      <div class="col-sm-8">
                                            <asp:TextBox ID="txtPublishdate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MM-yyyy" TargetControlID="txtPublishdate">
                                            </asp:CalendarExtender>
                                      </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                      <label class="col-sm-4"></label>
                                      <div class="col-sm-8">
                                          <%--<img src="../../../Images/dsimages/blank-frame.png" />--%>
                                          <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/dsimages/blank-frame.png" />
                                            <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                  </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <div class="col-sm-offset-4 col-sm-8">
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
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
