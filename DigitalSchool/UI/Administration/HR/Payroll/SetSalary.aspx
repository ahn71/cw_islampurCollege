<%@ Page Title="Salary Information" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetSalary.aspx.cs" Inherits="DS.UI.Administration.HR.Payroll.SetSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child {
            width: 20%;
            text-align: right;
            padding-right: 5px;           
        }
        .tbl-controlPanel td:nth-child(2) {
            width: 40%;
        }
        input[type="radio"] {
            margin: 10px;
            padding: 5px;
        }
        .controlLength {
            width: 200px;
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
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/Payroll/PayrollHome.aspx">Payroll Management</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/Payroll/SalarySetDetails.aspx">Employee Salary Details</a></li>
                <li class="active">Salary Information</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="rdoScale" />
            <asp:AsyncPostBackTrigger ControlID="rdoGross" />
            <asp:AsyncPostBackTrigger ControlID="txtGovSalary" />
            <asp:AsyncPostBackTrigger ControlID="txtSchoolSalary" />            
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Salary Information</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Index No :</td>
                                <td>
                                    <asp:Label ID="lblIndexNo" CssClass="controlLength" runat="server" ClientIDMode="Static"></asp:Label>
                                </td>
                                <td rowspan="10">
                                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                                  <%--  <asp:Image ID="etImage" runat="server" Width="150" Height="150" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />--%>
                                   <%-- <Image id="etImage" runat="server" width="150" height="150" src="http://www.placehold.it/150x150/EFEFEF/AAAAAA&text=no+image" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>Joining Date :</td>
                                <td>
                                    <asp:Label ID="lblJoiningDate" CssClass="controlLength" runat="server" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Name :</td>
                                <td>
                                    <asp:Label ID="lblName" CssClass="controlLength" runat="server" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Department :</td>
                                <td>
                                    <asp:Label ID="lblDepartment" CssClass="controlLength" runat="server" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Designation :</td>
                                <td>
                                    <asp:Label ID="lblDesignation" CssClass="controlLength" runat="server" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Salary Set</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Type</td>
                                <td>
                                    <asp:RadioButton ID="rdoScale" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="rdoButton"
                                        OnCheckedChanged="rdoScale_CheckedChanged" Checked="True" Text="On Scale Payment" />
                                    <asp:RadioButton ID="rdoGross" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="rdoButton"
                                        OnCheckedChanged="rdoGross_CheckedChanged" Text="On Gross Payment" />
                                </td>
                            </tr>
                            <tr>
                                <td>Gov/Basic</td>
                                <td>
                                    <asp:TextBox ID="txtGovSalary" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                        AutoPostBack="True" OnTextChanged="txtGovSalary_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>School</td>
                                <td>
                                    <asp:TextBox ID="txtSchoolSalary" runat="server" ClientIDMode="Static" CssClass="input controlLength" AutoPostBack="True"
                                        OnTextChanged="txtSchoolSalary_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Total Salary</td>
                                <td>
                                    <asp:Label ID="lblTotalSalary" runat="server" CssClass="controlLength" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" OnClientClick="return validateInputs();" 
                                        OnClick="btnSave_Click" />
                                    <input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
