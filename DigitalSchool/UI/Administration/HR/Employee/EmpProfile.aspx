<%@ Page Title="Employee/Teacher Profile" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EmpProfile.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.EmpProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table td:first-child, .table td:nth-child(3), .table td:nth-child(5){
            text-align: right;
            padding-right: 8px;            
        }
        .table td:nth-child(2), .table td:nth-child(4), .table td:nth-child(6),
         .table td:nth-child(8){
            color: #1fb5ad;           
            text-align: left;
        }
        .table{
            border-bottom: none;
        }
        .tableCss td:first-child{
            width: 18%
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpDetails.aspx">Employee Details</a></li>
                <li class="active">Employee Profile</li>                              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPrintpreview" />
                <asp:AsyncPostBackTrigger ControlID="btnwithoutImage" />
            </Triggers>
        <ContentTemplate>
        <div class="col-md-12">
            <section class="panel">
                <div class="panel-body profile-information">
                    <div class="col-md-3">
                        <div class="profile-pic text-center">
                            <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="profile-desk">
                            <h1>
                                <label id="lblName" runat="server"></label>                                
                            </h1>
                            <span class="text-muted">Employee(<label id="lblTitle" runat="server"></label>)</span>
                            <br /> 
                            <asp:Button ID="EditEmp" runat="server" CssClass="btn btn-primary" Text="Edit" 
                                OnClick="EditEmp_Click"/>
                            <asp:Button ID="btnPrintpreview" runat="server" CssClass="btn btn-primary" Text="Print with Image" 
                                OnClick="btnPrintpreview_Click" />
                            <asp:Button ID="btnwithoutImage" runat="server" CssClass="btn btn-primary" Text="Print without Image" 
                                OnClick="btnwithoutImage_Click" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <table class="table">
                            <tr>
                                <td>ID/Card No :</td>
                                <td>
                                    <label id="lblCardNo" runat="server"></label>
                                </td>
                            </tr>
                            <tr>
                                <td>Name :</td>
                                <td>
                                    <label id="lblName1" runat="server"></label>
                                </td>

                            </tr>
                            <tr>
                                <td>Employee Type :</td>
                                <td>
                                    <label id="lblEmpType" runat="server"></label>
                                </td>
                            </tr>
                            <tr runat="server" id="trTCode">
                                <td>Teacher Code Number :</td>
                                <td>
                                    <label id="lblTCodeNo" runat="server"></label>
                                </td>
                            </tr>


                        </table>
                    </div>
                </div>
            </section>
        </div>
            </ContentTemplate>
           </asp:UpdatePanel>
        <div class="col-md-12">
            <section class="panel">
                <header class="panel-heading tab-bg-dark-navy-blue">
                    <ul class="nav nav-tabs nav-justified ">
                        <li class="active">
                            <a data-toggle="tab" href="#EmpInfo">Employee Information</a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#PersonalInfo">Personal Information</a>
                        </li>                        
                        <li>
                            <a data-toggle="tab" href="#EducationalInfo">Educational Information</a>
                        </li>  
                        <li>
                            <a data-toggle="tab" href="#ExperienceInfo">Experience Information</a>
                        </li>    
                        <li>
                            <a data-toggle="tab" href="#OthersInfo">Others Information</a>
                        </li>                        
                    </ul>
                </header>
                <div class="panel-body">
                    <div class="tab-content tasi-tab">
                        <div id="EmpInfo" class="tab-pane active">
                            <div class="row">                                
                                <div class="col-md-12">
                                    <table class="table">                                     
                                        <tr>
                                            <td>Department :</td>
                                            <td>
                                                <label id="lblDepartment" runat="server"></label>
                                            </td>
                                            <td>Designation :</td>
                                            <td>
                                                <label id="lblDesignation" runat="server"></label>
                                            </td>
                                             <td>Shift :</td>
                                            <td>
                                                <label id="lblShift" runat="server"></label>
                                            </td>
                                            
                                            
                                                                                      
                                        </tr>
                                        <tr>
                                            <td>Examiner :</td>
                                            <td>
                                                <label id="lblExaminer" runat="server"></label>
                                            </td>
                                            <td>Joining Date :</td>
                                            <td>
                                                <label id="lblJoiningDate" runat="server"></label>
                                            </td> 
                                            <td>Duration in This Institute :</td>
                                            <td>
                                                <label id="lblDurationInThisInstitute" runat="server"></label>
                                            </td>

                                             
                                             
                                        </tr>
                                        <tr>
                                            <td>Job Type :</td>
                                            <td>
                                                <label id="lblJobType" runat="server"></label>
                                            </td> 
                                            <td>Last Degree :</td>
                                            <td>
                                                <label id="lblLastDegree" runat="server"></label>
                                            </td> 
                                            <td>Class Teacher of :</td>
                                            <td>
                                                <label id="lblClassTeacherOf" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>Phone :</td>
                                            <td>
                                                <label id="lblPhone" runat="server"></label>
                                            </td>
                                            <td>Mobile :</td>
                                            <td>
                                                <label id="lblMobile" runat="server"></label>
                                            </td>
                                            <td>Email :</td>
                                            <td> 
                                                <label id="lblEmail" runat="server"></label>                                               
                                            </td>                                           
                                        </tr>                                        
                                    </table>
                                </div>                                                            
                            </div>
                        </div>
                        <div id="PersonalInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table">
                                        <tr>
                                            <td>Father's Name :</td>
                                            <td>
                                                <label id="lblFatherName" runat="server"></label>
                                            </td>
                                            <td>Mother's Name :</td>
                                            <td>
                                                <label id="lblMotherName" runat="server"></label>
                                            </td>
                                            <td>Religion :</td>
                                            <td>
                                                <label id="lblReligion" runat="server"></label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>Marital Status :</td>
                                            <td>
                                                <label id="lblMaritalStatus" runat="server"></label>
                                            </td>
                                            <td>Birthday :</td>
                                            <td>
                                                <label id="lblBirthDay" runat="server"></label>
                                            </td>
                                            <td>Blood Group :</td>
                                            <td>
                                                <label id="lblBloodGroup" runat="server"></label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>Nationality :</td>
                                            <td>
                                                <label id="lblNationality" runat="server"></label>
                                            </td>
                                            <td>Gender</td>
                                            <td><label id="lblGender" runat="server"></label></td>
                                            <td></td>
                                            <td></td>                                           
                                        </tr>                                    
                                    </table>
                                     <div>Emergency Contact</div>
                                    <table class="table">
                                         <tr>
                                             <td>Name :</td>
                                            <td>
                                                <label id="lblEmergencyContactName" runat="server"></label>
                                            </td>
                                            <td>Relation :</td>
                                            <td><label id="lblEmergencyContactRelation" runat="server"></label></td>
                                            <td>Mobile :</td>
                                            <td><label id="lblEmergencyContactMobile" runat="server"></label></td> 
                                        </tr>
                                    </table>
                                    <div>Present Address</div>
                                    <table class="table">
                                         <tr>
                                             <td>Address :</td>
                                            <td>
                                                <label id="lblPresentAddress" runat="server"></label>
                                            </td>
                                            <td>District :</td>
                                            <td><label id="lblPresentDistrict" runat="server"></label></td>
                                            <td>Thana/Upazila :</td>
                                            <td><label id="lblPresentThanaUpazila" runat="server"></label></td> 
                                             <td>Post Office :</td>
                                            <td><label id="lblPresentPostOffice" runat="server"></label></td> 
                                        </tr>
                                    </table>

                                    <div>Permanent Address</div>
                                    <table class="table">
                                         <tr>
                                             <td>Address :</td>
                                            <td>
                                                <label id="lblPermanentAddress" runat="server"></label>
                                            </td>
                                            <td>District :</td>
                                            <td><label id="lblPermanentDistrict" runat="server"></label></td>
                                            <td>Thana/Upazila :</td>
                                            <td><label id="lblPermanentThanaUpazila" runat="server"></label></td> 
                                             <td>Post Office :</td>
                                            <td><label id="lblPermanentPostOffice" runat="server"></label></td> 
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>                        
                        <div id="EducationalInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">                                    
                                    <%--<table class="table tableCss">
                                       <tr>
                                           <th>SL NO</th>
                                           <th>Exam Name</th>
                                           <th>Group/Depertment</th>
                                           <th>Board/University</th>
                                           <th>Passing Year</th>
                                           <th>Result</th>
                                       </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>S.S.C</td>
                                            <td>Science</td>
                                            <td>Dhaka</td>
                                            <td>2002</td>
                                            <td>4.50</td>                                            
                                        </tr>
                                    </table>--%>
                                    <asp:GridView ID="gvEducationalInfo" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EIExamName" HeaderText="Exam Name" />
                                            <asp:BoundField DataField="EIDepertment" HeaderText="Group/Depertment" />
                                            <asp:BoundField DataField="EIBoard" HeaderText="Board/University" />
                                            <asp:BoundField DataField="EIPassingYear" HeaderText="Passing Year" />
                                            <asp:BoundField DataField="EIResult" HeaderText="Result" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>    
                        <div id="ExperienceInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">                                    
                                    <%--<table class="table tableCss">
                                        <tr>
                                           <th>SL NO</th>
                                           <th>Institution Name</th>
                                           <th>Designation</th>
                                           <th>Date (From)</th>
                                           <th>Date (To)</th>
                                           <th>Duration</th>
                                       </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>Nasirabad College</td>
                                            <td>Asst. Teacher</td>
                                            <td>01-01-2011</td>
                                            <td>30-11-2014</td>
                                            <td>3 Years 10 Months 29 Days</td>                                            
                                        </tr> 
                                    </table>   --%>
                                    <asp:GridView ID="gvExperienceInfo" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ExIInstName" HeaderText="Institution Name" />
                                            <asp:BoundField DataField="ExIDesignation" HeaderText="Designation" />
                                            <asp:BoundField DataField="ExIDDateFrom" HeaderText="Date (From)" />
                                            <asp:BoundField DataField="ExIDateTO" HeaderText="Date (To)" />
                                            <asp:BoundField DataField="ExIDuration" HeaderText="Duration" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div> 
                        <div id="OthersInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">                                    
                                                    <asp:GridView ID="gvOthersInfo" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OthersInfo" HeaderText="Others Activities" />
                                           
                                        </Columns>
                                    </asp:GridView>                    
                                </div>
                            </div>
                        </div> 
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
