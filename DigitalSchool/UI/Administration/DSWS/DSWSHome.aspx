<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DSWSHome.aspx.cs" Inherits="DS.UI.Administration.DSWS.DSWSHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
               <%-- <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>--%>
                
                <li>
                     <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li class="active">Website Settings Module</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
             <a runat="server" id="aNotices">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/academy.ico" alt="academy" />
                    </span>
                    <span>Notices</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/DSWS/AddSpesialDescription.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/academy.ico" alt="academy" />
                    </span>
                    <span>Add Special Description</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" id="aSpeeches">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Add President & Principal Speech</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/DSWS/PhotoAlbum.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Album</span>
                </div>
            </a>
        </div>      
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/DSWS/AddEvent.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Event</span>
                </div>
            </a>
        </div>  
        <div class="col-md-3">
            <a runat="server" id="aSlider" >
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Slider</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/DSWS/AddPageContent.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Pages</span>
                </div>
            </a>
        </div> 
        
        <div class="col-md-3">
            <a runat="server" id="aQuickLink">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Quick Link</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
            <a runat="server" id="aWSGeneralSettings">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>General Settings</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
