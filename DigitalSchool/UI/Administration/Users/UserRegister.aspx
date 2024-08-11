<%@ Page Title="User Registration Form" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="DS.UI.Administration.Users.UserRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
     <style type="text/css">        
        .tgPanel
        {
            width:100%;
        }
        /*.tbl-controlPanel{
            width:40%;
        }*/
        .tbl-controlPanel td{
            width: 100%;
        }
        .controlLength{
            width: 100%;
            color:#808080;
        }
        .tbl-controlPanel td:first-child{
            
            text-align: right;
            padding-right: 5px;
        }
         .checkBoxLists label {
             padding-left: 2px;
             margin-right: 3px;
             margin-left: 2px;
         }
         .radiButtonLists label {
             padding-left: 0px;
             margin-right: 0px;
             margin-left: 3px;
         }
         .btn {
            padding:6px 11px;
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
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        
    <div class="tgPanel">
        <div class="tgPanelHead">Add New Use</div>
         <div class="row tbl-controlPanel">
            <div class="col-sm-6 col-sm-offset-3">
                    <div class="row tbl-controlPanel">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="rdbAdviser" />
                           </Triggers>
                        <ContentTemplate>
                        <label class="col-sm-4">Adviser<span class="required">*</span></label>
                        <div class="col-sm-8">
                             <asp:RadioButtonList ID="rdbAdviser" RepeatLayout="Flow" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbAdviser_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="radiButtonLists">
                                <asp:ListItem Text="Yes" class="radio-inline" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="No" class="radio-inline" Value="0" Selected="True"></asp:ListItem>                        
                            </asp:RadioButtonList>

                             <asp:TextBox ID="txtCardNo" Visible="false" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                             <asp:Button ID="btnFind" Visible="false" CssClass="btn btn-primary" runat="server" Text="Find"
                              OnClientClick="return valid();" OnClick="btnFind_Click" />
                        </div>
                        
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">First Name<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Last Name<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Email<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Confirm Email<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtConfirmEmail" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">User Name<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">User Password<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Confirm Password<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtConfirmPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Status<span class="required">*</span></label>
                        <div class="col-sm-8"> 
                            <asp:RadioButtonList ID="rblAccountStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiButtonLists">
                                <asp:ListItem Text="Active" class="radio-inline" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactive" class="radio-inline" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4">User Type<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlUserTypeList" runat="server"  CssClass="input controlLength form-control" ClientIDMode="Static" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4"></label>
                        <div class="col-sm-8">
                            <asp:Button ID="btnSaveUserAccount" CssClass="btn btn-primary" runat="server" Text="Save"
                            OnClientClick="return validateInputs();" OnClick="btnSaveUserAccount_Click" />
                            <input type="reset" value="Reset" class="btn btn-default" />
                            <input type="reset" value="Account List" OnClick="window.location.href = '/UI/Administration/Users/UserAccountList.aspx'"  class="btn btn-default" />
                        </div>
                    </div>
                    
      
            </div>
        </div>
        <%-- <table class="tbl-controlPanel"> 
             <tr>
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="rdbAdviser" />
                       </Triggers>
        <ContentTemplate>
                <td>Adviser<span class="required">*</span></td>
                <td>
                    <div class="row">
                    <div class="col-md-6">
                    <asp:RadioButtonList ID="rdbAdviser" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbAdviser_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="radiButtonLists">
                        <asp:ListItem Text="Yes" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>                        
                    </asp:RadioButtonList>
                        </div>
                   <div class="col-md-3">
                    <asp:TextBox ID="txtCardNo" Visible="false" ClientIDMode="Static" runat="server" class="input controlLength"></asp:TextBox>
                        </div>
                    <div class="col-md-3">
                    <asp:Button ID="btnFind" Visible="false" CssClass="btn btn-primary" runat="server" Text="Find"
                        OnClientClick="return valid();" OnClick="btnFind_Click" />
                         </div>
                        </div>
                </td>
            </ContentTemplate>
                       </asp:UpdatePanel>
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
                <td>User Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtUserPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Confirm Password<span class="required">*</span></td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input controlLength"></asp:TextBox>
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
                <td style="vertical-align: top;">User Type<span class="required">*</span></td>
                <td>
                   <asp:DropDownList ID="ddlUserTypeList" runat="server"  CssClass="input controlLength" ClientIDMode="Static" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSaveUserAccount" CssClass="btn btn-primary" runat="server" Text="Save"
                        OnClientClick="return validateInputs();" OnClick="btnSaveUserAccount_Click" />
                    <input type="reset" value="Reset" class="btn btn-default" />
                    <input type="reset" value="Account List" OnClick="window.location.href = '/UI/Administration/Users/UserAccountList.aspx'"  class="btn btn-default" />
                </td>
            </tr>
        </table>--%>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
                        
   </ContentTemplate>
        </asp:UpdatePanel>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;
            if (validateText('txtFirstName', 1, 100, 'Enter a First Name') == false) return false;                             
            if (validateText('txtUserName', 1, 100, 'Enter a User Name') == false) return false;
            if (validateText('txtUserPassword', 1, 100, 'Enter a User Password') == false) return false;
            if (validateText('txtEmail', 1, 100, 'Enter a Email') == false) return false;
            if (validateCombo('ddlUserType', "0", 'Select User Type') == false) return false;
            return true;
        }
        function valid() {

            if (validateText('txtCardNo', 1, 100, 'Enter Card No') == false) return false;
            return true;
        }
    </script>
</asp:Content>
