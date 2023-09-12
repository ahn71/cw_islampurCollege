<%@ Page Title="General Settings" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="GeneralSettingsHome.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.GeneralSettingsHome" %>

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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li class="active">General Settings</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">        
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddDistrict.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/distric add.ico" alt="districadd" />
                    </span>
                    <span>Add District</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddThana.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/thana add.ico" alt="thana" />
                    </span>
                    <span>Add Thana/Upazila</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddPostOffice.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/thana add.ico" alt="thana" />
                    </span>
                    <span>Add Post Office</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddBoard.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/boardd.ico" alt="board" />
                    </span>
                    <span>Add Board</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AttendanceSettings.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/attendence settings.ico" alt="attendencesettings" />
                    </span>
                    <span>Absent Fine Settings</span>
                </div>
            </a>
        </div>        
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/SchoolSetup.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/school settings.ico" alt="schoolsettings" />
                    </span>
                    <span>College Setup</span>
                </div>
            </a>
        </div>
         <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Administration/Settings/GeneralSettings/ShiftConfig.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/shift configuration.ico" alt="ShiftConfig" />
                    </span>
                    <span>Shift Configuration</span>
                </div>
            </a>
        </div>  
         <div class="col-md-3">
            <a id="A2" runat="server" href="~/UI/Administration/Settings/GeneralSettings/CreateWeekdays.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/week days.ico" alt="weekdays" />
                    </span>
                    <span>Create Week Days</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Administration/Settings/GeneralSettings/OffDaysSet.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Timetable/off day.ico" alt="offday" />
                    </span>
                    <span>Off Days Settings</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
