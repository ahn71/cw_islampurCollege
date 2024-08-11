<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="DS.Admin.AddExam" %>
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
    <asp:HiddenField ID="lblExId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Exam Details Information</p>
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
                        <div id="divExamList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="tgPanelHead">Add Exam Type</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Exam Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEx_Name" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkDependency" runat="server" ClientIDMode="Static" Checked="false" />
                                        &nbsp; Dependency
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
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
            if (validateText('txtEx_Name', 1, 50, 'Enter a Exam Name') == false) return false;
            return true;
        }
        function editExam(ExamId) {
            $('#lblExId').val(ExamId);

            var strExam = $('#exname' + ExamId).html();
            $('#txtEx_Name').val(strExam);
            if ($('#chkid' + ExamId).is(':checked')) {
                $("#chkDependency").removeProp('checked');
                $("#chkDependency").click();
            }
            else {
                $("#chkDependency").removeProp('checked');
            }
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            $('#lblExId').val('');
            $("#chkDependency").removeProp('checked');
            $("#btnSave").val('Save');
            setFocus('txtEx_Name');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>

