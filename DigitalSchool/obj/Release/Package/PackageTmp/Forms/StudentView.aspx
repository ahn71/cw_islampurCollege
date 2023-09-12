<%@ Page Title="Student Profile View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentView.aspx.cs" Inherits="DigitalSchool.Forms.StudentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/reg_style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reg_wrapper">

        <asp:Button runat="server" ID="btnPrintpreview" Text="Preview" OnClick="btnPrintpreview_Click" />

    	<div class="box">
        	<h1>Student Information<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">

                  <tr>
                    <td class="level_col">Admission No</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblAdmissionNo" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="profile_image" rowspan="9" align="center" valign="middle">
                        <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Date</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblAdmissionDate" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Class</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblAdmissionClass" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Section</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                       <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Student Name</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Roll </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblStRoll" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Date of Birth</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                       <asp:Label ID="lblDateOfBirth" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Gender</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Mobile</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                </table>
            </div>
        </div>



    	<div class="box-2">
        	<h1>Parents Information <span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                  <tr>
                    <td class="level_col">Father's Name</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>
                    </td>
                     <td class="profile_image"></td>
                  </tr>

                  <tr>
                    <td class="level_col">Father's Occupation</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblFatherOccupation" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Father's Yearly Income</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblFatherYearlyIncome" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Mother's Name</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblMotherName" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Mother's Occupation</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblMotherOccupation" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                   <tr>
                    <td class="level_col">Mother's Yearly Income</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblMotherYearlyIncome" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                </table>
            </div>
        </div>



    	<div class="box-3">
        	<h1>Guardian Information( If parents are absent ) <span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                  <tr>
                    <td class="level_col">Guardian Name </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblGuardianName" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="profile_image"></td>
                  </tr>

                  <tr>
                    <td class="level_col">Relation </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblRelation" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Mobile No</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblGuardianMobile" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Guardian Address</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblGuardianAddress" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                 
                </table>
            </div>
        </div>



        <div class="box-2">
        	<h1>Permanent address <span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                  <tr>
                    <td class="level_col">Village</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblPaVillage" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="profile_image"></td>
                  </tr>
                  <tr>
                    <td class="level_col">Post Office</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblPaPostOffice" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="level_col">Thana/Upazila </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblPaThana" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="level_col">District </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                          <asp:Label ID="lblPaDistrict" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                </table>
            </div>
        </div>



        <div class="box-2">
        	<h1>Present address <span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                  <tr>
                    <td class="level_col">Village</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTaVillage" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="profile_image"></td>
                  </tr>

                  <tr>
                    <td class="level_col">Post Office</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTaPostOffice" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">Thana/Upazila </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTaThana" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                  <tr>
                    <td class="level_col">District </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTaDistrict" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>

                </table>
            </div>
        </div>



        <div class="box-3">
        	<h1>Other Information<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                  <tr>
                    <td class="level_col">Selected Exam </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblSelectedExam" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="profile_image"></td>
                  </tr>
                  <tr>
                    <td class="level_col">Roll </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblRoll" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="level_col">Passing Year</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblPassingYear" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="level_col">Board </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblBoard" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>


                    <tr>
                    <td class="level_col">Date  </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTransferDate" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                    <tr>
                    <td class="level_col">Previous School Name  </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblPreviousSchoolName" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                    <tr>
                    <td class="level_col">Transfer Certificate No(if any)  </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblTransferCertificateNo" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                    <tr>
                    <td class="level_col">That class would be admission </td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">
                        <asp:Label ID="lblThatClassWouldbeAdmission" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                 
                </table>
            </div>
        </div>
        <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="/Forms/AdmissionDetails.aspx" />

    </div>
</asp:Content>
