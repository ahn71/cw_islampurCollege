<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DS.UI.Adviser.ChangePassword" %>
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
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        DashBoard
                    </a>
                </li>                
                <li class="active">Change Password</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Change  Password</div>
        <table class="tbl-controlPanel">           
            
              <tr>
                <td>User Name</td>
                <td>
                    <asp:TextBox ID="txtUserName" ReadOnly="true" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Old Password</td>
                <td>
                    <asp:TextBox ID="txtOldPassword"  ReadOnly="true" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
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
                <td></td>
                <td>
                    <asp:Button ID="btnChangeUserAccount" CssClass="btn btn-primary" runat="server" Text="Change"
                       OnClick="btnChangeUserAccount_Click"  OnClientClick="return validateInputs();"  />                   
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {           
            if (validateText('txtNewPassword', 1, 100, 'Enter a New Password') == false) return false;
            if (validateText('txtConfirmPassword', 1, 100, 'Enter a Confirm Password') == false) return false;
            var newpass = document.getElementById("txtNewPassword").value;
            var confirmpass = document.getElementById("txtConfirmPassword").value;
            if (newpass != confirmpass) {               
                showMessage('New Password and Confirm Password Does Not Match!', 'warning');               
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
