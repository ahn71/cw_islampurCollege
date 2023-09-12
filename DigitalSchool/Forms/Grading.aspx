<%@ Page Title="Grading" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grading.aspx.cs" Inherits="DS.Forms.Grading" %>
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
    <asp:HiddenField ID="lblGId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Grading Details</p>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divGradingList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Grading</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Grade
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrade" runat="server" Width="110px" ClientIDMode="Static"
                                    Style="margin-right: 10px" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Mark Min 
                            </td>
                            <td>
                                <asp:TextBox ID="txtGradeMin" runat="server" Width="110px" ClientIDMode="Static"
                                    Style="margin-right: 10px" CssClass="input"></asp:TextBox>
                                Mark Max<asp:TextBox ID="txtGradeMax" runat="server" Width="110px"
                                    Style="margin-left: 10px;" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Point Min
                            </td>
                            <td>
                                <asp:TextBox ID="txtGPointMin" runat="server" Width="110px" ClientIDMode="Static"
                                    Style="margin-right: 10px" CssClass="input"></asp:TextBox>Point Max<asp:TextBox ID="txtGPointMax"
                                        runat="server" Width="110px" ClientIDMode="Static" Style="margin-left: 11px;" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="buttonBox" style="width: 117%">
                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                            runat="server" Text="Save" OnClick="btnSave_Click" />
                        <input type="button" value="Clear" class="btn btn-default" onclick="clearIt();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtGrade', 1, 15, 'Enter Grade Name') == false) return false;
            if (validateText('txtGradeMin', 1, 15, 'Enter Grade Minimum Range') == false) return false;
            if (validateText('txtGradeMax', 1, 15, 'Enter Grade Maximum Range') == false) return false;
            if (validateText('txtGPointMin', 1, 15, 'Enter Grade Point Min') == false) return false;
            if (validateText('txtGPointMax', 1, 15, 'Enter Grade Point Max') == false) return false;
            return true;
        }
        function editGrading(Id) {
            $('#lblGId').val(Id);
            var strGrade = $('#r_' + Id + ' td:first-child').html();
            var strGradeMin = $('#r_' + Id + ' td:nth-child(2)').html();
            var strGradeMax = $('#r_' + Id + ' td:nth-child(3)').html();
            var strGradePointMin = $('#r_' + Id + ' td:nth-child(4)').html();
            var strGradePointMax = $('#r_' + Id + ' td:nth-child(5)').html();
            $('#txtGrade').val(strGrade);
            $('#txtGradeMin').val(strGradeMin);
            $('#txtGradeMax').val(strGradeMax);
            $('#txtGPointMin').val(strGradePointMin);
            $('#txtGPointMax').val(strGradePointMax);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblGId').val('');
            $('#txtGrade').val('');
            $('#txtGradeMin').val('');
            $('#txtGradeMax').val('');
            $('#txtGPointMin').val('');
            $('#txtGPointMax').val('');
            setFocus('txtGrade');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SaveSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
