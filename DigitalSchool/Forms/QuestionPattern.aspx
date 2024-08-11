<%@ Page Title="Question Pattern" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuestionPattern.aspx.cs" Inherits="DS.Forms.QuestionPattern" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        .tgPanel {
            width: 450px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblQPId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <table class="col-md-12">
                    <tr>
                        <td></td>
                        <td>
                            <p class="text-right">Question Pattern</p>
                        </td>
                    </tr>                    
                </table>
            </div>
            <div class="col-md-6"></div>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="divQuestionPattern" class="datatables_wrapper" runat="server" 
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-4">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Question Pattern</div>
                            <table class="tbl-controlPanel">                                
                                <tr>
                                    <td>Question Pattern
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQPName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>                                
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearText();" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
        </div>        
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtQPName', 1, 30, 'Enter Question Pattern') == false) return false;
            return true;
        }
        function editQuestionPattern(QPId) {
            $('#lblQPId').val(QPId);
            var strQP = $('#r_' + QPId + ' td:first-child').html();
            $('#txtQPName').val(strQP);
            $("#btnSave").val('Update');
        }
        function clearText() {
            $('#lblQPId').val('');
            $('#txtQPName').val('');
            setFocus('txtQPName');
            $('#btnSave').val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearText();
        }
        function SaveSuccess() {
            showMessage('Save successfully', 'success');
            clearText();
        }
    </script>
</asp:Content>
