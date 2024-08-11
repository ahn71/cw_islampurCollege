<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacultyAttendanceSheetGenarate.aspx.cs" Inherits="DS.Forms.FacultyAttendanceSheetGenarate" %>
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
    <asp:HiddenField ID="lblFeesSettId" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <p class="text-right">Faculty Attendance Sheet List</p>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <div id="divAttendanceSheetList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Attendance Sheet Generate</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Month 
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlMonths" runat="server" Height="26px" Width="293px"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnGenerator" ClientIDMode="Static" runat="server" Text="Generate"
                                    OnClientClick="return validateInputs();" OnClick="btnGenerator_Click" />
                                &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlClassName").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlSession').val();
                var val3 = val1 + "" + val2;
                $('#lblbatch').html(val3);
            });
            $("#ddlSession").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlClassName').val();
                var val3 = val2 + "" + val1;
                $('#lblbatch').html(val3);
            });
        });
        function validateInputs() {
            if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            else if (validateText('ddlSession.Text', 1, 20, 'Select Session') == false) return false;
            return true;
        }
        function clearIt() {
            $('#ddlClassName').val('');
            $('#ddlSession').val('');
            $('#lblbatch').val('');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>
    