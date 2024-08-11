<%@ Page Title="School Info Settings" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SchoolSetup.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.SchoolSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
       .tgPanel{
           width: 100%;
       }
       .tbl-controlPanel{
           width:100%;
       }
       .tbl-controlPanel td{
           width: 50%;
       }
       .tbl-controlPanel td:first-child{          
           text-align: right;
           padding-right: 5px;
       }
       .tbl-controlPanel td input[type="text"],
       .tbl-controlPanel td input[type="password"],
       textarea
       {          
           width: 100%;
           background: #ffffff;
           color: inherit;
       }
       .tbl-controlPanel td input[type="password"]:focus,
       textarea:focus{
           border-color: none;
       }
       .input {
        color:#000;
        background:#ECEDEE;
       }
       .input:focus {
        color:#000;
        background:#fff;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblDSId" ClientIDMode="Static" Value="0" runat="server" />
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
                <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">College Info Settings</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">College Information Setup</div>
            <div class="row tbl-controlPanel">
                <div class="col-sm-3"></div>
                <div class="col-sm-6">
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">College Name<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSchoolName" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Address<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAddress" TextMode="MultiLine" Height="80px" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Country<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCountry" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Telephone</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtTelephone" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Fax</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFax" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Registration No</label>
                        <div class="col-sm-8">
                             <asp:TextBox ID="txtRegistrationNo" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row tbl-controlPanel">
                        <label class="col-sm-4">Email<span class="required">*</span></label>
                        <div class="col-sm-8">
                             <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                          <div class="row tbl-controlPanel">
                        <label class="col-sm-4"></label>
                        <div class="col-sm-8">
                             <asp:CheckBox runat="server" ID="ckbIsOnline" Text="IsOnline" />
                        </div>
                      </div>
                      <div class="row tbl-controlPanel" style="visibility:hidden">
                        <label class="col-sm-4">Email Password<span class="required">*</span></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmailPassword" ClientIDMode="Static" TextMode="Password" runat="server" CssClass="input form-control"></asp:TextBox>
                        </div>
                      </div>
                    <div class="row tbl-controlPanel">
                        <label class="col-sm-4"></label>
                        <div class="col-sm-8">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save"
                                OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                        </div>
                      </div>
                   
                </div>
                <div class="col-sm-3">
                    <div style="width: 100%; text-align: center; margin-top:20px;">
                        <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/images/profileImages/Logo.png" /><br />
                        <br />
                        <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onchange="previewFile()" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
            <%--<div class="col-md-8">
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Institution Name<span class="required">*</span></td>                        
                        <td>
                            <asp:TextBox ID="txtSchoolName" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Address<span class="required">*</span></td>                  
                        <td>
                            <asp:TextBox ID="txtAddress" TextMode="MultiLine" Height="80px" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Country<span class="required">*</span></td>                        
                        <td>
                            <asp:TextBox ID="txtCountry" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Telephone</td>                        
                        <td>
                            <asp:TextBox ID="txtTelephone" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Fax</td>                        
                        <td>
                            <asp:TextBox ID="txtFax" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Registration No</td>                        
                        <td>
                            <asp:TextBox ID="txtRegistrationNo" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email<span class="required">*</span></td>                        
                        <td>
                            <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                        <td>Email Password<span class="required">*</span></td>                        
                        <td>
                            <asp:TextBox ID="txtEmailPassword" ClientIDMode="Static" TextMode="Password" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>                        
                        <td>
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save"
                                OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                        </td>

                    </tr>
                </table>
            </div>
            <div class="col-md-2">
                <div style="width: 100%; text-align: center; margin-top:20px;">
                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/images/profileImages/Logo.png" /><br />
                    <br />
                    <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onchange="previewFile()" ClientIDMode="Static" />
                </div>
            </div>
            <div class="col-md-2"></div>--%> 
            <div class="clearfix"></div>           
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#lblMessage').text().length > 1) {
                showMessage($('#lblMessage').text(), '');
            }
        });
        function previewFile() {
            try {
                var preview = document.querySelector('#imgProfile');
                var file = document.querySelector('#FileUpload1').files[0];

                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
                var imagename = $('#FileUpload1').val();
                $('#HiddenField1').val(imagename);
            }
            catch (exception) {
                lblMessage.innerText = exception;
            }
        }
        function validateInputs() {
            if (validateText('txtSchoolName', 1, 100, 'Enter a Institution Name') == false) return false;
            if (validateText('txtAddress', 1, 200, 'Enter a Address') == false) return false;
            if (validateText('txtCountry', 1, 30, 'Enter a Country') == false) return false;           
            if (validateText('txtEmail', 1, 50, 'Enter a Email') == false) return false;            
            return true;
        }
    </script>
</asp:Content>
