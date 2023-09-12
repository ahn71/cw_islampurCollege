<%@ Page Title="Academic Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AcademicHome.aspx.cs" Inherits="DS.UI.Academics.AcademicHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <%--<a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li class="active">Academic Module</li>               
            </ul> 
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Students/StdHome.aspx">--%>               
            <a runat="server" id="aStudent">               
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_orange sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/Students.png" alt="Student"/>
                        </span>
                        <span>Student</span>
                    <%--Student Admission,Batch Assign,Promotion--%>   
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_orange sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/Attendance.png" alt="attendence"/>
                        </span>
                         <span>Attendance</span>
                               <%--Student,Staff or Faculty Attendance--%>  
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">--%>
            <a runat="server" id="ExamHome">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_orange sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/exam.png" alt="examination" />
                        </span>
                        <span>Exam</span>
                               <%--Result Processing--%>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_orange sub_mini_icon">
                               <img width="30" src="../../Images/dashboard_icon/timetable.png" alt="timetable" />
                        </span>
                       <span>Routine</span>
                               <%--Class Routine--%>
                    </div>
                </div>
            </a>
        </div>
        </div>
    <div class="row">
         <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Academic/StudentGuide/StudentGuideHome.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon sub_mini_orange sub_mini_icon">
                               <img width="30" src="../../Images/dashboard_icon/timetable.png" alt="timetable" />
                        </span>
                       <span>Guide Teacher</span>
                              <%--Assign Guide Teacher,List of Guide Teacher--%>
                    </div>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
