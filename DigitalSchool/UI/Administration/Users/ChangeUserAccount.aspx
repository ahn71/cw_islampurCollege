<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ChangeUserAccount.aspx.cs" Inherits="DS.UI.Administration.Users.ChangeUserAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <style type="text/css">        
        .tgPanel
        {
            width:100%;
        }
        .tbl-controlPanel{
            width:40%;
        }
        .tbl-controlPanel td{
            width: 100%;
        }
        .controlLength{
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            width: 25%;
            text-align: right;
            padding-right: 5px;
        }
         .checkBoxLists label {
             padding-left: 2px;
             margin-right: 3px;
             margin-left: 2px;
         }
         .radiButtonLists label {
             padding-left: 2px;
             margin-right: 3px;
             margin-left: 2px;
         }        
     </style>
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
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">Control Panel</a></li>
                <li class="active">User Registration Form</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Change User Account</div>
        <table class="tbl-controlPanel">  
             <tr>
                <td>Status<span class="required">*</span></td>
                <td>
                    <asp:RadioButtonList ID="rblAccountStatus" runat="server" RepeatDirection="Horizontal" CssClass="radiButtonLists">
                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>First Name<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>          

            <tr>
                <td>Last Name<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
           
          
             <tr>
                <td>Email<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>Confirm Email<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtConfirmEmail" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td>User Name<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Old Password</td>
                <td>
                    <asp:TextBox ID="txtOldPassword" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>New Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtNewPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Confirm Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td style="vertical-align: top;">User Type<span class="required">*</span></td>
                <td>
                   <asp:DropDownList ID="ddlUserTypeList" runat="server"  CssClass="input controlLength" ClientIDMode="Static" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnChangeUserAccount" CssClass="btn btn-primary" runat="server" Text="Change"
                        OnClientClick="return validateInputs();" OnClick="btnChangeUserAccount_Click"   />
                    <input type="reset" value="Reset" class="btn btn-default" />
                    <input type="reset" value="Account List" OnClick="window.location.href = '/UI/Administration/Users/UserAccountList.aspx'"  class="btn btn-default" />
                    <asp:Button ID="btnActivation" CssClass="btn btn-primary" runat="server" Text="Inactive" OnClientClick="return confirm('Do you want to change user status?')" OnClick="btnActivation_Click"
                         />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;
            if (validateText('txtFirstName', 1, 100, 'Enter a First Name') == false) return false;
            if (validateText('txtMiddleName', 1, 100, 'Enter a Middle Name') == false) return false;
            if (validateText('txtLastName', 1, 100, 'Enter a Last Name') == false) return false;
            if (validateText('txtInitial', 1, 100, 'Enter a Initial') == false) return false;
            if (validateText('txtUserName', 1, 100, 'Enter a User Name') == false) return false;
            if (validateText('txtUserPassword', 1, 100, 'Enter a User Password') == false) return false;
            if (validateText('txtEmail', 1, 100, 'Enter a Email') == false) return false;
            if (validateCombo('ddlUserType', "0", 'Select User Type') == false) return false;
            return true;
        }
    </script>
</asp:Content>
