<%@ Page Title="School Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchoolSetup.aspx.cs" Inherits="DS.Forms.SchoolSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/SchoolSetup.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            padding: 7px;
            width: 117px;
        }
        input[type="password"] {
            width: 307px;
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
    <div class="Sbox">
        <div style="text-align: center">
            <h1>School Setup<span class="Sedit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
        </div>
        <div class="main_box">
            <div class="tgPanel">
                <div style="float: left">
                    <table class="School_table">
                        <tr>
                            <td style="width: 125px;">School Name<span class="required">*</span></td>
                            <td style="width: 10px;">:</td>
                            <td>
                                <asp:TextBox ID="txtSchoolName" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">Address<span class="required">*</span></td>
                            <td style="vertical-align: top">:</td>
                            <td>
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Height="80px" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Country<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtCountry" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Telephone<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTelephone" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Fax<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtFax" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Registration No<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRegistrationNo" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email Password<span class="required">*</span></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtEmailPassword" ClientIDMode="Static" TextMode="Password" runat="server" CssClass="SRegInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                            </td>

                        </tr>
                    </table>
                </div>
                <div class="profile_image" style="float: right; margin-top: 20px">
                    <div style="width: 100%; text-align: center;">
                        <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/images/profileImages/Logo.png" /><br />
                        <br />
                        <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onchange="previewFile()" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
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
    </script>
</asp:Content>
