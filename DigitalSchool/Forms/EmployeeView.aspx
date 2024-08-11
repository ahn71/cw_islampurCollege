<%@ Page Title="Employee View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeView.aspx.cs" Inherits="DS.Forms.EmployeeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/reg_style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reg_wrapper">
        <asp:Button runat="server" ID="btnPrintpreview" Width="100px" Style="margin-left: 1px" CssClass="greenBtn"
            Text="Print Preview" OnClick="btnPrintpreview_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="blackBtn"
            PostBackUrl="/Forms/TeacherPartialInfo.aspx" />
        <div class="box">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                Information
                <span class="edit_button">
                    <a href="#">
                        <img src="images/file-edit.png" alt="" />
                    </a>
                </span>
            </h1>
            <div class="main_box">
                <table width="978" border="0">
                    <tr>
                        <td class="level_col">Card No</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblCardNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="profile_image" rowspan="9" align="center" valign="middle">
                            <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Joining Date </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblJoiningDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Name </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Gender </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Father's Name</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Mother's Name </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblMotherName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Religion </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Marital Status</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblMaritalStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Birth Day</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblBirthDay" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Present Address</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblPresentAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Parmanent Address </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblParmanentAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Blood Group </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblBloodGroup" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Last Degree</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblLastDegree" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Nationality </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="box-3">
            <h1>Academic Information
                <span class="edit_button">
                    <a href="#">
                        <img src="images/file-edit.png" alt="" />
                    </a>
                </span>
            </h1>
            <div class="main_box">
                <table width="978" border="0">
                    <tr>
                        <td class="level_col">Department </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="profile_image"></td>
                    </tr>
                    <tr>
                        <td class="level_col">Designation </td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Examiner</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblExaminer" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Status</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblEStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="box-2">
            <h1>
                <asp:Label ID="lblTitle1" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                Address 
                <span class="edit_button">
                    <a href="#">
                        <img src="images/file-edit.png" alt="" />
                    </a>
                </span>
            </h1>
            <div class="main_box">
                <table width="978" border="0">
                    <tr>
                        <td class="level_col">Phone</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="profile_image"></td>
                    </tr>
                    <tr>
                        <td class="level_col">Mobile</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="level_col">Email</td>
                        <td class="level_col_2">:</td>
                        <td class="level_col_3">
                            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
