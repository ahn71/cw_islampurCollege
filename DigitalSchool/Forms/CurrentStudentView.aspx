<%@ Page Title="Current Student View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentStudentView.aspx.cs" Inherits="DS.Forms.CurrentStudentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <link href="/Styles/reg_style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reg_wrapper">
        <asp:Button runat="server" ID="btnPrintpreview" Text="Print Preview" Width="110px" Visible="false" CssClass="greenBtn" />
        <input type="button" onclick="backUrl();" value="Back" class="blackBtn" />
        <div class="box">
            <h1>Student Information<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
                <table width="978" border="0">
                    <tr>
                        <td class="level_col">Class</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="profile_image" rowspan="9" align="center" valign="middle">
                            <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
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
                        <td class="level_col">Roll Number</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblStRoll" runat="server" Text=""></asp:Label>
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
                    <tr>
                        <td class="level_col">Blood Group</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblBloodGroup" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Religion</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Shift</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblShift" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Status</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="box-2">
            <h1>Parents Information <span class="edit_button"><a href="#">
                <img src="images/file-edit.png" alt="" /></a></span> </h1>
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
                        <td class="level_col">Father's Mobile</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label runat="server" ID="lblFathersMobile"></asp:Label>
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
                    <tr>
                        <td class="level_col">Mother's Mobile</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label runat="server" ID="lblMothersMobile"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Home Phone</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label runat="server" ID="lblHomePhone"></asp:Label>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <div class="box-3">
            <h1>Guardian Information( If parents are absent ) <span class="edit_button"><a href="#">
                <img src="images/file-edit.png" alt="" /></a></span> </h1>
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
            <h1>Present address <span class="edit_button"><a href="#">
                <img src="images/file-edit.png" alt="" /></a></span> </h1>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function backUrl() {
            goURL('/Forms/CurrentStudentInfo.aspx');
        }
    </script>



</asp:Content>
