<%@ Page Title="Student Assign By Batch" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAssignByBatch.aspx.cs" Inherits="DS.Forms.StudentAssignByBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel{
            width:500px;
        }
        .controlLength{
            width:200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="lblStudentId" ClientIDMode="Static" Value="" runat="server" />
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Student Promotion Settings</div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Previous Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlPreviousBatch" class="input controlLength" runat="server" ClientIDMode="Static"
                                            AutoPostBack="false">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Promotion To</td>
                                    <td>
                                        <asp:DropDownList ID="dlCurrentBatch" runat="server" ClientIDMode="Static"
                                            class="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSsign" runat="server" Text="Submit" CssClass="btn btn-primary"
                                            OnClientClick="return validateInputs();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtStName', 1, 20, 'Select student') == false) return false;
            else if (validateText('txtStRoll', 1, 20, 'Enter roll') == false) return false;
            return true;
        }
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function studentAssign(studentId) {
            $('#lblStudentId').val(studentId);
            $('#imgProfile').val();

            var strStname = $('#r_' + studentId + ' td:nth-child(3)').html();
            var strStRoll = $('#r_' + studentId + ' td:nth-child(4)').html();
            var strClass = $('#r_' + studentId + ' td:nth-child(1)').html();
            var strSection = $('#r_' + studentId + ' td:nth-child(2)').html();

            $('#txtStName').val(strStname);
            $('#txtStRoll').val(strStRoll);
            $('#dlClassName').val(strClass);
            $('#dlSectionName').val(strSection);
            // $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#txtStName').val('');
            $('#txtStRoll').val('');
            $('dlClassName').val('');
            $('dlSectionName').val('');
        }
    </script>
</asp:Content>
