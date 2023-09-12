<%@ Page Title="Employee Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeInformation.aspx.cs" Inherits="DS.Forms.EmployeeInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">        
        .tgInput {
            border:none!important;
            padding-right: 5px;
        }
        .tgbutton {
            width:65%;
            margin:20px auto;
        }
        input[type="checkbox"]
        {
            margin-right: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgPanelHead">Employee Information</div>
        <div class="tgInput">
            <table style="float: left; display: block; width: 78%; border: 0">
                <tr>
                    <td>Index Number</td>
                    <td>
                        <asp:TextBox ID="txtE_CardNo" TabIndex="0" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td>Joining Date <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_JoiningDate" TabIndex="0" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" CssClass="" runat="server" TargetControlID="txtE_JoiningDate"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Name <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_Name" runat="server" Width="157px"></asp:TextBox>
                        <asp:TextBox ID="txtTCodeNo" runat="server" Width="49px" ClientIDMode="Static" PlaceHolder="T Code"></asp:TextBox>
                    </td>
                    <td>Gender <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="dlGender" runat="server">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Father's Name<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_FathersName" runat="server"></asp:TextBox>
                    </td>
                    <td>Mother's Name <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_MothersName" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>Department <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="dlDepartments" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>Designation<span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="dlDesignation" runat="server">
                        </asp:DropDownList>
                </tr>
                <tr>
                    <td>Religion</td>
                    <td>
                        <asp:DropDownList ID="dlReligion" runat="server">
                            <asp:ListItem>Islam</asp:ListItem>
                            <asp:ListItem>Hindu</asp:ListItem>
                            <asp:ListItem>Christian</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Marital Status</td>
                    <td>
                        <asp:DropDownList ID="dlMaritalStatus" runat="server" Height="26px">
                            <asp:ListItem>Married</asp:ListItem>
                            <asp:ListItem>Unmarried</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Phone</td>
                    <td>
                        <asp:TextBox ID="txtE_Phone" runat="server" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                    <td>Mobile <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_Mobile" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtE_Email" runat="server"></asp:TextBox>
                    </td>
                    <td>Birth Day<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_Birthday" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtE_Birthday"></asp:CalendarExtender>
                </tr>
                <tr>
                    <td>Present Address<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_PresentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>Parmanent Address <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtE_ParmanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>Blood Group</td>
                    <td>
                        <asp:DropDownList ID="dlBloodGroup" runat="server">
                            <asp:ListItem>A+</asp:ListItem>
                            <asp:ListItem>A-</asp:ListItem>
                            <asp:ListItem>B+</asp:ListItem>
                            <asp:ListItem>B-</asp:ListItem>
                            <asp:ListItem>AB+</asp:ListItem>
                            <asp:ListItem>AB-</asp:ListItem>
                            <asp:ListItem>O+</asp:ListItem>
                            <asp:ListItem>O-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Last Degree<span class="required">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtE_LastDegree" TabIndex="0" runat="server" ClientIDMode="Static"></asp:TextBox>
                </tr>
                <tr>
                    <td>Status</td>
                    <td>
                        <asp:DropDownList ID="dlEStatus" runat="server">
                            <asp:ListItem>Permanent</asp:ListItem>
                            <asp:ListItem>Temporary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Nationality</td>
                    <td>
                        <asp:DropDownList ID="dlNationality" runat="server">
                            <asp:ListItem>Bangladeshi</asp:ListItem>
                            <asp:ListItem>Japanese</asp:ListItem>
                            <asp:ListItem>Pakistani</asp:ListItem>
                            <asp:ListItem>India</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="chkExaminer" runat="server" Checked="True" Text="Is Examiner" Width="150px" />
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="Is Active" TextAlign="Right" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <div style="float: right; margin-left: 2px; display: block; width: 20%">
                <div style="width: 100%; text-align: center;">
                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                </div>
                <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>
    <div class="tgbutton">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ClientIDMode="Static"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgProfile.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
        }
    </script>
</asp:Content>
