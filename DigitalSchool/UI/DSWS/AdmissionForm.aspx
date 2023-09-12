<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="AdmissionForm.aspx.cs" Inherits="DS.UI.DSWS.AdmissionForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admission-form {
            padding: 10px 30px;
        }

        .adform-title {
            color: #000;
            text-align: center;
            font-size: 25px;
            margin-bottom: 20px;
            background: #18a952;
            padding: 6px;
            color: #fff;
        }

        .group-info-box {
            border: 1px solid #c1c1c1;
            overflow: hidden;
            padding: 20px 5px 5px;
        }

            .group-info-box .control-label {
                padding-top: 6px;
            }

            .group-info-box .group-title {
                margin-top: -34px;
                position: absolute;
                background: #fff;
                padding: 0px 10px;
            }

        span.chkBox input {
            margin: 6px;
            transform: scale(1.5);
        }

        span.chkBox label {
            font-size: 16px;
        }

        .required {
            color: red;
        }

        .mt-150 {
            margin-top: 150px;
        }

        .mbm-10 {
            margin-bottom: 20px;
        }
        .form-control {
            border: 1px solid #6d86e5;
        }
        .input-group-addon {
            border: 1px solid #6d86e5;
        }
    </style>
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
        function printDiv() {

            var divToPrint = document.getElementById('page-wrapper1');

            var newWin = window.open('', 'Print-Window');
            newWin.document.open();
            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 10);

        }
        function validateInputs() {
            try {
               
                // if (validateText('txtMoneyReceiptNo', 1, 50, 'Enter valid Money Receipt No') == false) return false;
                if (document.getElementById("FileUpload1").files.length == 0) {
                    showMessage('Select Student Photo', 'error'); return false;
                }

                if (validateText('txtStudentName', 1, 100, 'Enter valid Student Name') == false) return false;
                if (validateText('txtStudentNameBn', 1, 100, 'Enter valid Student Name in Bengali') == false) return false;
                if (validateCombo('ddlGender', "0", 'Select Gender') == false) return false;
                if (validateText('txtDateOfBirth', 10, 10, 'Enter valid Date of Birth') == false) return false;
                if (validateCombo('ddlReligion', "0", 'Select Religion') == false) return false;
                if (validateText('txtStudentMobile', 11, 11, 'Enter valid Mobile') == false) return false;
                if (validateCombo('ddlShift', "0", 'Select a Shift') == false) return false;
                if (validateCombo('ddlClass', "0", 'Select a Class') == false) return false;
                if (validateCombo('ddlGroup', "0", 'Select a Group') == false) return false;
                if (validateCombo('ddlSection', "0", 'Select a Section') == false) return false;
                //if ($('#ddlClass :selected').text().toLowerCase().includes('honours')) {

                    if (validateText('txtNUAdmissionRoll', 1, 50, 'Enter valid Board Admission Roll') == false) return false;
                //}
                

                if (validateText('txtFatherName', 1, 100, 'Enter Father\'s Name') == false) return false;
                if (validateText('txtFatherNameBn', 1, 100, 'Enter Father\'s Name in Bengali') == false) return false;
                if (validateText('txtFatherMobile', 1, 100, 'Enter Father\'s Mobile') == false) return false;
                if (validateText('txtFatherOccupation', 1, 100, 'Enter Father\'s Occupation') == false) return false;
                if (validateText('txtFatherOccupationBn', 1, 100, 'Enter Father\'s Occupation in Bengali') == false) return false;
                if (validateText('txtMotherName', 1, 100, 'Enter Mother\'s Name') == false) return false;
                if (validateText('txtMotherNameBn', 1, 100, 'Enter Mother\'s Name in Bengali') == false) return false;
                if (validateText('txtMotherOccupation', 1, 100, 'Enter Mother\'s Occupation') == false) return false;
                if (validateText('txtMotherOccupationBn', 1, 100, 'Enter Mother\'s Occupation in Bengali') == false) return false;

                if (validateCombo('ddlParentsDistrict', "0", 'Select a District for Parents Address') == false) return false;
                if (validateCombo('ddlParentsUpazila', "0", 'Select a Upazila for Parents Address') == false) return false;
                if (validateCombo('ddlParentsPostOffice', "0", 'Select a Post Office for Parents Address') == false) return false;
                if (validateText('txtParentsVillage', 1, 100, 'Enter a Village for Parents Address') == false) return false;
                if (validateText('txtParentsVillageBn', 1, 100, 'Enter a Village in Bengali for Parents Address') == false) return false;

                if (validateText('txtGuardianName', 1, 100, 'Enter Guardian Name') == false) return false;
                if (validateText('txtGuardianRelation', 1, 100, 'Enter Guardian Relation') == false) return false;
                if (validateText('txtGuardianMobile', 11, 11, 'Enter Guardian Mobile') == false) return false;
                if ($("#txtStudentMobile").val() == $("#txtGuardianMobile").val()) {
                    showMessage('Student Mobile and Guardian Mobile No can\'t be the same', 'error'); return false;
                }
                if (validateText('txtGuardianAddress', 1, 200, 'Enter Guardian Address') == false) return false;

                if (validateCombo('ddlPermanentDistrict', "0", 'Select a District for Permanent Address') == false) return false;
                if (validateCombo('ddlPermanentUpazila', "0", 'Select a Upazila for Permanent Address') == false) return false;
                if (validateCombo('ddlPermanentPostOffice', "0", 'Select a Post Office for Permanent Address') == false) return false;
                if (validateText('txtPermanentVillage', 1, 100, 'Enter a Village for Permanent Address') == false) return false;
                if (validateText('txtPermanentVillageBn', 1, 100, 'Enter a Village in Bengali for Permanent Address') == false) return false;

                if (validateCombo('ddlPresentDistrict', "0", 'Select a District for Present Address') == false) return false;
                if (validateCombo('ddlPresentUpazila', "0", 'Select a Upazila for Present Address') == false) return false;
                if (validateCombo('ddlPresentPostOffice', "0", 'Select a Post Office for Present Address') == false) return false;
                if (validateText('txtPresentVillage', 1, 100, 'Enter a Village for Present Address') == false) return false;
                if (validateText('txtPresentVillageBn', 1, 100, 'Enter a Village in Bengali for Present Address') == false) return false;

                if (validateText('txtPreviousExamSchoolName', 1, 150, 'Enter SSC School Name') == false) return false;
                if (validateCombo('ddlPreviousExamBoard', "0", 'Select SSC Education Board') == false) return false;
                if (validateCombo('ddlPreviousExamPassingYear', "0", 'Select SSC Passing Year') == false) return false;
                if (validateText('txtPreviousExamRegistrationNo', 1, 20, 'Enter SSC Registration No') == false) return false;
                if (validateText('txtPreviousExamRollNo', 1, 20, 'Enter SSC Roll No') == false) return false;
                if (validateText('txtPreviousExamGPA', 1, 4, 'Enter SSC GPA') == false) return false;

                if ($('#ddlClass :selected').text().toLowerCase().includes('honours') || $('#ddlClass :selected').text().toLowerCase().includes('masters')) {
                    if (validateText('txtPreviousExamSchoolNameHSC', 1, 150, 'Enter College Name') == false) return false;
                if (validateCombo('ddlPreviousExamBoardHSC', "0", 'Select HSC Education Board') == false) return false;
                if (validateCombo('ddlPreviousExamPassingYearHSC', "0", 'Select HSC Passing Year') == false) return false;
                if (validateText('txtPreviousExamRegistrationNoHSC', 1, 20, 'Enter HSC Registration No') == false) return false;
                if (validateText('txtPreviousExamRollNoHSC', 1, 20, 'Enter HSC Roll No') == false) return false;
                if (validateText('txtPreviousExamGPAHSC', 1, 4, 'Enter HSC GPA') == false) return false;
                }

                if ($('#ddlClass :selected').text().toLowerCase().includes('masters')) {
                    if (validateText('txtPreviousExamSchoolNameHonours', 1, 150, 'Enter Honours/Preliminary Institute Name') == false) return false;
                if (validateCombo('ddlPreviousExamBoardHonours', "0", 'Select Honours/Preliminary Board') == false) return false;
                if (validateCombo('ddlPreviousExamPassingYearHonours', "0", 'Select Honours/Preliminary Passing Year') == false) return false;
                if (validateText('txtPreviousExamRegistrationNoHonours', 1, 20, 'Enter Honours/Preliminary Registration No') == false) return false;
                if (validateText('txtPreviousExamRollNoHonours', 1, 20, 'Enter Honours/Preliminary Roll No') == false) return false;
                if (validateText('txtPreviousExamGPAHonours', 1, 4, 'Enter Honours/Preliminary CGPA') == false) return false;
                }

                return true;
            }
            catch (e) {
                showMessage("Validation failed : " + e.message, 'error');
                console.log(e.message);
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <section class="page-section">
        
        <div runat="server" id="divContainer" class="container-fluid">
            <div class="page-inner admission-form">
                <div class="row">
                    <div class="col-md-12">
                        <h1 runat="server" class="adform-title">Student Admission Form </h1>
                        <h1 runat="server" class="adform-title" visible="false" >Student Admission Form1 </h1>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4" style="display: none">
                        <div class="row mt-150 mbm-10" >
                            <label for="name" class="col-sm-4 control-label">Bank Receipt No.<strong class="required">*</strong></label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMoneyReceiptNo" class="form-control" placeholder="Enter Bank Money Receipt No"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8" style="color:#ff0000">
                        <h5 style=" margin-left: 20px; "><strong>ফর্ম পূরণের নির্দেশিকা</strong></h5>
                        <ul>
                            
                            <li>অনলাইনে ভর্তি ফর্ম পূরণের ক্ষেত্রে লাল তারকা চিহ্নিত ঘর সমূহ সঠিকভাবে পূরণ করুন । </li>
                            <li>ছাত্র/ছাত্রীর ছবি সাইজ ( Width 1.6 inche X Height 2.0 inche ) এবং সর্ব্বোচ্চ ১৫০ কেবি হতে হবে । </li>
                            <li>ফর্ম পূরণের জন্য গুগল ক্রম ব্রাউজার ব্যাবহার করুন । </li>
                            <li>সঠিকভাবে ফর্ম পূরণের পর প্রিন্ট করার পূর্বে ব্রাউজার (গুগল ক্রম) এর প্রিন্ট সেটিং থেকে Paper Size: A4 সিলেক্ট করুন এবং Margin: Default করুন । </li>
                            <li>সঠিকভাবে ফর্ম পূরণের পর প্রিন্ট কপির(অফিস কপি) সাথে প্রয়োজনীয় সকল কাগজপত্র নিয়ে ভর্তি শাখায় যোগাযোগ করুন । </li>
							<li>সঠিকভাবে ফর্ম পূরণের পর ভর্তি ফি জমাদানের জন্য পেমেন্ট বাটনে ক্লিক করে পেমেন্ট সম্পন্ন করুন এবং ইনভয়েচ এর প্রিন্ট কপি ভর্তি শাখায় জমা দিন ।</li>
                        </ul>
                    </div>
                    <div class="col-md-4">
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Student Photo</h4>
                            <div class="row form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                            <div class="sphoto-view" style="">
                                                <p>
                                                    <asp:Image ID="imgProfile" class="profileImage" Style=" height: 160px; margin-right: 15px" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" /></p>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row form-group">
                                                <label for="name" class="col-sm-5 control-label">Upload Photo<strong class="required">*</strong></label>
                                                <div class="col-sm-7">
                                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="ddlParentsDistrict" />
                        <asp:AsyncPostBackTrigger ControlID="ddlParentsUpazila" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPermanentDistrict" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPermanentUpazila" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPresentDistrict" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPresentUpazila" />
                        <asp:AsyncPostBackTrigger ControlID="chkFather" />
                        <asp:AsyncPostBackTrigger ControlID="chkMother" />
                        <asp:AsyncPostBackTrigger ControlID="chkOther" />
                        <asp:AsyncPostBackTrigger ControlID="ckbSameAsPermanentAddress" />
                        <asp:PostBackTrigger ControlID="btnSubmit" />

                    </Triggers>
                    <ContentTemplate>
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Student Information</h4>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Student Name<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentName" class="form-control" placeholder="Enter Student Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">নাম<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Gender<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlGender" class="form-control">
                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Date of Birth<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDateOfBirth" class="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Religion<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlReligion" class="form-control">
                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                <asp:ListItem>Islam</asp:ListItem>
                                                <asp:ListItem>Hindu</asp:ListItem>
                                                <asp:ListItem>Christian</asp:ListItem>
                                                <asp:ListItem>Buddhist</asp:ListItem>
                                                <asp:ListItem>Upozati</asp:ListItem>
                                                <asp:ListItem>Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Blood Group</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlBloodGroup" class="form-control">
                                                <asp:ListItem Value="0">Unknown</asp:ListItem>
                                                <asp:ListItem>A+</asp:ListItem>
                                                <asp:ListItem>A-</asp:ListItem>
                                                <asp:ListItem>B+</asp:ListItem>
                                                <asp:ListItem>B-</asp:ListItem>
                                                <asp:ListItem>AB+</asp:ListItem>
                                                <asp:ListItem>AB-</asp:ListItem>
                                                <asp:ListItem>O+</asp:ListItem>
                                                <asp:ListItem>O-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">+88</span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentMobile" class="form-control" placeholder="Enter Student Mobile No"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Shift<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlShift" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Class<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlClass" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Group<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlGroup" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Section<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlSection" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="divNUAdmissionRoll" class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Board Admission Roll<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNUAdmissionRoll" class="form-control" placeholder="Enter Board Admission Roll"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <br />
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Parents Information</h4>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Father's Name<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherName" class="form-control" placeholder="Enter Father's Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">পিতার নাম <strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">+88</span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherMobile" class="form-control" placeholder="Enter Father's Mobile"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Occupation<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherOccupation" class="form-control" placeholder="Enter Father's Occupation"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">পেশা<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherOccupationBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Mother's Name<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherName" class="form-control" placeholder="Enter Mother's Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">মাতার নাম<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Mobile</label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">+88</span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherMobile" class="form-control" placeholder="Enter Mother's Mobile"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Occupation<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherOccupation" class="form-control" placeholder="Enter Mother's Occupation"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">পেশা<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherOccupationBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">District<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlParentsDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlParentsUpazila_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsPostOffice" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Village<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtParentsVillage" class="form-control" placeholder="Enter Present Village"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">গ্রাম<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtParentsVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Guardian Information</h4>

                            <div class="row form-group">
                                <div class="col-md-8"></div>
                                <div class="col-md-4">
                                    <div class="pull-right">
                                        <asp:CheckBox runat="server" ID="chkFather" AutoPostBack="true" CssClass="chkBox"
                                            ClientIDMode="Static" Text=" Father ?" OnCheckedChanged="chkFather_CheckedChanged" />
                                        <asp:CheckBox runat="server" ID="chkMother" AutoPostBack="true" CssClass="chkBox"
                                            ClientIDMode="Static" Text="  Mother ?" OnCheckedChanged="chkMother_CheckedChanged" />
                                        <asp:CheckBox runat="server" ID="chkOther" AutoPostBack="true" CssClass="chkBox"
                                            ClientIDMode="Static" Text=" Other ?" OnCheckedChanged="chkOther_CheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Guardian Name<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianName" class="form-control" placeholder="Enter Guardian Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Relation<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianRelation" class="form-control" placeholder="Enter Guardian Relation"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">+88</span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianMobile" class="form-control" placeholder="Enter Guardian Mobile"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Address<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianAddress" class="form-control" placeholder="Enter Guardian Address"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Permanent Address</h4>


                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">District<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentUpazila_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentPostOffice" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Village<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPermanentVillage" class="form-control" placeholder="Enter Present Village"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">গ্রাম<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPermanentVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Present Address</h4>
                            <div class="row form-group">
                                <div class="col-sm-9"></div>
                                <div class="col-sm-3 ">
                                    <div class="pull-right">
                                        <asp:CheckBox runat="server" ID="ckbSameAsPermanentAddress" AutoPostBack="true" CssClass="chkBox"
                                            ClientIDMode="Static" Text=" Same as Permanent Address ? " OnCheckedChanged="ckbSameAsPermanentAddress_CheckedChanged" />

                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">District <strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentUpazila_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentPostOffice" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Village<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPresentVillage" class="form-control" placeholder="Enter Present Village"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">গ্রাম<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPresentVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="group-info-box student-info">
                            <h4 class="group-title">Previous Institute (SSC) Information</h4>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Institute Name<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamSchoolName" class="form-control" placeholder="Enter Institute Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Board<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamBoard" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Passing Year<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamPassingYear" class="form-control">
                                       <%--         <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                <asp:ListItem Value="2010">2010</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Registration No<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRegistrationNo" class="form-control" placeholder="Enter Registration No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">Roll No<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRollNo" class="form-control" placeholder="Enter Roll No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">GPA<strong class="required">*</strong></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamGPA" class="form-control" placeholder="Enter GPA"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <br />
                        <div runat="server" id="divHSCInfo"  visible="false" class="group-info-box student-info">
<h4 class="group-title">Previous Institute (HSC) Information</h4>
<div class="row form-group">
	<div class="col-md-4">
		<div class="row">
			<label for="name" class="col-sm-4 control-label">Institute Name<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamSchoolNameHSC" class="form-control" placeholder="Enter Institute Name"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Board<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamBoardHSC" class="form-control"></asp:DropDownList>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Passing Year<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamPassingYearHSC" class="form-control">
			<%--		<asp:ListItem Value="0">...Select...</asp:ListItem>
					<asp:ListItem Value="2022">2022</asp:ListItem>
					<asp:ListItem Value="2021">2021</asp:ListItem>
					<asp:ListItem Value="2020">2020</asp:ListItem>
					<asp:ListItem Value="2019">2019</asp:ListItem>
					<asp:ListItem Value="2018">2018</asp:ListItem>
					<asp:ListItem Value="2017">2017</asp:ListItem>
					<asp:ListItem Value="2016">2016</asp:ListItem>
					<asp:ListItem Value="2015">2015</asp:ListItem>
					<asp:ListItem Value="2014">2014</asp:ListItem>
					<asp:ListItem Value="2013">2013</asp:ListItem>
					<asp:ListItem Value="2012">2012</asp:ListItem>
					<asp:ListItem Value="2011">2011</asp:ListItem>
					<asp:ListItem Value="2010">2010</asp:ListItem>--%>
				</asp:DropDownList>
			</div>
		</div>
	</div>

</div>
<div class="row form-group">
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Registration No<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRegistrationNoHSC" class="form-control" placeholder="Enter Registration No"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="name" class="col-sm-4 control-label">Roll No<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRollNoHSC" class="form-control" placeholder="Enter Roll No"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">GPA<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamGPAHSC" class="form-control" placeholder="Enter GPA"></asp:TextBox>
			</div>
		</div>
	</div>

</div>

</div>
                        <br />
                        <div runat="server" id="divHonoursInfo"  visible="false" class="group-info-box student-info">
<h4 class="group-title">Previous Institute (Honours /Preliminary Information) Information</h4>
<div class="row form-group">
	<div class="col-md-4">
		<div class="row">
			<label for="name" class="col-sm-4 control-label">Institute Name<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamSchoolNameHonours" class="form-control" placeholder="Enter Institute Name"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Board<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamBoardHonours" class="form-control"></asp:DropDownList>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Passing Year<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamPassingYearHonours" class="form-control">
					<asp:ListItem Value="0">...Select...</asp:ListItem>
					<asp:ListItem Value="2022">2022</asp:ListItem>
					<asp:ListItem Value="2021">2021</asp:ListItem>
					<asp:ListItem Value="2020">2020</asp:ListItem>
					<asp:ListItem Value="2019">2019</asp:ListItem>
					<asp:ListItem Value="2018">2018</asp:ListItem>
					<asp:ListItem Value="2017">2017</asp:ListItem>
					<asp:ListItem Value="2016">2016</asp:ListItem>
					<asp:ListItem Value="2015">2015</asp:ListItem>
					<asp:ListItem Value="2014">2014</asp:ListItem>
					<asp:ListItem Value="2013">2013</asp:ListItem>
					<asp:ListItem Value="2012">2012</asp:ListItem>
					<asp:ListItem Value="2011">2011</asp:ListItem>
					<asp:ListItem Value="2010">2010</asp:ListItem>
				</asp:DropDownList>
			</div>
		</div>
	</div>

</div>
<div class="row form-group">
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">Registration No<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRegistrationNoHonours" class="form-control" placeholder="Enter Registration No"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="name" class="col-sm-4 control-label">Roll No<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRollNoHonours" class="form-control" placeholder="Enter Roll No"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="col-md-4">
		<div class="row">
			<label for="namebn" class="col-sm-4 control-label">CGPA<strong class="required">*</strong></label>
			<div class="col-sm-8">
				<asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamGPAHonours" class="form-control" placeholder="Enter GPA"></asp:TextBox>
			</div>
		</div>
	</div>

</div>

</div>
                        <br />
                        <div class="group-info-box student-info">
                             <h4 class="group-title">TC Information (If any)</h4>

                            <div class="row form-group">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="name" class="col-sm-4 control-label">College Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtTCCollegeName" class="form-control" placeholder="Enter College Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label for="namebn" class="col-sm-4 control-label">Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtTCDate" class="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br />

                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="text-right">
                            <p>
                                <asp:Button runat="server" CssClass="btn-lg btn-primary" ClientIDMode="Static" ID="btnSubmit" Text="Submit" OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" /></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
