<%@ Page Title="Add New Subject" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="NewSubject.aspx.cs" Inherits="DS.UI.Academic.Examination.ManagedSubject.NewSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;            
        } 
        .NoneBorder{
            border: none;
        }
         input[type="checkbox"]{
            margin: 5px;
        }
        .dataTables_length, .dataTables_filter{
            display: none;
            padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSubId" ClientIDMode="Static" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Examination/ManagedSubject/SubjectManageHome.aspx">Subject Management</a></li>
                <li class="active">Add New Subject</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
         <div class="row">
             <div class="col-md-2">
              </div>
             <div class="col-md-4">
                    <h4 class="text-right"  style="float: left;">Subject List</h4>
                   <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here Subject/Course" />
                    </label>
                </div>
                 
             </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row"> 
             <div class="col-md-2">                 
                 </div>
            <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="tgPanel">
                            <div id="divSubjectList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                            </div>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </div>          
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add New Subject</div>
                             <div class="row tbl-controlPanel"> 
		                            <div class="col-xs-12 col-sm-8 col-sm-offset-2 boX">
			                            <div class="form-inline">
				                             <div class="form-group">
					                             <label for="exampleInputName2">Name</label>
						                            <asp:TextBox ID="txtSubName" runat="server"  ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
				                             </div>
				                            <div class="form-group">
					                             <label for="exampleInputName2">Order</label>
					                                <asp:TextBox ID="txtOrder" runat="server"  ClientIDMode="Static" CssClass="input form-control" Style="margin-top: 10px"></asp:TextBox>
                                                   
				                             </div> 
                                            <div class="form-group">
					                             <label for="exampleInputName2"></label>					                             
                                                    <asp:CheckBox ID="chkIsActive" ClientIDMode="Static" runat="server" Text="Is Active" />
				                             </div>
                                            
				                            <div class="form-group">
					                             <label for="exampleInputName2"></label>
					                                <div class="buttonBox" style="margin-left:22px;">
                                                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                                        &nbsp;<input type="button" class="btn btn-default" value="Reset" onclick="clearIt();" />                                
                                                    </div>  
				                             </div>
			                            </div>
	                              </div>
                             </div>
                          
                        </div>                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>            
        </div>       
       
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {            
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblClassList', '');
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {          
            if (validateText('txtSubName', 1, 60, 'Enter Subject Name') == false) return false;
            if (validateText('txtSubTotalMarks', 1, 10, 'Enter Order No') == false) return false;
            return true;
        }
        function editSubject(Id) {
            $('#lblSubId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
            $('#txtSubName').val(strAT);
           
            var strO = $('#r_' + Id + ' td:nth-child(2)').html();
            
            if (strO == "Yes") {              
                $('#chkIsActive').removeProp('checked');
                $('#chkIsActive').click();

            }
            else $('#chkIsActive').removeProp('checked');

            var strO = $('#r_' + Id + ' td:nth-child(3)').html();
            $('#txtOrder').val(strO);
            
            

            $("#btnSave").val('Update');
           
        }
        function editOptionlSubject(Id) {
            $('#lblSubId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
            $('#txtSubName').val(strAT);
            var strO = $('#r_' + Id + ' td:nth-child(1)').html();
            $('#txtOrder').val(strO);

            var serverURL = window.location.protocol + "//" + window.location.host + "/";
           
            


            
            $("#btnSave").val('Update'); 

            
        }
        function clearIt() {
            $('#lblSubId').val('');
            $('#txtSubName').val('');
            $('#txtSubCode').val('');
            $('#txtSubTotalMarks').val('');
            $('#txtOrder').val('');
            $("#chkIsOptional").removeProp('checked');
            $("#chkMandatory").removeProp('checked');
            setFocus('txtSubName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SaveSuccess() {
            loaddatatable();
            showMessage('Save Successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>