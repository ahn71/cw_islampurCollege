<%@ Page Title="Admission Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admission.aspx.cs" Inherits="DS.Forms.Adminssion" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/AdminssionStyle.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jquery-ui-datepekar.css" rel="stylesheet" />
    <link href="/Styles/popupStyle.css" rel="stylesheet" />
    <style type="text/css">
        .tgInput {
            border:none!important;
        }
        .tgbutton {
            width:65%;
            margin:20px auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <%--STUDENT INFORMATION--%>
            <div class="tgPanel">
                <div class="tgPanelHead">Student Information</div>
                <div class="tgInput">
                    <table style="float:left; display:block; width:70%; border:0">
                        <tr>
                            <td>Admission No<span class="required">*</span></td>
                            <td class="input_box">
                                <asp:TextBox ID="txtAdmissionNo" TabIndex="0" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Date <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtAdmissionDate" ClientIDMode="Static" TabIndex="1" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtAdmissionDate"></asp:CalendarExtender>
                        </tr>
                        <tr>
                            <td>Class <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlClass" TabIndex="2" runat="server" Height="26px" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>Section <span class="required">*</span></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSection" TabIndex="3" ClientIDMode="Static" runat="server" Height="26px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>Student Name <span class="required">*</span></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtStudentName" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Roll <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Religion<span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="dlReligion" TabIndex="2" runat="server" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>Islam</asp:ListItem>
                                    <asp:ListItem>Hindu</asp:ListItem>
                                    <asp:ListItem>Christian</asp:ListItem>
                                    <asp:ListItem>Buddhist</asp:ListItem>
                                    <asp:ListItem>Upozati</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShift" TabIndex="2" runat="server" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td>Date of Birth <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtDateOfBirth" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateOfBirth"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Gender<span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Mobile <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Session <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlSession" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Blood Group</td>
                            <td>
                                <asp:DropDownList ID="dlBloodGroup" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>Unknown</asp:ListItem>
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
                        </tr>
                    </table>
                    <div style="float: right; margin-left: 67px; display: block; width: 20%">
                        <div style="width: 100%; text-align: center;">
                            <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        </div>
                        <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkGuardian" />
        </Triggers>

        <ContentTemplate>
            <%--Parents Information--%>
            <div class="tgPanel">
                <div class="tgPanelHead">Parents Information
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="chkGuardian" AutoPostBack="true" OnCheckedChanged="Guardian_CheckedChanged" ClientIDMode="Static" Text="  Have Guardian ?" /></div>
                </div>
                <div class="tgInput">
                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td>Father&#39;s Name <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFatherName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Occupation <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFatherOccupation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Yearly Income</td>
                            <td>
                                <asp:TextBox ID="txtFatherYearlyIncome" runat="server" class="input" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Mother&#39;s Name <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMotherName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Occupation </td>
                            <td>
                                <asp:TextBox ID="txtMotherOccupation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Yearly Income </td>
                            <td>
                                <asp:TextBox ID="txtMotherYearlyIncome" runat="server" ClientIDMode="Static" class="input" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Father's Mobile
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="txtFathersMobile" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>Mother's Mobile
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="txtMothersMobile" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>Home Phone
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="txtHomePhone" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Father's Email
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="txtFatherEmail" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>Mother's Email
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="txtMotherEmail" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>&nbsp;
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="TextBox3" Visible="false" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <%--Guardian Information--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkGuardian" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="dviGuardian" class="tgPanel">
                <div class="tgPanelHead">Guardian Information ( If parents are absent )</div>
                <div class="tgInput">

                    <asp:Panel runat="server" ID="pnlGuardian">
                        <table style="display: block; width: 100%; border: 0">
                            <tr>
                                <td>Guardian Name </td>
                                <td>
                                    <asp:TextBox ID="txtGuardianName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>Relation </td>
                                <td>
                                    <asp:TextBox ID="txtGuardianRelation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td style="width: 80px">Mobile No</td>
                                <td>
                                    <asp:TextBox ID="txtGurdianMobile" runat="server" class="input" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Guardian Address </td>
                                <td>
                                    <asp:TextBox ID="txtGuardianAddress" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPADistrict" />
        </Triggers>
        <ContentTemplate>
            <%--Permanent address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">Permanent address</div>
                <div class="tgInput">

                    <table class="auto-style1">
                        <tr>
                            <td>Village <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPAVillage" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td>Post Office <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPAPostOffice" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Thana/Upazila <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlPAThana" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>District </td>
                            <td>
                                <asp:DropDownList ID="ddlPADistrict" runat="server" AutoPostBack="True" ClientIDMode="Static" class="ddl-box" Height="26px" OnSelectedIndexChanged="ddlPADistrict_SelectedIndexChanged">
                                    <asp:ListItem>---Select---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlTADistrict" />
            <asp:AsyncPostBackTrigger ControlID="chkSameAddress" />
        </Triggers>
        <ContentTemplate>
            <%--Present address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    Present address
                    <div style="float: right">
                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckedChanged" runat="server" ID="chkSameAddress" ClientIDMode="Static" />
                        <label for="chkSameAddress">Same</label>
                    </div>
                </div>
                <div class="tgInput">
                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td>Village <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtTAVillage" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td>Post Office <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtTAPostOffice" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Thana/Upazila <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlTAThana" runat="server" class="ddl-box" Height="26px" Width="190px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>District <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlTADistrict" runat="server" AutoPostBack="True" ClientIDMode="Static" class="ddl-box" Height="26px" OnSelectedIndexChanged="ddlTADistrict_SelectedIndexChanged" Width="190px">
                                    <asp:ListItem>---Select---</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <%--Other Information--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    Other Information
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="chkNotApplicable" ClientIDMode="Static" />
                        <label for="chkNotApplicable">Not applicable</label>
                    </div>
                </div>
                <div class="Tag-Box">
                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td style="width: 200px">Select Exam <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlExam" runat="server" class="ddl-box" Height="26px" Width="190px" ClientIDMode="Static">
                                    <asp:ListItem>P.S.C</asp:ListItem>
                                    <asp:ListItem>J.S.C</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Roll <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPSCRoll" runat="server" class="input" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                            <td>Passing Year <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlPassingYear" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>GPA <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtGpa" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Board <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlBoard" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td>Date <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtTrDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="txtTrDate"></asp:CalendarExtender>

                            </td>
                        </tr>
                        <tr>
                            <td>Previous School Name <span class="required">*</span></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPreviousSchoolName" runat="server" class="input" Width="100%" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td style="padding-left: 17px">Registraton</td>
                            <td>
                                <asp:TextBox ID="txtRegistration" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Transfer Certificate No(if any) </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransferCNo" runat="server" class="input" Width="100%" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>That class would be admission <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlThatClass" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgbutton">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ClientIDMode="Static" OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" OnClick="btnClear_Click" />
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

        function initAll() {
            $('input').click(function () {
                setTimeout($('.datepicker').css('top', 'auto'), 25);
            });
        }

        function sameData() {
            $("txtTAVillage").val() = $("txtPAVillage").val();
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $(document).on('keypress', 'input[type="text"]', function (e) {

                if (e.keyCode == 13) {
                    e.preventDefault();
                    var controlId = $(this).attr('id');

                    if (controlId == "txtAdmissionNo") setFocus("txtAdmissionDate");
                }

            });

        });


        function updateSuccess() {
            showMessage('Updated successfully', 'success');
        }

        function saveSuccess() {
            showMessage('Save successfully', 'success');
        }


        function validateInputs() {

            try {
                console.log('validating inputs');
                if (validateText('txtAdmissionNo', 1, 20, 'Enter a admission number') == false) return false;
                if (validateText('txtAdmissionDate', 1, 20, 'Enter a admission date') == false) return false;
                if (validateText('ddlClass', 1, 12, 'Enter a class') == false) return false;
                if (validateText('ddlSection', 1, 20, 'Enter a section') == false) return false;
                if (validateText('txtRoll', 1, 20, 'Enter a roll number') == false) return false;
                if (validateText('txtStudentName', 1, 200, 'Enter a full name') == false) return false;
                if (validateText('ddlGender', 1, 10, 'Enter a gender') == false) return false;
                if (validateText('txtDateOfBirth', 1, 20, 'Enter a date of birth') == false) return false;

                if (validateText('txtMobile', 1, 50, 'Enter a mobile') == false) return false;
                // if (validateText('FileUpload1', 1, 100, 'Enter a Image Name') == false) return false;
                if (validateText('txtFatherName', 1, 200, 'Enter a fathers name') == false) return false;
                if (validateText('txtFatherOccupation', 1, 50, 'Enter a fathers profession') == false) return false;
                // if (validateText('txtFatherYearlyIncome', 1,15, 'Enter a fathers yearly income') == false) return false;

                if (validateText('txtMotherName', 1, 150, 'Enter a mothers name') == false) return false;
                // if (validateText('txtMotherOccupation', 1, 100, 'Enter a mothers profession') == false) return false;
                // if (validateText('txtMotherYearlyIncome', 1, 15, 'Enter a mothers yearly income') == false) return false;

                if (validateText('txtPAVillage', 1, 200, 'Enter a permanent village') == false) return false;
                if (validateText('txtPAPostOffice', 1, 100, 'Enter a permanent post office') == false) return false;
                if (validateText('ddlPAThana', 1, 100, 'Enter a permanent thana/upazila') == false) return false;
                if (validateText('ddlPADistrict', 1, 100, 'Enter a permanent district') == false) return false;

                if (validateText('txtTAVillage', 1, 200, 'Enter a present village') == false) return false;
                if (validateText('txtTAPostOffice', 1, 100, 'Enter a present post office') == false) return false;
                if (validateText('ddlTAThana', 1, 100, 'Enter a present thana/upazila') == false) return false;
                if (validateText('ddlTADistrict', 1, 50, 'Enter a present district') == false) return false;

                if ($("#chkNotApplicable").is(':checked')) {
                }
                else {
                    if (validateText('txtPreviousSchoolName', 1, 200, 'Enter a previous school name') == false) return false;
                    if (validateText('txtTrDate', 1, 100, 'Enter a certified date') == false) return false;
                    if (validateText('ddlExam', 1, 100, 'Enter a preferred class') == false) return false;
                    if (validateText('txtGpa', 1, 100, 'Enter a P S C G PA') == false) return false;
                    if (validateText('txtPSCRoll', 1, 100, 'Enter a P S C roll number') == false) return false;
                    if (validateText('ddlBoard', 1, 100, 'Enter a P S C board') == false) return false;
                    if (validateText('ddlPassingYear', 1, 100, 'Enter a P S C passing year') == false) return false;
                }
                return true;
            }
            catch (e) {
                showMessage("Validation failed : " + e.message, 'error');
                console.log(e.message);
                return false;
            }
        }

        function acceptValidCharacter(event) {
            alert("ol");
            // var a = $(this).val(String.fromCharCode(event.keyCode()));
            alert(event.fromCharCode(onkeyup.keyCode));
            try {
                alert(String.fromCharCode(event.keyCode()));
            }
            catch (e) {

            }
        }
    </script>
    <script src="/Scripts/jquery.js"></script>
    <script src="/Scripts/jquery-ui-datepekar.js"></script>

</asp:Content>
