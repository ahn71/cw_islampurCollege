<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ReportHome.aspx.cs" Inherits="DS.UI.Reports.ReportHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                <li class="active">Reports Module</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/StudentInfoHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rstudent.png" alt="rstudent" />
                        </span>
                        <span>Students Info</span>    
                    </div>
                </div>
            </a>
        </div>
       <%-- <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StudentFine/StudentFineList.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Student Fine</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
         <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rstaff.png" alt="rstaff" />
                        </span>
                        <span>Teacher or Admin</span>    
                    </div>
                </div>
            </a>
        </div>
         <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/TimeTable/ClassRoutineReport.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rschedule.png" alt="rschedule" />
                        </span>
                        <span>Routine</span>    
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Attendance/AttendanceHome.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rattendence.png" alt="rattendence" />
                        </span>
                        <span>Attendance</span>    
                    </div>
                </div>
            </a>
        </div>       

        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Examination/ExaminationHome.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rExamination.png" alt="rExamination" />
                        </span>
                        <span>Examamination</span>    
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A2" runat="server" href="~/UI/Reports/Finance/FineReports.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_mediumpurple sub_mini_icon">
                             <img width="30" src="../../Images/dashboard_icon/rExamination.png" alt="rExamination" />
                        </span>
                        <span>Finance Reports</span>    
                    </div>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
