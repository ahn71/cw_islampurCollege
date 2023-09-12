<%@ Page Title="Add Designation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDesignation.aspx.cs" Inherits="DS.Admin.AddDsignation" %>

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
    <asp:HiddenField ID="lblDesignationId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Designation List</p>
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
                        <div id="divDesignationList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Designation</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtDes_Name" runat="server" Width="296px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="buttonBox">
                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save"
                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                        <input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtDes_Name', 1, 30, 'Enter Designation Name') == false) return false;
            return true;
        }
        function editEmployee(empId) {
            $('#lblDesignationId').val(empId);
            var strDesName = $('#r_' + empId + ' td:first-child').html();
            $('#txtDes_Name').val(strDesName);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblDesignationId').val('');
            $('#txtDes_Name').val('');
            setFocus('txtDes_Name');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
