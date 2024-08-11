<%@ Page Title="Salary Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetSalary.aspx.cs" Inherits="DS.SetSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/reg_style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            padding: 7px;
            width: 117px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="rdoScale" />
            <asp:AsyncPostBackTrigger ControlID="rdoGross" />
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
            <div class="box">
                <h1>Salary Information<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
                <div class="main_box">
                    <table width="978" border="0">
                        <tr>
                            <td class="level_col">Index No</td>
                            <td class="level_col_2">:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblIndexNo" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>

                            <td class="profile_image" rowspan="6" align="center" valign="middle">
                                <asp:Image ID="etImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td class="level_col">Joining Date</td>
                            <td class="level_col_2">:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblJoiningDate" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="level_col">Name </td>
                            <td class="level_col_2">:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblName" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="level_col">Department </td>
                            <td class="level_col_2">:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblDepartment" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="level_col">Designation</td>
                            <td class="level_col_2">:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblDesignation" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box">
                <h1>Salary Set<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
                <div class="main_box">
                    <table width="978" border="0">
                        <tr>
                            <td class="auto-style2">Type</td>
                            <td style="width: 15px">:</td>
                            <td class="level_col_3">
                                <asp:RadioButton ID="rdoScale" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="rdoButton" OnCheckedChanged="rdoScale_CheckedChanged" Checked="True" />
                                <label style="margin-left: 5px;" for="rdoScale">On Scale Payment</label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td class="level_col_3">
                                <asp:RadioButton ID="rdoGross" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="rdoButton" OnCheckedChanged="rdoGross_CheckedChanged" />
                                <label style="margin-left: 5px;" for="rdoGross">On Gross Payment</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Gov/Basic</td>
                            <td style="width: 15px">:</td>
                            <td class="level_col_3">
                                <asp:TextBox ID="txtGovSalary" runat="server" ClientIDMode="Static" CssClass="textBoxStyle" AutoPostBack="True" OnTextChanged="txtGovSalary_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">School</td>
                            <td>:</td>
                            <td class="level_col_3">
                                <asp:TextBox ID="txtSchoolSalary" runat="server" ClientIDMode="Static" CssClass="textBoxStyle" AutoPostBack="True" OnTextChanged="txtSchoolSalary_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Total Salary</td>
                            <td>:</td>
                            <td class="level_col_3">
                                <asp:Label ID="lblTotalSalary" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="bottom: 0px; height: auto; width: 190px; padding: 5px; text-align: center; background-color: rgba(0, 168, 0, 0.6); border: 1px solid green; margin-left: 240px; float: left; left: 0px; margin-right: 40px; margin-top: 5px;">
        <asp:Button ID="btnSave" runat="server" Text="Save" class="greenBtn" OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
        <input type="button" class="blackBtn" value="Clear" onclick="clearIt();" />
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function GovSalary(val) {
            alert(val);
        }
        function validateInputs() {
            if (validateText('lblIndexNo', 1, 50, 'Enter a Index Number') == false) return false;
            return true;
        }
        function clearIt() {
            $('#txtGovSalary').val('');
            $('#txtSchoolSalary').val('');
            $('#lblTotalSalary').val('');
            $('#lblIndexNo').val('');
            $('#lblName').val('');
            $('#lblGender').val('');
            $('#lblDepartment').val('');
            $('#lblDesignation').val('');

            setFocus('txtGovSalary');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>

