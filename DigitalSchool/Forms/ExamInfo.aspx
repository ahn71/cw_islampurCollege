<%@ Page Title="Exam Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamInfo.aspx.cs" Inherits="DS.Forms.ExamInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:HiddenField ID="lblDistrictId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Exam ID List</p>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divExamInfoId" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Exam Information Entry</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                            <asp:AsyncPostBackTrigger ControlID="dlExamType" />
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Batch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static" Width="261px"
                                            CssClass="input" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr runat="server" id="trGroup" visible="false">
                                    <td>Group
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                                            Width="261px" CssClass="input">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" Width="261px" CssClass="input"
                                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" runat="server"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ExamType
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlExamType" runat="server" ClientIDMode="Static" Width="261px"
                                            CssClass="input" OnSelectedIndexChanged="dlExamType_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="trDependency">
                                    <td>Dependency
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlDependency" runat="server" ClientIDMode="Static" Width="261px"
                                            CssClass="input">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Exam Id
                                    </td>
                                    <td>
                                        <asp:Label ID="lblExamId" runat="server" ClientIDMode="Static" Width="261px" Font-Size="14px"
                                            Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input id="tnReset" type="reset" value="Reset" class="btn btn-default" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('dlBatch', 1, 30, 'Select the batch name') == false) return false;
            else if (validateText('dlExamType', 1, 30, 'Select the exam type') == false) return false;
            else if (validateText('txtDate', 1, 30, 'Select the exam date') == false) return false;
            return true;
        }
        function editDistrict(districtId) {
            $('#lblDistrictId').val(districtId);
            var strDistrict = $('#r_' + districtId + ' td:first-child').html();
            $('#txtDistrict').val(strDistrict);
            $("#btnSaveDistrict").val('Update');
        }
        function clearIt() {
            $('#lblDistrictId').val('');
            $('#txtDistrict').val('');
            setFocus('txtDistrict');
            $("#btnSaveDistrict").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        $(document).ready(function () {
            $('#dlBatch').change(function () {
                if ($('#dlBatch').val() != "---Select" && $('#dlExamType').val() != "---Select---") {
                    $('#lblExamId').html($('#dlExamType').val() + "_" + $('#dlBatch').val() + "_" + $('#txtDate').val());
                }
            });
            $('#dlExamType').change(function () {
                if ($('#dlBatch').val() != "---Select" && $('#dlExamType').val() != "---Select---") {
                    $('#lblExamId').html($('#dlExamType').val() + "_" + $('#dlBatch').val() + "_" + $('#txtDate').val());
                    alert("OK");
                }
            });
        })
    </script>
</asp:Content>
