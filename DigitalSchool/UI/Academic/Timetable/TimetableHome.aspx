<%@ Page Title="Timetable Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="TimetableHome.aspx.cs" Inherits="DS.UI.Academics.Timetable.TimetableHome" %>

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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li class="active">Routine Module</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
      <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/BuildingHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/calass room alocation.ico" alt="roomallocation" />
                    </span>
                    <span>Building & Room Settings</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/ClassTimeSpecification.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/timings setup.ico" alt="settimings" />
                    </span>
                    <span>Set Timings</span>
                </div>
            </a>
        </div>
        <%--  <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/WorkAllotment.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/allotment.ico" alt="allotment" />
                    </span>
                    <span>Teacher Subject Assign</span>
                </div>
            </a>
        </div>--%>

        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetClassTimings.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/class timing.ico" alt="classtimings" />
                    </span>
                    <span>Class Routine</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/TimeTable/ClassRoutineReport.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/exam timing.ico" alt="examtimings" />
                    </span>
                    <span>Report</span>
                </div>
            </a>
        </div>
      
    </div>
     
  <%--  <div class="row">
         <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetExamTimings.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/exam timing.ico" alt="examtimings" />
                    </span>
                    <span>Exam Routine</span>
                </div>
            </a>
        </div>
      

        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/TeacherTimetable.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Teacher Timetable</h5>
                    </div>                    
                </div>
            </a>
        </div>      
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/AssignTeacherToExamRoom.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Assign Teacher To Exam Room</h5>
                    </div>                    
                </div>
            </a>
        </div> 
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">    
</asp:Content>
