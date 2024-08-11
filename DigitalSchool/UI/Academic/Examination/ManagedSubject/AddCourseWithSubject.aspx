<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddCourseWithSubject.aspx.cs" Inherits="DS.UI.Academic.Examination.ManagedSubject.AddCourseWithSubject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;            
        } 
        .NoneBorder{
            border: none;
        }        
        .dataTables_length, .dataTables_filter {
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
                <li class="active">Add Course With Subject</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float: left;">Add Course With Subject List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here Subject/Course" />
                    </label>
                </div>                
            </div>
            <div class="col-md-5"></div>
        </div>
         <div class="row">
            
                <div class="col-md-6">
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
                         <asp:HiddenField ID="lblCourseId" ClientIDMode="Static" runat="server" />
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Course With Subject</div>
                             
                           <table class="tbl-controlPanel">                                
                                <tr>
                                   <td>Select Subject</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubjectName" runat="server"  ClientIDMode="Static" CssClass="input controlLength form-control" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Course Name   </td>
                                 
                                    <td>
                                        <asp:TextBox ID="txtCourseName" runat="server" ClientIDMode="Static" Style="margin-right: 5px" CssClass="input form-control"></asp:TextBox>

                                    </td>
                                </tr>
                               <tr>
                                   <td>Ordering </td>
                                   <td>
                                      <asp:TextBox ID="txtOrder" runat="server"  ClientIDMode="Static" CssClass="input form-control" Style="margin-left: 0px"></asp:TextBox>
                                        
                                   </td>
                               </tr>
                               <tr>
                                   <td> </td>
                                   <td>                                      
                                        <asp:CheckBox ID="chkIsActive" ClientIDMode="Static" runat="server" Text="Is Active" />
                                   </td>
                               </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Reset" onclick="clearIt();" />
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
            $("#ddlSubjectName").select2();
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'gvClassSubject', '');
            });
            $('#gvClassSubject').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#gvClassSubject').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtCourseName', 1, 60, 'Enter Course Name') == false) return false;
            if (validateText('txtOrder', 1, 10, 'Enter Order no') == false) return false;
            return true;
        }

        function editSubject(Id) {
           
            $('#lblCourseId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
           // $('#txtCourseName').val(strAT);
            var strP = $('#r_' + Id + ' td:nth-child(2)').html();
            $('#txtCourseName').val(strP);
            var strP = $('#r_' + Id + ' td:nth-child(3)').html();
            $('#txtOrder').val(strP);
            var strO = $('#r_' + Id + ' td:nth-child(4)').html();
            
            if (strO == "Yes") {              
                $('#chkIsActive').removeProp('checked');
                $('#chkIsActive').click();

            }
            else $('#chkIsActive').removeProp('checked');

            var serverURL = window.location.protocol + "//" + window.location.host + "/";
            jx.load(serverURL + 'UI/Academic/Examination/ForUpdate.aspx?tbldata=CourseSubInfo,' + Id + '&val=' + Id + '&do=GetSubjectStatus', function (data) {

                var getStatus = data.split('_');
                $('#ddlSubjectName').val(getStatus[0]);
               // alert(getStatus);
                load();
                

            });

            $("#btnSave").val('Update');

        }

        function clearIt() {
            $('#lblCourseId').val('');
            $('#txtCourseName').val('');
            $('#txtOrder').val('');
            setFocus('txtCourseName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
          //  clearIt();
        }
        function SaveSuccess() {
            showMessage('Save Successfully', 'success');
           // clearIt();
        }
        function load() {
            $("#ddlSubjectName").select2();
            loaddatatable();
        }

    </script>
</asp:Content>
