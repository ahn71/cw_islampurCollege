<%@ Page Title="Add Section" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSection.aspx.cs" Inherits="DS.Admin.AddSection" %>
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
    <asp:HiddenField ID="lblSectionID" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Section List</p>
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
                        <div id="divSectionList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add SECTION</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Section Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtSectionName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="buttonBox">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="Cleartext();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtSectionName', 1, 30, 'Enter a Section Name') == false) return false;
            return true;
        }
        function editSection(SecId) {
            $('#lblSectionID').val(SecId);
            var strSec = $('#r_' + SecId + ' td:first-child').html();
            $('#txtSectionName').val(strSec);
            $("#btnSave").val('Update');
        }
        function Cleartext() {
            $('#txtSectionName').val('');
            $('#lblSectionID').val('');
            setFocus('txtSectionName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            Cleartext();
        }
    </script>
</asp:Content>

