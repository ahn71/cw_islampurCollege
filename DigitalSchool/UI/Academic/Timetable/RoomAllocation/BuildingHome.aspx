<%@ Page Title="Building And Classroom Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="BuildingHome.aspx.cs" Inherits="DS.UI.Academic.Timetable.RoomAllocation.BuildingHome" %>

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
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Routine Module</a></li>
                <li class="active">Building And Classroom Settings</li>                               
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
        <div class="col-md-3"></div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/ManagedBuildings.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/building manegment.ico" alt="buildingsmanagement" />
                    </span>
                    <span>Add Buildings</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/Allocated.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/building and class room.ico" alt="buildingallocatedsmanagement" />
                    </span>
                    <span>Classroom Allocated To Building</span>
                </div>
            </a>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
