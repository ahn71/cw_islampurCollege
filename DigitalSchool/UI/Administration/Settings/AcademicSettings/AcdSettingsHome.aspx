<%@ Page Title="Academic Settings" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AcdSettingsHome.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.AcdSettingsHome" %>

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
                <li class="active">Academic Settings</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">               
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AddClass.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add class.ico" alt="addclass" />
                    </span>
                    <span>Add Class</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AddSection.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add section.ico" alt="addsection" />
                    </span>
                    <span>Add Section</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageGroup.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/General Settings.ico" alt="GeneralSettings" />
                    </span>
                    <span>Add Group</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageClassGroup.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add class group.ico" alt="addclassgroup" />
                    </span>
                    <span>Add Class Group</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A2" runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageClassSection.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add class section.ico" alt="addclasssection" />
                    </span>
                    <span>Add Class Section</span>
                </div>
            </a>
        </div>   
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/CreateBatch.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add batch.ico" alt="addbatch" />
                    </span>
                    <span>Add Batch</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/StdType.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add batch.ico" alt="addbatch" />
                    </span>
                    <span>Student Type</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/BusInformation.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add batch.ico" alt="addbatch" />
                    </span>
                    <span>Bus Information</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/Location.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add batch.ico" alt="addbatch" />
                    </span>
                    <span>Location</span>
                </div>
            </a>
        </div>  
         <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Settings/AcademicSettings/Place.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Settings/add batch.ico" alt="addbatch" />
                    </span>
                    <span>Bus Stand</span>
                </div>
            </a>
        </div>     
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
