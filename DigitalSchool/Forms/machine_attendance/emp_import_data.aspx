<%@ Page Title="Data Import" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="emp_import_data.aspx.cs" Inherits="DS.Forms.machine_attendance.emp_import_data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .alignment {
            text-align: center;
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
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="tgPanel" style="width: 830px;">
                            <div class="tgPanelHead">Faculty anf Staff Attendance Import</div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel pull-right" style="width: 400px; padding: 10px 50px;">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblFullImport" Font-Bold="True">Full Import</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Shift
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                            Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAttendanceDate" PLaceHolder="Click For Calendar" runat="server" ClientIDMode="Static"
                                            CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                            PopupButtonID="imgAttendanceDate" Enabled="True"
                                            TargetControlID="txtAttendanceDate" ID="CExtApplicationDate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="Impirt" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtAttendanceDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnImport" ValidationGroup="Impirt" CssClass="btn btn-primary" runat="server" Text="Import"
                                            OnClientClick=" return InputValidationBasket2();" OnClick="btnImport_Click" />
                                        <asp:Button ID="Button3" runat="server" Text="Close" PostBackUrl="~/default.aspx" CssClass="btn btn-default" />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                                    <p>Wait attendance&nbsp; processing</p>
                                                </span>
                                                <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel pull-left" style="width: 400px; padding: 10px 50px;">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="Label2" Font-Bold="True">Partial Import</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Card
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCardNo" PLaceHolder="Type Card No" runat="server" ClientIDMode="Static"
                                            CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPartialAttDate" PLaceHolder="Click For Calendar" runat="server" ClientIDMode="Static"
                                            CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtPartialAttDate_CalendarExtender" Format="dd-MM-yyyy" runat="server" TargetControlID="txtPartialAttDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnPartialImport" CssClass="btn btn-primary" runat="server" Text="Import"
                                            OnClientClick=" return InputValidationBasket();" OnClick="btnPartialImport_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="Close" PostBackUrl="~/default.aspx" CssClass="btn btn-default" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function InputValidationBasket() {
            try {
                if ($('#txtCardNo').val().trim().length < 1) {
                    showMessage('Please type valid card no', 'error');
                    $('#txtCardNo').focus(); return false;
                }

                if ($('#txtPartialAttDate').val().trim().length == 0) {
                    showMessage('Please select date for partial attendance import', 'error');
                    $('#txtPartialAttDate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
        function InputValidationBasket2() {
            try {
                //if ($('#ddlShift').val().trim().length == 0) {
                //    showMessage('Please select a shift', 'error');
                //    $('#ddlShift').focus(); return false;
                //}

                if ($('#txtAttendanceDate').val().trim().length == 0) {
                    showMessage('Please select date for attendance import', 'error');
                    $('#txtAttendanceDate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
    </script>
</asp:Content>
