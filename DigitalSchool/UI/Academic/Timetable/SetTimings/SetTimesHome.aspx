<%@ Page Title="Set Timings Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetTimesHome.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetTimings.SetTimesHome" %>

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
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Timetable Module</a></li>
                <li class="active">Set Timings Management</li>                               
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
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/ClassTimeSetName.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/class time set.ico" alt="classtimeset" />
                    </span>
                    <span>Class Time Set Name</span>
                </div>
            </a>
        </div>--%>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/ClassTimeSpecification.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/class time specification.ico" alt="classtimespecification" />
                    </span>
                    <span>Class Time Specification</span>
                </div>
            </a>
        </div>
       <%-- <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/ExamTimeSetName.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/exam time set name.ico" alt="examtimesetname" />
                    </span>
                    <span>Exam Time Set Name</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/ExamTimeSpecification.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/exam time specification.ico" alt="examtimespecification" />
                    </span>
                    <span>Exam Time Specification</span>
                </div>
            </a>
        </div>--%>

        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/SetTimings/SessionDateTime.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Session Date Time</h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
