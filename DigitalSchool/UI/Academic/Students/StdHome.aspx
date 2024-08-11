<%@ Page Title="Student Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdHome.aspx.cs" Inherits="DS.UI.Academics.Students.StdHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                   <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li>
                    <%--<a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a>--%>
                    <a runat="server" id="aAcademicHome" >Academic Module</a>

                </li>
                <li class="active">Student Module</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Students/student-entry.aspx">--%>
            <a runat="server" id="aStudentAdd">
                <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/OldStudentEntry.ico" alt="oldstudent" />
                    </span>
                    <span>Student Entry</span>

                </div>
            </a>
        </div>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/StdAdmission.aspx">
               <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/Admission.ico" alt="stdAdmission" />
                    </span>                           
                        <span>Student Admission Form</span>    
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/AdmissionDetails.aspx">
                <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/AdmissionDetails.ico" alt="stdAdmissionDetails" />
                            </span>                           
                                <span>Admission Details</span>                          
                                
                          
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/AdmStdAssign.aspx">
                 <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/StudentAssign.ico" alt="BatchAssign" />
                            </span>                           
                                <span>New Student Batch Assign</span>                          
                                
                          
                        </div>
            </a>
        </div>--%>
        
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Students/CurrentStudentInfo.aspx">--%>
            <a runat="server" id="aStudentList">
                <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/CurrentStudentInfo.ico" alt="CurrentStdInfo" />
                            </span>                           
                                <span>Current StudentInfo</span>                          
                                
                          
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a id="A2" runat="server" href="~/UI/Academic/Students/StdActivation.aspx">--%>
            <a runat="server" id="aStudentActivation">
                 <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/student-active.png" alt="StdPromotion" />
                            </span>                           
                                <span>Student Active/Inactive</span>                          
                                
                          
                        </div>
            </a>
        </div>
         <div class="col-md-3">
            <%--<a id="A1" runat="server" href="~/UI/Academic/Students/StdSectionChange.aspx">--%>
            <a  runat="server" id="aStudentSectionChange">
                 <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/student-promotion.png" alt="StdPromotion" />
                            </span>                           
                                <span>Student Section Change</span>                          
                                
                          
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Students/StdPromotion.aspx">--%>
            <a runat="server" id="aStudentPromotionRoute">
                 <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/student-section-change.png" alt="StdPromotion" />
                            </span>                           
                                <span>Student Promotion</span>                          
                                
                          
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Students/admission-approval.aspx">--%>
            <a runat="server" id="aStudentAdmissionApproval">
                <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/student-section-change.png" alt="StdPromotion" />
                    </span>
                    <span>Admission Approval</span>
                </div>
            </a>
        </div>
        
        
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/StudentAssign.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Failed Student Promotion</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/SeparationClassNine.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Separation Class Nine-Ten</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/SetRollSubjectClassNine.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Set Roll Subject Class Nine-Ten</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/SeparationElevenTwelve.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Separation Class Eleven-Twelve</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Students/SetRollSubjectElevenTwelve.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Set Roll Subject Class Eleven-Twelve</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
