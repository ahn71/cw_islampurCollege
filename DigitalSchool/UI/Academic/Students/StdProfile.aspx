<%@ Page Title="Student Profile" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdProfile.aspx.cs" Inherits="DS.UI.Academic.Students.StdProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table td:first-child, .table td:nth-child(3), .table td:nth-child(5) {
            text-align: right;
            padding-right: 8px;
        }

        .table td:nth-child(2), .table td:nth-child(4), .table td:nth-child(6),
        .table td:nth-child(8) {
            color: #1fb5ad;
            text-align: left;
        }

        .table {
            border-bottom: none;
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
                   <%--<a id="A1" runat="server" href="~/Dashboard.aspx">--%>
                   <a runat="server" id="aDashboard">
                    <i class="fa fa-dashboard"></i>
                    Dashboard
                   </a>
                 </li>
                 <%--<li><a id="A2" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>--%>
                 <li><a id="aAcademicHome" runat="server">Academic Module</a></li>
                 <%--<li><a id="A3" runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>--%>
                 <li><a id="aStudentHome" runat="server">Student Module</a></li>
                 <li><a id="aStudentList" runat="server">Students Details</a></li>
                
                <li class="active">Student Profile</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <section class="panel">
                <div class="panel-body profile-information">
                    <div class="col-md-3">
                        <div class="profile-pic text-center">
                            <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="profile-desk">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <h1>
                                        <label id="lblStudentName" runat="server"></label> | <label id="lblStudentNameBn" runat="server"></label>
                                    </h1>
                                    <span class="text-muted">Roll:<label id="lblStRoll" runat="server"></label></span>
                                    <br />
                                    <asp:Button ID="EditStd" runat="server" CssClass="btn btn-primary" Text="Edit"
                                        OnClick="EditStd_Click" />
                                    <asp:Button ID="btnPrintpreview" runat="server" CssClass="btn btn-primary" Text="Print Profile"
                                        OnClick="btnPrintpreview_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </section>
        </div>
        <div class="col-md-12">
            <section class="panel">
                <header class="panel-heading tab-bg-dark-navy-blue">
                    <ul class="nav nav-tabs nav-justified ">
                        <li class="active">
                            <a data-toggle="tab" href="#StdInfo">Student Information</a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#ParentInfo">Parents Information</a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#GuardianInfo">Guardian Information</a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#Address">Address</a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#OtherInfo">Other Information</a>
                        </li>
                    </ul>
                </header>
                <div class="panel-body">
                    <div class="tab-content tasi-tab">
                        <div id="StdInfo" class="tab-pane active">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table">
                                        <tr>
                                            <td>Admission No :</td>
                                            <td>
                                                <label id="lblAdmissionNo" runat="server"></label>
                                            </td>
                                            <td>Date of Admission :</td>
                                            <td>
                                                <label id="lblAdmissionDate" runat="server"></label>
                                            </td>
                                            <td>Year :</td>
                                            <td>
                                                <label id="lblYear" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td>Shift :</td>
                                            <td>
                                                <label id="lblShift" runat="server"></label>
                                            </td>
                                            <td>Class :</td>
                                            <td>
                                                <label id="lblAdmissionClass" runat="server"></label>
                                            </td>
                                            <td>Group :</td>
                                            <td>
                                                <label id="lblGroup" runat="server"></label>
                                            </td>                                       
                                        </tr>
                                        <tr>
                                            <td>Section :</td>
                                            <td>
                                                <label id="lblSection" runat="server"></label>
                                            </td>                                            
                                            <td>Date of Birth :</td>
                                            <td>
                                                <label id="lblDateOfBirth" runat="server"></label>
                                            </td>
                                            <td>Gender :</td>
                                            <td>
                                                <label id="lblGender" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>                                            
                                            <td>Mobile :</td>
                                            <td>
                                                <label id="lblMobile" runat="server"></label>
                                            </td>
                                            <td>Blood Group :</td>
                                            <td>
                                                <label id="lblBloodGroup" runat="server"></label>
                                            </td>                                       
                                            <td>Religion :</td>
                                            <td>
                                                <label id="lblReligion" runat="server"></label>
                                            </td>                                           
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="ParentInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table">
                                        <tr>
                                            <td>Father's Name :</td>
                                            <td>
                                                <label id="lblFatherName" runat="server"></label>
                                            </td>
                                            <td>পিতার নাম :</td>
                                            <td>
                                                <label id="lblFatherNameBn" runat="server"></label>
                                            </td>                                            

                                        </tr>                                        
                                        <tr>
                                            <td>Father's Occupation :</td>
                                            <td>
                                                <label id="lblFatherOccupation" runat="server"></label>
                                            </td>
                                            <td>পেশা :</td>
                                            <td>
                                                <label id="lblFatherOccupationBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Father's Mobile :</td>
                                            <td>
                                                <label id="lblFathersMobile" runat="server"></label>
                                            </td>
                                            <td>পিতার মোবাইল নং :</td>
                                            <td>
                                                <label id="lblFathersMobileBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mother's Name :</td>
                                            <td>
                                                <label id="lblMotherName" runat="server"></label>
                                            </td>
                                            <td>মাতার নাম :</td>
                                            <td>
                                                <label id="lblMotherNameBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Mother's Occupation :</td>
                                            <td>
                                                <label id="lblMotherOccupation" runat="server"></label>
                                            </td>
                                            <td>পেশা :</td>
                                            <td>
                                                <label id="lblMotherOccupationBn" runat="server"></label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Mother's Mobile :</td>
                                            <td>
                                                <label id="lblMothersMobile" runat="server"></label>
                                            </td>
                                            <td>মাতার মোবাইল নং :</td>
                                            <td>
                                                <label id="lblMothersMobileBn" runat="server"></label>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>Village :</td>
                                            <td>
                                                <label id="lblParentsVillage" runat="server"></label>
                                            </td> 
                                              <td>গ্রাম :</td>
                                            <td>
                                                <label id="lblParentsVillageBn" runat="server"></label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>Post Office :</td>
                                            <td>
                                                <label id="lblParentsPostOffice" runat="server"></label>
                                            </td> 
                                              <td>ডাকঘর :</td>
                                            <td>
                                                <label id="lblParentsPostOfficeBn" runat="server"></label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>Upazila :</td>
                                            <td>
                                                <label id="lblParentsUpazila" runat="server"></label>
                                            </td> 
                                              <td>উপজেলা :</td>
                                            <td>
                                                <label id="lblParentsUpazilaBn" runat="server"></label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>District :</td>
                                            <td>
                                                <label id="lblParentsDistrict" runat="server"></label>
                                            </td> 
                                              <td>জেলা :</td>
                                            <td>
                                                <label id="lblParentsDistrictBn" runat="server"></label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="GuardianInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table">
                                        <tr>
                                            <td>Guardian Name :</td>
                                            <td>
                                                <label id="lblGuardianName" runat="server"></label>
                                            </td>
                                            <td>Relation :</td>
                                            <td>
                                                <label id="lblRelation" runat="server"></label>
                                            </td>
                                            <td>Mobile No :</td>
                                            <td>
                                                <label id="lblGuardianMobile" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Guardian Address :</td>
                                            <td colspan="5">
                                                <label id="lblGuardianAddress" runat="server"></label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="Address" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    <h5>Present Address</h5>
                                    <table class="table">
                                        <tr>
                                            <td>Village :</td>
                                            <td>
                                                <label id="lblTaVillage" runat="server"></label>
                                            </td>
                                            <td>গ্রাম :</td>
                                            <td>
                                                <label id="lblTaVillageBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Post Office :</td>
                                            <td>
                                                <label id="lblTaPostOffice" runat="server"></label>
                                            </td> 
                                            <td>ডাকঘর :</td>
                                            <td>
                                                <label id="lblTaPostOfficeBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Thana/Upazila :</td>
                                            <td>
                                                <label id="lblTaThana" runat="server"></label>
                                            </td>
                                            <td>থানা/উপজেলা :</td>
                                            <td>
                                                <label id="lblTaThanaBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>District :</td>
                                            <td>
                                                <label id="lblTaDistrict" runat="server"></label>
                                            </td>
                                            <td>জেলা :</td>
                                            <td>
                                                <label id="lblTaDistrictBn" runat="server"></label>
                                            </td>
                                           
                                        </tr>
                                    </table>
                                    <br />
                                    <h5>Permanent Address</h5>
                                    <table class="table">
                                        <tr>
                                            <td>Village :</td>
                                            <td>
                                                <label id="lblPaVillage" runat="server"></label>
                                            </td>
                                            <td>গ্রাম :</td>
                                            <td>
                                                <label id="lblPaVillageBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Post Office :</td>
                                            <td>
                                                <label id="lblPaPostOffice" runat="server"></label>
                                            </td>
                                            <td>ডাকঘর :</td>
                                            <td>
                                                <label id="lblPaPostOfficeBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Thana/Upazila :</td>
                                            <td>
                                                <label id="lblPaThana" runat="server"></label>
                                            </td>
                                            <td>থানা/উপজেলা :</td>
                                            <td>
                                                <label id="lblPaThanaBn" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>District :</td>
                                            <td>
                                                <label id="lblPaDistrict" runat="server"></label>
                                            </td>
                                            <td>জেলা :</td>
                                            <td>
                                                <label id="lblPaDistrictBn" runat="server"></label>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="OtherInfo" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    Previous Institute Info 
                                    <table class="table">
                                        <tr>
                                            <td>Institute :</td>
                                            <td colspan="2">
                                                <label id="lblPreviousSchoolName" runat="server"></label>
                                            </td>
                                            <td> Exam/Class :</td>
                                            <td>
                                                <label id="lblPreviousExam" runat="server"></label>
                                            </td>                                            
                                            <td>Passing Year :</td>
                                            <td>
                                                <label id="lblPreviousPassingYear" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td>Board :</td>
                                            <td>
                                                <label id="lblPreviousBoard" runat="server"></label>
                                            </td>
                                            <td>Exam Roll :</td>
                                            <td>
                                                <label id="lblPreviousRoll" runat="server"></label>
                                            </td>
                                            <td>Reg. No:</td>
                                            <td>
                                                <label id="lblPreviousReg" runat="server"></label>
                                            </td>
                                             <td>GPA:</td>
                                            <td>
                                                <label id="lblPreviousGPA" runat="server"></label>
                                            </td>                                  
                                        </tr>                                        
                                    </table>
                                    TC Information
                                    <table class="table">
                                        <tr>
                                            <td>Institute Name:</td>
                                            <td>
                                                <label id="lblTCInstituteName" runat="server"></label>
                                            </td>
                                            <td>Date :</td>
                                            <td>
                                                <label id="lblTCDate" runat="server"></label>
                                            </td>                                          
                                        </tr>
                                    </table>
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
