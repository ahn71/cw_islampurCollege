<%@ Page Title="Administration Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdministrationHome.aspx.cs" Inherits="DS.UI.Administration.AdministrationHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                   <%-- <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>--%>
                     <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li class="active">Administration Module</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <span class="mini-stat-icon comtar sub_mini_icon">
                        <img width="30" src="../../Images/moduleicon/students.png" alt="student" />
                    </span>
                    <div class="mini-stat-info sub_mini_info">
                        <span>Finance</span>    
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/hrHome.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                               <img width="30" src="../../Images/dashboard_icon/HR.png" alt="HR" />
                        </span>
                        <span>Human Resource</span>    
                    </div>
                </div>
            </a>
        </div>
      <%--  <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Inventory/InventoryHome.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h4 class="text-center">Inventory</h4>
                    </div>
                </div>
            </a>
        </div>--%>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/contron.png" alt="Controlpanel" />
                        </span>
                        <span>Control Panel</span>    
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/Sattings.png" alt="settings" />
                        </span>
                        <span>Settings</span>    
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="aWebsite" runat="server" >
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration btn-lg">
                    <div class="mini-stat-info sub_mini_info">
                        <span class="mini-stat-icon comtar sub_mini_icon">
                                <img width="30" src="../../Images/dashboard_icon/website.png" alt="settings" />
                        </span>
                        <span>Website</span>    
                    </div>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
