<%@ Page Title="Attendance Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceSettings.aspx.cs" Inherits="DS.Forms.AttendanceSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblAbsentId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Attendance Count Type</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Select Type</td>
                                    <td>
                                        <asp:RadioButtonList ID="rbAttendanceCountType" runat="server">
                                            <asp:ListItem>Manually System</asp:ListItem>
                                            <asp:ListItem>Machine System</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnAttendanceCountType" Text="Save" ClientIDMode="Static"
                                            CssClass="btn btn-primary" OnClick="btnAttendanceCountType_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Absent Fine Count</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkAbsentFineCount" ClientIDMode="Static" AutoPostBack="true"
                                            OnCheckedChanged="chkAbsentFineCount_CheckedChanged" />
                                        <label for="chkAbsentFineCount">Absent Fine Count</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Amount
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtAbsentFineAmount" ClientIDMode="Static" CssClass="input"
                                                    Width="176px" Visible="false"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkAbsentFineCount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnSaveFineAmount" OnClick="btnSaveFineAmount_Click"
                                            Text="Save" ClientIDMode="Static" CssClass="btn btn-primary" />
                                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear"
                                            ClientIDMode="Static" CssClass="btn btn-default" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div runat="server" id="divAbsentFineList" class="datatables_wrapper"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveFineAmount" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function editAbsentAmount(absId) {
            $('#lblAbsentId').val(absId);

            var strAbsentAmount = $('#r_' + absId + ' td:first-child').html();

            $('#txtAbsentFineAmount').val(strAbsentAmount);
            $("#btnSaveFineAmount").val('Update');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
