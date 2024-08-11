<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="UsersHome.aspx.cs" Inherits="DS.UI.Administration.Users.UsersHome" %>

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
                <li class="active">User Management Module</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/UserType.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/add user.ico" alt="adduser" />
                    </span>
                    <span>Add User Type</span>
                </div>
            </a>
        </div>
         <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/UserRegister.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/user account.ico" alt="useraccount" />
                    </span>
                    <span>Add User Account</span>
                </div>
            </a>
        </div>

         <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/UserAccountList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/user account list.ico" alt="useraccountlist" />
                    </span>
                    <span>List of Account</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/StudentAccount.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/strudent account .ico" alt="strudentaccount " />
                    </span>
                    <span>Add Student Account</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/StudentAccountList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/strudent account list.ico" alt="strudentaccountlist" />
                    </span>
                    <span>List of Student Account</span>
                </div>
            </a>
        </div>
         <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/ChangePageInfo.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/user account list.ico" alt="adduser" />
                    </span>
                    <span>Page Setup & Set Privilege</span>
                </div>
            </a>
        </div>
       
         <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Users/OffMainModule.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Controlpanel/modiule.ico" alt="module" />
                    </span>
                    <span>Main Module Privilege</span>
                </div>
            </a>
        </div>
          
        
            
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
