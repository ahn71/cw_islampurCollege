<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddPresidentSpeech.aspx.cs" Inherits="DS.UI.Administration.DSWS.AddPresidentSpeech" %>
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
         .rbl label {
             margin-left:7px;
             margin-right:7px;
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
               <%--<li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>    --%>       
                <li>
                     <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>                
                <li class="active">Add President & Principal Speech</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
           
                       
                                 
    <div class="">          
        <div class="row">
            <div class="col-md-6">
                <div class="dataTables_filter_New" style="float: right;margin-right:-227px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
            </div>
                <h4 class="text-right">Speech List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>        
        <div class="row">
            <%--<div class="col-md-2"></div>--%>
            <div class="col-md-7">
                <div class="tgPanel">
                           <asp:GridView ID="gvSpeechList" CssClass="table table-bordered padingtable" DataKeyNames="SPId,ImgPath" AutoGenerateColumns="False" runat="server" AllowPaging="true"  PageSize="5" OnPageIndexChanging="gvSpeechList_PageIndexChanging" OnRowCommand="gvSpeechList_RowCommand" OnRowDeleting="gvSpeechList_RowDeleting">
                                <PagerStyle CssClass="gridview" />
                               <Columns>
                                   <asp:BoundField HeaderText="SL"  DataField="Sl"/>
                                   <asp:BoundField HeaderText="Name"  DataField="PresidentName"/>
                                   <asp:BoundField HeaderText="Speech" DataField="Speech" />
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
                <div class="tgPanelHead">Add Speech</div>               
                <div class="tgPanel"> 
                     <div class="row tbl-controlPanel">
                        <div class="col-sm-10 col-sm-offset-1">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:RadioButtonList CssClass="rbl radiobuttonlist" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" ID="rblType" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                                        <asp:ListItem class="radio-inline" Value="0" Selected="true">President</asp:ListItem>
                                         <asp:ListItem class="radio-inline" Value="1">Principal</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                                     <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Name</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtPresidentName" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                </div>
                              </div>
                               <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Speech</label>
                                  <div class="col-sm-8">
                                      <asp:TextBox ID="txtSpeech" runat="server" ClientIDMode="Static" class="input controlLength form-control" TextMode="MultiLine"></asp:TextBox>
                                  </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click"/>
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
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
                 searchTable($(this).val(), 'MainContent_gvSpeechList', '');
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
             if (validateText('txtPresidentName', 1, 50, 'Enter President Name') == false) return false;
             if (validateText('txtSpeech', 1,1000, 'Enter President Speech') == false) return false;
             return true;
         }
        
         function clearIt() {
             $('#txtPresidentName').val('');
             $('#txtSpeech').val('');  
             $("#btnSubmit").val('Save');   
             $("#<%=imgProfile.ClientID%>").attr('src', '');
             setFocus('txtPresidentName');
         }        
    </script>
</asp:Content>
