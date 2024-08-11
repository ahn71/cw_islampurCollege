<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DS.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="row">
      
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li class="active">
                    <%--<a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li class="active"></li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3" runat="server" id="AcademicModuleDB">
            <a  runat="server" id="aAcademicHome">
                <div class="mini-stat mini_stat_sub_icon clearfix">
                    <span class="mini-stat-icon orange">
                        <i class="fa fa-book"></i>
                    </span>
                    <div class="mini-stat-info">
                        <span>Academic</span>
                        All Academic Task Processing Modules
                    </div>
                </div>
            </a>
            <a id="aStudent" runat="server">            
            <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                <span class="mini-stat-icon sub_mini_icon sub_mini_orange">
                    <img width="30" src="Images/dashboard_icon/Students.png" alt="Student"/>
                </span>
                <div class="mini-stat-info sub_mini_info">
                    <span>Student</span>
                </div>
            </div>
            </a>
            <a id="A2" runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg effects">
                            <span class="mini-stat-icon sub_mini_icon sub_mini_orange">
                                <img width="30" src="Images/dashboard_icon/Attendance.png" alt="attendence"/>
                            </span>
                            <div class="mini-stat-info sub_mini_info">
                                <span>Attendance</span>
                                
                            </div>
                        </div>
            </a>
            <%--<a id="A3" runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">--%>
            <a  runat="server" id="aExamHome">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                            <span class="mini-stat-icon sub_mini_icon sub_mini_orange">
                                <img width="30" src="Images/dashboard_icon/exam.png" alt="examination" />
                            </span>
                            <div class="mini-stat-info sub_mini_info">
                                <span>Examination</span>
                                
                            </div>
                        </div>
            </a>

            <a id="A4" runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                            <span class="mini-stat-icon sub_mini_icon  sub_mini_orange">
                                <img width="30" src="Images/dashboard_icon/timetable.png" alt="timetable" />
                            </span>
                            <div class="mini-stat-info sub_mini_info">
                                <span>Routine</span>
                                
                            </div>
                        </div>
            </a>
             <a id="A17" runat="server" href="~/UI/Academic/StudentGuide/StudentGuideHome.aspx" >
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                            <span class="mini-stat-icon sub_mini_icon  sub_mini_orange">
                                <img width="30" src="Images/dashboard_icon/Adviser.png" alt="adviser" />
                            </span>
                            <div class="mini-stat-info sub_mini_info">
                                <span>Guide Teacher</span>
                                
                            </div>
                        </div>
            </a>
        </div>
        <div class="col-md-3" runat="server" id="AdministrationModuleDB" >
            <a runat="server" id="aAdministrationHome">
                <div class="mini-stat mini_stat_sub_icon clearfix">
                    <span class="mini-stat-icon tar">
                        <i class="fa fa-wheelchair"></i>
                    </span>
                    <div class="mini-stat-info">
                        <span style="font-size: 18px">Administration</span>
                        All Administration Task Processing Module
                    </div>
                </div>
            </a>


            <a id="A5" runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">

                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <span class="mini-stat-icon comtar sub_mini_icon">
                        <img width="30" src="../../Images/moduleicon/students.png" alt="student" />
                    </span>
                    <div class="mini-stat-info sub_mini_info">
                        <span>Finance</span>    
                    </div>
                </div>
            </a>
            <a id="A6" runat="server" href="~/UI/Administration/HR/hrHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                               <img width="30" src="Images/dashboard_icon/HR.png" alt="HR" />
                        </span>
                        <span>Human Resource</span>    
                    </div>
                </div>
            </a>
            <a id="A7" runat="server" href="~/UI/Administration/Users/UsersHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="Images/dashboard_icon/contron.png" alt="Controlpanel" />
                        </span>
                        <span>Control Panel</span>    
                    </div>
                </div>
            </a>
            <a id="A8" runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="Images/dashboard_icon/Sattings.png" alt="settings" />
                        </span>
                        <span>Settings</span>    
                    </div>
                </div>
            </a>
            <a id="aWebsite" runat="server">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="Images/dashboard_icon/website.png" alt="Website" />
                        </span>
                        <span>Website</span>    
                    </div>
                </div>
            </a>

        </div>
        <div class="col-md-3" runat="server" id="ReportsModuleDB">
            <a runat="server" href="~/UI/Reports/ReportHome.aspx">
                <div class="mini-stat mini_stat_sub_icon clearfix">
                    <span class="mini-stat-icon pink">
                        <i class="fa fa-paste"></i>
                    </span>
                    <div class="mini-stat-info">
                        <span>Report</span>
                        All Report Processing Module
                    </div>
                </div>
            </a>


           
            <a id="A9" runat="server" href="~/UI/Reports/Students/StudentInfoHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_mediumpurple">
                             <img width="30" src="../../Images/dashboard_icon/rstudent.png" alt="rstudent" />
                        </span>
                        <span>Students Info</span>    
                    </div>
                </div>
            </a>
        
            <a id="A10" runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_mediumpurple">
                             <img width="30" src="../../Images/dashboard_icon/rstaff.png" alt="rstaff" />
                        </span>
                        <span>Employees Info </span>    
                    </div>
                </div>
            </a>
        
            <a id="A11" runat="server" href="~/UI/Reports/TimeTable/ScheduleHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_mediumpurple">
                             <img width="30" src="../../Images/dashboard_icon/rschedule.png" alt="rschedule" />
                        </span>
                        <span>Routine</span>    
                    </div>
                </div>
            </a>
        
            <a id="A12" runat="server" href="~/UI/Reports/Attendance/AttendanceHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg" >
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_mediumpurple">
                             <img width="30" src="../../Images/dashboard_icon/rattendence.png" alt="rattendence" />
                        </span>
                        <span>Attendance</span>    
                    </div>
                </div>
            </a>
        
           <a runat="server" href="~/UI/Reports/Examination/ExaminationHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_mediumpurple">
                             <img width="30" src="../../Images/dashboard_icon/rExamination.png" alt="rExamination" />
                        </span>
                        <span>Examination</span>    
                    </div>
                </div>                
            </a>        

        </div>
        <div class="col-md-3" runat="server" id="NotificationModuleDB">
            <a runat="server" href="~/UI/Notification/SendSMS.aspx?TabIndex=0">
                <div class="mini-stat mini_stat_sub_icon clearfix">
                    <span class="mini-stat-icon green">
                        <i class="fa fa-bullhorn"></i>
                    </span>
                    <div class="mini-stat-info">
                        <span>Notification</span>
                        SMS, Email, Notices Processing Module
                    </div>
                </div>
            </a>


            <a id="A13" runat="server" href="~/UI/Notification/SendSMS.aspx?TabIndex=0">

                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_notification btn-lg">
                    <span class="mini-stat-icon sub_mini_icon sub_mini_forestgreen">
                        <img width="30" src="../../Images/dashboard_icon/absent.png" alt="student" />
                    </span>
                    <div class="mini-stat-info sub_mini_info">
                        <span>Today's Absent Students</span>    
                    </div>
                </div>
            </a>
            <a id="A14" runat="server" href="~/UI/Notification/SendSMS.aspx?TabIndex=1">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_notification btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_forestgreen">
                                <img width="30" src="../../Images/dashboard_icon/fail.png" alt="failstudent" />
                        </span>
                        <span>Fail Students List</span>    
                    </div>
                </div>
            </a>
            <a id="A15" runat="server" href="~/UI/Notification/SendSMS.aspx?TabIndex=2">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_notification btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_forestgreen">
                                <img width="30" src="../../Images/dashboard_icon/notice.png" alt="notice" />
                        </span>
                        <span>Notice</span>    
                    </div>
                </div>
            </a>
            <a id="A16" runat="server" href="~/UI/Notification/SendSMS.aspx?TabIndex=3">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_notification btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_forestgreen">
                                <img width="30" src="../../Images/dashboard_icon/greeting.png" alt="greeting" />
                        </span>
                        <span>Greetings</span>    
                    </div>
                </div>
            </a>
            <a id="A19" runat="server" href="~/UI/Notification/Template.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_notification btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_icon sub_mini_forestgreen">
                                <img width="30" src="../../Images/dashboard_icon/template.png" alt="template" />
                        </span>
                        <span>Template</span>    
                    </div>
                </div>
            </a>
            
        </div>
    </div>  

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    
    
</asp:Content>
