<%@ Page Title="System Settings Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SettingsHome.aspx.cs" Inherits="DS.UI.Administration.Settings.SettingsHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a href="/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li class="active">System Settings Module</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AcdSettingsHome.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/academy.ico" alt="academy" />
                    </span>
                    <span>Academic Settings</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>General Settings</span>
                </div>
            </a>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
