<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExaminationHome.aspx.cs" Inherits="DS.UI.Reports.Examination.ExaminationHome" %>
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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                
                <li class="active">Examination</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Examination/ExamReports.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/examination.ico" />
                    </span>
                    <span>Exam Reports</span>
                </div> 
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Examination/ExamOverView.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/overview.ico" />
                    </span>
                    <span>Exam Overview</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Examination/AcademicTranscript.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Academic Transcript</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/Examination/DependencyCnvtMarks.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Dependency Convert Marks</span>
                </div> 
            </a>
        </div>
       <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Reports/Examination/DependencyExamResult.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Dependency Exam Result</span>
                </div> 
            </a>
        </div>
        <div class="col-md-3">
            <a id="A2" runat="server" href="~/UI/Reports/Examination/StudentSubjectList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Student Subject List</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a id="A4" runat="server" href="~/UI/Reports/Examination/StudentGrpSubList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Student Group Subject List</span>
                </div> 
            </a>
        </div>
        <div class="col-md-3">
            <a id="A5" runat="server" href="~/UI/Reports/Examination/TransferCertificate.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Transfer Certificate (TC)</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a id="A6" runat="server" href="~/UI/Reports/Examination/MonthlyTestReport.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Monthly Test</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a id="A10" runat="server" href="~/UI/Reports/Examination/AdmitCard.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Admit Card</span>
                </div> 
            </a>
        </div>
          <div class="col-md-3">
            <a id="A7" runat="server" href="~/UI/Reports/Examination/ExamineeNumber.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Subject Wise Student's Number</span>
                </div> 
            </a>
        </div>
 <div class="col-md-3">
            <a id="A8" runat="server" href="~/UI/Reports/Examination/AttendanceSheetInXm.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Attendance Sheet</span>
                </div> 
            </a>
        </div>
         <div class="col-md-3">
            <a id="A9" runat="server" href="~/UI/Reports/Examination/SubjectWiseNumberSheet.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Number Sheet</span>
                </div> 
            </a>
        </div>
        <div class="col-md-3">
            <a id="A11" runat="server" href="~/UI/Reports/Examination/SeatPlanSticker.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Seat Plan Sticker</span>
                </div> 
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
