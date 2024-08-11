<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DS.UI.StudentManage.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style type="text/css">
           .tgPanelHead {
                text-align:center;
           }     
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
                    <a id="A1" runat="server" href="~/UI/StudentManage/StudentManage.aspx">
                        <i class="fa fa-dashboard"></i>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </a>
                </li>                
                <li class="active">Change Password</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        
    <div class="tgPanel">
        <div class="tgPanelHead">Change Password</div>
        <table class="tbl-controlPanel">   
            
              <tr>
                <td>User Name<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server"  class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Old Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtOldPassword" ClientIDMode="Static" runat="server"   class="input controlLength"></asp:TextBox>
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
                <td></td>
                <td>
                    <asp:Button ID="btnSaveUserAccount" CssClass="btn btn-primary" runat="server" Text="Change"
                        OnClientClick="return validateInputs();" OnClick="btnSaveUserAccount_Click"  />
                    <input type="reset" value="Reset" class="btn btn-default" />                    
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
