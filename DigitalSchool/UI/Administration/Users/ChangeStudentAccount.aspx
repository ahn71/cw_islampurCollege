<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ChangeStudentAccount.aspx.cs" Inherits="DS.UI.Administration.Users.ChangeStudentAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">        
        .tgPanel
        {
            width:100%;
        }
        .tbl-controlPanel{
            width:500px;
        }
        .tbl-controlPanel td{
            width: 100%;
        }
        .controlLength{
            width: 65%;
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
                <li class="active">Student Account Change</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        
    <div class="tgPanel">
        <div class="tgPanelHead">Change Student Acccount</div>
        <table class="tbl-controlPanel">   
            
              <tr>
                <td>User Name<span class="required">*</span></td>
                <td>
                    <asp:DropDownList ID="ddlAdmissionNo" runat="server" CssClass="input controlLength"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Old Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtOldPassword" ClientIDMode="Static" runat="server"  class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>New Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtNewPassword" ClientIDMode="Static" TextMode="Password" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Confirm Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" ClientIDMode="Static" TextMode="Password" runat="server" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Status<span class="required">*</span></td>
                <td>
                    <asp:RadioButtonList ID="rblAccountStatus" runat="server" RepeatDirection="Horizontal" CssClass="radiButtonLists">
                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>        
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSaveUserAccount" CssClass="btn btn-primary" runat="server" Text="Change"
                        OnClientClick="return validateInputs();" OnClick="btnSaveUserAccount_Click"  />
                    <input type="reset" value="Reset" class="btn btn-default" />
                    <input type="reset" value="Account List" OnClick="window.location.href = '/UI/Administration/Users/StudentAccountList.aspx'"  class="btn btn-default" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateCombo('MainContent_ddlAdmissionNo', "0", 'Select Admission No') == false) return false;
            if (validateText('txtUserPassword', 1, 100, 'Enter a User Password') == false) return false;
            if (validateText('txtNewPassword', 1, 100, 'Enter New Password') == false) return false;
            if (validateText('txtConfirmPassword', 1, 100, 'Enter Confirm Password') == false) return false;
            if ($('#txtNewPassword').val() != $('#txtConfirmPassword').val()) {
                showMessage('UserPassword And ConfirmPassword Does not Match!', 'warning')
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
