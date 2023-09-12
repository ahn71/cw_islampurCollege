<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ClassScheduleHome.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ClassScheduleHome" %>
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
                 <li><a runat="server" href="~/UI/Reports/TimeTable/ScheduleHome.aspx">Schedule</a></li>
               
                <li class="active">Class Schedule</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/TimeTable/ClassRoutineReport.aspx">               
                   <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/Class Wise Schedule.ico" />
                    </span>
                    <span>Class Wise</span>
                </div>                                
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/TimeTable/ClassRoutine For_Teacher.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/teacher wiseSchedule.ico" />
                    </span>
                    <span>Teacher Wise</span>
                </div> 
            </a>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
